using SqlSugar;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Entity;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Upsert;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.Workflow.FormReviewAction;
using SystemAdmin.Model.FormBusiness.Workflow.FormReviewFlow;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Entity;

namespace SystemAdmin.Repository.FormBusiness.Workflow
{
    public class FormReviewAction
    {
        private readonly CurrentUser _loginuser;
        private readonly SqlSugarScope _db;
        private readonly LocalizationService _localization;
        private readonly Language _lang;

        public FormReviewAction(CurrentUser loginuser, SqlSugarScope db, LocalizationService localization, Language lang)
        {
            _loginuser = loginuser;
            _db = db;
            _localization = localization;
            _lang = lang;
        }

        /// <summary>
        /// 表单签核
        /// </summary>
        public async Task<bool> FromApprove(ReviewForm reviewForm)
        {
            long formId = long.Parse(reviewForm.FormId);
            var (stepInfo, ruleId) = await GetCurrentStepInfo(formId);

            // 规则1：手动操作处理当前步骤
            bool hasPendingUsers = await ProcessStepApproval(formId, stepInfo, ReviewType.Manual, reviewForm.Comment);

            if (hasPendingUsers)
            {
                // 会签未完成，通知该步骤剩余待签人（归属人 + 其代理人）
                await NotifyPendingReviewers(formId, stepInfo.StepId);
                return true;
            }

            var nextStep = await GetNextStep(ruleId, stepInfo.StepId);

            if (nextStep.NextStepId == 0)
            {
                await ApproveForm(formId);
                return true;
            }

            // 推进步骤
            await AdvanceCurrentStep(formId, nextStep.NextStepId);

            // 规则2：继续检查后续步骤是否自动推进
            bool needNotify = await AutoApproveIfSelfInNextSteps(formId);

            if (needNotify)
            {
                // 循环停止时 CurrentStepId 已指向需要人工签核的步骤
                var (currentStepInfo, _) = await GetCurrentStepInfo(formId);
                await NotifyPendingReviewers(formId, currentStepInfo.StepId);
            }

            return true;
        }

        /// <summary>
        /// 规则2：循环检查后续步骤，根据签核模式决定是否自动推进
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        private async Task<bool> AutoApproveIfSelfInNextSteps(long formId)
        {
            while (true)
            {
                var (stepInfo, ruleId) = await GetCurrentStepInfo(formId);

                // 每轮循环进入时先初始化当前步骤的待审批人
                await EnsurePendingReviewExists(formId, stepInfo.StepId);

                // 跳过条件
                if (await ShouldSkipStep(formId, stepInfo))
                {
                    await _db.Deleteable<PendingReviewEntity>()
                             .Where(pending => pending.FormId == formId && pending.StepId == stepInfo.StepId)
                             .ExecuteCommandAsync();

                    var skippedNext = await GetNextStep(ruleId, stepInfo.StepId);

                    if (skippedNext.NextStepId == 0)
                    {
                        await ApproveForm(formId);
                        return false; // 签核到底，无需通知
                    }

                    await AdvanceCurrentStep(formId, skippedNext.NextStepId);
                    continue;
                }

                // 当前步骤待审批人中是否包含自己（含代理情况）
                var selfPendingUserId = await _db.Queryable<PendingReviewEntity>()
                                                 .With(SqlWith.NoLock)
                                                 .LeftJoin<UserAgentEntity>((pending, agent) => pending.ReviewUserId == agent.SubstituteUserId && agent.StartTime <= DateTime.Now && agent.EndTime >= DateTime.Now)
                                                 .Where((pending, agent) => pending.FormId == formId && pending.StepId == stepInfo.StepId && (pending.ReviewUserId == _loginuser.UserId || agent.AgentUserId == _loginuser.UserId))
                                                 .Select((pending, agent) => pending.ReviewUserId)
                                                 .FirstAsync();

                // 不包含自己，停止自动推进
                if (selfPendingUserId == 0)
                {
                    return true; // 需要通知当前步骤的待签人
                }
                else
                {
                    string reviewMode = stepInfo.ReviewMode;
                    if (reviewMode == ReviewMode.Single.ToEnumString())
                    {
                        var selfAppointments = await GetStepReviewUsers(formId, stepInfo, _loginuser.UserId);

                        await _db.Deleteable<PendingReviewEntity>()
                                 .Where(pending => pending.FormId == formId && pending.StepId == stepInfo.StepId)
                                 .ExecuteCommandAsync();

                        await InsertReviewRecords(formId, stepInfo.StepId, selfAppointments, ReviewType.Automatic, string.Empty, _loginuser.UserId);
                    }
                    else if (reviewMode == ReviewMode.OrSingle.ToEnumString())
                    {
                        var selfAppointments = await GetStepReviewUsers(formId, stepInfo, _loginuser.UserId);

                        var otherPendingUserIds = await _db.Queryable<PendingReviewEntity>()
                                                           .With(SqlWith.NoLock)
                                                           .Where(pending => pending.FormId == formId
                                                                          && pending.StepId == stepInfo.StepId
                                                                          && pending.ReviewUserId != selfPendingUserId)
                                                           .Select(pending => pending.ReviewUserId)
                                                           .ToListAsync();

                        await _db.Deleteable<PendingReviewEntity>()
                                 .Where(pending => pending.FormId == formId && pending.StepId == stepInfo.StepId)
                                 .ExecuteCommandAsync();

                        await InsertReviewRecords(formId, stepInfo.StepId, selfAppointments, ReviewType.Automatic, string.Empty, _loginuser.UserId);

                        foreach (long otherUserId in otherPendingUserIds)
                        {
                            var otherAppointments = await GetStepReviewUsers(formId, stepInfo, otherUserId);
                            await InsertReviewRecords(formId, stepInfo.StepId, otherAppointments, ReviewType.Automatic, string.Empty, otherUserId);
                        }
                    }
                    else if (reviewMode == ReviewMode.AndSingle.ToEnumString())
                    {
                        var selfAppointments = await GetStepReviewUsers(formId, stepInfo, _loginuser.UserId);

                        await _db.Deleteable<PendingReviewEntity>()
                                 .Where(pending => pending.FormId == formId && pending.StepId == stepInfo.StepId && pending.ReviewUserId == selfPendingUserId)
                                 .ExecuteCommandAsync();

                        await InsertReviewRecords(formId, stepInfo.StepId, selfAppointments, ReviewType.Automatic, string.Empty, _loginuser.UserId);

                        // 会签还有其他人未签，停止自动推进
                        bool othersPending = await _db.Queryable<PendingReviewEntity>()
                                                      .With(SqlWith.NoLock)
                                                      .Where(pending => pending.FormId == formId && pending.StepId == stepInfo.StepId)
                                                      .AnyAsync();
                        if (othersPending)
                        {
                            return true; // 需要通知剩余待签人
                        }
                    }
                }

                // 签核完成，推进到下一步骤
                var nextStep = await GetNextStep(ruleId, stepInfo.StepId);

                if (nextStep.NextStepId == 0)
                {
                    await ApproveForm(formId);
                    return false; // 签核到底，无需通知
                }

                await AdvanceCurrentStep(formId, nextStep.NextStepId);
            }
        }

