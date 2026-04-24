using SqlSugar;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.Workflow.ReviewFlowManager;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;

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

        /// <summary>
        /// 查询表单审批流程
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<List<FormReviewFlow>> GetFullReviewFlow(long formId)
        {
            var formReviewFlowList = new List<FormReviewFlow>();

            var formDetail = await _db.Queryable<FormInstanceEntity>()
                                    .With(SqlWith.NoLock)
                                    .InnerJoin<FormTypeEntity>((instance, formtype) => instance.FormTypeId == formtype.FormTypeId)
                                    .InnerJoin<UserInfoEntity>((instance, formtype, user) => instance.ApplicantUserId == user.UserId)
                                    .InnerJoin<DepartmentInfoEntity>((instance, formtype, user, dept) => user.DepartmentId == dept.DepartmentId)
                                    .InnerJoin<DepartmentLevelEntity>((instance, formtype, user, dept, deptlevel) => dept.DepartmentLevelId == deptlevel.DepartmentLevelId)
                                    .InnerJoin<PositionInfoEntity>((instance, formtype, user, dept, deptlevel, position) => user.PositionId == position.PositionId)
                                    .Where((instance, formtype, user, dept, deptlevel, position) => instance.FormId == formId)
                                    .Select((instance, formtype, user, dept, deptlevel, position) => new
                                    {
                                        instance.FormId,
                                        instance.FormTypeId,
                                        instance.RuleId,
                                        ApplicantUserId = user.UserId,
                                        ApplicantDeptId = dept.DepartmentId,
                                        ApplicantUserName = _lang.Locale == "zh-CN"
                                                            ? user.UserNameCn
                                                            : user.UserNameEn,
                                        DeptLevelSort = deptlevel.SortOrder,
                                        PositionSort = position.SortOrder
                                    }).FirstAsync();
            // 所属规则步骤
            var ruleStep = await _db.Queryable<WorkflowRuleStepEntity>()
                                    .With(SqlWith.NoLock)
                                    .Where(rule => rule.RuleId == formDetail.RuleId && rule.SortOrder == 1)
                                    .FirstAsync();
            var currentStepId = ruleStep.CurrentStepId;
            while (currentStepId != -1)
            {
                var formReviewFlow = new FormReviewFlow();
                var stepInfo = await _db.Queryable<WorkflowStepEntity>()
                                        .With(SqlWith.NoLock)
                                        .Where(step => step.StepId == currentStepId)
                                        .Select(step => new
                                        {
                                            StepName = _lang.Locale == "zh-CN"
                                                       ? step.StepNameCn
                                                       : step.StepNameEn,
                                            step.IsStartStep,
                                            step.Assignment
                                        }).FirstAsync();

                formReviewFlow.StepName = stepInfo.StepName;
                if (stepInfo.IsStartStep == 1)
                {
                    var stepReviewUser = await GetStartStepReviewUser(formDetail.ApplicantUserId);
                    formReviewFlow.stepReviewUser.Add(stepReviewUser);
                }
                else if (stepInfo.Assignment == Assignment.Org.ToEnumString())
                {
                    var orgInfo = await _db.Queryable<WorkflowStepOrgEntity>()
                                           .With(SqlWith.NoLock)
                                           .Where(step => step.StepId == currentStepId)
                                           .FirstAsync();
                }

                formReviewFlowList.Add(formReviewFlow);
                currentStepId = -1;
            }

            return formReviewFlowList;
        }

        /// <summary>
        /// 查询起始步骤的审核人员
        /// </summary>
        /// <returns></returns>
        public async Task<StepReviewUser> GetStartStepReviewUser(long applicantUserId)
        {
            var stepReviewUser = new StepReviewUser();

            bool isChinese = _lang.Locale == "zh-CN";
            string userNameCol = isChinese ? "users.UserNameCn" : "users.UserNameEn";
            string agentNameCol = isChinese ? "agentusers.UserNameCn" : "agentusers.UserNameEn";
            string dicNameCol = isChinese ? "dic.DicNameCn" : "dic.DicNameEn";

            string sql = $@"SELECT
                                {userNameCol}   AS UserName,
                                {agentNameCol}  AS AgentUserName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL THEN
                                        (
                                            SELECT {dicNameCol}
                                            FROM Basic.DictionaryInfo dic
                                            WHERE dic.DicType = 'AppointmentType'
                                              AND dic.DicCode = 'Agent'
                                        )
                                    ELSE
                                        (
                                            SELECT {dicNameCol}
                                            FROM Basic.DictionaryInfo dic
                                            WHERE dic.DicType = 'AppointmentType'
                                              AND dic.DicCode = 'Actual'
                                        )
                                END AS AppointmentTypeName
                            FROM
                                Basic.UserInfo users
                                LEFT JOIN Basic.UserAgent agent ON users.UserId = agent.SubstituteUserId
                                LEFT JOIN Basic.UserInfo agentusers ON agent.AgentUserId = agentusers.UserId
                            WHERE
                                users.UserId = @ApplicantUserId";

            var result = await _db.Ado.SqlQueryAsync<StepReviewUser>(
                sql,
                new SugarParameter("@ApplicantUserId", applicantUserId)
            );

            return result.FirstOrDefault() ?? new StepReviewUser();
        }
    }
}
