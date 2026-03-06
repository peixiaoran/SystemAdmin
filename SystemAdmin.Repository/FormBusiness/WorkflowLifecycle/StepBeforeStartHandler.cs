using Dm.util;
using SqlSugar;
using System.Collections;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormAudit.Entity;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.WorkflowLifecycle;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Entity;

namespace SystemAdmin.Repository.FormBusiness.WorkflowLifecycle
{
    /// <summary>
    /// 审批步骤启动处理器
    /// </summary>
    public class StepBeforeStartHandler
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;
        //private readonly string _this = "FormBusiness.WorkflowLifecycle.StepBeforeStart";

        public StepBeforeStartHandler(SqlSugarScope db, Language lang)
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
            // 查询表单类型基础信息
            var formTypeInfo = await _db.Queryable<FormTypeEntity>()
                                        .Where(formtype => formtype.FormTypeId == formTypeId)
                                        .FirstAsync();

            var nowTime = DateTime.Now;
            var ym = nowTime.ToString("yyMM");
            int seq = 0;
            // 查当月记录
            var stat = await _db.Queryable<FormCountingEntity>()
                                .Where(formmonth => formmonth.FormTypeId == formTypeInfo.FormTypeId && formmonth.YM == ym)
                                .FirstAsync();
            if (stat == null)
            {
                seq = 1;
                await _db.Insertable(new FormCountingEntity
                {
                    FormTypeId = formTypeInfo.FormTypeId,
                    YM = ym,
                    Total = seq,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                }).ExecuteCommandAsync();
            }
            else
            {
                seq = stat.Total + 1;
                await _db.Updateable<FormCountingEntity>()
                         .SetColumns(formmonth => formmonth.Total == seq)
                         .Where(formmonth => formmonth.FormTypeId == formTypeInfo.FormTypeId && formmonth.YM == ym)
                         .ExecuteCommandAsync();
            }
            return $"{formTypeInfo.Prefix}-{ym}{seq.ToString("D" + 4)}";
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
        /// 查询流程开始步骤Id
        /// </summary>
        /// <returns></returns>
        public async Task<long> GetWorkFlowIsStartStepId(long formTypeId)
        {
            return await _db.Queryable<WorkflowStepEntity>()
                            .With(SqlWith.NoLock)
                            .Where(step => step.FormTypeId == formTypeId && step.IsStartStep == 1)
                            .Select(step => step.StepId)
                            .FirstAsync();
        }

        /// <summary>
        /// 查询表单需要签核审批人
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<List<WorkflowApproveUser>> GetWorkflowAllApproveUser(long formId)
        {
            List<WorkflowApproveUser> approveList = new List<WorkflowApproveUser>();

            // 查询表单申请人信息
            var appUserInfo = await _db.Queryable<FormInfoEntity>()
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
                                     .InnerJoin<FormInfoEntity>((step, form) => step.FormTypeId == form.FormTypeId)
                                     .Where((step, form) => form.FormId == formId && step.IsStartStep == 1)
                                     .Select((step, form) => step.StepId)
                                     .FirstAsync();
            while (nowStepId > -1)
            {
                var nowStep = await _db.Queryable<WorkflowStepEntity>()
                                       .With(SqlWith.NoLock)
                                       .Where(step => step.StepId == nowStepId)
                                       .FirstAsync();
                // 组织架构
                if (nowStep.Assignment == Assignment.Org.ToEnumString())
                {
                    if (nowStep.IsStartStep == 1)
                    {
                        WorkflowApproveUser approveItem = new WorkflowApproveUser();

                        // 查询签核类型（本、兼、代、自动指派）
                        var appTypePrimary = await _db.Queryable<DictionaryInfoEntity>()
                                                      .With(SqlWith.NoLock)
                                                      .Where(app => app.DicType == "AppointmentType" && app.DicCode == "Primary")
                                                      .FirstAsync();

                        List<StepApproveUser> stepApproveUsers = new List<StepApproveUser>();
                        stepApproveUsers.Add(new StepApproveUser
                        {
                            UserId = appUserInfo.UserId,
                            UserName = _lang.Locale == "zh-CN"
                                              ? appUserInfo.UserNameCn
                                              : appUserInfo.UserNameEn,
                            AppointmentTypeName = _lang.Locale == "zh-CN"
                                              ? appTypePrimary.DicNameCn
                                              : appTypePrimary.DicNameEn
                        });

                        approveItem.StepId = nowStep.StepId;
                        approveItem.StepName = _lang.Locale == "zh-CN"
                                               ? nowStep.StepNameCn
                                               : nowStep.StepNameEn;
                        approveItem.stepApproveUsers = stepApproveUsers;
                        approveList.Add(approveItem);
                    }
                    else
                    {
                        // 申请人所有的上级部门
                        var parentDeptList = await _db.Queryable<DepartmentInfoEntity>()
                                                      .With(SqlWith.NoLock)
                                                      .ToParentListAsync(dept => dept.ParentId, appUserInfo.DepartmentId);

                        // 步骤审批要求（部门级别、职级）
                        var stepOrg = await _db.Queryable<WorkflowStepOrgEntity>()
                                               .With(SqlWith.NoLock)
                                               .Where(org => org.StepId == nowStep.StepId)
                                               .FirstAsync();
                        // 查询此步骤的审批人
                        var orgApproveUser = await GetStepApproveUserByOrg(appUserInfo, nowStep.ApproveMode, parentDeptList, stepOrg.DeptLeaveId, stepOrg.PositionId);

                        WorkflowApproveUser approveItem = new WorkflowApproveUser();
                        approveItem.StepId = nowStep.StepId;
                        approveItem.StepName = _lang.Locale == "zh-CN"
                                               ? nowStep.StepNameCn
                                               : nowStep.StepNameEn;
                        approveItem.stepApproveUsers = orgApproveUser;
                        approveList.Add(approveItem);
                    }
                }

                // 查找下一个步骤
                var stepCondition = await _db.Queryable<WorkflowStepConditionEntity>()
                                             .With(SqlWith.NoLock)
                                             .Where(condition => condition.StepId == nowStep.StepId)
                                             .ToListAsync();

                List<long> conditionIds = new List<long>();
                foreach (var conItem in stepCondition)
                {
                    if (conItem.ConditionId == -1)
                    {
                        conditionIds.Add(conItem.ConditionId);
                    }
                }

                // 赋值下一个步骤Id
                nowStepId = stepCondition.Where(condition => conditionIds.Contains(condition.ConditionId) && condition.ExecuteMatched == 1).First().NextStepId;
            }
            return approveList;
        }