        /// <summary>
        /// 规则1、处理某步骤的签核
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="stepInfo"></param>
        /// <param name="reviewType"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        private async Task<bool> ProcessStepApproval(long formId, WorkflowStepEntity stepInfo, ReviewType reviewType, string comment)
        {
            string reviewMode = stepInfo.ReviewMode;
            var selfAppointments = await GetStepReviewUsers(formId, stepInfo, _loginuser.UserId);

            // 取当前登录用户在待审批人表中对应的归属人 UserId（登录用户可能是代理人）
            long selfOriginalUserId = await _db.Queryable<PendingReviewEntity>()
                                               .With(SqlWith.NoLock)
                                               .LeftJoin<UserAgentEntity>((pending, agent) => pending.ReviewUserId == agent.SubstituteUserId && agent.StartTime <= DateTime.Now && agent.EndTime >= DateTime.Now)
                                               .Where((pending, agent) => pending.FormId == formId && pending.StepId == stepInfo.StepId && (pending.ReviewUserId == _loginuser.UserId || agent.AgentUserId == _loginuser.UserId))
                                               .Select((pending, agent) => pending.ReviewUserId)
                                               .FirstAsync();

            if (reviewMode == ReviewMode.Single.ToEnumString())
            {
                await _db.Deleteable<PendingReviewEntity>()
                         .Where(pending => pending.FormId == formId && pending.StepId == stepInfo.StepId)
                         .ExecuteCommandAsync();

                await InsertReviewRecords(formId, stepInfo.StepId, selfAppointments, reviewType, comment, _loginuser.UserId);
            }
            else if (reviewMode == ReviewMode.OrSingle.ToEnumString())
            {
                // 排除自己，其余人仍需记录
                var otherPendingUserIds = await _db.Queryable<PendingReviewEntity>()
                                                   .With(SqlWith.NoLock)
                                                   .Where(pending => pending.FormId == formId && pending.StepId == stepInfo.StepId && pending.ReviewUserId != selfOriginalUserId)
                                                   .Select(pending => pending.ReviewUserId)
                                                   .ToListAsync();

                await _db.Deleteable<PendingReviewEntity>()
                         .Where(pending => pending.FormId == formId && pending.StepId == stepInfo.StepId)
                         .ExecuteCommandAsync();

                await InsertReviewRecords(formId, stepInfo.StepId, selfAppointments, reviewType, comment, _loginuser.UserId);

                foreach (long otherUserId in otherPendingUserIds)
                {
                    // 其他人的自动操作记录：操作人视为归属人本人
                    var otherAppointments = await GetStepReviewUsers(formId, stepInfo, otherUserId);
                    await InsertReviewRecords(formId, stepInfo.StepId, otherAppointments, ReviewType.Automatic, string.Empty, otherUserId);
                }
            }
            else if (reviewMode == ReviewMode.AndSingle.ToEnumString())
            {
                // 会签：按归属人 UserId 删除自己的待审批记录
                await _db.Deleteable<PendingReviewEntity>()
                         .Where(pending => pending.FormId == formId && pending.StepId == stepInfo.StepId && pending.ReviewUserId == selfOriginalUserId)
                         .ExecuteCommandAsync();

                await InsertReviewRecords(formId, stepInfo.StepId, selfAppointments, reviewType, comment, _loginuser.UserId);
            }

            await _db.Updateable<FormInstanceEntity>()
                     .SetColumns(instance => instance.FormStatus == FormStatus.UnderReview.ToEnumString())
                     .Where(instance => instance.FormId == formId)
                     .ExecuteCommandAsync();

            return await _db.Queryable<PendingReviewEntity>()
                            .With(SqlWith.NoLock)
                            .Where(pending => pending.FormId == formId && pending.StepId == stepInfo.StepId)
                            .AnyAsync();
        }

        /// <summary>
        /// 初始化指定步骤的待审批人
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="stepId"></param>
        /// <returns></returns>
        private async Task EnsurePendingReviewExists(long formId, long stepId)
        {
            bool hasPending = await _db.Queryable<PendingReviewEntity>()
                                       .With(SqlWith.NoLock)
                                       .Where(pending => pending.FormId == formId && pending.StepId == stepId)
                                       .AnyAsync();
            if (hasPending)
            {
                return;
            }
            else
            {
                var stepInfo = await _db.Queryable<WorkflowStepEntity>()
                                        .With(SqlWith.NoLock)
                                        .Where(step => step.StepId == stepId)
                                        .FirstAsync();

                var reviewUsers = await GetStepReviewUsers(formId, stepInfo);

                // 始终存实/兼归属人（UserId），不存代理人
                var pendingRecords = reviewUsers
                                    .Select(u => u.UserId)
                                    .Distinct()
                                    .Select(userId => new PendingReviewEntity
                                    {
                                        FormId = formId,
                                        StepId = stepId,
                                        ReviewUserId = userId,
                                    }).ToList();
                await _db.Insertable(pendingRecords).ExecuteCommandAsync();
            }
        }

