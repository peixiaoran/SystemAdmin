using SqlSugar;
using System.Text.RegularExpressions;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormAudit.Entity;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
using SystemAdmin.Model.FormBusiness.Forms.FormLifecycle.FormBeforeStart;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;
using SystemAdmin.Repository.FormBusiness.Forms.FormLifecycle;

namespace SystemAdmin.Repository.FormBusiness.FormLifecycle
{
    /// <summary>
    /// 表单开始前处理类
    /// </summary>
    public class FormBeforeStart
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;
        private readonly WorkflowConditionFun _workflowConditionFun;
        //private readonly string _this = "FormBusiness.FormLifecycle.FormBeforeStart";

        public FormBeforeStart(SqlSugarScope db, Language lang, WorkflowConditionFun workflowConditionFun)
        {
            _db = db;
            _lang = lang;
            _workflowConditionFun = workflowConditionFun;
        }

        /// <summary>
        /// 生成表单编号
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<string> GenerateFormNo(long userId, long formTypeId)
        {
            var formType = await _db.Queryable<FormTypeEntity>().FirstAsync(formtype => formtype.FormTypeId == formTypeId);

            var now = DateTime.Now;
            var ym = now.ToString("yyMM");

            var seq = 1;

            var counting = await _db.Queryable<FormCountingEntity>().FirstAsync(formcounting => formcounting.FormTypeId == formTypeId && formcounting.YM == ym);

            if (counting == null)
            {
                var entity = new FormCountingEntity
                {
                    FormTypeId = formTypeId,
                    YM = ym,
                    Total = seq,
                    CreatedBy = userId,
                    CreatedDate = now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                await _db.Insertable(entity).ExecuteCommandAsync();
            }
            else
            {
                seq = counting.Total + 1;

                await _db.Updateable<FormCountingEntity>()
                         .SetColumns(formcounting => new FormCountingEntity
                         {
                             Total = seq
                         }).Where(formcounting => formcounting.FormTypeId == formTypeId && formcounting.YM == ym)
                         .ExecuteCommandAsync();
            }

            return $"{formType.Prefix}-{ym}{seq:D4}";
        }

        /// <summary>
        /// 初始化表单
        /// </summary>
        public async Task<FormInfoEntity> InitFormInfo(long userId, long formTypeId)
        {
            // 获取表单编号
            long startStepId = await GetWorkFlowIsStartStepId(formTypeId);
            var formNo = await GenerateFormNo(userId, formTypeId);
            var insertForm = new FormInfoEntity()
            {
                FormId = SnowFlakeSingle.Instance.NextId(),
                FormNo = formNo,
                FormTypeId = formTypeId,
                FormStatus = FormStatus.PendingSubmission.ToEnumString(),
                LastConditionId = null,
                LastStepId = null,
                NowStepId = startStepId,
                CreatedBy = userId,
                CreatedDate = DateTime.Now,
            };
            await _db.Insertable(insertForm).ExecuteCommandAsync();
            return insertForm;
        }

        /// <summary>
        /// 保存表单信息
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="loginuserId"></param>
        /// <returns></returns>
        public async Task<int> SaveFormInfo(long formId, long loginuserId)
        {
            return await _db.Updateable<FormInfoEntity>()
                            .SetColumns(forminfo => new FormInfoEntity
                            {
                                ModifiedBy = loginuserId,
                                ModifiedDate = DateTime.Now,
                            }).Where(forminfo => forminfo.FormId == formId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 添加表单待签核人
        /// </summary>
        /// <returns></returns>
        public async Task<int> AddPendingApprover(PendingApprovalEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
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
        public async Task<FormWorkflowInfo> GetWorkflowAllApproveUser(long formId)
        {
            var formWorkflow = new FormWorkflowInfo();

            // 查询表单当前步骤
            formWorkflow.FormId = formId;
            formWorkflow.NowStepId = await _db.Queryable<FormInfoEntity>().Where(form => form.FormId == formId).Select(form => form.NowStepId).FirstAsync();

            var workflowApproveList = new List<WorkflowApproveUser>();

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
            var workflowStepId = await _db.Queryable<WorkflowStepEntity>()
                                          .With(SqlWith.NoLock)
                                          .InnerJoin<FormInfoEntity>((stepinfo, form) => stepinfo.FormTypeId == form.FormTypeId)
                                          .Where((stepinfo, form) => form.FormId == formId && stepinfo.IsStartStep == 1)
                                          .Select((stepinfo, form) => stepinfo.StepId)
                                          .FirstAsync();
            while (workflowStepId > -1)
            {
                var nowStep = await _db.Queryable<WorkflowStepEntity>()
                                       .With(SqlWith.NoLock)
                                       .Where(stepinfo => stepinfo.StepId == workflowStepId)
                                       .FirstAsync();

                // 如果是开始步骤，审批人默认为申请人
                if (nowStep.IsStartStep == 1)
                {
                    WorkflowApproveUser approveItem = new WorkflowApproveUser();

                    // 查询签核类型（本）
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
                    workflowApproveList.Add(approveItem);
                }

                // 依照组织架构
                else if (nowStep.Assignment == Assignment.Org.ToEnumString())
                {
                    // 步骤要求（部门级别、职级）
                    var stepOrg = await _db.Queryable<WorkflowStepOrgEntity>()
                                           .With(SqlWith.NoLock)
                                           .Where(org => org.StepId == nowStep.StepId)
                                           .FirstAsync();

                    // 组织架构部门级别、职级信息
                    var orgDeptLeave = await _db.Queryable<DepartmentLevelEntity>()
                                                .With(SqlWith.NoLock)
                                                .Where(deptleave => deptleave.DepartmentLevelId == stepOrg.DeptLeaveId)
                                                .FirstAsync();
                    var orgPosition = await _db.Queryable<UserPositionEntity>()
                                                .With(SqlWith.NoLock)
                                                .Where(position => position.PositionId == stepOrg.PositionId)
                                                .FirstAsync();

                    // 申请人部门等级、职级信息
                    var applyDeptLevel = await _db.Queryable<UserInfoEntity>()
                                                  .With(SqlWith.NoLock)
                                                  .LeftJoin<DepartmentInfoEntity>((user, dept) => user.DepartmentId == dept.DepartmentId)
                                                  .LeftJoin<DepartmentLevelEntity>((user, dept, deptlevel) => dept.DepartmentLevelId == deptlevel.DepartmentLevelId)
                                                  .Where((user, dept, deptlevel) => user.UserId == applyUserInfo.UserId)
                                                  .Select((user, dept, deptlevel) => deptlevel)
                                                  .FirstAsync();
                    var applyPosition = await _db.Queryable<UserInfoEntity>()
                                                 .With(SqlWith.NoLock)
                                                 .LeftJoin<UserPositionEntity>((user, position) => user.PositionId == position.PositionId)
                                                 .Where((user, position) => user.UserId == applyUserInfo.UserId)
                                                 .Select((user, position) => position)
                                                 .FirstAsync();

                    // 如果申请人部门等级、职级大于组织架构步骤的部门等级，则职级覆盖跳过
                    if (applyDeptLevel.SortOrder <= orgDeptLeave.SortOrder && applyPosition.SortOrder <= orgPosition.SortOrder)
                    {
                        WorkflowApproveUser approveItem = new WorkflowApproveUser();

                        // 查询签核类型（职级覆盖跳过）
                        var applyTypeSkip = await _db.Queryable<DictionaryInfoEntity>()
                                                     .With(SqlWith.NoLock)
                                                     .Where(app => app.DicType == "AppointmentType" && app.DicCode == AppointmentType.HierarchySkipped.ToEnumString())
                                                     .FirstAsync();

                        List<StepApproveUser> stepApproveUser = new List<StepApproveUser>();
                        stepApproveUser.Add(new StepApproveUser
                        {
                            AppointmentType = applyTypeSkip.DicCode,
                            AppointmentTypeName = _lang.Locale == "zh-CN"
                                       ? applyTypeSkip.DicNameCn
                                       : applyTypeSkip.DicNameEn
                        });

                        approveItem.StepId = nowStep.StepId;
                        approveItem.StepName = _lang.Locale == "zh-CN"
                                               ? nowStep.StepNameCn
                                               : nowStep.StepNameEn;
                        approveItem.stepApproveUser = stepApproveUser;
                        workflowApproveList.Add(approveItem);
                    }
                    else
                    {
                        // 查询此步骤的审批人
                        var orgApproveUser = await GetStepApproveUserByOrg(applyUserInfo.UserId, nowStep.ApproveMode, stepOrg.DeptLeaveId, stepOrg.PositionId);

                        WorkflowApproveUser approveItem = new WorkflowApproveUser();
                        approveItem.StepId = nowStep.StepId;
                        approveItem.StepName = _lang.Locale == "zh-CN"
                                               ? nowStep.StepNameCn
                                               : nowStep.StepNameEn;
                        approveItem.stepApproveUser = orgApproveUser;
                        workflowApproveList.Add(approveItem);
                    }
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
                    workflowApproveList.Add(approveItem);
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
                    workflowApproveList.Add(approveItem);
                }

                // 查询步骤下面的分支走向
                var branchList = await _db.Queryable<WorkflowStepBranchEntity>()
                                          .With(SqlWith.NoLock)
                                          .Where(branch => branch.StepId == nowStep.StepId)
                                          .Select(branch => branch)
                                          .ToListAsync();

                // 声明符合条件的分支
                var accBranchList = new List<AccordanceBranch>();
                foreach (var branchItem in branchList)
                {
                    // 如果是默认分支则跳过
                    if (branchItem.ConditionId != -1)
                    {
                        // 寻找符合条件的分支添加到 accBranchList
                        var condition = await _db.Queryable<WorkflowConditionEntity>().Where(condition => condition.ConditionId == branchItem.ConditionId).FirstAsync();

                        // 获取条件表达式方法
                        var method = _workflowConditionFun.GetType().GetMethod(condition.HandlerKey);

                        if (method != null)
                        {
                            // 执行表达式方法结果
                            bool result = await EvaluateExpression(condition.HandlerKey, formId);
                            if (result)
                            {
                                var conitem = new AccordanceBranch();
                                conitem.ConditionId = condition.ConditionId;
                                conitem.ExecuteMatched = branchItem.ExecuteMatched;
                                conitem.NextStepId = branchItem.NextStepId;
                                accBranchList.Add(conitem);
                            }
                        }
                    }
                }

                // 如果没有符合条件，则将默认分支的下一步骤赋值
                if (accBranchList.Count() == 0)
                {
                    workflowStepId = branchList.Where(branch => branch.ConditionId == -1).First().NextStepId;
                }
                // 如果有多个符合条件，则选择执行唯一的分支的下一步骤赋值
                else
                {
                    workflowStepId = accBranchList.First().NextStepId;
                }
            }

            // 标识当前步骤的签核人签核状态
            var pendingAppUser = await _db.Queryable<PendingApprovalEntity>()
                                           .Where(pending => pending.FormId == formId)
                                           .ToListAsync();

            // 找到符合当前步骤的签核人，再匹配待签核人是否已经签核
            var nowStepUser = workflowApproveList.Where(approve => approve.StepId == formWorkflow.NowStepId).First().stepApproveUser;
            foreach (var item in nowStepUser)
            {
                var isPend = pendingAppUser.Where(pending => pending.ApproveUserId == item.UserId).Count();
                item.IsPending = isPend > 0 ? 0 : 1;
            }

            formWorkflow.WorkflowApproveUser = workflowApproveList;
            return formWorkflow;
        }

        /// <summary>
        /// 查询步骤依照组织架构
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

            // 查询指定部门所有上级部门Ids
            var parentDeptList = await _db.Queryable<DepartmentInfoEntity>()
                                          .With(SqlWith.NoLock)
                                          .ToParentListAsync(dept => dept.ParentId, applyUser.DepartmentId);
            var parentDeptIds = parentDeptList.Select(dept => dept.DepartmentId).ToList();
            var deptInSql = string.Join(", ", parentDeptIds);

            #region 查询符合步骤条件的签核人，带有注本、代、兼、兼代并按照入职时间正序排序
            var candidateUserSql = $@";WITH Dic AS
                                        (
                                            SELECT
                                                DicCode,
                                                CASE 
                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                    ELSE DicNameEn
                                                END AS DicName
                                            FROM Basic.DictionaryInfo WITH (NOLOCK)
                                            WHERE DicType = 'AppointmentType'
                                        ),
                                        MainJob AS
                                        (
                                            SELECT
                                                1 AS SortGroup,
                                                users.UserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN users.UserNameCn ELSE users.UserNameEn END AS UserName,
                                                users.HireDate,
                                                CASE 
                                                    WHEN agentuser.UserId IS NULL THEN @pprimary
                                                    ELSE @pagent
                                                END AS AppointmentType,
                                                CASE 
                                                    WHEN agentuser.UserId IS NULL THEN d1.DicName
                                                    ELSE d2.DicName
                                                END AS AppointmentTypeName,
                                                agentuser.UserId AS AgentUserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn ELSE agentuser.UserNameEn END AS AgentUserName
                                            FROM Basic.UserInfo users WITH (NOLOCK)
                                            INNER JOIN Basic.UserPosition position WITH (NOLOCK)
                                                ON users.PositionId = position.PositionId
                                            INNER JOIN Basic.DepartmentInfo dept WITH (NOLOCK)
                                                ON users.DepartmentId = dept.DepartmentId
                                            LEFT JOIN Basic.UserAgent agent WITH (NOLOCK)
                                                ON users.UserId = agent.SubstituteUserId
                                                AND agent.StartTime <= GETDATE()
                                                AND agent.EndTime >= GETDATE()
                                            LEFT JOIN Basic.UserInfo agentuser WITH (NOLOCK)
                                                ON agent.AgentUserId = agentuser.UserId
                                            LEFT JOIN Dic d1
                                                ON d1.DicCode = @pprimary
                                            LEFT JOIN Dic d2
                                                ON d2.DicCode = @pagent
                                            WHERE
                                                users.IsEmployed = 1
                                                AND users.IsFreeze = 0
                                                AND users.IsApproval = 1
                                                AND dept.DepartmentId IN ({deptInSql})
                                                AND dept.DepartmentLevelId = @pdeptLevelId
                                                AND users.PositionId = @pposId
                                                AND position.SortOrder < @pappUserPosOrder
                                        ),
                                        PartTimeJob AS
                                        (
                                            SELECT
                                                2 AS SortGroup,
                                                users.UserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN users.UserNameCn ELSE users.UserNameEn END AS UserName,
                                                users.HireDate,
                                                CASE 
                                                    WHEN agentuser.UserId IS NULL THEN @pconcurrent
                                                    ELSE @pconcurrentagent
                                                END AS AppointmentType,
                                                CASE 
                                                    WHEN agentuser.UserId IS NULL THEN d3.DicName
                                                    ELSE d4.DicName
                                                END AS AppointmentTypeName,
                                                agentuser.UserId AS AgentUserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn ELSE agentuser.UserNameEn END AS AgentUserName
                                            FROM Basic.UserPartTime part WITH (NOLOCK)
                                            INNER JOIN Basic.UserInfo users WITH (NOLOCK)
                                                ON part.UserId = users.UserId
                                            INNER JOIN Basic.UserPosition position WITH (NOLOCK)
                                                ON part.PartTimePositionId = position.PositionId
                                            INNER JOIN Basic.DepartmentInfo dept WITH (NOLOCK)
                                                ON part.PartTimeDeptId = dept.DepartmentId
                                            LEFT JOIN Basic.UserAgent agent WITH (NOLOCK)
                                                ON users.UserId = agent.SubstituteUserId
                                                AND agent.StartTime <= GETDATE()
                                                AND agent.EndTime >= GETDATE()
                                            LEFT JOIN Basic.UserInfo agentuser WITH (NOLOCK)
                                                ON agent.AgentUserId = agentuser.UserId
                                            LEFT JOIN Dic d3
                                                ON d3.DicCode = @pconcurrent
                                            LEFT JOIN Dic d4
                                                ON d4.DicCode = @pconcurrentagent
                                            WHERE
                                                users.IsEmployed = 1
                                                AND users.IsFreeze = 0
                                                AND users.IsApproval = 1
                                                AND dept.DepartmentId IN ({deptInSql})
                                                AND dept.DepartmentLevelId = @pdeptLevelId
                                                AND part.PartTimePositionId = @pposId
                                                AND position.SortOrder < @pappUserPosOrder
                                        )
                                        SELECT
                                            t.UserId,
                                            t.UserName,
                                            t.AppointmentType,
                                            t.AppointmentTypeName,
                                            t.AgentUserId,
                                            t.AgentUserName
                                        FROM
                                        (
                                            SELECT * FROM MainJob
                                            UNION ALL
                                            SELECT * FROM PartTimeJob
                                        ) t
                                        ORDER BY
                                            t.SortGroup ASC,
                                            t.HireDate ASC;";
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

            for (int i = 0; i < parentDeptIds.Count; i++)
            {
                candidateUserPar.Add(new SugarParameter($"@dept{i}", parentDeptIds[i]));
            }

            // 查询符合条件审批人候选人，注本、代、兼、兼代并按照入职时间正序排序
            var candidateList = await _db.Ado.SqlQueryAsync<StepApproveUser>(candidateUserSql, candidateUserPar);
            if (candidateList.Count() > 0)
            {
                // 按照本、代、兼、兼代的顺序筛选最终审批人
                finalApproveUser = await GetFinalStepApproveUserList(candidateList, approveMode, "PirCon");
            }

            // 如果没有符合条件的审批人，则查询本部门及以上的符合条件审批人（自动指派）
            else
            {
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
                        #region 查询高阶审批人候选人，注本、代、兼、兼代的标识并按照职级倒序、入职时间正序排序
                        var highCandidateUserSql = $@";WITH Dic AS
                                                        (
                                                            SELECT
                                                                DicCode,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                                    ELSE DicNameEn
                                                                END AS DicName
                                                            FROM Basic.DictionaryInfo WITH (NOLOCK)
                                                            WHERE DicType = 'AppointmentType'
                                                        ),
                                                        MainJob AS
                                                        (
                                                            SELECT
                                                                1 AS SortGroup,
                                                                users.UserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN users.UserNameCn
                                                                    ELSE users.UserNameEn
                                                                END AS UserName,
                                                                position.SortOrder,
                                                                users.HireDate,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN @pautoprimary
                                                                    ELSE @pautoagent
                                                                END AS AppointmentType,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN d1.DicName
                                                                    ELSE d2.DicName
                                                                END AS AppointmentTypeName,
                                                                agentuser.UserId AS AgentUserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn
                                                                    ELSE agentuser.UserNameEn
                                                                END AS AgentUserName
                                                            FROM Basic.UserInfo users WITH (NOLOCK)
                                                            INNER JOIN Basic.UserPosition position
                                                                ON users.PositionId = position.PositionId
                                                            INNER JOIN Basic.DepartmentInfo dept
                                                                ON users.DepartmentId = dept.DepartmentId
                                                            INNER JOIN Basic.DepartmentLevel deptlevel
                                                                ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                                                            LEFT JOIN Basic.UserAgent agent
                                                                ON users.UserId = agent.SubstituteUserId
                                                               AND agent.StartTime <= GETDATE()
                                                               AND agent.EndTime >= GETDATE()
                                                            LEFT JOIN Basic.UserInfo agentuser
                                                                ON agent.AgentUserId = agentuser.UserId
                                                            LEFT JOIN Dic d1
                                                                ON d1.DicCode = @pautoprimary
                                                            LEFT JOIN Dic d2
                                                                ON d2.DicCode = @pautoagent
                                                            WHERE users.IsEmployed = 1
                                                              AND users.IsFreeze = 0
                                                              AND users.IsApproval = 1
                                                              AND dept.DepartmentId IN ({deptInSql})
                                                              AND deptlevel.SortOrder = @pdeptlevelOrder
                                                              AND position.SortOrder = @pposOrder
                                                              AND position.SortOrder < @pappUserPosOrder
                                                        ),
                                                        PartTimeJob AS
                                                        (
                                                            SELECT
                                                                2 AS SortGroup,
                                                                users.UserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN users.UserNameCn
                                                                    ELSE users.UserNameEn
                                                                END AS UserName,
                                                                position.SortOrder,
                                                                users.HireDate,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN @pautoconcurrent
                                                                    ELSE @pautoconcurrentagent
                                                                END AS AppointmentType,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN d3.DicName
                                                                    ELSE d4.DicName
                                                                END AS AppointmentTypeName,
                                                                agentuser.UserId AS AgentUserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn
                                                                    ELSE agentuser.UserNameEn
                                                                END AS AgentUserName
                                                            FROM Basic.UserPartTime part WITH (NOLOCK)
                                                            INNER JOIN Basic.UserInfo users
                                                                ON part.UserId = users.UserId
                                                            INNER JOIN Basic.UserPosition position
                                                                ON part.PartTimePositionId = position.PositionId
                                                            INNER JOIN Basic.DepartmentInfo dept
                                                                ON part.PartTimeDeptId = dept.DepartmentId
                                                            INNER JOIN Basic.DepartmentLevel deptlevel
                                                                ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                                                            LEFT JOIN Basic.UserAgent agent
                                                                ON users.UserId = agent.SubstituteUserId
                                                               AND agent.StartTime <= GETDATE()
                                                               AND agent.EndTime >= GETDATE()
                                                            LEFT JOIN Basic.UserInfo agentuser
                                                                ON agent.AgentUserId = agentuser.UserId
                                                            LEFT JOIN Dic d3
                                                                ON d3.DicCode = @pautoconcurrent
                                                            LEFT JOIN Dic d4
                                                                ON d4.DicCode = @pautoconcurrentagent
                                                            WHERE users.IsEmployed = 1
                                                              AND users.IsFreeze = 0
                                                              AND users.IsApproval = 1
                                                              AND dept.DepartmentId IN ({deptInSql})
                                                              AND deptlevel.SortOrder = @pdeptlevelOrder
                                                              AND position.SortOrder = @pposOrder
                                                              AND position.SortOrder < @pappUserPosOrder
                                                        )
                                                        SELECT
                                                            t.UserId,
                                                            t.UserName,
                                                            t.AppointmentType,
                                                            t.AppointmentTypeName,
                                                            t.AgentUserId,
                                                            t.AgentUserName
                                                        FROM
                                                        (
                                                            SELECT * FROM MainJob
                                                            UNION ALL
                                                            SELECT * FROM PartTimeJob
                                                        ) t
                                                        ORDER BY
                                                            t.SortGroup ASC,
                                                            t.SortOrder DESC,
                                                            t.HireDate ASC;";
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
                            // 按照本、代、兼、兼代的顺序筛选最终审批人
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
        /// 查询步骤依照指定部门、职级
        /// </summary>
        /// <param name="approveMode"></param>
        /// <param name="deptId"></param>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public async Task<List<StepApproveUser>> GetStepApproveUserByDeptUser(string approveMode, long deptId, long positionId)
        {
            // 步骤最终审批人列表
            var finalApproveUser = new List<StepApproveUser>();

            // 指定部门信息
            var deptInfo = await _db.Queryable<DepartmentInfoEntity>().Where(dept => dept.DepartmentId == deptId).FirstAsync();

            #region 查询符合步骤条件的签核人，带有注本、代、兼、兼代的标识并按照入职时间正序排序
            var candidateUserSql = @";WITH Dic AS
                                        (
                                            SELECT
                                                DicCode,
                                                CASE 
                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                    ELSE DicNameEn
                                                END AS DicName
                                            FROM Basic.DictionaryInfo WITH (NOLOCK)
                                            WHERE DicType = 'AppointmentType'
                                        ),
                                        MainJob AS
                                        (
                                            SELECT
                                                1 AS SortGroup,
                                                users.UserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN users.UserNameCn ELSE users.UserNameEn END AS UserName,
                                                users.HireDate,
                                                CASE 
                                                    WHEN agentuser.UserId IS NULL THEN @pprimary
                                                    ELSE @pagent
                                                END AS AppointmentType,
                                                CASE 
                                                    WHEN agentuser.UserId IS NULL THEN d1.DicName
                                                    ELSE d2.DicName
                                                END AS AppointmentTypeName,
                                                agentuser.UserId AS AgentUserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn ELSE agentuser.UserNameEn END AS AgentUserName
                                            FROM Basic.UserInfo users WITH (NOLOCK)
                                            INNER JOIN Basic.UserPosition position 
                                                ON users.PositionId = position.PositionId
                                            INNER JOIN Basic.DepartmentInfo dept 
                                                ON users.DepartmentId = dept.DepartmentId
                                            LEFT JOIN Basic.UserAgent agent 
                                                ON users.UserId = agent.SubstituteUserId
                                               AND agent.StartTime <= GETDATE()
                                               AND agent.EndTime >= GETDATE()
                                            LEFT JOIN Basic.UserInfo agentuser 
                                                ON agent.AgentUserId = agentuser.UserId
                                            LEFT JOIN Dic d1 ON d1.DicCode = @pprimary
                                            LEFT JOIN Dic d2 ON d2.DicCode = @pagent
                                            WHERE
                                                users.IsEmployed = 1
                                                AND users.IsFreeze = 0
                                                AND users.IsApproval = 1
                                                AND dept.DepartmentId = @pdeptId
                                                AND users.PositionId = @pposId
                                        ),
                                        PartTimeJob AS
                                        (
                                            SELECT
                                                2 AS SortGroup,
                                                users.UserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN users.UserNameCn ELSE users.UserNameEn END AS UserName,
                                                users.HireDate,
                                                CASE 
                                                    WHEN agentuser.UserId IS NULL THEN @pconcurrent
                                                    ELSE @pconcurrentagent
                                                END AS AppointmentType,
                                                CASE 
                                                    WHEN agentuser.UserId IS NULL THEN d3.DicName
                                                    ELSE d4.DicName
                                                END AS AppointmentTypeName,
                                                agentuser.UserId AS AgentUserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn ELSE agentuser.UserNameEn END AS AgentUserName
                                            FROM Basic.UserPartTime part WITH (NOLOCK)
                                            INNER JOIN Basic.UserInfo users 
                                                ON part.UserId = users.UserId
                                            INNER JOIN Basic.UserPosition position 
                                                ON part.PartTimePositionId = position.PositionId
                                            INNER JOIN Basic.DepartmentInfo dept 
                                                ON part.PartTimeDeptId = dept.DepartmentId
                                            LEFT JOIN Basic.UserAgent agent 
                                                ON users.UserId = agent.SubstituteUserId
                                               AND agent.StartTime <= GETDATE()
                                               AND agent.EndTime >= GETDATE()
                                            LEFT JOIN Basic.UserInfo agentuser 
                                                ON agent.AgentUserId = agentuser.UserId
                                            LEFT JOIN Dic d3 ON d3.DicCode = @pconcurrent
                                            LEFT JOIN Dic d4 ON d4.DicCode = @pconcurrentagent
                                            WHERE
                                                users.IsEmployed = 1
                                                AND users.IsFreeze = 0
                                                AND users.IsApproval = 1
                                                AND dept.DepartmentId = @pdeptId
                                                AND part.PartTimePositionId = @pposId
                                        )
                                        SELECT
                                            t.UserId,
                                            t.UserName,
                                            t.AppointmentType,
                                            t.AppointmentTypeName,
                                            t.AgentUserId,
                                            t.AgentUserName
                                        FROM
                                        (
                                            SELECT * FROM MainJob
                                            UNION ALL
                                            SELECT * FROM PartTimeJob
                                        ) t
                                        ORDER BY
                                            t.SortGroup ASC,
                                            t.HireDate ASC;";
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

            // 查询符合条件审批人候选人，注本、代、兼、兼代并按照入职时间正序排序
            var candidateList = await _db.Ado.SqlQueryAsync<StepApproveUser>(candidateUserSql, candidateUserPar);
            if (candidateList.Count() > 0)
            {
                // 按照本、代、兼、兼代的顺序筛选最终审批人
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
                var deptInSql = string.Join(", ", parentDeptIds);

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
                        #region 查询高阶审批人候选人，注本、代、兼、兼代并按照职级倒序、入职时间正序排序
                        var highCandidateUserSql = $@";WITH Dic AS
                                                        (
                                                            SELECT
                                                                DicCode,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                                    ELSE DicNameEn
                                                                END AS DicName
                                                            FROM Basic.DictionaryInfo WITH (NOLOCK)
                                                            WHERE DicType = 'AppointmentType'
                                                        ),
                                                        MainJob AS
                                                        (
                                                            SELECT
                                                                1 AS SortGroup,
                                                                users.UserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN users.UserNameCn
                                                                    ELSE users.UserNameEn
                                                                END AS UserName,
                                                                position.SortOrder,
                                                                users.HireDate,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN @pautoprimary
                                                                    ELSE @pautoagent
                                                                END AS AppointmentType,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN d1.DicName
                                                                    ELSE d2.DicName
                                                                END AS AppointmentTypeName,
                                                                agentuser.UserId AS AgentUserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn
                                                                    ELSE agentuser.UserNameEn
                                                                END AS AgentUserName
                                                            FROM Basic.UserInfo users WITH (NOLOCK)
                                                            INNER JOIN Basic.UserPosition position
                                                                ON users.PositionId = position.PositionId
                                                            INNER JOIN Basic.DepartmentInfo dept
                                                                ON users.DepartmentId = dept.DepartmentId
                                                            INNER JOIN Basic.DepartmentLevel deptlevel
                                                                ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                                                            LEFT JOIN Basic.UserAgent agent
                                                                ON users.UserId = agent.SubstituteUserId
                                                               AND agent.StartTime <= GETDATE()
                                                               AND agent.EndTime >= GETDATE()
                                                            LEFT JOIN Basic.UserInfo agentuser
                                                                ON agent.AgentUserId = agentuser.UserId
                                                            LEFT JOIN Dic d1
                                                                ON d1.DicCode = @pautoprimary
                                                            LEFT JOIN Dic d2
                                                                ON d2.DicCode = @pautoagent
                                                            WHERE
                                                                users.IsEmployed = 1
                                                                AND users.IsFreeze = 0
                                                                AND users.IsApproval = 1
                                                                AND dept.DepartmentId IN ({deptInSql})
                                                                AND deptlevel.SortOrder = @pdeptlevelOrder
                                                                AND position.SortOrder = @pposOrder
                                                        ),
                                                        PartTimeJob AS
                                                        (
                                                            SELECT
                                                                2 AS SortGroup,
                                                                users.UserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN users.UserNameCn
                                                                    ELSE users.UserNameEn
                                                                END AS UserName,
                                                                position.SortOrder,
                                                                users.HireDate,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN @pautoconcurrent
                                                                    ELSE @pautoconcurrentagent
                                                                END AS AppointmentType,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN d3.DicName
                                                                    ELSE d4.DicName
                                                                END AS AppointmentTypeName,
                                                                agentuser.UserId AS AgentUserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn
                                                                    ELSE agentuser.UserNameEn
                                                                END AS AgentUserName
                                                            FROM Basic.UserPartTime part WITH (NOLOCK)
                                                            INNER JOIN Basic.UserInfo users
                                                                ON part.UserId = users.UserId
                                                            INNER JOIN Basic.UserPosition position
                                                                ON part.PartTimePositionId = position.PositionId
                                                            INNER JOIN Basic.DepartmentInfo dept
                                                                ON part.PartTimeDeptId = dept.DepartmentId
                                                            INNER JOIN Basic.DepartmentLevel deptlevel
                                                                ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                                                            LEFT JOIN Basic.UserAgent agent
                                                                ON users.UserId = agent.SubstituteUserId
                                                               AND agent.StartTime <= GETDATE()
                                                               AND agent.EndTime >= GETDATE()
                                                            LEFT JOIN Basic.UserInfo agentuser
                                                                ON agent.AgentUserId = agentuser.UserId
                                                            LEFT JOIN Dic d3
                                                                ON d3.DicCode = @pautoconcurrent
                                                            LEFT JOIN Dic d4
                                                                ON d4.DicCode = @pautoconcurrentagent
                                                            WHERE
                                                                users.IsEmployed = 1
                                                                AND users.IsFreeze = 0
                                                                AND users.IsApproval = 1
                                                                AND dept.DepartmentId IN ({deptInSql})
                                                                AND deptlevel.SortOrder = @pdeptlevelOrder
                                                                AND position.SortOrder = @pposOrder
                                                        )
                                                        SELECT
                                                            t.UserId,
                                                            t.UserName,
                                                            t.AppointmentType,
                                                            t.AppointmentTypeName,
                                                            t.AgentUserId,
                                                            t.AgentUserName
                                                        FROM
                                                        (
                                                            SELECT * FROM MainJob
                                                            UNION ALL
                                                            SELECT * FROM PartTimeJob
                                                        ) t
                                                        ORDER BY
                                                            t.SortGroup ASC,
                                                            t.SortOrder DESC,
                                                            t.HireDate ASC;";
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
                            // 按照本、代、兼、兼代的顺序筛选最终审批人
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
        /// 查询步骤依照指定员工
        /// </summary>
        /// <param name="approveMode"></param>
        /// <param name="stepUserId"></param>
        /// <returns></returns>
        public async Task<List<StepApproveUser>> GetStepApproveUserByUser(string approveMode, long stepUserId)
        {
            // 步骤最终审批人列表
            var finalApproveUser = new List<StepApproveUser>();

            // 申请人信息
            var stepUser = await _db.Queryable<UserInfoEntity>().Where(user => user.UserId == stepUserId).FirstAsync();

            #region 查询符合步骤条件的签核人，带有注本、代、兼、兼代并按照入职时间正序排序
            var candidateUserSql = @";WITH Dic AS
                                        (
                                            SELECT
                                                DicCode,
                                                CASE
                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                    ELSE DicNameEn
                                                END AS DicName
                                            FROM Basic.DictionaryInfo WITH (NOLOCK)
                                            WHERE DicType = 'AppointmentType'
                                        ),
                                        MainJob AS
                                        (
                                            SELECT
                                                1 AS SortGroup,
                                                users.UserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN users.UserNameCn ELSE users.UserNameEn END AS UserName,
                                                users.HireDate,
                                                CASE
                                                    WHEN agentuser.UserId IS NULL THEN @pprimary
                                                    ELSE @pagent
                                                END AS AppointmentType,
                                                CASE
                                                    WHEN agentuser.UserId IS NULL THEN d1.DicName
                                                    ELSE d2.DicName
                                                END AS AppointmentTypeName,
                                                agentuser.UserId AS AgentUserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn ELSE agentuser.UserNameEn END AS AgentUserName
                                            FROM Basic.UserInfo users WITH (NOLOCK)
                                            INNER JOIN Basic.UserPosition position
                                                ON users.PositionId = position.PositionId
                                            INNER JOIN Basic.DepartmentInfo dept
                                                ON users.DepartmentId = dept.DepartmentId
                                            LEFT JOIN Basic.UserAgent agent
                                                ON users.UserId = agent.SubstituteUserId
                                               AND agent.StartTime <= GETDATE()
                                               AND agent.EndTime >= GETDATE()
                                            LEFT JOIN Basic.UserInfo agentuser
                                                ON agent.AgentUserId = agentuser.UserId
                                            LEFT JOIN Dic d1
                                                ON d1.DicCode = @pprimary
                                            LEFT JOIN Dic d2
                                                ON d2.DicCode = @pagent
                                            WHERE
                                                users.IsEmployed = 1
                                                AND users.IsFreeze = 0
                                                AND users.IsApproval = 1
                                                AND users.UserId = @puserId
                                        ),
                                        PartTimeJob AS
                                        (
                                            SELECT
                                                2 AS SortGroup,
                                                users.UserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN users.UserNameCn ELSE users.UserNameEn END AS UserName,
                                                users.HireDate,
                                                CASE
                                                    WHEN agentuser.UserId IS NULL THEN @pconcurrent
                                                    ELSE @pconcurrentagent
                                                END AS AppointmentType,
                                                CASE
                                                    WHEN agentuser.UserId IS NULL THEN d3.DicName
                                                    ELSE d4.DicName
                                                END AS AppointmentTypeName,
                                                agentuser.UserId AS AgentUserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn ELSE agentuser.UserNameEn END AS AgentUserName
                                            FROM Basic.UserPartTime part WITH (NOLOCK)
                                            INNER JOIN Basic.UserInfo users
                                                ON part.UserId = users.UserId
                                            INNER JOIN Basic.UserPosition position
                                                ON part.PartTimePositionId = position.PositionId
                                            INNER JOIN Basic.DepartmentInfo dept
                                                ON part.PartTimeDeptId = dept.DepartmentId
                                            LEFT JOIN Basic.UserAgent agent
                                                ON users.UserId = agent.SubstituteUserId
                                               AND agent.StartTime <= GETDATE()
                                               AND agent.EndTime >= GETDATE()
                                            LEFT JOIN Basic.UserInfo agentuser
                                                ON agent.AgentUserId = agentuser.UserId
                                            LEFT JOIN Dic d3
                                                ON d3.DicCode = @pconcurrent
                                            LEFT JOIN Dic d4
                                                ON d4.DicCode = @pconcurrentagent
                                            WHERE
                                                users.IsEmployed = 1
                                                AND users.IsFreeze = 0
                                                AND users.IsApproval = 1
                                                AND part.UserId = @puserId
                                        )
                                        SELECT
                                            t.UserId,
                                            t.UserName,
                                            t.AppointmentType,
                                            t.AppointmentTypeName,
                                            t.AgentUserId,
                                            t.AgentUserName
                                        FROM
                                        (
                                            SELECT * FROM MainJob
                                            UNION ALL
                                            SELECT * FROM PartTimeJob
                                        ) t
                                        ORDER BY
                                            t.SortGroup ASC,
                                            t.HireDate ASC;";
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

            // 查询符合条件审批人候选人，注本、代、兼、兼代并按照入职时间正序排序
            var candidateList = await _db.Ado.SqlQueryAsync<StepApproveUser>(candidateUserSql, candidateUserPar);
            if (candidateList.Count() > 0)
            {
                // 按照本、代、兼、兼代的顺序筛选最终审批人
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
                var deptInSql = string.Join(", ", parentDeptIds);

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
                        #region 查询高阶审批人候选人，注本、代、兼、兼代并按照职级倒序、入职时间正序排序
                        var highCandidateUserSql = $@";WITH Dic AS
                                                        (
                                                            SELECT
                                                                DicCode,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                                    ELSE DicNameEn
                                                                END AS DicName
                                                            FROM Basic.DictionaryInfo WITH (NOLOCK)
                                                            WHERE DicType = 'AppointmentType'
                                                        ),
                                                        MainJob AS
                                                        (
                                                            SELECT
                                                                1 AS SortGroup,
                                                                users.UserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN users.UserNameCn
                                                                    ELSE users.UserNameEn
                                                                END AS UserName,
                                                                position.SortOrder,
                                                                users.HireDate,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN @pautoprimary
                                                                    ELSE @pautoagent
                                                                END AS AppointmentType,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN d1.DicName
                                                                    ELSE d2.DicName
                                                                END AS AppointmentTypeName,
                                                                agentuser.UserId AS AgentUserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn
                                                                    ELSE agentuser.UserNameEn
                                                                END AS AgentUserName
                                                            FROM Basic.UserInfo users WITH (NOLOCK)
                                                            INNER JOIN Basic.UserPosition position
                                                                ON users.PositionId = position.PositionId
                                                            INNER JOIN Basic.DepartmentInfo dept
                                                                ON users.DepartmentId = dept.DepartmentId
                                                            INNER JOIN Basic.DepartmentLevel deptlevel
                                                                ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                                                            LEFT JOIN Basic.UserAgent agent
                                                                ON users.UserId = agent.SubstituteUserId
                                                               AND agent.StartTime <= GETDATE()
                                                               AND agent.EndTime >= GETDATE()
                                                            LEFT JOIN Basic.UserInfo agentuser
                                                                ON agent.AgentUserId = agentuser.UserId
                                                            LEFT JOIN Dic d1
                                                                ON d1.DicCode = @pautoprimary
                                                            LEFT JOIN Dic d2
                                                                ON d2.DicCode = @pautoagent
                                                            WHERE
                                                                users.IsEmployed = 1
                                                                AND users.IsFreeze = 0
                                                                AND users.IsApproval = 1
                                                                AND dept.DepartmentId IN ({deptInSql})
                                                                AND deptlevel.SortOrder = @pdeptlevelOrder
                                                                AND position.SortOrder = @pposOrder
                                                                AND position.SortOrder < @pstepUserPosOrder
                                                        ),
                                                        PartTimeJob AS
                                                        (
                                                            SELECT
                                                                2 AS SortGroup,
                                                                users.UserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN users.UserNameCn
                                                                    ELSE users.UserNameEn
                                                                END AS UserName,
                                                                position.SortOrder,
                                                                users.HireDate,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN @pautoconcurrent
                                                                    ELSE @pautoconcurrentagent
                                                                END AS AppointmentType,
                                                                CASE
                                                                    WHEN agentuser.UserId IS NULL THEN d3.DicName
                                                                    ELSE d4.DicName
                                                                END AS AppointmentTypeName,
                                                                agentuser.UserId AS AgentUserId,
                                                                CASE
                                                                    WHEN @plocale = 'zh-CN' THEN agentuser.UserNameCn
                                                                    ELSE agentuser.UserNameEn
                                                                END AS AgentUserName
                                                            FROM Basic.UserPartTime part WITH (NOLOCK)
                                                            INNER JOIN Basic.UserInfo users
                                                                ON part.UserId = users.UserId
                                                            INNER JOIN Basic.UserPosition position
                                                                ON part.PartTimePositionId = position.PositionId
                                                            INNER JOIN Basic.DepartmentInfo dept
                                                                ON part.PartTimeDeptId = dept.DepartmentId
                                                            INNER JOIN Basic.DepartmentLevel deptlevel
                                                                ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                                                            LEFT JOIN Basic.UserAgent agent
                                                                ON users.UserId = agent.SubstituteUserId
                                                               AND agent.StartTime <= GETDATE()
                                                               AND agent.EndTime >= GETDATE()
                                                            LEFT JOIN Basic.UserInfo agentuser
                                                                ON agent.AgentUserId = agentuser.UserId
                                                            LEFT JOIN Dic d3
                                                                ON d3.DicCode = @pautoconcurrent
                                                            LEFT JOIN Dic d4
                                                                ON d4.DicCode = @pautoconcurrentagent
                                                            WHERE
                                                                users.IsEmployed = 1
                                                                AND users.IsFreeze = 0
                                                                AND users.IsApproval = 1
                                                                AND dept.DepartmentId IN ({deptInSql})
                                                                AND deptlevel.SortOrder = @pdeptlevelOrder
                                                                AND position.SortOrder = @pposOrder
                                                                AND position.SortOrder < @pstepUserPosOrder
                                                        )
                                                        SELECT
                                                            t.UserId,
                                                            t.UserName,
                                                            t.AppointmentType,
                                                            t.AppointmentTypeName,
                                                            t.AgentUserId,
                                                            t.AgentUserName
                                                        FROM
                                                        (
                                                            SELECT * FROM MainJob
                                                            UNION ALL
                                                            SELECT * FROM PartTimeJob
                                                        ) t
                                                        ORDER BY
                                                            t.SortGroup ASC,
                                                            t.SortOrder DESC,
                                                            t.HireDate ASC;";
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
                            // 按照本、代、兼、兼代的顺序筛选最终审批人
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
            List<StepApproveUser> finalApproveUserList = new();

            string appTypePrimary = seekType == "PirCon"
                ? AppointmentType.Primary.ToEnumString()
                : AppointmentType.AutoPrimary.ToEnumString();

            string appTypeAgent = seekType == "PirCon"
                ? AppointmentType.Agent.ToEnumString()
                : AppointmentType.AutoAgent.ToEnumString();

            string appTypeConcurrent = seekType == "PirCon"
                ? AppointmentType.Concurrent.ToEnumString()
                : AppointmentType.AutoConcurrent.ToEnumString();

            string appTypeConcurrentAgent = seekType == "PirCon"
                ? AppointmentType.ConcurrentAgent.ToEnumString()
                : AppointmentType.AutoConcurrentAgent.ToEnumString();

            if (approveMode == ApproveMode.Single.ToEnumString())
            {
                var priorityOrder = new[]
                {
                    appTypePrimary,           // 实
                    appTypeAgent,             // 代
                    appTypeConcurrent,        // 兼
                    appTypeConcurrentAgent    // 兼代
                };

                foreach (var appointmentType in priorityOrder)
                {
                    var matchedUsers = stepApproveUser.Where(user => user.AppointmentType == appointmentType).ToList();

                    if (matchedUsers.Count > 0)
                    {
                        finalApproveUserList = matchedUsers;
                        break;
                    }
                }
            }
            else if (approveMode == ApproveMode.AndSingle.ToEnumString() || approveMode == ApproveMode.OrSingle.ToEnumString())
            {
                finalApproveUserList = stepApproveUser;
            }

            return finalApproveUserList;
        }

        /// <summary>
        /// 条件方法表达式结果
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        private async Task<bool> EvaluateExpression(string expression, long formId)
        {
            expression = Regex.Replace(expression, @"\s+", "");

            // 拆方法
            var tokens = Regex.Split(expression, @"&&|\|\|");

            // 提取操作符
            var operators = Regex.Matches(expression, @"&&|\|\|")
                                 .Select(m => m.Value)
                                 .ToList();

            if (tokens.Length == 0)
                return false;

            // 第一个方法
            bool result = await InvokeMethod(tokens[0], formId);

            for (int i = 1; i < tokens.Length; i++)
            {
                string op = operators[i - 1];

                // ✅ 短路逻辑（关键优化）
                if (op == "&&" && !result)
                    return false;

                if (op == "||" && result)
                    return true;

                bool next = await InvokeMethod(tokens[i], formId);

                if (op == "&&")
                    result = result && next;
                else
                    result = result || next;
            }

            return result;
        }

        /// <summary>
        /// 表达式调用方法
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<bool> InvokeMethod(string methodName, long formId)
        {
            var method = _workflowConditionFun.GetType().GetMethod(methodName);

            if (method == null)
                throw new Exception($"找不到方法: {methodName}");

            var resultObj = method.Invoke(_workflowConditionFun, new object[] { formId });

            if (resultObj is Task<bool> task)
                return await task;

            throw new Exception($"方法 {methodName} 必须返回 Task<bool>");
        }
    }
}
