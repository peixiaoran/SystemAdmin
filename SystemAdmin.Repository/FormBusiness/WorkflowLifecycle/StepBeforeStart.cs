using SqlSugar;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormAudit.Entity;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.WorkflowLifecycle.StepBeforeStart;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;

namespace SystemAdmin.Repository.FormBusiness.WorkflowLifecycle
{
    /// <summary>
    /// 审批步骤
    /// </summary>
    public class StepBeforeStart
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;
        //private readonly string _this = "FormBusiness.WorkflowLifecycle.StepBeforeStart";

        public StepBeforeStart(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 初始化表单
        /// </summary>
        public async Task<FormInfoEntity> InitFormInfo(long userId, long formTypeId)
        {
            // 获取表单编号
            long startStepId = await GetWorkFlowIsStartStepId(formTypeId);
            var formNo = await GenerateFormNo(userId, formTypeId);
            FormInfoEntity insertForm = new FormInfoEntity()
            {
                FormId = SnowFlakeSingle.Instance.NextId(),
                FormNo = formNo,
                FormTypeId = formTypeId,
                Description = "",
                ImportanceCode = ImportanceType.Normal.ToEnumString(),
                FormStatus = FormStatus.PendingSubmission.ToEnumString(),
                NowConditionId = null,
                NowStepId = startStepId,
                CreatedBy = userId,
                CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };
            await _db.Insertable(insertForm).ExecuteCommandAsync();
            return insertForm;
        }

        /// <summary>
        /// 保存表单信息
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="description"></param>
        /// <param name="importanceCode"></param>
        /// <param name="modifiedUserId"></param>
        /// <returns></returns>
        public async Task<int> SaveFormInfo(long formId, string description, string formStatus, string importanceCode, long modifiedUserId)
        {
            return await _db.Updateable<FormInfoEntity>()
                            .SetColumns(forminfo => new FormInfoEntity
                            {
                                Description = description,
                                ImportanceCode = importanceCode,
                                FormStatus = formStatus,
                                ModifiedBy = modifiedUserId,
                                ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            }).Where(forminfo => forminfo.FormId == formId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 生成表单编号
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<string> GenerateFormNo(long userId, long formTypeId)
        {
            var formType = await _db.Queryable<FormTypeEntity>().FirstAsync(x => x.FormTypeId == formTypeId);

            var now = DateTime.Now;
            var ym = now.ToString("yyMM");

            var seq = 1;

            var counting = await _db.Queryable<FormCountingEntity>()
                .FirstAsync(x => x.FormTypeId == formTypeId && x.YM == ym);

            if (counting == null)
            {
                await _db.Insertable(new FormCountingEntity
                {
                    FormTypeId = formTypeId,
                    YM = ym,
                    Total = seq,
                    CreatedBy = userId,
                    CreatedDate = now.ToString("yyyy-MM-dd HH:mm:ss")
                }).ExecuteCommandAsync();
            }
            else
            {
                seq = counting.Total + 1;

                await _db.Updateable<FormCountingEntity>()
                    .SetColumns(x => new FormCountingEntity
                    {
                        Total = seq
                    })
                    .Where(x => x.FormTypeId == formTypeId && x.YM == ym)
                    .ExecuteCommandAsync();
            }

            return $"{formType.Prefix}-{ym}{seq:D4}";
        }

        /// <summary>
        /// 重要程度下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<ImportanceDropDto>> GetImportanceDropDown()
        {
            return await _db.Queryable<DictionaryInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Where(dic => dic.DicType == "ImportanceType")
                            .Select(dic => new ImportanceDropDto()
                            {
                                ImportanceCode = dic.DicCode,
                                ImportanceName = _lang.Locale == "zh-CN"
                                                 ? dic.DicNameCn
                                                 : dic.DicNameEn,
                            }).ToListAsync();
        }

        /// <summary>
        /// 查询开始步骤
        /// </summary>
        /// <returns></returns>
        public async Task<long> GetWorkFlowIsStartStepId(long formTypeId)
        {
            return await _db.Queryable<WorkflowStepEntity>()
                            .With(SqlWith.NoLock)
                            .Where(stepinfo => stepinfo.FormTypeId == formTypeId && stepinfo.IsStartStep == 1)
                            .Select(stepinfo => stepinfo.StepId)
                            .FirstAsync();
        }

        /// <summary>
        /// 查询表单审批流程
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<List<WorkflowApproveUser>> GetWorkflowAllApproveUser(long formId)
        {
            List<WorkflowApproveUser> approveList = new List<WorkflowApproveUser>();

            // 查询表单申请人信息
            var applyUserInfo = await _db.Queryable<FormInfoEntity>()
                                         .With(SqlWith.NoLock)
                                         .InnerJoin<UserInfoEntity>((form, user) => form.CreatedBy == user.UserId)
                                         .Where((form, user) => form.FormId == formId)
                                         .Select((form, user) => new UserInfoEntity()
                                         {
                                             UserId = user.UserId,
                                             DepartmentId = user.DepartmentId,
                                             PositionId = user.PositionId,
                                             UserNameCn = user.UserNameCn,
                                             UserNameEn = user.UserNameEn,
                                         }).FirstAsync();

            // 查询表单审批开始步骤
            var nowStepId = await _db.Queryable<WorkflowStepEntity>()
                                     .With(SqlWith.NoLock)
                                     .InnerJoin<FormInfoEntity>((stepinfo, form) => stepinfo.FormTypeId == form.FormTypeId)
                                     .Where((stepinfo, form) => form.FormId == formId && stepinfo.IsStartStep == 1)
                                     .Select((stepinfo, form) => stepinfo.StepId)
                                     .FirstAsync();
            while (nowStepId > -1)
            {
                var nowStep = await _db.Queryable<WorkflowStepEntity>()
                                       .With(SqlWith.NoLock)
                                       .Where(stepinfo => stepinfo.StepId == nowStepId)
                                       .FirstAsync();

                // 如果是开始步骤，审批人默认为申请人
                if (nowStep.IsStartStep == 1)
                {
                    WorkflowApproveUser approveItem = new WorkflowApproveUser();

                    // 查询签核类型（本、兼、代、自动指派）
                    var applyTypePrimary = await _db.Queryable<DictionaryInfoEntity>()
                                                  .With(SqlWith.NoLock)
                                                  .Where(app => app.DicType == "AppointmentType" && app.DicCode == AppointmentType.Primary.ToEnumString())
                                                  .FirstAsync();

                    List<StepApproveUser> stepApproveUser = new List<StepApproveUser>();
                    stepApproveUser.Add(new StepApproveUser
                    {
                        UserId = applyUserInfo.UserId,
                        UserName = _lang.Locale == "zh-CN"
                                   ? applyUserInfo.UserNameCn
                                   : applyUserInfo.UserNameEn,
                        AppointmentType = applyTypePrimary.DicCode,
                        AppointmentTypeName = _lang.Locale == "zh-CN"
                                   ? applyTypePrimary.DicNameCn
                                   : applyTypePrimary.DicNameEn
                    });

                    approveItem.StepId = nowStep.StepId;
                    approveItem.StepName = _lang.Locale == "zh-CN"
                                           ? nowStep.StepNameCn
                                           : nowStep.StepNameEn;
                    approveItem.stepApproveUser = stepApproveUser;
                    approveList.Add(approveItem);
                }

                // 依照组织架构
                else if (nowStep.Assignment == Assignment.Org.ToEnumString())
                {
                    // 申请人所有上级部门
                    var parentDeptList = await _db.Queryable<DepartmentInfoEntity>()
                                                  .With(SqlWith.NoLock)
                                                  .ToParentListAsync(dept => dept.ParentId, applyUserInfo.DepartmentId);

                    // 步骤要求（部门级别、职级）
                    var stepOrg = await _db.Queryable<WorkflowStepOrgEntity>()
                                           .With(SqlWith.NoLock)
                                           .Where(org => org.StepId == nowStep.StepId)
                                           .FirstAsync();
                    // 查询此步骤的审批人
                    var orgApproveUser = await GetStepApproveUserByOrg(applyUserInfo.UserId, nowStep.ApproveMode, stepOrg.DeptLeaveId, stepOrg.PositionId);

                    WorkflowApproveUser approveItem = new WorkflowApproveUser();
                    approveItem.StepId = nowStep.StepId;
                    approveItem.StepName = _lang.Locale == "zh-CN"
                                           ? nowStep.StepNameCn
                                           : nowStep.StepNameEn;
                    approveItem.stepApproveUser = orgApproveUser;
                    approveList.Add(approveItem);
                }

                // 依照指定部门、职级
                else if (nowStep.Assignment == Assignment.DeptUser.ToEnumString())
                {
                    // 步骤要求（部门级别、职级）
                    var stepDeptUser = await _db.Queryable<WorkflowStepDeptUserEntity>()
                                                .With(SqlWith.NoLock)
                                                .Where(org => org.StepId == nowStep.StepId)
                                                .FirstAsync();

                    // 查询此步骤的审批人
                    var orgApproveUser = await GetStepApproveUserByDeptUser(nowStep.ApproveMode, stepDeptUser.DepartmentId, stepDeptUser.PositionId);

                    WorkflowApproveUser approveItem = new WorkflowApproveUser();
                    approveItem.StepId = nowStep.StepId;
                    approveItem.StepName = _lang.Locale == "zh-CN"
                                           ? nowStep.StepNameCn
                                           : nowStep.StepNameEn;
                    approveItem.stepApproveUser = orgApproveUser;
                    approveList.Add(approveItem);
                }

                // 依照指定员工
                else if (nowStep.Assignment == Assignment.User.ToEnumString())
                {
                    // 步骤要求（部门级别、职级）
                    var stepDeptUser = await _db.Queryable<WorkflowStepUserEntity>()
                                                .With(SqlWith.NoLock)
                                                .Where(stepuser => stepuser.StepId == nowStep.StepId)
                                                .FirstAsync();

                    // 查询此步骤的审批人
                    var orgApproveUser = await GetStepApproveUserByUser(nowStep.ApproveMode, stepDeptUser.UserId);

                    WorkflowApproveUser approveItem = new WorkflowApproveUser();
                    approveItem.StepId = nowStep.StepId;
                    approveItem.StepName = _lang.Locale == "zh-CN"
                                           ? nowStep.StepNameCn
                                           : nowStep.StepNameEn;
                    approveItem.stepApproveUser = orgApproveUser;
                    approveList.Add(approveItem);

                }

                // 查找下一个步骤
                var stepBranch = await _db.Queryable<WorkflowStepBranchEntity>()
                                          .With(SqlWith.NoLock)
                                          .Where(condition => condition.StepId == nowStep.StepId)
                                          .ToListAsync();

                List<long> conditionIds = new List<long>();
                foreach (var conItem in stepBranch)
                {
                    if (conItem.ConditionId == -1)
                    {
                        conditionIds.Add(conItem.ConditionId);
                    }
                }

                // 赋值下一个步骤Id
                nowStepId = stepBranch.Where(condition => conditionIds.Contains(condition.ConditionId) && condition.ExecuteMatched == 1).First().NextStepId;
            }
            return approveList;
        }

        /// <summary>
        /// 查询步骤依照组织架构审批人
        /// </summary>
        /// <param name="applyUserId"></param>
        /// <param name="approveMode"></param>
        /// <param name="deptLevelId"></param>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public async Task<List<StepApproveUser>> GetStepApproveUserByOrg(long applyUserId, string approveMode, long deptLevelId, long positionId)
        {
            // 步骤最终审批人列表
            var finalApproveUser = new List<StepApproveUser>();

            // 申请人信息
            var applyUser = await _db.Queryable<UserInfoEntity>().Where(user => user.UserId == applyUserId).FirstAsync();

            // 申请人职级信息
            var applyPositon = await _db.Queryable<UserPositionEntity>().Where(position => position.PositionId == applyUser.PositionId).FirstAsync();

            #region 查询符合步骤条件的签核人，带有注本、兼、代的标识并按照入职时间正序排序
            var candidateUserSql = @"SELECT
                                            t.UserId,
                                            t.UserName,
                                            t.AppointmentType,
                                            t.AppointmentTypeName,
                                            t.AgentUserId,
                                            t.AgentUserName
                                        FROM
                                        (
                                            SELECT
                                                users.UserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN users.UserNameCn ELSE users.UserNameEn END AS UserName,
                                                users.HireDate,
                                                CASE
                                                    WHEN agentuser.UserId IS NULL THEN
                                                        (SELECT TOP 1 DicCode
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pprimary)
                                                    ELSE
                                                        (SELECT TOP 1 DicCode
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pagent)
                                                END AS AppointmentType,
                                                CASE
                                                    WHEN agentuser.UserId IS NULL THEN
                                                        (SELECT TOP 1 
                                                                CASE WHEN @plocale = 'zh-CN' THEN DicNameCn ELSE DicNameEn END
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pprimary)
                                                    ELSE
                                                        (SELECT TOP 1 
                                                                CASE WHEN @plocale = 'zh-CN' THEN DicNameCn ELSE DicNameEn END
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pagent)
                                                END AS AppointmentTypeName,
                                                agentuser.UserId AS AgentUserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn ELSE agentuser.UserNameEn END AS AgentUserName
                                            FROM Basic.UserInfo users WITH (NOLOCK)
                                            INNER JOIN Basic.UserPosition position ON users.PositionId = position.PositionId
                                            INNER JOIN Basic.DepartmentInfo dept ON users.DepartmentId = dept.DepartmentId
                                            INNER JOIN Basic.UserPosition pos ON users.PositionId = pos.PositionId
                                            LEFT JOIN Basic.UserAgent agent ON users.UserId = agent.SubstituteUserId
                                                AND agent.StartTime <= GETDATE()
                                                AND agent.EndTime >= GETDATE()
                                            LEFT JOIN Basic.UserInfo agentuser ON agent.AgentUserId = agentuser.UserId
                                            WHERE
                                                users.IsEmployed = 1
                                                AND users.IsFreeze = 0
                                                AND users.IsApproval = 1
                                                AND dept.DepartmentLevelId = @pdeptLevelId
                                                AND users.PositionId = @pposId
                                                AND position.SortOrder < @pappUserPosOrder

                                            UNION ALL

                                            SELECT
                                                users.UserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN users.UserNameCn ELSE users.UserNameEn END AS UserName,
                                                users.HireDate,
                                                CASE
                                                    WHEN agentuser.UserId IS NULL THEN
                                                        (SELECT TOP 1 DicCode
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pconcurrent)
                                                    ELSE
                                                        (SELECT TOP 1 DicCode
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pconcurrentagent)
                                                END AS AppointmentType,
                                                CASE
                                                    WHEN agentuser.UserId IS NULL THEN
                                                        (SELECT TOP 1 
                                                                CASE WHEN @plocale = 'zh-CN' THEN DicNameCn ELSE DicNameEn END
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pconcurrent)
                                                    ELSE
                                                        (SELECT TOP 1 
                                                                CASE WHEN @plocale = 'zh-CN' THEN DicNameCn ELSE DicNameEn END
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pconcurrentagent)
                                                END AS AppointmentTypeName,
                                                agentuser.UserId AS AgentUserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn ELSE agentuser.UserNameEn END AS AgentUserName
                                            FROM Basic.UserPartTime part WITH (NOLOCK)
                                            INNER JOIN Basic.UserInfo users ON part.UserId = users.UserId
                                            INNER JOIN Basic.UserPosition position ON part.PartTimePositionId = position.PositionId
                                            INNER JOIN Basic.DepartmentInfo dept ON part.PartTimeDeptId = dept.DepartmentId
                                            LEFT JOIN Basic.UserAgent agent ON users.UserId = agent.SubstituteUserId
                                                AND agent.StartTime <= GETDATE()
                                                AND agent.EndTime >= GETDATE()
                                            LEFT JOIN Basic.UserInfo agentuser ON agent.AgentUserId = agentuser.UserId
                                            WHERE
                                                users.IsEmployed = 1
                                                AND users.IsFreeze = 0
                                                AND users.IsApproval = 1
                                                AND dept.DepartmentLevelId = @pdeptLevelId
                                                AND part.PartTimePositionId = @pposId
                                                AND position.SortOrder < @pappUserPosOrder
                                        ) t ORDER BY t.HireDate ASC;";
            #endregion

            var candidateUserPar = new List<SugarParameter>
            {
                new SugarParameter("@plocale", _lang.Locale),
                new SugarParameter("@pprimary", AppointmentType.Primary.ToEnumString()),
                new SugarParameter("@pagent", AppointmentType.Agent.ToEnumString()),
                new SugarParameter("@pconcurrent", AppointmentType.Concurrent.ToEnumString()),
                new SugarParameter("@pconcurrentagent", AppointmentType.ConcurrentAgent.ToEnumString()),
                new SugarParameter("@pdeptLevelId", deptLevelId),
                new SugarParameter("@pposId", positionId),
                new SugarParameter("@pappUserPosOrder", applyPositon.SortOrder),
            };

            // 查询符合条件审批人候选人，注本、兼、代、兼代的标识并按照入职时间正序排序
            var candidateList = await _db.Ado.SqlQueryAsync<StepApproveUser>(candidateUserSql, candidateUserPar);
            if (candidateList.Count() > 0)
            {
                // 按照本、兼、代、兼代的顺序筛选最终审批人
                finalApproveUser = await GetFinalStepApproveUserList(candidateList, approveMode, "PirCon");
            }

            // 如果没有符合条件的审批人，则查询本部门及以上的符合条件审批人（自动指派）
            else
            {
                // 查询指定部门所有上级部门Ids
                var parentDeptList = await _db.Queryable<DepartmentInfoEntity>()
                                              .With(SqlWith.NoLock)
                                              .ToParentListAsync(dept => dept.ParentId, applyUser.DepartmentId);
                var parentDeptIds = parentDeptList.Select(dept => dept.DepartmentId).ToList();

                var deptLevelMaxOrder = await _db.Queryable<DepartmentLevelEntity>()
                                                 .With(SqlWith.NoLock)
                                                 .MaxAsync(deptlevel => deptlevel.SortOrder);
                var posMaxOrder = await _db.Queryable<UserPositionEntity>()
                                           .With(SqlWith.NoLock)
                                           .MaxAsync(position => position.SortOrder);
                var stepDeptStartOrder = await _db.Queryable<DepartmentLevelEntity>()
                                                  .With(SqlWith.NoLock)
                                                  .Where(dept => dept.DepartmentLevelId == deptLevelId)
                                                  .MaxAsync(position => position.SortOrder);
                var stepPosStartOrder = await _db.Queryable<UserPositionEntity>()
                                                 .With(SqlWith.NoLock)
                                                 .Where(position => position.PositionId == positionId)
                                                 .MaxAsync(position => position.SortOrder);

                List<StepApproveUser> hightLevelApproveUser = new List<StepApproveUser>();

                // 依次按照部门级别递减
                for (int itemDeptLevelOrder = stepDeptStartOrder; itemDeptLevelOrder > 0 && itemDeptLevelOrder <= deptLevelMaxOrder;)
                {
                    // 依次按照职级递减
                    for (int itemPosOrder = stepPosStartOrder; itemPosOrder > 0 && itemPosOrder <= posMaxOrder;)
                    {
                        var deptInSql = string.Join(", ", parentDeptIds);

                        #region 查询高阶审批人候选人，注本、兼、代、兼代的标识并按照职级倒序、入职时间正序排序
                        var highCandidateUserSql = $@"SELECT
                                                            t.UserId,
                                                            t.UserName,
                                                            t.AppointmentType,
                                                            t.AppointmentTypeName,
                                                            t.AgentUserId,
                                                            t.AgentUserName
                                                        FROM
                                                        (
                                                            SELECT
                                                                users.UserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN users.UserNameCn
                                                                    ELSE users.UserNameEn
                                                                END AS UserName,
                                                                position.SortOrder,
                                                                users.HireDate,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN
                                                                        (
                                                                            SELECT TOP 1 DicCode
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoprimary
                                                                        )
                                                                    ELSE
                                                                        (
                                                                            SELECT TOP 1 DicCode
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoagent
                                                                        )
                                                                END AS AppointmentType,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN
                                                                        (
                                                                            SELECT TOP 1
                                                                                CASE
                                                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                                                    ELSE DicNameEn
                                                                                END
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoprimary
                                                                        )
                                                                    ELSE
                                                                        (
                                                                            SELECT TOP 1
                                                                                CASE
                                                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                                                    ELSE DicNameEn
                                                                                END
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoagent
                                                                        )
                                                                END AS AppointmentTypeName,
                                                                agentuser.UserId AS AgentUserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn
                                                                    ELSE agentuser.UserNameEn
                                                                END AS AgentUserName
                                                            FROM Basic.UserInfo users WITH (NOLOCK)
                                                            INNER JOIN Basic.UserPosition position ON users.PositionId = position.PositionId
                                                            INNER JOIN Basic.DepartmentInfo dept ON users.DepartmentId = dept.DepartmentId
                                                            INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                                                            INNER JOIN Basic.UserPosition pos ON users.PositionId = pos.PositionId
                                                            LEFT JOIN Basic.UserAgent agent ON users.UserId = agent.SubstituteUserId
                                                               AND agent.StartTime <= GETDATE()
                                                               AND agent.EndTime >= GETDATE()
                                                            LEFT JOIN Basic.UserInfo agentuser
                                                                ON agent.AgentUserId = agentuser.UserId
                                                            WHERE 
                                                              users.IsEmployed = 1
                                                              AND users.IsFreeze = 0
                                                              AND users.IsApproval = 1
                                                              AND dept.DepartmentId IN ({deptInSql})
                                                              AND deptlevel.SortOrder = @pdeptlevelOrder
                                                              AND position.SortOrder = @pposOrder
                                                              AND position.SortOrder < @pappUserPosOrder

                                                            UNION ALL

                                                            SELECT
                                                                users.UserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN users.UserNameCn
                                                                    ELSE users.UserNameEn
                                                                END AS UserName,
                                                                position.SortOrder,
                                                                users.HireDate,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN
                                                                        (
                                                                            SELECT TOP 1 DicCode
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoconcurrent
                                                                        )
                                                                    ELSE
                                                                        (
                                                                            SELECT TOP 1 DicCode
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoconcurrentagent
                                                                        )
                                                                END AS AppointmentType,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN
                                                                        (
                                                                            SELECT TOP 1
                                                                                CASE
                                                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                                                    ELSE DicNameEn
                                                                                END
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoconcurrent
                                                                        )
                                                                    ELSE
                                                                        (
                                                                            SELECT TOP 1
                                                                                CASE
                                                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                                                    ELSE DicNameEn
                                                                                END
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoconcurrentagent
                                                                        )
                                                                END AS AppointmentTypeName,
                                                                agentuser.UserId AS AgentUserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn
                                                                    ELSE agentuser.UserNameEn
                                                                END AS AgentUserName
                                                            FROM Basic.UserPartTime part WITH (NOLOCK)
                                                            INNER JOIN Basic.UserInfo users ON part.UserId = users.UserId
                                                            INNER JOIN Basic.UserPosition position ON part.PartTimePositionId = position.PositionId
                                                            INNER JOIN Basic.DepartmentInfo dept ON part.PartTimeDeptId = dept.DepartmentId
                                                            INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                                                            LEFT JOIN Basic.UserAgent agent ON users.UserId = agent.SubstituteUserId
                                                               AND agent.StartTime <= GETDATE()
                                                               AND agent.EndTime >= GETDATE()
                                                            LEFT JOIN Basic.UserInfo agentuser ON agent.AgentUserId = agentuser.UserId
                                                            WHERE 
                                                              users.IsEmployed = 1
                                                              AND users.IsFreeze = 0
                                                              AND users.IsApproval = 1
                                                              AND dept.DepartmentId IN ({deptInSql})
                                                              AND deptlevel.SortOrder = @pdeptlevelOrder
                                                              AND position.SortOrder = @pposOrder
                                                              AND position.SortOrder < @pappUserPosOrder
                                                        ) t ORDER BY SortOrder DESC,t.HireDate ASC;";
                        #endregion

                        var highCandidateUserPar = new List<SugarParameter>
                        {
                            new SugarParameter("@plocale", _lang.Locale),
                            new SugarParameter("@pautoprimary", AppointmentType.AutoPrimary.ToEnumString()),
                            new SugarParameter("@pautoagent", AppointmentType.AutoAgent.ToEnumString()),
                            new SugarParameter("@pautoconcurrent", AppointmentType.AutoConcurrent.ToEnumString()),
                            new SugarParameter("@pautoconcurrentagent", AppointmentType.AutoConcurrentAgent.ToEnumString()),
                            new SugarParameter("@pdeptlevelOrder", itemDeptLevelOrder),
                            new SugarParameter("@pposOrder", itemPosOrder),
                            new SugarParameter("@pappUserPosOrder", applyPositon.SortOrder),
                        };
                        for (int i = 0; i < parentDeptIds.Count; i++)
                        {
                            highCandidateUserPar.Add(new SugarParameter($"@dept{i}", parentDeptIds[i]));
                        }

                        // 查询符合条件候选审批人
                        var highLevelUser = await _db.Ado.SqlQueryAsync<StepApproveUser>(highCandidateUserSql, highCandidateUserPar);
                        if (highLevelUser.Count > 0)
                        {
                            // 按照本、兼、代、兼代的顺序筛选最终审批人
                            hightLevelApproveUser = await GetFinalStepApproveUserList(highLevelUser, approveMode, "Auto");
                            itemPosOrder = -1;
                        }
                        else
                        {
                            itemPosOrder--;
                        }
                    }
                    if (hightLevelApproveUser.Count > 0)
                    {
                        itemDeptLevelOrder = -1;
                    }
                    else
                    {
                        itemDeptLevelOrder--;
                    }
                }
                finalApproveUser.AddRange(hightLevelApproveUser);
            }
            return finalApproveUser;
        }

        /// <summary>
        /// 查询步骤依照指定部门、职级审批人
        /// </summary>
        /// <param name="approveMode"></param>
        /// <param name="parentDeptList"></param>
        /// <param name="deptId"></param>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public async Task<List<StepApproveUser>> GetStepApproveUserByDeptUser(string approveMode, long deptId, long positionId)
        {
            // 步骤最终审批人列表
            var finalApproveUser = new List<StepApproveUser>();

            // 指定部门信息
            var deptInfo = await _db.Queryable<DepartmentInfoEntity>().Where(dept => dept.DepartmentId == deptId).FirstAsync();

            #region 查询符合步骤条件的签核人，带有注本、兼、代的标识并按照入职时间正序排序
            var candidateUserSql = @"SELECT
                                            t.UserId,
                                            t.UserName,
                                            t.AppointmentType,
                                            t.AppointmentTypeName,
                                            t.AgentUserId,
                                            t.AgentUserName
                                        FROM
                                        (
                                            SELECT
                                                users.UserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN users.UserNameCn ELSE users.UserNameEn END AS UserName,
                                                users.HireDate,
                                                CASE
                                                    WHEN agentuser.UserId IS NULL THEN
                                                        (SELECT TOP 1 DicCode
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pprimary)
                                                    ELSE
                                                        (SELECT TOP 1 DicCode
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pagent)
                                                END AS AppointmentType,
                                                CASE
                                                    WHEN agentuser.UserId IS NULL THEN
                                                        (SELECT TOP 1 
                                                                CASE WHEN @plocale = 'zh-CN' THEN DicNameCn ELSE DicNameEn END
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pprimary)
                                                    ELSE
                                                        (SELECT TOP 1 
                                                                CASE WHEN @plocale = 'zh-CN' THEN DicNameCn ELSE DicNameEn END
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pagent)
                                                END AS AppointmentTypeName,
                                                agentuser.UserId AS AgentUserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn ELSE agentuser.UserNameEn END AS AgentUserName
                                            FROM Basic.UserInfo users WITH (NOLOCK)
                                            INNER JOIN Basic.UserPosition position ON users.PositionId = position.PositionId
                                            INNER JOIN Basic.DepartmentInfo dept ON users.DepartmentId = dept.DepartmentId
                                            INNER JOIN Basic.UserPosition pos ON users.PositionId = pos.PositionId
                                            LEFT JOIN Basic.UserAgent agent ON users.UserId = agent.SubstituteUserId
                                                AND agent.StartTime <= GETDATE()
                                                AND agent.EndTime >= GETDATE()
                                            LEFT JOIN Basic.UserInfo agentuser ON agent.AgentUserId = agentuser.UserId
                                            WHERE
                                                users.IsEmployed = 1
                                                AND users.IsFreeze = 0
                                                AND users.IsApproval = 1
                                                AND dept.DepartmentId = @pdeptId
                                                AND users.PositionId = @pposId

                                            UNION ALL

                                            SELECT
                                                users.UserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN users.UserNameCn ELSE users.UserNameEn END AS UserName,
                                                users.HireDate,
                                                CASE
                                                    WHEN agentuser.UserId IS NULL THEN
                                                        (SELECT TOP 1 DicCode
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pconcurrent)
                                                    ELSE
                                                        (SELECT TOP 1 DicCode
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pconcurrentagent)
                                                END AS AppointmentType,
                                                CASE
                                                    WHEN agentuser.UserId IS NULL THEN
                                                        (SELECT TOP 1 
                                                                CASE WHEN @plocale = 'zh-CN' THEN DicNameCn ELSE DicNameEn END
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pconcurrent)
                                                    ELSE
                                                        (SELECT TOP 1 
                                                                CASE WHEN @plocale = 'zh-CN' THEN DicNameCn ELSE DicNameEn END
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pconcurrentagent)
                                                END AS AppointmentTypeName,
                                                agentuser.UserId AS AgentUserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn ELSE agentuser.UserNameEn END AS AgentUserName
                                            FROM Basic.UserPartTime part WITH (NOLOCK)
                                            INNER JOIN Basic.UserInfo users ON part.UserId = users.UserId
                                            INNER JOIN Basic.UserPosition position ON part.PartTimePositionId = position.PositionId
                                            INNER JOIN Basic.DepartmentInfo dept ON part.PartTimeDeptId = dept.DepartmentId
                                            LEFT JOIN Basic.UserAgent agent ON users.UserId = agent.SubstituteUserId
                                                AND agent.StartTime <= GETDATE()
                                                AND agent.EndTime >= GETDATE()
                                            LEFT JOIN Basic.UserInfo agentuser ON agent.AgentUserId = agentuser.UserId
                                            WHERE
                                                users.IsEmployed = 1
                                                AND users.IsFreeze = 0
                                                AND users.IsApproval = 1
                                                AND dept.DepartmentId = @pdeptId
                                                AND part.PartTimePositionId = @pposId
                                        ) t ORDER BY t.HireDate ASC;";
            #endregion

            var candidateUserPar = new List<SugarParameter>
            {
                new SugarParameter("@plocale", _lang.Locale),
                new SugarParameter("@pprimary", AppointmentType.Primary.ToEnumString()),
                new SugarParameter("@pagent", AppointmentType.Agent.ToEnumString()),
                new SugarParameter("@pconcurrent", AppointmentType.Concurrent.ToEnumString()),
                new SugarParameter("@pconcurrentagent", AppointmentType.ConcurrentAgent.ToEnumString()),
                new SugarParameter("@pdeptId", deptId),
                new SugarParameter("@pposId", positionId),
            };

            // 查询符合条件审批人候选人，注本、兼、代、兼代的标识并按照入职时间正序排序
            var candidateList = await _db.Ado.SqlQueryAsync<StepApproveUser>(candidateUserSql, candidateUserPar);
            if (candidateList.Count() > 0)
            {
                // 按照本、兼、代、兼代的顺序筛选最终审批人
                finalApproveUser = await GetFinalStepApproveUserList(candidateList, approveMode, "PirCon");
            }

            // 如果没有符合条件的审批人，则查询本部门及以上的符合条件审批人（自动指派）
            else
            {
                // 查询指定部门所有上级部门Ids
                var parentDeptList = await _db.Queryable<DepartmentInfoEntity>()
                                              .With(SqlWith.NoLock)
                                              .ToParentListAsync(dept => dept.ParentId, deptId);
                var parentDeptIds = parentDeptList.Select(dept => dept.DepartmentId).ToList();

                var deptLevelMaxOrder = await _db.Queryable<DepartmentLevelEntity>()
                                                 .With(SqlWith.NoLock)
                                                 .MaxAsync(deptlevel => deptlevel.SortOrder);
                var posMaxOrder = await _db.Queryable<UserPositionEntity>()
                                           .With(SqlWith.NoLock)
                                           .MaxAsync(position => position.SortOrder);
                var stepDeptStartOrder = await _db.Queryable<DepartmentLevelEntity>()
                                                  .With(SqlWith.NoLock)
                                                  .Where(dept => dept.DepartmentLevelId == deptInfo.DepartmentLevelId)
                                                  .MaxAsync(position => position.SortOrder);
                var stepPosStartOrder = await _db.Queryable<UserPositionEntity>()
                                                 .With(SqlWith.NoLock)
                                                 .Where(position => position.PositionId == positionId)
                                                 .MaxAsync(position => position.SortOrder);

                List<StepApproveUser> hightLevelApproveUser = new List<StepApproveUser>();

                // 依次按照部门级别递减
                for (int itemDeptLevelOrder = stepDeptStartOrder; itemDeptLevelOrder > 0 && itemDeptLevelOrder <= deptLevelMaxOrder;)
                {
                    // 依次按照职级递减
                    for (int itemPosOrder = stepPosStartOrder; itemPosOrder > 0 && itemPosOrder <= posMaxOrder;)
                    {
                        var deptInSql = string.Join(", ", parentDeptIds);

                        #region 查询高阶审批人候选人，注本、兼、代、兼代的标识并按照职级倒序、入职时间正序排序
                        var highCandidateUserSql = $@"SELECT
                                                            t.UserId,
                                                            t.UserName,
                                                            t.AppointmentType,
                                                            t.AppointmentTypeName,
                                                            t.AgentUserId,
                                                            t.AgentUserName
                                                        FROM
                                                        (
                                                            SELECT
                                                                users.UserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN users.UserNameCn
                                                                    ELSE users.UserNameEn
                                                                END AS UserName,
                                                                position.SortOrder,
                                                                users.HireDate,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN
                                                                        (
                                                                            SELECT TOP 1 DicCode
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoprimary
                                                                        )
                                                                    ELSE
                                                                        (
                                                                            SELECT TOP 1 DicCode
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoagent
                                                                        )
                                                                END AS AppointmentType,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN
                                                                        (
                                                                            SELECT TOP 1
                                                                                CASE
                                                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                                                    ELSE DicNameEn
                                                                                END
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoprimary
                                                                        )
                                                                    ELSE
                                                                        (
                                                                            SELECT TOP 1
                                                                                CASE
                                                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                                                    ELSE DicNameEn
                                                                                END
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoagent
                                                                        )
                                                                END AS AppointmentTypeName,
                                                                agentuser.UserId AS AgentUserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn
                                                                    ELSE agentuser.UserNameEn
                                                                END AS AgentUserName
                                                            FROM Basic.UserInfo users WITH (NOLOCK)
                                                            INNER JOIN Basic.UserPosition position ON users.PositionId = position.PositionId
                                                            INNER JOIN Basic.DepartmentInfo dept ON users.DepartmentId = dept.DepartmentId
                                                            INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                                                            INNER JOIN Basic.UserPosition pos ON users.PositionId = pos.PositionId
                                                            LEFT JOIN Basic.UserAgent agent ON users.UserId = agent.SubstituteUserId
                                                               AND agent.StartTime <= GETDATE()
                                                               AND agent.EndTime >= GETDATE()
                                                            LEFT JOIN Basic.UserInfo agentuser
                                                                ON agent.AgentUserId = agentuser.UserId
                                                            WHERE 
                                                              users.IsEmployed = 1
                                                              AND users.IsFreeze = 0
                                                              AND users.IsApproval = 1
                                                              AND dept.DepartmentId IN ({deptInSql})
                                                              AND deptlevel.SortOrder = @pdeptlevelOrder
                                                              AND position.SortOrder = @pposOrder

                                                            UNION ALL

                                                            SELECT
                                                                users.UserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN users.UserNameCn
                                                                    ELSE users.UserNameEn
                                                                END AS UserName,
                                                                position.SortOrder,
                                                                users.HireDate,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN
                                                                        (
                                                                            SELECT TOP 1 DicCode
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoconcurrent
                                                                        )
                                                                    ELSE
                                                                        (
                                                                            SELECT TOP 1 DicCode
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoconcurrentagent
                                                                        )
                                                                END AS AppointmentType,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN
                                                                        (
                                                                            SELECT TOP 1
                                                                                CASE
                                                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                                                    ELSE DicNameEn
                                                                                END
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoconcurrent
                                                                        )
                                                                    ELSE
                                                                        (
                                                                            SELECT TOP 1
                                                                                CASE
                                                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                                                    ELSE DicNameEn
                                                                                END
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoconcurrentagent
                                                                        )
                                                                END AS AppointmentTypeName,
                                                                agentuser.UserId AS AgentUserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn
                                                                    ELSE agentuser.UserNameEn
                                                                END AS AgentUserName
                                                            FROM Basic.UserPartTime part WITH (NOLOCK)
                                                            INNER JOIN Basic.UserInfo users ON part.UserId = users.UserId
                                                            INNER JOIN Basic.UserPosition position ON part.PartTimePositionId = position.PositionId
                                                            INNER JOIN Basic.DepartmentInfo dept ON part.PartTimeDeptId = dept.DepartmentId
                                                            INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                                                            LEFT JOIN Basic.UserAgent agent ON users.UserId = agent.SubstituteUserId
                                                               AND agent.StartTime <= GETDATE()
                                                               AND agent.EndTime >= GETDATE()
                                                            LEFT JOIN Basic.UserInfo agentuser ON agent.AgentUserId = agentuser.UserId
                                                            WHERE 
                                                              users.IsEmployed = 1
                                                              AND users.IsFreeze = 0
                                                              AND users.IsApproval = 1
                                                              AND dept.DepartmentId IN ({deptInSql})
                                                              AND deptlevel.SortOrder = @pdeptlevelOrder
                                                              AND position.SortOrder = @pposOrder
                                                        ) t ORDER BY SortOrder DESC,t.HireDate ASC;";
                        #endregion

                        var highCandidateUserPar = new List<SugarParameter>
                        {
                            new SugarParameter("@plocale", _lang.Locale),
                            new SugarParameter("@pautoprimary", AppointmentType.AutoPrimary.ToEnumString()),
                            new SugarParameter("@pautoagent", AppointmentType.AutoAgent.ToEnumString()),
                            new SugarParameter("@pautoconcurrent", AppointmentType.AutoConcurrent.ToEnumString()),
                            new SugarParameter("@pautoconcurrentagent", AppointmentType.AutoConcurrentAgent.ToEnumString()),
                            new SugarParameter("@pdeptlevelOrder", itemDeptLevelOrder),
                            new SugarParameter("@pposOrder", itemPosOrder),
                        };
                        for (int i = 0; i < parentDeptIds.Count; i++)
                        {
                            highCandidateUserPar.Add(new SugarParameter($"@dept{i}", parentDeptIds[i]));
                        }

                        // 查询符合条件候选审批人
                        var highLevelUser = await _db.Ado.SqlQueryAsync<StepApproveUser>(highCandidateUserSql, highCandidateUserPar);
                        if (highLevelUser.Count > 0)
                        {
                            // 按照本、兼、代、兼代的顺序筛选最终审批人
                            hightLevelApproveUser = await GetFinalStepApproveUserList(highLevelUser, approveMode, "Auto");
                            itemPosOrder = -1;
                        }
                        else
                        {
                            itemPosOrder--;
                        }
                    }
                    if (hightLevelApproveUser.Count > 0)
                    {
                        itemDeptLevelOrder = -1;
                    }
                    else
                    {
                        itemDeptLevelOrder--;
                    }
                }
                finalApproveUser.AddRange(hightLevelApproveUser);
            }
            return finalApproveUser;
        }

        /// <summary>
        /// 查询步骤依照组织架构审批人
        /// </summary>
        /// <param name="stepUser"></param>
        /// <param name="approveMode"></param>
        /// <returns></returns>
        public async Task<List<StepApproveUser>> GetStepApproveUserByUser(string approveMode,long stepUserId)
        {
            // 步骤最终审批人列表
            var finalApproveUser = new List<StepApproveUser>();

            // 申请人信息
            var stepUser = await _db.Queryable<UserInfoEntity>().Where(user => user.UserId == stepUserId).FirstAsync();

            #region 查询符合步骤条件的签核人，带有注本、兼、代的标识并按照入职时间正序排序
            var candidateUserSql = @"SELECT
                                            t.UserId,
                                            t.UserName,
                                            t.AppointmentType,
                                            t.AppointmentTypeName,
                                            t.AgentUserId,
                                            t.AgentUserName
                                        FROM
                                        (
                                            SELECT
                                                users.UserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN users.UserNameCn ELSE users.UserNameEn END AS UserName,
                                                users.HireDate,
                                                CASE
                                                    WHEN agentuser.UserId IS NULL THEN
                                                        (SELECT TOP 1 DicCode
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pprimary)
                                                    ELSE
                                                        (SELECT TOP 1 DicCode
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pagent)
                                                END AS AppointmentType,
                                                CASE
                                                    WHEN agentuser.UserId IS NULL THEN
                                                        (SELECT TOP 1 
                                                                CASE WHEN @plocale = 'zh-CN' THEN DicNameCn ELSE DicNameEn END
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pprimary)
                                                    ELSE
                                                        (SELECT TOP 1 
                                                                CASE WHEN @plocale = 'zh-CN' THEN DicNameCn ELSE DicNameEn END
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pagent)
                                                END AS AppointmentTypeName,
                                                agentuser.UserId AS AgentUserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn ELSE agentuser.UserNameEn END AS AgentUserName
                                            FROM Basic.UserInfo users WITH (NOLOCK)
                                            INNER JOIN Basic.UserPosition position ON users.PositionId = position.PositionId
                                            INNER JOIN Basic.DepartmentInfo dept ON users.DepartmentId = dept.DepartmentId
                                            INNER JOIN Basic.UserPosition pos ON users.PositionId = pos.PositionId
                                            LEFT JOIN Basic.UserAgent agent ON users.UserId = agent.SubstituteUserId
                                                AND agent.StartTime <= GETDATE()
                                                AND agent.EndTime >= GETDATE()
                                            LEFT JOIN Basic.UserInfo agentuser ON agent.AgentUserId = agentuser.UserId
                                            WHERE
                                                users.IsEmployed = 1
                                                AND users.IsFreeze = 0
                                                AND users.IsApproval = 1
                                                AND users.UserId = @puserId

                                            UNION ALL

                                            SELECT
                                                users.UserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN users.UserNameCn ELSE users.UserNameEn END AS UserName,
                                                users.HireDate,
                                                CASE
                                                    WHEN agentuser.UserId IS NULL THEN
                                                        (SELECT TOP 1 DicCode
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pconcurrent)
                                                    ELSE
                                                        (SELECT TOP 1 DicCode
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pconcurrentagent)
                                                END AS AppointmentType,
                                                CASE
                                                    WHEN agentuser.UserId IS NULL THEN
                                                        (SELECT TOP 1 
                                                                CASE WHEN @plocale = 'zh-CN' THEN DicNameCn ELSE DicNameEn END
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pconcurrent)
                                                    ELSE
                                                        (SELECT TOP 1 
                                                                CASE WHEN @plocale = 'zh-CN' THEN DicNameCn ELSE DicNameEn END
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = @pconcurrentagent)
                                                END AS AppointmentTypeName,
                                                agentuser.UserId AS AgentUserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn ELSE agentuser.UserNameEn END AS AgentUserName
                                            FROM Basic.UserPartTime part WITH (NOLOCK)
                                            INNER JOIN Basic.UserInfo users ON part.UserId = users.UserId
                                            INNER JOIN Basic.UserPosition position ON part.PartTimePositionId = position.PositionId
                                            INNER JOIN Basic.DepartmentInfo dept ON part.PartTimeDeptId = dept.DepartmentId
                                            LEFT JOIN Basic.UserAgent agent ON users.UserId = agent.SubstituteUserId
                                                AND agent.StartTime <= GETDATE()
                                                AND agent.EndTime >= GETDATE()
                                            LEFT JOIN Basic.UserInfo agentuser ON agent.AgentUserId = agentuser.UserId
                                            WHERE
                                                users.IsEmployed = 1
                                                AND users.IsFreeze = 0
                                                AND users.IsApproval = 1
                                                AND part.UserId = @puserId
                                        ) t ORDER BY t.HireDate ASC;";
            #endregion

            var candidateUserPar = new List<SugarParameter>
            {
                new SugarParameter("@plocale", _lang.Locale),
                new SugarParameter("@pprimary", AppointmentType.Primary.ToEnumString()),
                new SugarParameter("@pagent", AppointmentType.Agent.ToEnumString()),
                new SugarParameter("@pconcurrent", AppointmentType.Concurrent.ToEnumString()),
                new SugarParameter("@pconcurrentagent", AppointmentType.ConcurrentAgent.ToEnumString()),
                new SugarParameter("@puserId", stepUser.UserId),
            };

            // 查询符合条件审批人候选人，注本、兼、代、兼代的标识并按照入职时间正序排序
            var candidateList = await _db.Ado.SqlQueryAsync<StepApproveUser>(candidateUserSql, candidateUserPar);
            if (candidateList.Count() > 0)
            {
                // 按照本、兼、代、兼代的顺序筛选最终审批人
                finalApproveUser = await GetFinalStepApproveUserList(candidateList, approveMode, "PirCon");
            }

            // 如果没有符合条件的审批人，则查询本部门及以上的符合条件审批人（自动指派）
            else
            {
                // 申请人所有上级部门Ids
                var parentDeptList = await _db.Queryable<DepartmentInfoEntity>()
                                              .With(SqlWith.NoLock)
                                              .ToParentListAsync(dept => dept.ParentId, stepUser.DepartmentId);
                var parentDeptIds = parentDeptList.Select(dept => dept.DepartmentId).ToList();


                // 指定员工部门级别信息
                var stepUserDeptLevel = await _db.Queryable<DepartmentInfoEntity>().Where(dept => dept.DepartmentId == stepUser.DepartmentId).FirstAsync();
                // 指定员工职级信息
                var stepUserPositon = await _db.Queryable<UserPositionEntity>().Where(position => position.PositionId == stepUser.PositionId).FirstAsync();


                var deptLevelMaxOrder = await _db.Queryable<DepartmentLevelEntity>()
                                                 .With(SqlWith.NoLock)
                                                 .MaxAsync(deptlevel => deptlevel.SortOrder);
                var posMaxOrder = await _db.Queryable<UserPositionEntity>()
                                           .With(SqlWith.NoLock)
                                           .MaxAsync(position => position.SortOrder);
                var stepDeptStartOrder = await _db.Queryable<DepartmentLevelEntity>()
                                                .With(SqlWith.NoLock)
                                                .Where(dept => dept.DepartmentLevelId == stepUserDeptLevel.DepartmentLevelId)
                                                .MaxAsync(position => position.SortOrder);
                var stepPosStartOrder = await _db.Queryable<UserPositionEntity>()
                                               .With(SqlWith.NoLock)
                                               .Where(position => position.PositionId == stepUser.PositionId)
                                               .MaxAsync(position => position.SortOrder);

                List<StepApproveUser> hightLevelApproveUser = new List<StepApproveUser>();

                // 依次按照部门级别递减
                for (int itemDeptLevelOrder = stepDeptStartOrder; itemDeptLevelOrder > 0 && itemDeptLevelOrder <= deptLevelMaxOrder;)
                {
                    // 依次按照职级递减
                    for (int itemPosOrder = stepPosStartOrder; itemPosOrder > 0 && itemPosOrder <= posMaxOrder;)
                    {
                        var deptInSql = string.Join(", ", parentDeptIds);

                        #region 查询高阶审批人候选人，注本、兼、代、兼代的标识并按照职级倒序、入职时间正序排序
                        var highCandidateUserSql = $@"SELECT
                                                            t.UserId,
                                                            t.UserName,
                                                            t.AppointmentType,
                                                            t.AppointmentTypeName,
                                                            t.AgentUserId,
                                                            t.AgentUserName
                                                        FROM
                                                        (
                                                            SELECT
                                                                users.UserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN users.UserNameCn
                                                                    ELSE users.UserNameEn
                                                                END AS UserName,
                                                                position.SortOrder,
                                                                users.HireDate,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN
                                                                        (
                                                                            SELECT TOP 1 DicCode
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoprimary
                                                                        )
                                                                    ELSE
                                                                        (
                                                                            SELECT TOP 1 DicCode
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoagent
                                                                        )
                                                                END AS AppointmentType,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN
                                                                        (
                                                                            SELECT TOP 1
                                                                                CASE
                                                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                                                    ELSE DicNameEn
                                                                                END
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoprimary
                                                                        )
                                                                    ELSE
                                                                        (
                                                                            SELECT TOP 1
                                                                                CASE
                                                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                                                    ELSE DicNameEn
                                                                                END
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoagent
                                                                        )
                                                                END AS AppointmentTypeName,
                                                                agentuser.UserId AS AgentUserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn
                                                                    ELSE agentuser.UserNameEn
                                                                END AS AgentUserName
                                                            FROM Basic.UserInfo users WITH (NOLOCK)
                                                            INNER JOIN Basic.UserPosition position ON users.PositionId = position.PositionId
                                                            INNER JOIN Basic.DepartmentInfo dept ON users.DepartmentId = dept.DepartmentId
                                                            INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                                                            INNER JOIN Basic.UserPosition pos ON users.PositionId = pos.PositionId
                                                            LEFT JOIN Basic.UserAgent agent ON users.UserId = agent.SubstituteUserId
                                                               AND agent.StartTime <= GETDATE()
                                                               AND agent.EndTime >= GETDATE()
                                                            LEFT JOIN Basic.UserInfo agentuser
                                                                ON agent.AgentUserId = agentuser.UserId
                                                            WHERE 
                                                              users.IsEmployed = 1
                                                              AND users.IsFreeze = 0
                                                              AND users.IsApproval = 1
                                                              AND dept.DepartmentId IN ({deptInSql})
                                                              AND deptlevel.SortOrder = @pdeptlevelOrder
                                                              AND position.SortOrder = @pposOrder
                                                              AND position.SortOrder < @pstepUserPosOrder

                                                            UNION ALL

                                                            SELECT
                                                                users.UserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN users.UserNameCn
                                                                    ELSE users.UserNameEn
                                                                END AS UserName,
                                                                position.SortOrder,
                                                                users.HireDate,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN
                                                                        (
                                                                            SELECT TOP 1 DicCode
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoconcurrent
                                                                        )
                                                                    ELSE
                                                                        (
                                                                            SELECT TOP 1 DicCode
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoconcurrentagent
                                                                        )
                                                                END AS AppointmentType,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN
                                                                        (
                                                                            SELECT TOP 1
                                                                                CASE
                                                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                                                    ELSE DicNameEn
                                                                                END
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoconcurrent
                                                                        )
                                                                    ELSE
                                                                        (
                                                                            SELECT TOP 1
                                                                                CASE
                                                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                                                    ELSE DicNameEn
                                                                                END
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = @pautoconcurrentagent
                                                                        )
                                                                END AS AppointmentTypeName,
                                                                agentuser.UserId AS AgentUserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn
                                                                    ELSE agentuser.UserNameEn
                                                                END AS AgentUserName
                                                            FROM Basic.UserPartTime part WITH (NOLOCK)
                                                            INNER JOIN Basic.UserInfo users ON part.UserId = users.UserId
                                                            INNER JOIN Basic.UserPosition position ON part.PartTimePositionId = position.PositionId
                                                            INNER JOIN Basic.DepartmentInfo dept ON part.PartTimeDeptId = dept.DepartmentId
                                                            INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                                                            LEFT JOIN Basic.UserAgent agent ON users.UserId = agent.SubstituteUserId
                                                               AND agent.StartTime <= GETDATE()
                                                               AND agent.EndTime >= GETDATE()
                                                            LEFT JOIN Basic.UserInfo agentuser ON agent.AgentUserId = agentuser.UserId
                                                            WHERE 
                                                              users.IsEmployed = 1
                                                              AND users.IsFreeze = 0
                                                              AND users.IsApproval = 1
                                                              AND dept.DepartmentId IN ({deptInSql})
                                                              AND deptlevel.SortOrder = @pdeptlevelOrder
                                                              AND position.SortOrder = @pposOrder
                                                              AND position.SortOrder < @pstepUserPosOrder
                                                        ) t ORDER BY SortOrder DESC,t.HireDate ASC;";
                        #endregion

                        var highCandidateUserPar = new List<SugarParameter>
                        {
                            new SugarParameter("@plocale", _lang.Locale),
                            new SugarParameter("@pautoprimary", AppointmentType.AutoPrimary.ToEnumString()),
                            new SugarParameter("@pautoagent", AppointmentType.AutoAgent.ToEnumString()),
                            new SugarParameter("@pautoconcurrent", AppointmentType.AutoConcurrent.ToEnumString()),
                            new SugarParameter("@pautoconcurrentagent", AppointmentType.AutoConcurrentAgent.ToEnumString()),
                            new SugarParameter("@pdeptlevelOrder", itemDeptLevelOrder),
                            new SugarParameter("@pposOrder", itemPosOrder),
                            new SugarParameter("@pstepUserPosOrder", stepUserPositon.SortOrder),
                        };
                        for (int i = 0; i < parentDeptIds.Count; i++)
                        {
                            highCandidateUserPar.Add(new SugarParameter($"@dept{i}", parentDeptIds[i]));
                        }

                        // 查询符合条件候选审批人
                        var highLevelUser = await _db.Ado.SqlQueryAsync<StepApproveUser>(highCandidateUserSql, highCandidateUserPar);
                        if (highLevelUser.Count > 0)
                        {
                            // 按照本、兼、代、兼代的顺序筛选最终审批人
                            hightLevelApproveUser = await GetFinalStepApproveUserList(highLevelUser, approveMode, "Auto");
                            itemPosOrder = -1;
                        }
                        else
                        {
                            itemPosOrder--;
                        }
                    }
                    if (hightLevelApproveUser.Count > 0)
                    {
                        itemDeptLevelOrder = -1;
                    }
                    else
                    {
                        itemDeptLevelOrder--;
                    }
                }
                finalApproveUser.AddRange(hightLevelApproveUser);
            }
            return finalApproveUser;
        }

        /// <summary>
        /// 按照本、兼、代、兼代的顺序筛选最终审批人
        /// </summary>
        /// <param name="stepApproveUser"></param>
        /// <param name="approveMode"></param>
        /// <param name="seekType"></param>
        /// <returns></returns>
        public async Task<List<StepApproveUser>> GetFinalStepApproveUserList(List<StepApproveUser> stepApproveUser, string approveMode, string seekType)
        {
            List<StepApproveUser> finalApproveUserList = new List<StepApproveUser>();

            string AppTypePrimary = seekType == "PirCon"
                ? AppointmentType.Primary.ToEnumString() : AppointmentType.AutoPrimary.ToEnumString();
            string AppTypeAgent = seekType == "PirCon"
                ? AppointmentType.Agent.ToEnumString() : AppointmentType.AutoAgent.ToEnumString();
            string AppTypeConcurrent = seekType == "PirCon"
                ? AppointmentType.Concurrent.ToEnumString() : AppointmentType.AutoConcurrent.ToEnumString();
            string AppTypeConcurrentAgent = seekType == "PirCon"
                ? AppointmentType.ConcurrentAgent.ToEnumString() : AppointmentType.AutoConcurrentAgent.ToEnumString();

            if (approveMode == ApproveMode.Single.ToEnumString())
            {
                var primary = stepApproveUser.Where(user => user.AppointmentType == AppTypePrimary).Count();
                var Agent = stepApproveUser.Where(user => user.AppointmentType == AppTypeAgent).Count();
                var Concurrent = stepApproveUser.Where(user => user.AppointmentType == AppTypeConcurrent).Count();
                var ConcurrentAgent = stepApproveUser.Where(user => user.AppointmentType == AppTypeConcurrentAgent).Count();
                if (primary > 0)
                {
                    finalApproveUserList = stepApproveUser.Where(user => user.AppointmentType == AppTypePrimary).ToList();
                }
                else if (Agent > 0)
                {
                    finalApproveUserList = stepApproveUser.Where(user => user.AppointmentType == AppTypeAgent).ToList();
                }
                else if (Concurrent > 0)
                {
                    finalApproveUserList = stepApproveUser.Where(user => user.AppointmentType == AppTypeConcurrent).ToList();
                }
                else if (ConcurrentAgent > 0)
                {
                    finalApproveUserList = stepApproveUser.Where(user => user.AppointmentType == AppTypeConcurrentAgent).ToList();
                }
            }
            else if (approveMode == ApproveMode.AndSingle.ToEnumString() || approveMode == ApproveMode.OrSingle.ToEnumString())
            {
                finalApproveUserList = stepApproveUser;
            }
            return finalApproveUserList;
        }
    }
}