        /// <summary>
        /// 查询步骤所有审批人身份，不传 reviewUserId 时返回全部
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="stepInfo"></param>
        /// <param name="reviewUserId"></param>
        /// <returns></returns>
        private async Task<List<UserAppointment>> GetStepReviewUsers(long formId, WorkflowStepEntity stepInfo, long? reviewUserId = null)
        {
            var formDetail = await _db.Queryable<FormInstanceEntity>()
                                      .With(SqlWith.NoLock)
                                      .InnerJoin<UserInfoEntity>((instance, user) => instance.ApplicantUserId == user.UserId)
                                      .InnerJoin<DepartmentInfoEntity>((instance, user, dept) => user.DepartmentId == dept.DepartmentId)
                                      .InnerJoin<DepartmentLevelEntity>((instance, user, dept, deptlevel) => dept.DepartmentLevelId == deptlevel.DepartmentLevelId)
                                      .InnerJoin<PositionInfoEntity>((instance, user, dept, deptlevel, position) => user.PositionId == position.PositionId)
                                      .Where((instance, user, dept, deptlevel, position) => instance.FormId == formId)
                                      .Select((instance, user, dept, deptlevel, position) => new ApplicantFormDetail
                                      {
                                          FormId = instance.FormId,
                                          RuleId = instance.RuleId,
                                          ApplicantUserId = user.UserId,
                                          ApplicantDeptId = dept.DepartmentId,
                                          DeptLevelSort = deptlevel.SortOrder,
                                          PositionSort = position.SortOrder,
                                      }).FirstAsync();

            var applicantDept = await _db.Queryable<DepartmentInfoEntity>()
                                         .With(SqlWith.NoLock)
                                         .ToParentListAsync(dept => dept.ParentId, formDetail.ApplicantDeptId);

            List<UserAppointment> result;

            if (stepInfo.IsStartStep == 1)
            {
                result = await GetStartReviewUsers(formDetail.ApplicantUserId, stepInfo.ReviewMode);
            }
            else if (stepInfo.Assignment == Assignment.Org.ToEnumString())
            {
                var orgInfo = await _db.Queryable<WorkflowStepOrgEntity>()
                                       .With(SqlWith.NoLock)
                                       .Where(org => org.StepId == stepInfo.StepId)
                                       .FirstAsync();
                result = await GetOrgReviewUsers(applicantDept, orgInfo.DeptLeaveId, orgInfo.PositionId, stepInfo.ReviewMode);
            }
            else if (stepInfo.Assignment == Assignment.DeptUser.ToEnumString())
            {
                var deptUserInfo = await _db.Queryable<WorkflowStepDeptUserEntity>()
                                            .With(SqlWith.NoLock)
                                            .Where(org => org.StepId == stepInfo.StepId)
                                            .FirstAsync();
                result = await GetDeptUserReviewUsers(deptUserInfo.DepartmentId, deptUserInfo.PositionId, stepInfo.ReviewMode);
            }
            else if (stepInfo.Assignment == Assignment.User.ToEnumString())
            {
                var userInfo = await _db.Queryable<WorkflowStepUserEntity>()
                                        .With(SqlWith.NoLock)
                                        .Where(org => org.StepId == stepInfo.StepId)
                                        .FirstAsync();
                result = await GetUserReviewUsers(userInfo.UserId, stepInfo.ReviewMode);
            }
            else
            {
                return new List<UserAppointment>();
            }

            // 传入 reviewUserId 时，只返回匹配该人的记录
            if (reviewUserId.HasValue)
                result = result.Where(result => result.UserId == reviewUserId.Value || result.AgentUserId == reviewUserId.Value).ToList();

            return result;
        }


        #region 查询各指派类型审批人身份

        /// <summary>
        /// 起始步骤 - 查询审批人身份
        /// </summary>
        private async Task<List<UserAppointment>> GetStartReviewUsers(long applicantUserId, string reviewMode)
        {
            bool isSingle = reviewMode == ReviewMode.Single.ToEnumString();
            string topN = isSingle ? "TOP 1" : "";

            var (actual, agent, concurrent, concurrentAgent, autoActual, autoAgent, autoConcurrent, autoConcurrentAgent) = AppointmentEnumStrings();
            var now = DateTime.Now;

            string orderBy = BuildOrderBy(isSingle, isAuto: false);

            string sql = $@"SELECT {topN}
                                t.UserId,
                                t.AgentUserId,
                                t.AppointmentTypeCode
                            FROM (
                                SELECT
                                    users.UserId,
                                    ISNULL(agentusers.UserId, 0)           AS AgentUserId,
                                    CASE WHEN agent.AgentUserId IS NOT NULL THEN @Agent ELSE @Actual END AS AppointmentTypeCode,
                                    users.HireDate
                                FROM Basic.UserInfo users
                                LEFT JOIN Basic.UserAgent agent     ON users.UserId      = agent.SubstituteUserId
                                                                   AND agent.StartTime  <= @Now
                                                                   AND agent.EndTime    >= @Now
                                LEFT JOIN Basic.UserInfo agentusers ON agent.AgentUserId = agentusers.UserId
                                WHERE users.UserId = @ApplicantUserId
                            ) t
                            {orderBy}";

            var result = await _db.Ado.SqlQueryAsync<UserAppointment>(sql, new[]
            {
                new SugarParameter("@Now", now),
                new SugarParameter("@Actual", actual),
                new SugarParameter("@Agent", agent),
                new SugarParameter("@ApplicantUserId", applicantUserId),
            });

            return result ?? new List<UserAppointment>();
        }

