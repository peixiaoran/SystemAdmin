using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.Workflow.ReviewFlowManager;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Entity;

namespace SystemAdmin.Repository.FormBusiness.Workflow
{
    public class ReviewFlowManager
    {
        private readonly CurrentUser _loginuser;
        private readonly SqlSugarScope _db;
        private readonly LocalizationService _localization;
        private readonly Language _lang;

        public ReviewFlowManager(CurrentUser loginuser, SqlSugarScope db, LocalizationService localization, Language lang)
        {
            _loginuser = loginuser;
            _db = db;
            _localization = localization;
            _lang = lang;
        }

        //public async Task<List<FormReviewFlow>> GetFormReviewFlow(long formId)
        //{
        //    var formReviewFlowList = new List<FormReviewFlow>();

        //    var formDetail = await _db.Queryable<FormInstanceEntity>()
        //                            .With(SqlWith.NoLock)
        //                            .InnerJoin<FormTypeEntity>((instance, formType) => instance.FormTypeId == formType.FormTypeId)
        //                            .InnerJoin<UserInfoEntity>((instance, formType, user) => instance.ApplicantUserId == user.UserId)
        //                            .InnerJoin<DepartmentInfoEntity>((instance, formType, user, dept) => user.DepartmentId == dept.DepartmentId)
        //                            .InnerJoin<DepartmentLevelEntity>((instance, formType, user, dept, deptLevel) => dept.DepartmentLevelId == deptLevel.DepartmentLevelId)
        //                            .InnerJoin<PositionInfoEntity>((instance, formType, user, dept, deptLevel, position) => user.PositionId == position.PositionId)
        //                            .LeftJoin<UserAgentEntity>((instance, formType, user, dept, deptLevel, position, agent) => user.UserId == agent.SubstituteUserId)
        //                            .LeftJoin<UserInfoEntity>((instance, formType, user, dept, deptLevel, position, agent, agentUser) => agent.AgentUserId == agentUser.UserId)
        //                            .LeftJoin<FormReviewLimitEntity>((instance, formType, user, dept, deptLevel, position, agent, agentUser, reviewLimit) => formType.FormTypeId == reviewLimit.FormTypeId && position.PositionId == reviewLimit.PositionId)
        //                            .Where((instance, formType, user, dept, deptLevel, position, agent, agentUser, reviewLimit) => instance.FormId == formId)
        //                            .Select((instance, formType, user, dept, deptLevel, position, agent, agentUser, reviewLimit) => new
        //                            {
        //                                instance.FormId,
        //                                instance.FormTypeId,
        //                                instance.RuleId,
        //                                ApplicantUserId = user.UserId,
        //                                ApplicantDeptId = dept.DepartmentId,
        //                                ApplicantUserName = _lang.Locale == "zh-CN"
        //                                    ? user.UserNameCn
        //                                    : user.UserNameEn,
        //                                DeptLevelSort = deptLevel.SortOrder,
        //                                PositionSort = position.SortOrder,
        //                                IsSubstitute = agent.SubstituteUserId,
        //                                AgentUserId = agent.AgentUserId,
        //                                AgentUserName = _lang.Locale == "zh-CN"
        //                                    ? agentUser.UserNameCn
        //                                    : agentUser.UserNameEn,
        //                                MaxPositionId = reviewLimit.MaxPositionId,
        //                            }).FirstAsync();

        //    // 所属分支初始步骤
        //    var branchStep = await _db.Queryable<WorkflowRuleEntity>()
        //                              .With(SqlWith.NoLock)
        //                              .Where(rule => rule.RuleId == formDetail.RuleId && rule.SortOrder == 1)
        //                              .FirstAsync();
        //    var currentStep = branchStep.StepId;
        //    while (currentStep != -1)
        //    {
        //        var formReviewFlow = new FormReviewFlow();

        //        var stepInfo = await _db.Queryable<WorkflowStepEntity>()
        //                                .With(SqlWith.NoLock)
        //                                .Where(step => step.StepId == currentStep)
        //                                .FirstAsync();

        //        if (stepInfo.Assignment == Assignment.Org.ToEnumString())
        //        {
        //            var orgInfo = await _db.Queryable<WorkflowStepOrgEntity>()
        //                                   .With(SqlWith.NoLock)
        //                                   .Where(step => step.StepId == currentStep)
        //                                   .FirstAsync();

        //            if (stepInfo.IsStartStep == 1)
        //            {
        //                //await GetStepReviewUser(orgInfo.DeptLeaveId, orgInfo.PositionId, formDetail.ApplicantUserId, formDetail.ApplicantDeptId, formDetail.DeptLevelSort, formDetail.PositionSort);
        //            }

        //            // 查找符合部门级别的部门信息
                   
        //        }
        //    }
        //}
    }
}
