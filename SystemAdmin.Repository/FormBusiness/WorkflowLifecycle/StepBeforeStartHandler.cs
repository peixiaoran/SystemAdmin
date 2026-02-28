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
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness.WorkflowLifecycle.StepBeforeStart";

        public StepBeforeStartHandler(SqlSugarScope db, Language lang, LocalizationService _localizationService)
        {
            _db = db;
            _lang = lang;
            _localization = _localizationService;
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
        /// 重要程度下拉框
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
            List<WorkflowApproveUser> workflowAllApproveUser = new List<WorkflowApproveUser>();

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
                        WorkflowApproveUser workflowApproveItem = new WorkflowApproveUser();

                        List<StepApproveUser> stepApproveUsers = new List<StepApproveUser>();
                        stepApproveUsers.Add(new StepApproveUser
                        {
                            UserId = appUserInfo.UserId,
                            UserName = _lang.Locale == "zh-CN"
                                       ? appUserInfo.UserNameCn
                                       : appUserInfo.UserNameEn,
                            AppointmentType = "",
                            AppointmentTypeName = ""
                        });

                        workflowApproveItem.StepId = nowStep.StepId;
                        workflowApproveItem.StepName = _lang.Locale == "zh-CN"
                                                       ? nowStep.StepNameCn
                                                       : nowStep.StepNameEn;
                        workflowApproveItem.stepApproveUsers = stepApproveUsers;
                        workflowAllApproveUser.Add(workflowApproveItem);
                    }
                    else
                    {
                        // 申请人所有的上级部门
                        var parentDeptTreeList = await _db.Queryable<DepartmentInfoEntity>()
                                                          .With(SqlWith.NoLock)
                                                          .ToParentListAsync(dept => dept.ParentId, appUserInfo.DepartmentId);

                        var stepOrg = await _db.Queryable<WorkflowStepOrgEntity>()
                                               .With(SqlWith.NoLock)
                                               .Where(org => org.StepId == nowStep.StepId)
                                               .FirstAsync();

                        var orgApproveUser = await GetStepApproveUserByOrg(appUserInfo.UserId, appUserInfo.DepartmentId, appUserInfo.PositionId, nowStep.ApproveMode, parentDeptTreeList, stepOrg.DeptLeaveId, stepOrg.PositionIds);

                        WorkflowApproveUser workflowApproveItem = new WorkflowApproveUser();
                        workflowApproveItem.StepId = nowStep.StepId;
                        workflowApproveItem.StepName = _lang.Locale == "zh-CN"
                                                       ? nowStep.StepNameCn
                                                       : nowStep.StepNameEn;
                        workflowApproveItem.stepApproveUsers = orgApproveUser;
                        workflowAllApproveUser.Add(workflowApproveItem);
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
            return workflowAllApproveUser;
        }

        /// <summary>
        /// 查询组织架构步骤的审批人
        /// </summary>
        /// <param name="appUserId"></param>
        /// <param name="appDeptId"></param>
        /// <param name="appPositionId"></param>
        /// <param name="approveMode"></param>
        /// <param name="deptLevelIds"></param>
        /// <param name="userPositionIds"></param>
        /// <returns></returns>
        public async Task<List<StepApproveUser>> GetStepApproveUserByOrg(long appUserId, long appDeptId, long appPositionId, string approveMode, List<DepartmentInfoEntity> parentDeptList, long deptLevelId, string userPositionIds)
        {
            var positionIdSet = userPositionIds
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToHashSet();

            var finalApprovers = new List<StepApproveUser>();

            // 申请人所有上级部门Ids
            var parentDeptIdsList = parentDeptList.Select(dept => dept.DepartmentId).ToList();

            // 查询此步骤符合级别的部门
            var stepParentDeptIdsList = parentDeptList
                .Where(dept => dept.DepartmentLevelId == deptLevelId)
                .Select(dept => dept.DepartmentId)
                .ToList();

            // 查询签核类型（实、兼、代、自动升级）
            var appointmentType = _db.Queryable<DictionaryInfoEntity>()
                                     .With(SqlWith.NoLock)
                                     .Where(appointment => appointment.DicType == "AppointmentType")
                                     .ToList();

            var appTypeIncumbent = appointmentType.Where(app => app.DicCode == "Incumbent").First();
            // 查询实职步骤条件审批人
            var approveIncumbentUsers = await _db.Queryable<UserInfoEntity>()
                                           .InnerJoin<UserPositionEntity>((user, position) => user.PositionId == position.PositionId)
                                           .With(SqlWith.NoLock)
                                           .Where((user, position) => stepParentDeptIdsList.Contains(user.DepartmentId) && positionIdSet.Contains(user.PositionId) && user.IsApproval == 1 && user.PositionId != appPositionId)
                                           .OrderByDescending((user, position) => position.SortOrder)
                                           .Select((user, position) => new StepApproveUser()
                                           {
                                               UserId = user.UserId,
                                               UserName = _lang.Locale == "zh-CN"
                                                          ? user.UserNameCn
                                                          : user.UserNameEn,
                                               PositionSortOrder = position.SortOrder,
                                               AppointmentType = appTypeIncumbent.DicCode,
                                               AppointmentTypeName = _lang.Locale == "zh-CN"
                                                                     ? appTypeIncumbent.DicNameCn
                                                                     : appTypeIncumbent.DicNameEn
                                           }).ToListAsync();
            // 找到实职当中有被代理的人员并替换成代理人
            approveIncumbentUsers = await GetApproveUserAgent(approveIncumbentUsers);

            if (approveIncumbentUsers.Count() > 0)
            {
                if (approveMode == ApproveMode.Ss.ToEnumString())
                {
                    finalApprovers = approveIncumbentUsers.Take(1).ToList();
                }
                else if (approveMode == ApproveMode.Cs.ToEnumString())
                {
                    finalApprovers = approveIncumbentUsers.ToList();
                }
            }
            // 如果没有符合条件的审批人，则查询本部门及以上的符合条件审批人（自动升级）
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
                                               .Where(position => positionIdSet.Contains(position.PositionId))
                                               .MaxAsync(position => position.SortOrder);

                List<StepApproveUser> heightLevelApproveUser = new List<StepApproveUser>();
                for (long itemDeptOrder = stepDeptMaxOrder; itemDeptOrder > 0 && itemDeptOrder <= deptLevelMaxOrder;)
                {
                    for (int itemPosOrder = stepPosMaxOrder; itemPosOrder > 0 && itemPosOrder <= posMaxOrder;)
                    {
                        var highLevelUserInfo = await _db.Queryable<UserInfoEntity>()
                                                         .With(SqlWith.NoLock)
                                                         .InnerJoin<UserPositionEntity>((user, position) => user.PositionId == position.PositionId)
                                                         .InnerJoin<DepartmentInfoEntity>((user, position, dept) => user.DepartmentId == dept.DepartmentId)
                                                         .InnerJoin<DepartmentLevelEntity>((user, position, dept, deptlevel) => dept.DepartmentLevelId == deptlevel.DepartmentLevelId)
                                                         .Where((user, position, dept, deptlevel) => parentDeptIdsList.Contains(dept.DepartmentId) && position.SortOrder == itemPosOrder && deptlevel.SortOrder == itemDeptOrder && position.PositionId != appPositionId && user.IsApproval == 1)
                                                         .OrderByDescending((user, position, dept, deptlevel) => position.SortOrder)
                                                         .Select((user, position, dept, deptlevel) => user)
                                                         .ToListAsync();
                        if (highLevelUserInfo.Count > 0)
                        {
                            var appTypeEscalation = appointmentType.Where(app => app.DicCode == "Auto-Incumbent").First();
                            if (approveMode == ApproveMode.Ss.ToEnumString())
                            {
                                heightLevelApproveUser = highLevelUserInfo
                                    .Select(user => new StepApproveUser
                                    {
                                        UserId = user.UserId,
                                        UserName = _lang.Locale == "zh-CN"
                                                   ? user.UserNameCn
                                                   : user.UserNameEn,
                                        AppointmentType = appTypeEscalation.DicCode,
                                        AppointmentTypeName = _lang.Locale == "zh-CN"
                                                              ? appTypeEscalation.DicNameCn
                                                              : appTypeEscalation.DicNameEn
                                    }).Take(1).ToList();
                            }
                            else if (approveMode == ApproveMode.Cs.ToEnumString())
                            {
                                heightLevelApproveUser = highLevelUserInfo
                                    .Select(user => new StepApproveUser
                                    {
                                        UserId = user.UserId,
                                        UserName = _lang.Locale == "zh-CN"
                                                   ? user.UserNameCn
                                                   : user.UserNameEn,
                                        AppointmentType = appTypeEscalation.DicCode,
                                        AppointmentTypeName = _lang.Locale == "zh-CN"
                                                              ? appTypeEscalation.DicNameCn
                                                              : appTypeEscalation.DicNameEn
                                    }).ToList();
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

        /// <summary>
        /// 查询签核人员的代理人员
        /// </summary>
        /// <param name="approveUser"></param>
        /// <returns></returns>
        public async Task<List<StepApproveUser>> GetApproveUserAgent(List<StepApproveUser> approveUser)
        {
            // 查询签核类型（实、兼、代、自动升级）
            var appointmentType = _db.Queryable<DictionaryInfoEntity>()
                                     .With(SqlWith.NoLock)
                                     .Where(appointment => appointment.DicType == "AppointmentType")
                                     .ToList();

            var appTypeActing = appointmentType.Where(app => app.DicCode == "Acting").First();
            var allUser = _db.Queryable<UserInfoEntity>()
                             .InnerJoin<UserPositionEntity>((user, position) => user.PositionId == position.PositionId);

            var ss = allUser.ToList();

            var userAgent = _db.Queryable<UserAgentEntity>()
                               .InnerJoin<UserInfoEntity>((useragent, user) => useragent.SubstituteUserId == user.UserId)
                               .Where((useragent, user) => useragent.SubstituteUserId == user.UserId);

            foreach (var appuser in approveUser)
            {
                var agentItem = userAgent.Where((useragent, user) => useragent.SubstituteUserId == appuser.UserId).First();
                if (agentItem != null)
                {
                    var agent = allUser.Where((user, position) => user.UserId == agentItem.AgentUserId).First();
                    appuser.UserId = agent.UserId;
                    appuser.UserName = _lang.Locale == "zh-CN"
                                    ? agent.UserNameCn
                                    : agent.UserNameEn;
                    appuser.AppointmentType = appTypeActing.DicCode;
                    appuser.AppointmentTypeName = _lang.Locale == "zh-CN"
                                               ? appTypeActing.DicNameCn
                                               : appTypeActing.DicNameEn;
                }
            }
            return approveUser;
        }
    }
}