        /// <summary>
        /// 查询审批人身份 - 按组织架构
        /// </summary>
        public async Task<List<UserAppointment>> GetOrgReviewUsers(List<DepartmentInfoEntity> applicantParentDept, long deptLeaveId, long positionId, string reviewMode)
        {
            var deptlevel = await _db.Queryable<DepartmentLevelEntity>()
                                     .With(SqlWith.NoLock)
                                     .Where(deptlevel => deptlevel.DepartmentLevelId == deptLeaveId)
                                     .FirstAsync();

            var posInfo = await _db.Queryable<PositionInfoEntity>()
                                   .With(SqlWith.NoLock)
                                   .Where(position => position.PositionId == positionId)
                                   .FirstAsync();

            string parentDeptIdsStr = string.Join(",", applicantParentDept.Select(dept => dept.DepartmentId));
            bool isSingle = reviewMode == ReviewMode.Single.ToEnumString();
            string topN = isSingle ? "TOP 1" : "";

            var (actual, agent, concurrent, concurrentAgent, autoActual, autoAgent, autoConcurrent, autoConcurrentAgent) = AppointmentEnumStrings();
            var now = DateTime.Now;

            string exactOrderBy = BuildOrderBy(isSingle, isAuto: false);
            string autoOrderBy = BuildOrderBy(isSingle, isAuto: true);

            var exactResult = await _db.Ado.SqlQueryAsync<UserAppointment>($@"
                SELECT {topN}
                    t.UserId, t.AgentUserId, t.AppointmentTypeCode
                FROM (
                    SELECT
                        users.UserId,
                        ISNULL(agentusers.UserId, 0)           AS AgentUserId,
                        CASE WHEN agent.AgentUserId IS NOT NULL THEN @Agent ELSE @Actual END AS AppointmentTypeCode,
                        users.HireDate
                    FROM Basic.UserInfo users
                    INNER JOIN Basic.DepartmentInfo  dept      ON users.DepartmentId     = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo    position  ON users.PositionId       = position.PositionId
                    LEFT JOIN  Basic.UserAgent       agent     ON users.UserId           = agent.SubstituteUserId
                                                              AND agent.StartTime       <= @Now AND agent.EndTime >= @Now
                    LEFT JOIN  Basic.UserInfo agentusers       ON agent.AgentUserId      = agentusers.UserId
                    WHERE dept.DepartmentId IN ({parentDeptIdsStr})
                      AND deptlevel.SortOrder = @DeptLevelSort AND position.SortOrder = @PositionSort
                      AND users.IsApproval = 1 AND users.IsEmployed = 1 AND users.IsFreeze = 0
                    UNION ALL
                    SELECT
                        users.UserId,
                        ISNULL(agentusers.UserId, 0)           AS AgentUserId,
                        CASE WHEN agent.AgentUserId IS NOT NULL THEN @ConcurrentAgent ELSE @Concurrent END AS AppointmentTypeCode,
                        users.HireDate
                    FROM Basic.UserPartTime partime
                    INNER JOIN Basic.UserInfo        users     ON partime.UserId             = users.UserId
                    INNER JOIN Basic.DepartmentInfo  dept      ON partime.PartTimeDeptId     = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo    position  ON partime.PartTimePositionId = position.PositionId
                    LEFT JOIN  Basic.UserAgent       agent     ON users.UserId               = agent.SubstituteUserId
                                                              AND agent.StartTime           <= @Now AND agent.EndTime >= @Now
                    LEFT JOIN  Basic.UserInfo agentusers       ON agent.AgentUserId          = agentusers.UserId
                    WHERE dept.DepartmentId IN ({parentDeptIdsStr})
                      AND deptlevel.SortOrder = @DeptLevelSort AND position.SortOrder = @PositionSort
                      AND users.IsApproval = 1 AND users.IsEmployed = 1 AND users.IsFreeze = 0
                ) t
                {exactOrderBy}",
                new[]
                {
                    new SugarParameter("@Now", now),
                    new SugarParameter("@DeptLevelSort", deptlevel.SortOrder),
                    new SugarParameter("@PositionSort", posInfo.SortOrder),
                    new SugarParameter("@Actual", actual),
                    new SugarParameter("@Agent", agent),
                    new SugarParameter("@Concurrent", concurrent), 
                    new SugarParameter("@ConcurrentAgent",concurrentAgent),
                });

            if (exactResult.Any())
                return exactResult;

            int currentPositionSort = posInfo.SortOrder - 1;
            int currentDeptLevelSort = deptlevel.SortOrder;

            while (currentPositionSort >= 1)
            {
                while (currentDeptLevelSort >= 1)
                {
                    var autoResult = await _db.Ado.SqlQueryAsync<UserAppointment>($@"
                        SELECT {topN}
                            t.UserId, t.AgentUserId, t.AppointmentTypeCode
                        FROM (
                            SELECT
                                users.UserId,
                                ISNULL(agentusers.UserId, 0)           AS AgentUserId,
                                CASE WHEN agent.AgentUserId IS NOT NULL THEN @AutoAgent ELSE @AutoActual END AS AppointmentTypeCode,
                                users.HireDate
                            FROM Basic.UserInfo users
                            INNER JOIN Basic.DepartmentInfo  dept      ON users.DepartmentId     = dept.DepartmentId
                            INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                            INNER JOIN Basic.PositionInfo    position  ON users.PositionId       = position.PositionId
                            LEFT JOIN  Basic.UserAgent       agent     ON users.UserId           = agent.SubstituteUserId
                                                                      AND agent.StartTime       <= @Now AND agent.EndTime >= @Now
                            LEFT JOIN  Basic.UserInfo agentusers       ON agent.AgentUserId      = agentusers.UserId
                            WHERE dept.DepartmentId IN ({parentDeptIdsStr})
                              AND position.SortOrder = @CurrentPositionSort AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsApproval = 1 AND users.IsEmployed = 1 AND users.IsFreeze = 0
                            UNION ALL
                            SELECT
                                users.UserId,
                                ISNULL(agentusers.UserId, 0)           AS AgentUserId,
                                CASE WHEN agent.AgentUserId IS NOT NULL THEN @AutoConcurrentAgent ELSE @AutoConcurrent END AS AppointmentTypeCode,
                                users.HireDate
                            FROM Basic.UserPartTime partime
                            INNER JOIN Basic.UserInfo        users     ON partime.UserId             = users.UserId
                            INNER JOIN Basic.DepartmentInfo  dept      ON partime.PartTimeDeptId     = dept.DepartmentId
                            INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                            INNER JOIN Basic.PositionInfo    position  ON partime.PartTimePositionId = position.PositionId
                            LEFT JOIN  Basic.UserAgent       agent     ON users.UserId               = agent.SubstituteUserId
                                                                      AND agent.StartTime           <= @Now AND agent.EndTime >= @Now
                            LEFT JOIN  Basic.UserInfo agentusers       ON agent.AgentUserId          = agentusers.UserId
                            WHERE dept.DepartmentId IN ({parentDeptIdsStr})
                              AND position.SortOrder = @CurrentPositionSort AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsApproval = 1 AND users.IsEmployed = 1 AND users.IsFreeze = 0
                        ) t
                        {autoOrderBy}",
                        new[]
                        {
                            new SugarParameter("@Now", now),
                            new SugarParameter("@CurrentPositionSort", currentPositionSort),
                            new SugarParameter("@CurrentDeptLevelSort", currentDeptLevelSort),
                            new SugarParameter("@AutoActual", autoActual),
                            new SugarParameter("@AutoAgent", autoAgent),
                            new SugarParameter("@AutoConcurrent", autoConcurrent),
                            new SugarParameter("@AutoConcurrentAgent", autoConcurrentAgent),
                        });

                    if (autoResult.Any())
                        return autoResult;

                    currentDeptLevelSort--;
                }

                currentPositionSort--;
                currentDeptLevelSort = deptlevel.SortOrder;
            }

            return new List<UserAppointment>();
        }

