using SqlSugar;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormAudit.Entity;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.WorkflowLifecycle;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;

namespace SystemAdmin.Repository.FormBusiness.WorkflowLifecycle
{
    /// <summary>
    /// 审批步骤启动处理器
    /// </summary>
    public class StepBeforeStartHandler
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

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
        /// 查询表单所有审批人
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
                                       .FirstAsync();

                // 寻找当前步骤的审批人
                if (nowStep.IsStartStep == 1)
                {
                    WorkflowApproveUser workflowApproveItem = new WorkflowApproveUser();
                    workflowApproveItem.StepId = nowStep.StepId;
                    workflowApproveItem.StepName = _lang.Locale == "zh-CN" 
                                                   ? nowStep.StepNameCn 
                                                   : nowStep.StepNameEn;
                    workflowApproveItem.AppmoveUserId = appUserInfo.UserId;
                    workflowApproveItem.AppmoveUserName = _lang.Locale == "zh-CN" 
                                                   ? appUserInfo.UserNameCn 
                                                   : appUserInfo.UserNameEn;
                }
                else
                {
                    List<WorkflowApproveUser> workflowApproveItems = new List<WorkflowApproveUser>();

                    // 组织架构审批步骤
                    if (nowStep.Assignment == Assignment.Org.ToEnumString())
                    {
                        var stepOrg = await _db.Queryable<WorkflowStepOrgEntity>()
                                               .With(SqlWith.NoLock)
                                               .Where(org => org.StepId == nowStep.StepId)
                                               .FirstAsync();
                        // 查询组织架构审批人
                        var orgApproveUser = await GetStepApproveUserByOrg(appUserInfo.UserId, appUserInfo.DepartmentId, nowStep.ApproveMode, stepOrg.DeptLeaveIds, stepOrg.DeptLeaveIds);
                        foreach (var approveUser in orgApproveUser)
                        {
                            WorkflowApproveUser workflowApproveItem = new WorkflowApproveUser();
                            workflowApproveItem.StepId = nowStep.StepId;
                            workflowApproveItem.StepName = _lang.Locale == "zh-CN"
                                                           ? nowStep.StepNameCn
                                                           : nowStep.StepNameEn;
                            workflowApproveItem.AppmoveUserId = approveUser.UserId;
                            workflowApproveItem.AppmoveUserName = approveUser.UserName;
                            workflowApproveItems.Add(workflowApproveItem);
                            workflowAllApproveUser.AddRange(workflowApproveItem);
                        }
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

                nowStepId = stepCondition.Where(condition => conditionIds.Contains(condition.ConditionId) && condition.ExecuteMatched == 1).First().NextStepId;
            }
            return workflowAllApproveUser;
        }

        /// <summary>
        /// 查询组织架构步骤的审批人
        /// </summary>
        /// <param name="appdeptId"></param>
        /// <param name="approveMode"></param>
        /// <param name="deptLevelIds"></param>
        /// <param name="userPositionIds"></param>
        /// <returns></returns>
        public async Task<List<StepApproveUser>> GetStepApproveUserByOrg(long appUserId, long appdeptId, string approveMode, string deptLevelIds, string userPositionIds)
        {
            // 字符串解析为列表
            var deptLevelIdList = deptLevelIds.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                              .Select(long.Parse)
                                              .ToList();
            var userPositionIdList = deptLevelIds.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                                 .Select(long.Parse)
                                                 .ToList();

            // 查询符合级别的部门
            var depts = await _db.Queryable<DepartmentInfoEntity>()
                                 .With(SqlWith.NoLock)
                                 .ToParentListAsync(dept => dept.ParentId, appdeptId);
            var deptIds = depts.Where(dept => deptLevelIdList.Contains(dept.DepartmentLevelId))
                               .Select(dept => dept.DepartmentId)
                               .ToList();

            var approveUsers = new List<StepApproveUser>();
            // 如果是单签，只取符合职级最老入职员工
            if (approveMode == ApproveMode.Ss.ToEnumString())
            {
                approveUsers = await _db.Queryable<UserInfoEntity>()
                                        .With(SqlWith.NoLock)
                                        .Where(user => deptIds.Contains(user.DepartmentId) && userPositionIdList.Contains(user.PositionId) && user.UserId != appUserId)
                                        .OrderBy(user => user.HireDate)
                                        .Select(user => new StepApproveUser
                                        {
                                            UserId = user.UserId,
                                            UserName = user.UserNameCn,
                                        }).Take(1).ToListAsync();
            }
            // 如果是会签，则取符合职级所有员工
            else if (approveMode == ApproveMode.Cs.ToEnumString())
            {
                approveUsers = await _db.Queryable<UserInfoEntity>()
                                           .With(SqlWith.NoLock)
                                           .Where(user => user.DepartmentId == appdeptId && userPositionIds.Contains(user.PositionId.ToString()) && user.UserId != appUserId)
                                           .Select(user => new StepApproveUser
                                           {
                                               UserId = user.UserId,
                                               UserName = user.UserNameCn,
                                           }).ToListAsync();
            }
            if (approveUsers.Count==0)
            {
                StepApproveUser workflowApproveUser = new StepApproveUser()
                {
                    UserId = 0,
                    UserName = "无"
                };
                approveUsers.Add(workflowApproveUser);
            }
            return approveUsers;
        }
    }
}
