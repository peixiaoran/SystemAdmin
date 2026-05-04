using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Commands;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Queries;
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
        [EndpointSummary("[流程规则步骤] 表单组别下拉")]
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDrop()
        {
            return await _workflowRuleStepService.GetFormGroupDrop();
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程规则步骤] 表单类别下拉")]
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDrop([FromForm] string formGroupId)
        {
            return await _workflowRuleStepService.GetFormTypeDrop(formGroupId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程规则步骤] 规则下拉")]
        public async Task<Result<List<WorkflowRuleDropDto>>> GetWorkflowRuleDrop([FromForm] string formTypeId)
        {
            return await _workflowRuleStepService.GetWorkflowRuleDrop(formTypeId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程规则步骤] 步骤下拉")]
        public async Task<Result<List<WorkflowStepDropDto>>> GetWorkflowStepDrop([FromForm] string formTypeId)
        {
            return await _workflowRuleStepService.GetWorkflowStepDrop(formTypeId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程规则步骤] 新增规则步骤")]
        public async Task<Result<int>> InsertWorkflowRuleStep([FromBody] WorkflowRuleStepUpsert upsert)
        {
            return await _workflowRuleStepService.InsertWorkflowRuleStep(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程规则步骤] 删除规则步骤")]
        public async Task<Result<int>> DeleteWorkflowRuleStep([FromForm] string ruleId, [FromForm] string currentStepId)
        {
            return await _workflowRuleStepService.DeleteWorkflowRuleStep(ruleId, currentStepId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程规则步骤] 修改规则步骤")]
        public async Task<Result<int>> UpdateWorkflowRuleStep([FromBody] WorkflowRuleStepUpsert upsert)
        {
            return await _workflowRuleStepService.UpdateWorkflowRuleStep(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程规则步骤] 查询规则步骤实体")]
        public async Task<Result<WorkflowRuleStepDto>> GetWorkflowRuleStepEntity([FromForm] string ruleId, [FromForm] string currentStepId)
        {
            return await _workflowRuleStepService.GetWorkflowRuleStepEntity(ruleId, currentStepId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程规则步骤] 查询规则步骤列表")]
        public async Task<Result<List<WorkflowRuleStepDto>>> GetWorkflowRuleStepList([FromForm] string ruleId)
        {
            return await _workflowRuleStepService.GetWorkflowRuleStepList(ruleId);
        }
    }
}
