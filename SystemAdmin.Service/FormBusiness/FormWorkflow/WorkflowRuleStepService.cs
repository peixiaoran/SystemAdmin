using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Repository.FormBusiness.FormWorkflow;

namespace SystemAdmin.Service.FormBusiness.FormWorkflow
{
    public class WorkflowRuleStepService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<WorkflowRuleStepService> _logger;
        private readonly SqlSugarScope _db;
        private readonly WorkflowRuleStepRepository _workflowRuleStep;
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness.FormWorkflow.WorkflowRuleStep";

        public WorkflowRuleStepService(CurrentUser loginuser, ILogger<WorkflowRuleStepService> logger, SqlSugarScope db, WorkflowRuleStepRepository workflowRuleStep, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _workflowRuleStep = workflowRuleStep;
            _localization = localization;
        }
    }
}
