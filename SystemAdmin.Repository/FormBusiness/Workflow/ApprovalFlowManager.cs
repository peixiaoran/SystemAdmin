using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.Workflow.ApprovalFlowManager;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Entity;

namespace SystemAdmin.Repository.FormBusiness.Workflow
{
    public class ApprovalFlowManager
    {
        private readonly CurrentUser _loginuser;
        private readonly SqlSugarScope _db;
        private readonly LocalizationService _localization;
        private readonly Language _lang;

        public ApprovalFlowManager(CurrentUser loginuser, SqlSugarScope db, LocalizationService localization, Language lang)
        {
            _loginuser = loginuser;
            _db = db;
            _localization = localization;
            _lang = lang;
        }

        public async Task<List<FormApprovalFlow>> GetFormApprovalFlow(long formId)
        {
            var formApprovalFlow = new List<FormApprovalFlow>();

            // 表单信息
            var formInfo = await _db.Queryable<FormInstanceEntity>()
                                    .With(SqlWith.NoLock)
                                    .Where(form => form.FormId == formId)
                                    .FirstAsync();
            
            // 申请人信息
            var applicantUser = await _db.Queryable<UserInfoEntity>()
                                         .With(SqlWith.NoLock)
                                         .InnerJoin<PositionInfoEntity>((user, position) => user.PositionId == position.PositionId)
                                         .LeftJoin<UserAgentEntity>((user, position, agent) => user.UserId == agent.SubstituteUserId)
                                         .InnerJoin<UserInfoEntity>((user, position, agent, agentuser) => agent.AgentUserId == agentuser.UserId)
                                         .Where((user, position, agent, agentuser) => user.UserId == formInfo.ApplicantUserId)
                                         .Select((user, position, agent, agentuser) => new
                                         {
                                             user.UserId,
                                             UserName = _lang.Locale == "zh-CN"
                                                       ? user.UserNameCn
                                                       : user.UserNameEn,
                                             position.SortOrder,
                                             IsSubstitute = agent.SubstituteUserId,
                                             agent.AgentUserId,
                                             AgentUserName = _lang.Locale == "zh-CN"
                                                       ? agentuser.UserNameCn
                                                       : user.UserNameEn,
                                         }).FirstAsync();

            // 所属分支步骤
            var branchStep = await _db.Queryable<WorkflowBranchStepEntity>()
                                      .With(SqlWith.NoLock)
                                      .Where(branchstep => branchstep.BranchId == formInfo.BranchId && branchstep.SortOrder == 1)
                                      .FirstAsync();

            var currentStep = branchStep.StepId;
            while (currentStep != -1)
            {
                var stepApprovalUser = new StepApprovalUser();

                var stepInfo = await _db.Queryable<WorkflowStepEntity>()
                                        .With(SqlWith.NoLock)
                                        .Where(step => step.StepId == currentStep)
                                        .FirstAsync();

                if (stepInfo.Assignment == Assignment.Org.ToEnumString())
                {
                    var orgInfo = await _db.Queryable<WorkflowStepOrgEntity>()
                                           .With(SqlWith.NoLock)
                                           .Where(step => step.StepId == currentStep)
                                           .FirstAsync();
                    if (stepInfo.IsStartStep == 1)
                    {
                        GetUserFormApprovalFlow();
                    }

                    // 查找符合部门级别的部门信息
                    var parentDept = await _db.Queryable<DepartmentInfoEntity>().ToParentListAsync(dept => dept.ParentId, appUser.DepartmentId, dept => dept.DepartmentLevelId == orgInfo.DeptLeaveId);
                    var userInfo = await _db.Queryable<UserInfoEntity>()
                                            .With(SqlWith.NoLock)
                                            .InnerJoin<DepartmentInfoEntity>((user, dept) => user.DepartmentId == dept.DepartmentId)
                                            .InnerJoin<DepartmentLevelEntity>((user, dept, deptlevel) => dept.DepartmentLevelId == deptlevel.DepartmentLevelId)
                                            .InnerJoin<PositionInfoEntity>((user, dept, deptlevel, position) => user.PositionId == position.PositionId)
                                            .Where((user, dept, deptlevel, position) => deptlevel.DepartmentLevelId == orgInfo.DeptLeaveId && position.PositionId == orgInfo.PositionId && user.IsEmployed==1 && user.IsFreeze==0 && )
                                            .FirstAsync();
                }
            }
        }

        public async Task<List<FormApprovalFlow>> GetUserFormApprovalFlow(long formId, List<long> userIds)
        {
            var formApprovalFlow = new List<FormApprovalFlow>();
            foreach (var userid in userIds)
            {
                var formRecord = await _db.Queryable<FormReviewRecordEntity>()
                                      .With(SqlWith.NoLock)
                                      .Where(form => form.FormId == formId)
                                      .OrderByDescending(form => form.ReviewDateTime)
                                      .ToListAsync();

                // 找到最后一条 Rejected 的位置
                var lastRejectedIndex = formRecord.FindIndex(form => form.ReviewStatus == ReviewResult.Rejected.ToEnumString());

                // 取 Rejected 之前的数据（即时间更新的数据）
                var result = lastRejectedIndex >= 0 ? formRecord.Take(lastRejectedIndex).ToList() : new List<FormReviewRecordEntity>();

                // 审批身份
                var appointment = await _db.Queryable<DictionaryInfoEntity>()
                                           .With(SqlWith.NoLock)
                                           .Where(dic => dic.DicType == "AppointmentType")
                                           .ToListAsync();
            }
            return formApprovalFlow;
        }
    }
}