        /// <summary>
        /// 查询组织架构步骤的审批人
        /// </summary>
        /// <param name="appUserEntity"></param>
        /// <param name="approveMode"></param>
        /// <param name="parentDeptList"></param>
        /// <param name="deptLevelId"></param>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public async Task<List<StepApproveUser>> GetStepApproveUserByOrg(UserInfoEntity appUserEntity, string approveMode, List<DepartmentInfoEntity> parentDeptList, long deptLevelId, long positionId)
        {
            // 步骤最终审批人列表
            var finalApprovers = new List<StepApproveUser>();

            // 申请人职级信息
            var appPosSortOrder = await _db.Queryable<UserPositionEntity>().Where(position => position.PositionId == appUserEntity.PositionId).FirstAsync();

            // 申请人所有上级部门Ids
            var parentDeptIds = parentDeptList.Select(dept => dept.DepartmentId).ToList();

            // 查询符合步骤条件的签核人，带有注本、兼、代的标识并按照入职时间正序排序
            var appPirConUserSql = @"SELECT
                                            t.UserId,
                                            t.UserName,
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
                                                        (SELECT TOP 1 
                                                                CASE WHEN @plocale = 'zh-CN' THEN DicNameCn ELSE DicNameEn END
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = 'Primary')
                                                    ELSE
                                                        (SELECT TOP 1 
                                                                CASE WHEN @plocale = 'zh-CN' THEN DicNameCn ELSE DicNameEn END
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = 'Agent')
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
                                                AND users.IsApproval = 1
                                                AND dept.DepartmentLevelId = @pdeptLevelId
                                                AND users.PositionId = @pposId
                                                AND position.SortOrder < @pappPosSortOrder

                                            UNION ALL

                                            SELECT
                                                users.UserId,
                                                CASE WHEN @plocale = 'zh-CN' THEN users.UserNameCn ELSE users.UserNameEn END AS UserName,
                                                users.HireDate,
                                                CASE
                                                    WHEN agentuser.UserId IS NULL THEN
                                                        (SELECT TOP 1 
                                                                CASE WHEN @plocale = 'zh-CN' THEN DicNameCn ELSE DicNameEn END
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = 'Concurrent')
                                                    ELSE
                                                        (SELECT TOP 1 
                                                                CASE WHEN @plocale = 'zh-CN' THEN DicNameCn ELSE DicNameEn END
                                                         FROM Basic.DictionaryInfo
                                                         WHERE DicType = 'AppointmentType' AND DicCode = 'Concurrent-Agent')
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
                                                AND users.IsApproval = 1
                                                AND dept.DepartmentLevelId = @pdeptLevelId
                                                AND part.PartTimePositionId = @pposId
                                                AND position.SortOrder < @pappPosSortOrder
                                        ) t ORDER BY t.HireDate ASC;";