        /// <summary>
        /// 查询审批人身份 - 按指定部门职级
        /// </summary>
        public async Task<List<UserAppointment>> GetDeptUserReviewUsers(long departmentId, long positionId, string reviewMode)
        {
            var posInfo = await _db.Queryable<PositionInfoEntity>()
                                   .With(SqlWith.NoLock)
                                   .Where(position => position.PositionId == positionId)
                                   .FirstAsync();

            var deptInfo = await _db.Queryable<DepartmentInfoEntity>()
                                    .With(SqlWith.NoLock)
                                    .Where(dept => dept.DepartmentId == departmentId)
                                    .FirstAsync();

            var deptlevel = await _db.Queryable<DepartmentLevelEntity>()
                                     .With(SqlWith.NoLock)
                                     .Where(deptlevel => deptlevel.DepartmentLevelId == deptInfo.DepartmentLevelId)
                                     .FirstAsync();

            bool isSingle = reviewMode == ReviewMode.Single.ToEnumString();
            string topN = isSingle ? "TOP 1" : "";

            var (actual, agent, concurrent, concurrentAgent, autoActual, autoAgent, autoConcurrent, autoConcurrentAgent) = AppointmentEnumStrings();
            var now = DateTime.Now;

            string exactOrderBy = BuildOrderBy(isSingle, isAuto: false);
            string autoOrderBy = BuildOrderBy(isSingle, isAuto: true);

            var exactResult = await _db.Ado.SqlQueryAsync<UserAppointment>($@"
                SELECT {topN}
                    t.UserId, t.AgentUserId, t.AppointmentTypeCode
                FROM (
                    SELECT
                        users.UserId,
                        ISNULL(agentusers.UserId, 0)           AS AgentUserId,
                        CASE WHEN agent.AgentUserId IS NOT NULL THEN @Agent ELSE @Actual END AS AppointmentTypeCode,
                        users.HireDate
                    FROM Basic.UserInfo users
                    INNER JOIN Basic.DepartmentInfo  dept      ON users.DepartmentId     = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo    position  ON users.PositionId       = position.PositionId
                    LEFT JOIN  Basic.UserAgent       agent     ON users.UserId           = agent.SubstituteUserId
                                                              AND agent.StartTime       <= @Now AND agent.EndTime >= @Now
                    LEFT JOIN  Basic.UserInfo agentusers       ON agent.AgentUserId      = agentusers.UserId
                    WHERE dept.DepartmentId = @DepartmentId AND position.SortOrder = @PositionSort
                      AND users.IsApproval = 1 AND users.IsEmployed = 1 AND users.IsFreeze = 0
                    UNION ALL
                    SELECT
                        users.UserId,
                        ISNULL(agentusers.UserId, 0)           AS AgentUserId,
                        CASE WHEN agent.AgentUserId IS NOT NULL THEN @ConcurrentAgent ELSE @Concurrent END AS AppointmentTypeCode,
                        users.HireDate
                    FROM Basic.UserPartTime partime
                    INNER JOIN Basic.UserInfo        users     ON partime.UserId             = users.UserId
                    INNER JOIN Basic.DepartmentInfo  dept      ON partime.PartTimeDeptId     = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo    position  ON partime.PartTimePositionId = position.PositionId
                    LEFT JOIN  Basic.UserAgent       agent     ON users.UserId               = agent.SubstituteUserId
                                                              AND agent.StartTime           <= @Now AND agent.EndTime >= @Now
                    LEFT JOIN  Basic.UserInfo agentusers       ON agent.AgentUserId          = agentusers.UserId
                    WHERE dept.DepartmentId = @DepartmentId AND position.SortOrder = @PositionSort
                      AND users.IsApproval = 1 AND users.IsEmployed = 1 AND users.IsFreeze = 0
                ) t
                {exactOrderBy}",
                new[]
                {
                    new SugarParameter("@Now", now),
                    new SugarParameter("@DepartmentId", departmentId),
                    new SugarParameter("@PositionSort", posInfo.SortOrder),
                    new SugarParameter("@Actual", actual),
                    new SugarParameter("@Agent", agent),
                    new SugarParameter("@Concurrent", concurrent),
                    new SugarParameter("@ConcurrentAgent", concurrentAgent),
                });

            if (exactResult.Any())
                return exactResult;

            int currentPositionSort = posInfo.SortOrder - 1;
            int currentDeptLevelSort = deptlevel.SortOrder;

            while (currentPositionSort >= 1)
            {
                while (currentDeptLevelSort >= 1)
                {
                    var autoResult = await _db.Ado.SqlQueryAsync<UserAppointment>($@"
                        SELECT {topN}
                            t.UserId, t.AgentUserId, t.AppointmentTypeCode
                        FROM (
                            SELECT
                                users.UserId,
                                ISNULL(agentusers.UserId, 0)           AS AgentUserId,
                                CASE WHEN agent.AgentUserId IS NOT NULL THEN @AutoAgent ELSE @AutoActual END AS AppointmentTypeCode,
                                users.HireDate
                            FROM Basic.UserInfo users
                            INNER JOIN Basic.DepartmentInfo  dept      ON users.DepartmentId     = dept.DepartmentId
                            INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                            INNER JOIN Basic.PositionInfo    position  ON users.PositionId       = position.PositionId
                            LEFT JOIN  Basic.UserAgent       agent     ON users.UserId           = agent.SubstituteUserId
                                                                      AND agent.StartTime       <= @Now AND agent.EndTime >= @Now
                            LEFT JOIN  Basic.UserInfo agentusers       ON agent.AgentUserId      = agentusers.UserId
                            WHERE dept.DepartmentId = @DepartmentId
                              AND position.SortOrder = @CurrentPositionSort AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsApproval = 1 AND users.IsEmployed = 1 AND users.IsFreeze = 0
                            UNION ALL
                            SELECT
                                users.UserId,
                                ISNULL(agentusers.UserId, 0)           AS AgentUserId,
                                CASE WHEN agent.AgentUserId IS NOT NULL THEN @AutoConcurrentAgent ELSE @AutoConcurrent END AS AppointmentTypeCode,
                                users.HireDate
                            FROM Basic.UserPartTime partime
                            INNER JOIN Basic.UserInfo        users     ON partime.UserId             = users.UserId
                            INNER JOIN Basic.DepartmentInfo  dept      ON partime.PartTimeDeptId     = dept.DepartmentId
                            INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                            INNER JOIN Basic.PositionInfo    position  ON partime.PartTimePositionId = position.PositionId
                            LEFT JOIN  Basic.UserAgent       agent     ON users.UserId               = agent.SubstituteUserId
                                                                      AND agent.StartTime           <= @Now AND agent.EndTime >= @Now
                            LEFT JOIN  Basic.UserInfo agentusers       ON agent.AgentUserId          = agentusers.UserId
                            WHERE dept.DepartmentId = @DepartmentId
                              AND position.SortOrder = @CurrentPositionSort AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsApproval = 1 AND users.IsEmployed = 1 AND users.IsFreeze = 0
                        ) t
                        {autoOrderBy}",
                        new[]
                        {
                            new SugarParameter("@Now", now),
                            new SugarParameter("@DepartmentId", departmentId),
                            new SugarParameter("@CurrentPositionSort", currentPositionSort),
                            new SugarParameter("@CurrentDeptLevelSort", currentDeptLevelSort),
                            new SugarParameter("@AutoActual", autoActual),
                            new SugarParameter("@AutoAgent", autoAgent),
                            new SugarParameter("@AutoConcurrent", autoConcurrent),
                            new SugarParameter("@AutoConcurrentAgent", autoConcurrentAgent),
                        });

                    if (autoResult.Any())
                        return autoResult;

                    currentDeptLevelSort--;
                }

                currentPositionSort--;
                currentDeptLevelSort = deptlevel.SortOrder;
            }

            return new List<UserAppointment>();
        }

