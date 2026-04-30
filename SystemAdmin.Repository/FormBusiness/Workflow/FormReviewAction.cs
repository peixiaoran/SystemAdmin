using SqlSugar;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Entity;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Upsert;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.Workflow.FormReviewFlow;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;

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
        /// 表单审批
        /// </summary>
        /// <param name="reviewForm"></param>
        /// <returns></returns>
        public async Task<bool> FromApprove(ReviewForm reviewForm)
        {
            var stepInfo = _db.Queryable<FormInstanceEntity>()
                              .With(SqlWith.NoLock)
                              .InnerJoin<WorkflowStepEntity>((instance, step) => instance.CurrentStepId == step.StepId)
                              .Where((instance, step) => instance.FormId == long.Parse(reviewForm.FormId))
                              .Select((instance, step) => step)
                              .First();
            // 删除待审批人员并新增审批记录
            var isAnyPendUser = await DeletePendAndInsertRecord(long.Parse(reviewForm.FormId), stepInfo.StepId, stepInfo.ReviewMode, _loginuser.UserId);

            if (isAnyPendUser)
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 删除待审批人员并新增审批记录
        /// </summary>
        /// <param name="reviewMode"></param>
        /// <param name="reviewUserId"></param>
        /// <returns></returns>
        public async Task<bool> DeletePendAndInsertRecord(long formId,long currentStepId, string reviewMode, long reviewUserId)
        {
            if (reviewMode == ReviewMode.Single.ToEnumString() || reviewMode == ReviewMode.OrSingle.ToEnumString())
            {
                await _db.Deleteable<PendingReviewEntity>()
                         .Where(pending => pending.FormId == formId)
                         .ExecuteCommandAsync();
            }
            else if (reviewMode == ReviewMode.AndSingle.ToEnumString())
            {
                await _db.Deleteable<PendingReviewEntity>()
                         .Where(pending => pending.FormId == formId && pending.ReviewUserId == _loginuser.UserId)
                         .ExecuteCommandAsync();
            }

            var reviewTypes = await GetReviewUserAppointmentType(formId, currentStepId, reviewUserId);

            var entities = reviewTypes.Select(type => new FormReviewRecordEntity
            {
                FormId = formId,
                StepId = currentStepId,
                ReviewResult = ReviewResult.Approve.ToEnumString(),
                RejectStepId = null,
                Comment = string.Empty,
                ReviewType = ReviewType.Manual.ToEnumString(),
                ReviewAppointment = type,
                ReviewUserId = _loginuser.UserId,
                ReviewDateTime = DateTime.Now,
            }).ToList();
            await _db.Insertable(entities).ExecuteCommandAsync();

            return await _db.Queryable<PendingReviewEntity>().Where(pending => pending.FormId == formId).AnyAsync();
        }

        /// <summary>
        /// 查询审核人审批身份
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="currentStepId"></param>
        /// <param name="reviewUserId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetReviewUserAppointmentType(long formId, long currentStepId, long reviewUserId)
        {
            var appointTypeList = new List<string>();
            var userReview = new List<UserReview>();

            var formDetail = await _db.Queryable<FormInstanceEntity>()
                                      .With(SqlWith.NoLock)
                                      .InnerJoin<FormTypeEntity>((instance, formtype) => instance.FormTypeId == formtype.FormTypeId)
                                      .InnerJoin<UserInfoEntity>((instance, formtype, user) => instance.ApplicantUserId == user.UserId)
                                      .InnerJoin<DepartmentInfoEntity>((instance, formtype, user, dept) => user.DepartmentId == dept.DepartmentId)
                                      .InnerJoin<DepartmentLevelEntity>((instance, formtype, user, dept, deptlevel) => dept.DepartmentLevelId == deptlevel.DepartmentLevelId)
                                      .InnerJoin<PositionInfoEntity>((instance, formtype, user, dept, deptlevel, position) => user.PositionId == position.PositionId)
                                      .Where((instance, formtype, user, dept, deptlevel, position) => instance.FormId == formId)
                                      .Select((instance, formtype, user, dept, deptlevel, position) => new ApplicantFormDetail
                                      {
                                          FormId = instance.FormId,
                                          FormTypeId = instance.FormTypeId,
                                          RuleId = instance.RuleId,
                                          CurrentStepId = instance.CurrentStepId,
                                          ApplicantUserId = user.UserId,
                                          ApplicantDeptId = dept.DepartmentId,
                                          DeptLevelSort = deptlevel.SortOrder,
                                          PositionSort = position.SortOrder
                                      }).FirstAsync();

            // 申请人上级部门列表（包含申请人所在部门）
            var applicantDept = await _db.Queryable<DepartmentInfoEntity>()
                                         .With(SqlWith.NoLock)
                                         .ToParentListAsync(dept => dept.ParentId, formDetail.ApplicantDeptId);

            var stepInfo = await _db.Queryable<WorkflowStepEntity>()
                                    .With(SqlWith.NoLock)
                                    .Where(step => step.StepId == currentStepId)
                                    .Select(step => new
                                    {
                                        StepName = _lang.Locale == "zh-CN"
                                                   ? step.StepNameCn
                                                   : step.StepNameEn,
                                        step.IsStartStep,
                                        step.Assignment,
                                        step.ReviewMode,
                                    }).FirstAsync();
            if (stepInfo.IsStartStep == 1)
            {
                userReview = await GetStartReviewUserAppointment(reviewUserId);
            }
            else if (stepInfo.Assignment == Assignment.Org.ToEnumString())
            {
                var orgInfo = await _db.Queryable<WorkflowStepOrgEntity>()
                                       .With(SqlWith.NoLock)
                                       .Where(step => step.StepId == currentStepId)
                                       .FirstAsync();

                // 查询部门级别信息和职位信息
                var deptInfo = await _db.Queryable<DepartmentLevelEntity>()
                                        .With(SqlWith.NoLock)
                                        .Where(dept => dept.DepartmentLevelId == orgInfo.DeptLeaveId)
                                        .FirstAsync();
                var posInfo = await _db.Queryable<PositionInfoEntity>()
                                       .With(SqlWith.NoLock)
                                       .Where(pos => pos.PositionId == orgInfo.PositionId)
                                       .FirstAsync();

                appointTypeList = await GetOrgReviewUserAppointment(applicantDept, orgInfo.DeptLeaveId, orgInfo.PositionId, reviewUserId);

            }
            else if (stepInfo.Assignment == Assignment.DeptUser.ToEnumString())
            {
                var deptUserInfo = await _db.Queryable<WorkflowStepDeptUserEntity>()
                                            .With(SqlWith.NoLock)
                                            .Where(step => step.StepId == currentStepId)
                                            .FirstAsync();
                appointTypeList = await GetDeptUserReviewUserAppointment(deptUserInfo.DepartmentId, deptUserInfo.PositionId, reviewUserId);
            }
            else if (stepInfo.Assignment == Assignment.User.ToEnumString())
            {
                var userInfo = await _db.Queryable<WorkflowStepUserEntity>()
                                        .With(SqlWith.NoLock)
                                        .Where(step => step.StepId == currentStepId)
                                        .FirstAsync();
                appointTypeList = await GetUserReviewUserAppointment(userInfo.UserId, reviewUserId);
            }

            return appointTypeList;
        }

        /// <summary>
        /// 查询起始步骤审核人员身份
        /// </summary>
        /// <param name="reviewUserId">审批人Id</param>
        /// <returns></returns>
        public async Task<List<UserReview>> GetStartReviewUserAppointment(long reviewUserId)
        {
            bool isChinese = _lang.Locale == "zh-CN";
            string userNameCol = isChinese ? "users.UserNameCn" : "users.UserNameEn";
            string agentNameCol = isChinese ? "agentusers.UserNameCn" : "agentusers.UserNameEn";
            string dicNameCol = isChinese ? "dic.DicNameCn" : "dic.DicNameEn";

            var actual = AppointmentType.Actual.ToEnumString();
            var agent = AppointmentType.Agent.ToEnumString();
            var now = DateTime.Now;

            #region 查找审批人员身份
            string sql = $@"
                    SELECT
                        users.UserId,
                        {userNameCol} AS UserName,
                        agentusers.UserId AS AgentUserId,
                        {agentNameCol} AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId = @ReviewUserId
                            THEN (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Agent
                            )
                            ELSE (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Actual
                            )
                        END AS AppointmentTypeName,
                        CASE
                            WHEN agent.AgentUserId = @ReviewUserId THEN @Agent
                            ELSE @Actual
                        END AS AppointmentTypeCode
                    FROM Basic.UserInfo users
                    LEFT JOIN Basic.UserAgent agent
                           ON users.UserId = agent.SubstituteUserId
                          AND agent.StartTime <= @Now
                          AND agent.EndTime >= @Now
                    LEFT JOIN Basic.UserInfo agentusers
                           ON agent.AgentUserId = agentusers.UserId
                    WHERE users.IsApproval = 1
                      AND users.IsEmployed = 1
                      AND users.IsFreeze = 0
                      AND (
                            users.UserId = @ReviewUserId
                            OR agent.AgentUserId = @ReviewUserId
              )";

            var parameters = new[]
            {
                new SugarParameter("@ReviewUserId", reviewUserId),
                new SugarParameter("@Now", now),

                new SugarParameter("@Actual", actual),
                new SugarParameter("@Agent", agent),
            };
            var result = await _db.Ado.SqlQueryAsync<UserReview>(sql, parameters);
            #endregion

            return result ?? new List<UserReview>();
        }

        /// <summary>
        /// 查询按照组织架构审核人员身份
        /// </summary>
        /// <param name="applicantParentDept"></param>
        /// <param name="deptLeaveId"></param>
        /// <param name="positionId"></param>
        /// <param name="reviewUserId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetOrgReviewUserAppointment(List<DepartmentInfoEntity> applicantParentDept, long deptLeaveId, long positionId, long reviewUserId)
        {
            bool isChinese = _lang.Locale == "zh-CN";
            string dicNameCol = isChinese ? "dic.DicNameCn" : "dic.DicNameEn";

            var deptlevel = await _db.Queryable<DepartmentLevelEntity>()
                                     .With(SqlWith.NoLock)
                                     .Where(deptlevel => deptlevel.DepartmentLevelId == deptLeaveId)
                                     .FirstAsync();

            var posInfo = await _db.Queryable<PositionInfoEntity>()
                                   .With(SqlWith.NoLock)
                                   .Where(position => position.PositionId == positionId)
                                   .FirstAsync();

            var parentDeptIds = applicantParentDept.Select(dept => dept.DepartmentId).ToList();
            string parentDeptIdsStr = string.Join(",", parentDeptIds);

            var actual = AppointmentType.Actual.ToEnumString();
            var agent = AppointmentType.Agent.ToEnumString();
            var concurrent = AppointmentType.Concurrent.ToEnumString();
            var concurrentAgent = AppointmentType.ConcurrentAgent.ToEnumString();

            var autoActual = AppointmentType.AutoActual.ToEnumString();
            var autoAgent = AppointmentType.AutoAgent.ToEnumString();
            var autoConcurrent = AppointmentType.AutoConcurrent.ToEnumString();
            var autoConcurrentAgent = AppointmentType.AutoConcurrentAgent.ToEnumString();

            var now = DateTime.Now;

            #region 查找审批人员身份
            // ────────────────────────────────────────────────
            // 第一次：精确匹配
            // 取全部：实职、代理、兼职、兼职代理
            // ────────────────────────────────────────────────
            var exactResult = await _db.Ado.SqlQueryAsync<string>($@"
                SELECT DISTINCT AppointmentTypeCode
                FROM (

                    -- 实职 / 代理
                    SELECT
                        CASE
                            WHEN agent.AgentUserId = @ReviewUserId THEN @Agent
                            ELSE @Actual
                        END AS AppointmentTypeCode
                    FROM Basic.UserInfo users
                    INNER JOIN Basic.DepartmentInfo  dept      ON users.DepartmentId     = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo    position  ON users.PositionId       = position.PositionId
                    LEFT JOIN Basic.UserAgent agent            ON users.UserId           = agent.SubstituteUserId
                                                               AND agent.StartTime      <= @Now
                                                               AND agent.EndTime        >= @Now
                    WHERE dept.DepartmentId IN ({parentDeptIdsStr})
                      AND deptlevel.SortOrder = @DeptLevelSort
                      AND position.SortOrder  = @PositionSort
                      AND users.IsApproval    = 1
                      AND users.IsEmployed    = 1
                      AND users.IsFreeze      = 0
                      AND (
                            users.UserId = @ReviewUserId
                            OR agent.AgentUserId = @ReviewUserId
                          )

                    UNION ALL

                    -- 兼职 / 兼职代理
                    SELECT
                        CASE
                            WHEN agent.AgentUserId = @ReviewUserId THEN @ConcurrentAgent
                            ELSE @Concurrent
                        END AS AppointmentTypeCode
                    FROM Basic.UserPartTime partime
                    INNER JOIN Basic.UserInfo users            ON partime.UserId             = users.UserId
                    INNER JOIN Basic.DepartmentInfo dept       ON partime.PartTimeDeptId     = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo position     ON partime.PartTimePositionId = position.PositionId
                    LEFT JOIN Basic.UserAgent agent            ON users.UserId               = agent.SubstituteUserId
                                                               AND agent.StartTime          <= @Now
                                                               AND agent.EndTime            >= @Now
                    WHERE dept.DepartmentId IN ({parentDeptIdsStr})
                      AND deptlevel.SortOrder = @DeptLevelSort
                      AND position.SortOrder  = @PositionSort
                      AND users.IsApproval    = 1
                      AND users.IsEmployed    = 1
                      AND users.IsFreeze      = 0
                      AND (
                            users.UserId = @ReviewUserId
                            OR agent.AgentUserId = @ReviewUserId
                          )

                ) combined",
                new[]
                {
                    new SugarParameter("@Now", now),
                    new SugarParameter("@ReviewUserId", reviewUserId),
                    new SugarParameter("@DeptLevelSort", deptlevel.SortOrder),
                    new SugarParameter("@PositionSort", posInfo.SortOrder),

                    new SugarParameter("@Actual", actual),
                    new SugarParameter("@Agent", agent),
                    new SugarParameter("@Concurrent", concurrent),
                    new SugarParameter("@ConcurrentAgent", concurrentAgent),
                });

            if (exactResult.Any())
                return exactResult;

            // ────────────────────────────────────────────────
            // 第二次：自动指派
            // 取全部：自动实职、自动代理、自动兼职、自动兼职代理
            // ────────────────────────────────────────────────
            int currentPositionSort = posInfo.SortOrder - 1;
            int currentDeptLevelSort = deptlevel.SortOrder;

            while (currentPositionSort >= 1)
            {
                while (currentDeptLevelSort >= 1)
                {
                    var autoResult = await _db.Ado.SqlQueryAsync<string>($@"
                        SELECT DISTINCT AppointmentTypeCode
                        FROM (

                            -- 自动实职 / 自动代理
                            SELECT
                                CASE
                                    WHEN agent.AgentUserId = @ReviewUserId THEN @AutoAgent
                                    ELSE @AutoActual
                                END AS AppointmentTypeCode
                            FROM Basic.UserInfo users
                            INNER JOIN Basic.DepartmentInfo  dept      ON users.DepartmentId     = dept.DepartmentId
                            INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                            INNER JOIN Basic.PositionInfo position     ON users.PositionId       = position.PositionId
                            LEFT JOIN Basic.UserAgent agent            ON users.UserId           = agent.SubstituteUserId
                                                                       AND agent.StartTime      <= @Now
                                                                       AND agent.EndTime        >= @Now
                            WHERE dept.DepartmentId IN ({parentDeptIdsStr})
                              AND position.SortOrder  = @CurrentPositionSort
                              AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsApproval    = 1
                              AND users.IsEmployed    = 1
                              AND users.IsFreeze      = 0
                              AND (
                                    users.UserId = @ReviewUserId
                                    OR agent.AgentUserId = @ReviewUserId
                                  )

                            UNION ALL

                            -- 自动兼职 / 自动兼职代理
                            SELECT
                                CASE
                                    WHEN agent.AgentUserId = @ReviewUserId THEN @AutoConcurrentAgent
                                    ELSE @AutoConcurrent
                                END AS AppointmentTypeCode
                            FROM Basic.UserPartTime partime
                            INNER JOIN Basic.UserInfo users            ON partime.UserId             = users.UserId
                            INNER JOIN Basic.DepartmentInfo dept       ON partime.PartTimeDeptId     = dept.DepartmentId
                            INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                            INNER JOIN Basic.PositionInfo position     ON partime.PartTimePositionId = position.PositionId
                            LEFT JOIN Basic.UserAgent agent            ON users.UserId               = agent.SubstituteUserId
                                                                       AND agent.StartTime          <= @Now
                                                                       AND agent.EndTime            >= @Now
                            WHERE dept.DepartmentId IN ({parentDeptIdsStr})
                              AND position.SortOrder  = @CurrentPositionSort
                              AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsApproval    = 1
                              AND users.IsEmployed    = 1
                              AND users.IsFreeze      = 0
                              AND (
                                    users.UserId = @ReviewUserId
                                    OR agent.AgentUserId = @ReviewUserId
                                  )

                        ) combined",
                        new[]
                        {
                            new SugarParameter("@Now", now),
                            new SugarParameter("@ReviewUserId", reviewUserId),
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
            #endregion

            return new List<string>();
        }

        /// <summary>
        /// 查询按照指定部门指定职级审核人员身份
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <param name="reviewUserId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetDeptUserReviewUserAppointment(long departmentId, long positionId, long reviewUserId)
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

            var actual = AppointmentType.Actual.ToEnumString();
            var agent = AppointmentType.Agent.ToEnumString();
            var concurrent = AppointmentType.Concurrent.ToEnumString();
            var concurrentAgent = AppointmentType.ConcurrentAgent.ToEnumString();

            var autoActual = AppointmentType.AutoActual.ToEnumString();
            var autoAgent = AppointmentType.AutoAgent.ToEnumString();
            var autoConcurrent = AppointmentType.AutoConcurrent.ToEnumString();
            var autoConcurrentAgent = AppointmentType.AutoConcurrentAgent.ToEnumString();

            var now = DateTime.Now;

            #region 查找审批人员身份
            var exactResult = await _db.Ado.SqlQueryAsync<string>($@"
                SELECT DISTINCT AppointmentTypeCode
                FROM (

                    -- 实职 / 代理
                    SELECT
                        CASE
                            WHEN agent.AgentUserId = @ReviewUserId THEN @Agent
                            ELSE @Actual
                        END AS AppointmentTypeCode
                    FROM Basic.UserInfo users
                    INNER JOIN Basic.DepartmentInfo dept       ON users.DepartmentId = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo position     ON users.PositionId = position.PositionId
                    LEFT JOIN Basic.UserAgent agent            ON users.UserId = agent.SubstituteUserId
                                                               AND agent.StartTime <= @Now
                                                               AND agent.EndTime >= @Now
                    WHERE dept.DepartmentId = @DepartmentId
                      AND position.SortOrder = @PositionSort
                      AND users.IsApproval = 1
                      AND users.IsEmployed = 1
                      AND users.IsFreeze = 0
                      AND (
                            users.UserId = @ReviewUserId
                            OR agent.AgentUserId = @ReviewUserId
                          )

                    UNION ALL

                    -- 兼职 / 兼职代理
                    SELECT
                        CASE
                            WHEN agent.AgentUserId = @ReviewUserId THEN @ConcurrentAgent
                            ELSE @Concurrent
                        END AS AppointmentTypeCode
                    FROM Basic.UserPartTime partime
                    INNER JOIN Basic.UserInfo users            ON partime.UserId = users.UserId
                    INNER JOIN Basic.DepartmentInfo dept       ON partime.PartTimeDeptId = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo position     ON partime.PartTimePositionId = position.PositionId
                    LEFT JOIN Basic.UserAgent agent            ON users.UserId = agent.SubstituteUserId
                                                               AND agent.StartTime <= @Now
                                                               AND agent.EndTime >= @Now
                    WHERE dept.DepartmentId = @DepartmentId
                      AND position.SortOrder = @PositionSort
                      AND users.IsApproval = 1
                      AND users.IsEmployed = 1
                      AND users.IsFreeze = 0
                      AND (
                            users.UserId = @ReviewUserId
                            OR agent.AgentUserId = @ReviewUserId
                          )

                ) t",
                new[]
                {
                    new SugarParameter("@Now", now),
                    new SugarParameter("@DepartmentId", departmentId),
                    new SugarParameter("@PositionSort", posInfo.SortOrder),
                    new SugarParameter("@ReviewUserId", reviewUserId),

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
                    var autoResult = await _db.Ado.SqlQueryAsync<string>($@"
                        SELECT DISTINCT AppointmentTypeCode
                        FROM (

                            -- 自动实职 / 自动代理
                            SELECT
                                CASE
                                    WHEN agent.AgentUserId = @ReviewUserId THEN @AutoAgent
                                    ELSE @AutoActual
                                END AS AppointmentTypeCode
                            FROM Basic.UserInfo users
                            INNER JOIN Basic.DepartmentInfo dept       ON users.DepartmentId = dept.DepartmentId
                            INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                            INNER JOIN Basic.PositionInfo position     ON users.PositionId = position.PositionId
                            LEFT JOIN Basic.UserAgent agent            ON users.UserId = agent.SubstituteUserId
                                                                       AND agent.StartTime <= @Now
                                                                       AND agent.EndTime >= @Now
                            WHERE dept.DepartmentId = @DepartmentId
                              AND position.SortOrder = @CurrentPositionSort
                              AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsApproval = 1
                              AND users.IsEmployed = 1
                              AND users.IsFreeze = 0
                              AND (
                                    users.UserId = @ReviewUserId
                                    OR agent.AgentUserId = @ReviewUserId
                                  )

                            UNION ALL

                            -- 自动兼职 / 自动兼职代理
                            SELECT
                                CASE
                                    WHEN agent.AgentUserId = @ReviewUserId THEN @AutoConcurrentAgent
                                    ELSE @AutoConcurrent
                                END AS AppointmentTypeCode
                            FROM Basic.UserPartTime partime
                            INNER JOIN Basic.UserInfo users            ON partime.UserId = users.UserId
                            INNER JOIN Basic.DepartmentInfo dept       ON partime.PartTimeDeptId = dept.DepartmentId
                            INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                            INNER JOIN Basic.PositionInfo position     ON partime.PartTimePositionId = position.PositionId
                            LEFT JOIN Basic.UserAgent agent            ON users.UserId = agent.SubstituteUserId
                                                                       AND agent.StartTime <= @Now
                                                                       AND agent.EndTime >= @Now
                            WHERE dept.DepartmentId = @DepartmentId
                              AND position.SortOrder = @CurrentPositionSort
                              AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsApproval = 1
                              AND users.IsEmployed = 1
                              AND users.IsFreeze = 0
                              AND (
                                    users.UserId = @ReviewUserId
                                    OR agent.AgentUserId = @ReviewUserId
                                  )
                        ) t",
                        new[]
                        {
                            new SugarParameter("@Now", now),
                            new SugarParameter("@DepartmentId", departmentId),
                            new SugarParameter("@CurrentPositionSort", currentPositionSort),
                            new SugarParameter("@CurrentDeptLevelSort", currentDeptLevelSort),
                            new SugarParameter("@ReviewUserId", reviewUserId),

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
            #endregion

            return new List<string>();
        }

        /// <summary>
        /// 查询按照指定人审核人员身份
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="reviewUserId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetUserReviewUserAppointment(long userId, long reviewUserId)
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

            var actual = AppointmentType.Actual.ToEnumString();
            var agent = AppointmentType.Agent.ToEnumString();
            var concurrent = AppointmentType.Concurrent.ToEnumString();
            var concurrentAgent = AppointmentType.ConcurrentAgent.ToEnumString();

            var autoActual = AppointmentType.AutoActual.ToEnumString();
            var autoAgent = AppointmentType.AutoAgent.ToEnumString();
            var autoConcurrent = AppointmentType.AutoConcurrent.ToEnumString();
            var autoConcurrentAgent = AppointmentType.AutoConcurrentAgent.ToEnumString();

            var now = DateTime.Now;

            #region 查找审批人员身份
            var exactResult = await _db.Ado.SqlQueryAsync<string>($@"
                SELECT DISTINCT AppointmentTypeCode
                FROM (

                    -- 实职 / 代理
                    SELECT
                        CASE
                            WHEN agent.AgentUserId = @ReviewUserId THEN @Agent
                            ELSE @Actual
                        END AS AppointmentTypeCode
                    FROM Basic.UserInfo users
                    LEFT JOIN Basic.UserAgent agent
                           ON users.UserId = agent.SubstituteUserId
                          AND agent.StartTime <= @Now
                          AND agent.EndTime >= @Now
                    WHERE users.UserId = @UserId
                      AND users.IsApproval = 1
                      AND users.IsEmployed = 1
                      AND users.IsFreeze = 0
                      AND (
                            users.UserId = @ReviewUserId
                            OR agent.AgentUserId = @ReviewUserId
                          )

                    UNION ALL

                    -- 兼职 / 兼职代理
                    SELECT
                        CASE
                            WHEN agent.AgentUserId = @ReviewUserId THEN @ConcurrentAgent
                            ELSE @Concurrent
                        END AS AppointmentTypeCode
                    FROM Basic.UserPartTime partime
                    INNER JOIN Basic.UserInfo users ON partime.UserId = users.UserId
                    LEFT JOIN Basic.UserAgent agent
                           ON users.UserId = agent.SubstituteUserId
                          AND agent.StartTime <= @Now
                          AND agent.EndTime >= @Now
                    WHERE partime.UserId = @UserId
                      AND users.IsApproval = 1
                      AND users.IsEmployed = 1
                      AND users.IsFreeze = 0
                      AND (
                            users.UserId = @ReviewUserId
                            OR agent.AgentUserId = @ReviewUserId
                          )

                ) combined",
                new[]
                {
                    new SugarParameter("@Now", now),
                    new SugarParameter("@UserId", userId),
                    new SugarParameter("@ReviewUserId", reviewUserId),

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
                    var autoResult = await _db.Ado.SqlQueryAsync<string>($@"
                        SELECT DISTINCT AppointmentTypeCode
                        FROM (

                            -- 自动实职 / 自动代理
                            SELECT
                                CASE
                                    WHEN agent.AgentUserId = @ReviewUserId THEN @AutoAgent
                                    ELSE @AutoActual
                                END AS AppointmentTypeCode
                            FROM Basic.UserInfo users
                            INNER JOIN Basic.DepartmentInfo dept
                                    ON users.DepartmentId = dept.DepartmentId
                            INNER JOIN Basic.DepartmentLevel deptlevel
                                    ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                            INNER JOIN Basic.PositionInfo position
                                    ON users.PositionId = position.PositionId
                            LEFT JOIN Basic.UserAgent agent
                                   ON users.UserId = agent.SubstituteUserId
                                  AND agent.StartTime <= @Now
                                  AND agent.EndTime >= @Now
                            WHERE dept.DepartmentId = @DepartmentId
                              AND position.SortOrder = @CurrentPositionSort
                              AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsApproval = 1
                              AND users.IsEmployed = 1
                              AND users.IsFreeze = 0
                              AND (
                                    users.UserId = @ReviewUserId
                                    OR agent.AgentUserId = @ReviewUserId
                                  )

                            UNION ALL

                            -- 自动兼职 / 自动兼职代理
                            SELECT
                                CASE
                                    WHEN agent.AgentUserId = @ReviewUserId THEN @AutoConcurrentAgent
                                    ELSE @AutoConcurrent
                                END AS AppointmentTypeCode
                            FROM Basic.UserPartTime partime
                            INNER JOIN Basic.UserInfo users
                                    ON partime.UserId = users.UserId
                            INNER JOIN Basic.DepartmentInfo dept
                                    ON partime.PartTimeDeptId = dept.DepartmentId
                            INNER JOIN Basic.DepartmentLevel deptlevel
                                    ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                            INNER JOIN Basic.PositionInfo position
                                    ON partime.PartTimePositionId = position.PositionId
                            LEFT JOIN Basic.UserAgent agent
                                   ON users.UserId = agent.SubstituteUserId
                                  AND agent.StartTime <= @Now
                                  AND agent.EndTime >= @Now
                            WHERE dept.DepartmentId = @DepartmentId
                              AND position.SortOrder = @CurrentPositionSort
                              AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsApproval = 1
                              AND users.IsEmployed = 1
                              AND users.IsFreeze = 0
                              AND (
                                    users.UserId = @ReviewUserId
                                    OR agent.AgentUserId = @ReviewUserId
                                  )

                        ) combined",
                        new[]
                        {
                            new SugarParameter("@Now", now),
                            new SugarParameter("@DepartmentId", userInfo.DepartmentId),
                            new SugarParameter("@CurrentPositionSort", currentPositionSort),
                            new SugarParameter("@CurrentDeptLevelSort", currentDeptLevelSort),
                            new SugarParameter("@ReviewUserId", reviewUserId),

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

            #endregion

            return new List<string>();
        }
    }
}