            var appPirConUserPar = new List<SugarParameter>
            {
                new SugarParameter("@plocale", _lang.Locale),
                new SugarParameter("@pdeptLevelId", deptLevelId),
                new SugarParameter("@pposId", positionId),
                new SugarParameter("@pappPosSortOrder", appPosSortOrder.SortOrder),
            };

            // 查询符合条件审批人
            var approveUser = await _db.Ado.SqlQueryAsync<StepApproveUser>(appPirConUserSql, appPirConUserPar);
            if (approveUser.Count() > 0)
            {
                if (approveMode == ApproveMode.Ss.ToEnumString())
                {
                    finalApprovers = approveUser.Take(1).ToList();
                }
                else if (approveMode == ApproveMode.Cs.ToEnumString())
                {
                    finalApprovers = approveUser;
                }
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
                var stepDeptMaxOrder = await _db.Queryable<DepartmentLevelEntity>()
                                                .With(SqlWith.NoLock)
                                                .Where(dept => dept.DepartmentLevelId == deptLevelId)
                                                .MaxAsync(position => position.SortOrder);
                var stepPosMaxOrder = await _db.Queryable<UserPositionEntity>()
                                               .With(SqlWith.NoLock)
                                               .Where(position => position.PositionId == positionId)
                                               .MaxAsync(position => position.SortOrder);

                List<StepApproveUser> heightLevelApproveUser = new List<StepApproveUser>();

                for (int itemDeptOrder = stepDeptMaxOrder; itemDeptOrder > 0 && itemDeptOrder <= deptLevelMaxOrder;)
                {
                    for (int itemPosOrder = stepPosMaxOrder; itemPosOrder > 0 && itemPosOrder <= posMaxOrder;)
                    {
                        var deptInSql = string.Join(", ", parentDeptIds);

                        // 查询高阶签核人，带有注本、兼、代的标识并按照职级倒序、入职时间正序排序
                        var highAppPirConUserSql = $@"SELECT
                                                            t.UserId,
                                                            t.UserName,
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
                                                                            SELECT TOP 1
                                                                                CASE
                                                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                                                    ELSE DicNameEn
                                                                                END
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = 'Auto-Primary'
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
                                                                              AND DicCode = 'Auto-Agent'
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
                                                              AND users.IsApproval = 1
                                                              AND dept.DepartmentId IN ({deptInSql})
                                                              AND deptlevel.SortOrder = @psortOrder
                                                              AND position.SortOrder < @pappPosSortOrder

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
                                                                            SELECT TOP 1
                                                                                CASE
                                                                                    WHEN @plocale = 'zh-CN' THEN DicNameCn
                                                                                    ELSE DicNameEn
                                                                                END
                                                                            FROM Basic.DictionaryInfo
                                                                            WHERE DicType = 'AppointmentType'
                                                                              AND DicCode = 'Auto-Concurrent'
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
                                                                              AND DicCode = 'Auto-Concurrent-Agent'
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
                                                              AND users.IsApproval = 1
                                                              AND dept.DepartmentId IN ({deptInSql})
                                                              AND deptlevel.SortOrder = @psortOrder
                                                              AND position.SortOrder < @pappPosSortOrder
                                                        ) t ORDER BY SortOrder DESC,t.HireDate ASC;";

                        var highAppPirConUserPar = new List<SugarParameter>
                        {
                            new SugarParameter("@plocale", _lang.Locale),
                            new SugarParameter("@psortOrder", itemDeptOrder),
                            new SugarParameter("@pappPosSortOrder", appPosSortOrder.SortOrder),
                        };
                        for (int i = 0; i < parentDeptIds.Count; i++)
                        {
                            highAppPirConUserPar.Add(new SugarParameter($"@dept{i}", parentDeptIds[i]));
                        }

                        // 查询符合条件审批人
                        var highLevelUser = await _db.Ado.SqlQueryAsync<StepApproveUser>(highAppPirConUserSql, highAppPirConUserPar);
                        if (highLevelUser.Count > 0)
                        {
                            if (approveMode == ApproveMode.Ss.ToEnumString())
                            {
                                heightLevelApproveUser = highLevelUser.Take(1).ToList();
                            }
                            else if (approveMode == ApproveMode.Cs.ToEnumString())
                            {
                                heightLevelApproveUser = highLevelUser;
                            }
                            itemDeptOrder = -1;
                            itemPosOrder = -1;
                        }
                        else
                        {
                            itemPosOrder--;
                        }
                    }
                    if (heightLevelApproveUser.Count > 0)
                    {
                        itemDeptOrder = -1;
                    }
                    else
                    {
                        itemDeptOrder--;
                    }
                }
                finalApprovers.AddRange(heightLevelApproveUser);
            }
            return finalApprovers;
        }
    }
}