        /// <summary>
        /// 查询审批人身份 - 按指定人
        /// </summary>
        public async Task<List<UserAppointment>> GetUserReviewUsers(long userId, string reviewMode)
        {
            var userInfo = await _db.Queryable<UserInfoEntity>()
                                    .With(SqlWith.NoLock)
                                    .Where(user => user.UserId == userId)
                                    .FirstAsync();

            var posInfo = await _db.Queryable<PositionInfoEntity>()
                                   .With(SqlWith.NoLock)
                                   .Where(position => position.PositionId == userInfo.PositionId)
                                   .FirstAsync();

            var deptInfo = await _db.Queryable<DepartmentInfoEntity>()
                                    .With(SqlWith.NoLock)
                                    .Where(dept => dept.DepartmentId == userInfo.DepartmentId)
                                    .FirstAsync();

            var deptlevel = await _db.Queryable<DepartmentLevelEntity>()
                                     .With(SqlWith.NoLock)
                                     .Where(deptlevel => deptlevel.DepartmentLevelId == deptInfo.DepartmentLevelId)
                                     .FirstAsync();

            bool isSingle = reviewMode == ReviewMode.Single.ToEnumString();
            string topN = isSingle ? "TOP 1" : "";

            var (actual, agent, concurrent, concurrentAgent, autoActual, autoAgent, autoConcurrent, autoConcurrentAgent) = AppointmentEnumStrings();
            var now = DateTime.Now;

            string exactOrderBy = BuildOrderBy(isSingle, isAuto: false);
            string autoOrderBy = BuildOrderBy(isSingle, isAuto: true);

            var exactResult = await _db.Ado.SqlQueryAsync<UserAppointment>($@"
                SELECT {topN}
                    t.UserId, t.AgentUserId, t.AppointmentTypeCode
                FROM (
                    SELECT
                        users.UserId,
                        ISNULL(agentusers.UserId, 0)           AS AgentUserId,
                        CASE WHEN agent.AgentUserId IS NOT NULL THEN @Agent ELSE @Actual END AS AppointmentTypeCode,
                        users.HireDate
                    FROM Basic.UserInfo users
                    LEFT JOIN Basic.UserAgent agent     ON users.UserId       = agent.SubstituteUserId
                                                       AND agent.StartTime  <= @Now AND agent.EndTime >= @Now
                    LEFT JOIN Basic.UserInfo agentusers ON agent.AgentUserId  = agentusers.UserId
                    WHERE users.UserId = @UserId
                      AND users.IsApproval = 1 AND users.IsEmployed = 1 AND users.IsFreeze = 0
                    UNION ALL
                    SELECT
                        users.UserId,
                        ISNULL(agentusers.UserId, 0)           AS AgentUserId,
                        CASE WHEN agent.AgentUserId IS NOT NULL THEN @ConcurrentAgent ELSE @Concurrent END AS AppointmentTypeCode,
                        users.HireDate
                    FROM Basic.UserPartTime partime
                    INNER JOIN Basic.UserInfo  users     ON partime.UserId    = users.UserId
                    LEFT JOIN  Basic.UserAgent agent     ON users.UserId      = agent.SubstituteUserId
                                                       AND agent.StartTime  <= @Now AND agent.EndTime >= @Now
                    LEFT JOIN  Basic.UserInfo agentusers ON agent.AgentUserId = agentusers.UserId
                    WHERE partime.UserId = @UserId
                      AND users.IsApproval = 1 AND users.IsEmployed = 1 AND users.IsFreeze = 0
                ) t
                {exactOrderBy}",
                new[]
                {
                    new SugarParameter("@Now", now),
                    new SugarParameter("@UserId", userId),
                    new SugarParameter("@Actual", actual),
                    new SugarParameter("@Agent", agent),
                    new SugarParameter("@Concurrent", concurrent),
                    new SugarParameter("@ConcurrentAgent", concurrentAgent),
                });

            if (exactResult.Any())
                return exactResult;

            int currentPositionSort = posInfo.SortOrder - 1;
            int currentDeptLevelSort = deptlevel.SortOrder;

            while (currentPositionSort >= 1)
            {
                while (currentDeptLevelSort >= 1)
                {
                    var autoResult = await _db.Ado.SqlQueryAsync<UserAppointment>($@"
                        SELECT {topN}
                            t.UserId, t.AgentUserId, t.AppointmentTypeCode
                        FROM (
                            SELECT
                                users.UserId,
                                ISNULL(agentusers.UserId, 0)           AS AgentUserId,
                                CASE WHEN agent.AgentUserId IS NOT NULL THEN @AutoAgent ELSE @AutoActual END AS AppointmentTypeCode,
                                users.HireDate
                            FROM Basic.UserInfo users
                            INNER JOIN Basic.DepartmentInfo  dept      ON users.DepartmentId     = dept.DepartmentId
                            INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                            INNER JOIN Basic.PositionInfo    position  ON users.PositionId       = position.PositionId
                            LEFT JOIN  Basic.UserAgent       agent     ON users.UserId           = agent.SubstituteUserId
                                                                      AND agent.StartTime       <= @Now AND agent.EndTime >= @Now
                            LEFT JOIN  Basic.UserInfo agentusers       ON agent.AgentUserId      = agentusers.UserId
                            WHERE dept.DepartmentId = @DepartmentId
                              AND position.SortOrder = @CurrentPositionSort AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsApproval = 1 AND users.IsEmployed = 1 AND users.IsFreeze = 0
                            UNION ALL
                            SELECT
                                users.UserId,
                                ISNULL(agentusers.UserId, 0)           AS AgentUserId,
                                CASE WHEN agent.AgentUserId IS NOT NULL THEN @AutoConcurrentAgent ELSE @AutoConcurrent END AS AppointmentTypeCode,
                                users.HireDate
                            FROM Basic.UserPartTime partime
                            INNER JOIN Basic.UserInfo        users     ON partime.UserId             = users.UserId
                            INNER JOIN Basic.DepartmentInfo  dept      ON partime.PartTimeDeptId     = dept.DepartmentId
                            INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                            INNER JOIN Basic.PositionInfo    position  ON partime.PartTimePositionId = position.PositionId
                            LEFT JOIN  Basic.UserAgent       agent     ON users.UserId               = agent.SubstituteUserId
                                                                      AND agent.StartTime           <= @Now AND agent.EndTime >= @Now
                            LEFT JOIN  Basic.UserInfo agentusers       ON agent.AgentUserId          = agentusers.UserId
                            WHERE dept.DepartmentId = @DepartmentId
                              AND position.SortOrder = @CurrentPositionSort AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsApproval = 1 AND users.IsEmployed = 1 AND users.IsFreeze = 0
                        ) t
                        {autoOrderBy}",
                        new[]
                        {
                            new SugarParameter("@Now", now),
                            new SugarParameter("@DepartmentId", userInfo.DepartmentId),
                            new SugarParameter("@CurrentPositionSort", currentPositionSort),
                            new SugarParameter("@CurrentDeptLevelSort", currentDeptLevelSort),
                            new SugarParameter("@AutoActual", autoActual),
                            new SugarParameter("@AutoAgent", autoAgent),
                            new SugarParameter("@AutoConcurrent", autoConcurrent),
                            new SugarParameter("@AutoConcurrentAgent", autoConcurrentAgent),
                        });

                    if (autoResult.Any())
                        return autoResult;

                    currentDeptLevelSort--;
                }

                currentPositionSort--;
                currentDeptLevelSort = deptlevel.SortOrder;
            }

            return new List<UserAppointment>();
        }
        #endregion

