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
        //                            .InnerJoin<FormTypeEntity>((instance, formtype) => instance.FormTypeId == formtype.FormTypeId)
        //                            .InnerJoin<UserInfoEntity>((instance, formtype, user) => instance.ApplicantUserId == user.UserId)
        //                            .InnerJoin<DepartmentInfoEntity>((instance, formtype, user, dept) => user.DepartmentId == dept.DepartmentId)
        //                            .InnerJoin<DepartmentLevelEntity>((instance, formtype, user, dept, deptlevel) => dept.DepartmentLevelId == deptlevel.DepartmentLevelId)
        //                            .InnerJoin<PositionInfoEntity>((instance, formtype, user, dept, deptlevel, position) => user.PositionId == position.PositionId)
        //                            .LeftJoin<UserAgentEntity>((instance, formtype, user, dept, deptlevel, position, agent) => user.UserId == agent.SubstituteUserId)
        //                            .LeftJoin<UserInfoEntity>((instance, formtype, user, dept, deptlevel, position, agent, agentUser) => agent.AgentUserId == agentUser.UserId)
        //                            .Where((instance, formtype, user, dept, deptlevel, position, agent, agentUser) => instance.FormId == formId && agent.StartTime <= DateTime.Now && agent.EndTime >= DateTime.Now)
        //                            .Select((instance, formtype, user, dept, deptlevel, position, agent, agentUser) => new
        //                            {
        //                                instance.FormId,
        //                                instance.FormTypeId,
        //                                instance.RuleId,
        //                                ApplicantUserId = user.UserId,
        //                                ApplicantDeptId = dept.DepartmentId,
        //                                ApplicantUserName = _lang.Locale == "zh-CN"
        //                                    ? user.UserNameCn
        //                                    : user.UserNameEn,
        //                                DeptLevelSort = deptlevel.SortOrder,
        //                                PositionSort = position.SortOrder,
        //                                IsSubstitute = agent.SubstituteUserId,
        //                                AgentUserId = agent.AgentUserId,
        //                                AgentUserName = _lang.Locale == "zh-CN"
        //                                    ? agentUser.UserNameCn
        //                                    : agentUser.UserNameEn,
        //                            }).FirstAsync();

        //    // 所属分支初始步骤
        //    var branchStep = await _db.Queryable<WorkflowStepEntity>()
        //                              .With(SqlWith.NoLock)
        //                              .Where(rule => rule.FormTypeId == formDetail.RuleId && rule.SortOrder == 1)
        //                              .FirstAsync();
        //    var currentStep = branchStep.StepId;
        //    while (currentStep != -1)
        //    {
        //        var formReviewFlow = new FormReviewFlow();
        //        var stepInfo = await _db.Queryable<WorkflowRuleStepEntity>()
        //                                .With(SqlWith.NoLock)
        //                                .Where(step => step.RuleId == currentStep)
        //                                .FirstAsync();

        //        if (stepInfo.IsStartStep == 1)
        //        {

        //        }
        //        else if (stepInfo.Assignment == Assignment.Org.ToEnumString())
        //        {
        //            var orgInfo = await _db.Queryable<WorkflowStepOrgEntity>()
        //                                   .With(SqlWith.NoLock)
        //                                   .Where(step => step.StepId == currentStep)
        //                                   .FirstAsync();
        //        }
        //    }
        //}
    }
}
