using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Service.FormBusiness.FormWorkflow;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.FormBusiness.FormWorkflow
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/FormBusiness/FormWorkFlow/[controller]/[action]")]
    [ApiController]
    public class WorkflowRuleStep : ControllerBase
    {
        private readonly WorkflowRuleStepService _workflowRuleStepService;
        public WorkflowRuleStep(WorkflowRuleStepService workflowRuleStepService)
        {
            _workflowRuleStepService = workflowRuleStepService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程规则详情] 表单组别下拉")]
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDrop()
        {
            return await _workflowRuleStepService.GetFormGroupDrop();
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程规则详情] 表单类别下拉")]
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDrop([FromForm] string formGroupId)
        {
            return await _workflowRuleService.GetFormTypeDrop(formGroupId);
        }
    }
}