        #region 步骤跳过 / 推进 / 状态更新

        /// <summary>
        /// 检查当前步骤是否应跳过
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="stepInfo"></param>
        /// <returns></returns>
        private async Task<bool> ShouldSkipStep(long formId, WorkflowStepEntity stepInfo)
        {
            if (stepInfo.Assignment != Assignment.Org.ToEnumString())
            {
                return false;
            }
            else
            {
                var formDetail = await _db.Queryable<FormInstanceEntity>()
                                          .With(SqlWith.NoLock)
                                          .InnerJoin<UserInfoEntity>((instance, user) => instance.ApplicantUserId == user.UserId)
                                          .InnerJoin<DepartmentInfoEntity>((instance, user, dept) => user.DepartmentId == dept.DepartmentId)
                                          .InnerJoin<DepartmentLevelEntity>((instance, user, dept, deptlevel) => dept.DepartmentLevelId ==     deptlevel.DepartmentLevelId)
                                          .InnerJoin<PositionInfoEntity>((instance, user, dept, deptlevel, position) => user.PositionId ==     position.PositionId)
                                          .Where((instance, user, dept, deptlevel, position) => instance.FormId == formId)
                                          .Select((instance, user, dept, deptlevel, position) => new
                                          {
                                              DeptLevelSort = deptlevel.SortOrder,
                                              PositionSort = position.SortOrder,
                                          }).FirstAsync();

                var orgInfo = await _db.Queryable<WorkflowStepOrgEntity>()
                                       .With(SqlWith.NoLock)
                                       .Where(o => o.StepId == stepInfo.StepId)
                                       .FirstAsync();

                var configDeptLevel = await _db.Queryable<DepartmentLevelEntity>()
                                               .With(SqlWith.NoLock)
                                               .Where(d => d.DepartmentLevelId == orgInfo.DeptLeaveId)
                                               .FirstAsync();

                var configPosition = await _db.Queryable<PositionInfoEntity>()
                                              .With(SqlWith.NoLock)
                                              .Where(p => p.PositionId == orgInfo.PositionId)
                                              .FirstAsync();

                return formDetail.DeptLevelSort < configDeptLevel.SortOrder || formDetail.PositionSort <= configPosition.SortOrder;
            }
        }

        /// <summary>
        /// 取当前步骤对应的流程规则
        /// </summary>
        /// <param name="ruleId"></param>
        /// <param name="currentStepId"></param>
        /// <returns></returns>
        private async Task<WorkflowRuleStepEntity> GetNextStep(long ruleId, long currentStepId)
        {
            return await _db.Queryable<WorkflowRuleStepEntity>()
                            .Where(rulestep => rulestep.RuleId == ruleId && rulestep.CurrentStepId == currentStepId)
                            .FirstAsync();
        }

        /// <summary>
        /// 推进表单下一步骤
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="nextStepId"></param>
        /// <returns></returns>
        private async Task AdvanceCurrentStep(long formId, long nextStepId)
        {
            await _db.Updateable<FormInstanceEntity>()
                     .SetColumns(instance => new FormInstanceEntity
                     {
                         CurrentStepId = nextStepId,
                     }).Where(instance => instance.FormId == formId)
                     .ExecuteCommandAsync();
        }

        /// <summary>
        /// 更改表单为签核完成
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        private async Task ApproveForm(long formId)
        {
            await _db.Updateable<FormInstanceEntity>()
                     .SetColumns(instance => new FormInstanceEntity
                     {
                         FormStatus = FormStatus.Approved.ToEnumString(),
                         ModifiedBy = _loginuser.UserId,
                         ModifiedDate = DateTime.Now
                     }).Where(instance => instance.FormId == formId)
                     .ExecuteCommandAsync();
        }

        /// <summary>
        /// 获取表单当前步骤信息，同时返回 FormInstance 的 RuleId
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        private async Task<(WorkflowStepEntity StepInfo, long RuleId)> GetCurrentStepInfo(long formId)
        {
            var row = await _db.Queryable<FormInstanceEntity>()
                               .With(SqlWith.NoLock)
                               .InnerJoin<WorkflowStepEntity>((instance, step) => instance.CurrentStepId == step.StepId)
                               .Where((instance, step) => instance.FormId == formId)
                               .Select((instance, step) => new
                               {
                                   RuleId = instance.RuleId,
                                   StepInfo = step,
                               }).FirstAsync();

            return (row.StepInfo, row.RuleId);
        }
        #endregion

        /// <summary>
        /// 记录审批日志
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="stepId"></param>
        /// <param name="appointments"></param>
        /// <param name="reviewType"></param>
        /// <param name="comment"></param>
        /// <param name="operatorUserId"></param>
        /// <returns></returns>
        private async Task InsertReviewRecords(long formId, long stepId, List<UserAppointment> appointments, ReviewType reviewType, string comment, long operatorUserId)
        {
            if (!appointments.Any())
            {
                return;
            }
            else
            {
                var agentActual = AppointmentType.Agent.ToEnumString();
                var agentConcurrent = AppointmentType.ConcurrentAgent.ToEnumString();
                var autoAgentActual = AppointmentType.AutoAgent.ToEnumString();
                var autoAgentConc = AppointmentType.AutoConcurrentAgent.ToEnumString();

                var records = appointments.Select(appoint =>
                {
                    bool isAgentOp = appoint.AgentUserId != 0 && appoint.AgentUserId == operatorUserId;

                    // 代理操作：ReviewUserId 取代理人，AppointmentTypeCode 保持代理身份（Agent/ConcurrentAgent）
                    // 本人操作：ReviewUserId 取归属人，AppointmentTypeCode 换回实/兼身份（去掉 Agent 后缀）
                    string appointmentCode;
                    long reviewUserId;

                    if (isAgentOp)
                    {
                        appointmentCode = appoint.AppointmentTypeCode; // 已是代理身份
                        reviewUserId = operatorUserId;
                    }
                    else
                    {
                        // 本人操作，将 AppointmentTypeCode 中的代理身份还原为实/兼身份
                        appointmentCode = appoint.AppointmentTypeCode switch
                        {
                            var c when c == agentActual => AppointmentType.Actual.ToEnumString(),
                            var c when c == agentConcurrent => AppointmentType.Concurrent.ToEnumString(),
                            var c when c == autoAgentActual => AppointmentType.AutoActual.ToEnumString(),
                            var c when c == autoAgentConc => AppointmentType.AutoConcurrent.ToEnumString(),
                            _ => appoint.AppointmentTypeCode,
                        };
                        reviewUserId = appoint.UserId;
                    }

                    return new FormReviewRecordEntity
                    {
                        FormId = formId,
                        StepId = stepId,
                        ReviewResult = ReviewResult.Approve.ToEnumString(),
                        RejectStepId = null,
                        Comment = comment,
                        ReviewType = reviewType.ToEnumString(),
                        ReviewAppointment = appointmentCode,
                        OriginalUserId = appoint.UserId,
                        ReviewUserId = reviewUserId,
                        ReviewDateTime = DateTime.Now,
                    };
                }).ToList();
                await _db.Insertable(records).ExecuteCommandAsync();
            }
        }

        #region 邮件通知
        /// <summary>
        /// 查询指定步骤的待签核人（归属人 + 其当前有效代理人），去重后发送邮件通知
        /// </summary>
        private async Task NotifyPendingReviewers(long formId, long stepId)
        {
            var now = DateTime.Now;

            var pendingUserIds = await _db.Queryable<PendingReviewEntity>()
                                          .With(SqlWith.NoLock)
                                          .Where(pending => pending.FormId == formId && pending.StepId == stepId)
                                          .Select(pending => pending.ReviewUserId)
                                          .ToListAsync();

            if (!pendingUserIds.Any())
                return;

            // 查出每个归属人当前有效的代理人，一并纳入通知列表
            var agentUserIds = await _db.Queryable<UserAgentEntity>()
                                        .With(SqlWith.NoLock)
                                        .Where(agent => pendingUserIds.Contains(agent.SubstituteUserId) && agent.StartTime <= now && agent.EndTime >= now)
                                        .Select(agent => agent.AgentUserId)
                                        .ToListAsync();

            var notifyUserIds = pendingUserIds
                .Concat(agentUserIds)
                .Distinct()
                .ToList();

            await SendReviewNotification(notifyUserIds);
        }

        /// <summary>
        /// 发送签核邮件通知
        /// </summary>
        /// <param name="notifyUserIds">需要通知的用户 Id 列表（包含归属人及其代理人）</param>
        private async Task SendReviewNotification(List<long> notifyUserIds)
        {
            // TODO: 实现邮件发送逻辑
            await Task.CompletedTask;
        }
        #endregion


        #region 工具
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="isSingle"></param>
        /// <param name="isAuto"></param>
        /// <returns></returns>
        private string BuildOrderBy(bool isSingle, bool isAuto)
        {
            if (!isSingle)
            {
                return "ORDER BY t.HireDate DESC";
            }
            else
            {
                string c0 = isAuto ? AppointmentType.AutoActual.ToEnumString() : AppointmentType.Actual.ToEnumString();
                string c1 = isAuto ? AppointmentType.AutoAgent.ToEnumString() : AppointmentType.Agent.ToEnumString();
                string c2 = isAuto ? AppointmentType.AutoConcurrent.ToEnumString() : AppointmentType.Concurrent.ToEnumString();
                string c3 = isAuto ? AppointmentType.AutoConcurrentAgent.ToEnumString() : AppointmentType.ConcurrentAgent.ToEnumString();

                return $@"ORDER BY CASE t.AppointmentTypeCode
                        WHEN '{c0}' THEN 0
                        WHEN '{c1}' THEN 1
                        WHEN '{c2}' THEN 2
                        WHEN '{c3}' THEN 3
                        ELSE 9
                    END ASC, t.HireDate DESC";
            }
        }

        /// <summary>
        /// 一次性取出所有 AppointmentType 枚举字符串
        /// </summary>
        /// <returns></returns>
        private (string actual, string agent, string concurrent, string concurrentAgent,string autoActual, string autoAgent, string autoConcurrent, string autoConcurrentAgent) AppointmentEnumStrings() =>
        (
            AppointmentType.Actual.ToEnumString(),
            AppointmentType.Agent.ToEnumString(),
            AppointmentType.Concurrent.ToEnumString(),
            AppointmentType.ConcurrentAgent.ToEnumString(),
            AppointmentType.AutoActual.ToEnumString(),
            AppointmentType.AutoAgent.ToEnumString(),
            AppointmentType.AutoConcurrent.ToEnumString(),
            AppointmentType.AutoConcurrentAgent.ToEnumString()
        );
        #endregion
    }
}