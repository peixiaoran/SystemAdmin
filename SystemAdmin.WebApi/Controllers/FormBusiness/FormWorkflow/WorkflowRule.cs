using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Commands;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Queries;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Service.FormBusiness.FormWorkflow;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.FormBusiness.FormWorkflow
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/FormBusiness/FormWorkFlow/[controller]/[action]")]
    [ApiController]
    public class WorkflowRule : ControllerBase
    {
        private readonly WorkflowRuleService _workflowRuleService;
        public WorkflowRule(WorkflowRuleService workflowRuleService)
        {
            _workflowRuleService = workflowRuleService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程规则详情] 表单组别下拉")]
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDrop()
        {
            return await _workflowRuleService.GetFormGroupDrop();
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程规则详情] 表单类别下拉")]
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDrop([FromForm] string formGroupId)
        {
            return await _workflowRuleService.GetFormTypeDrop(formGroupId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程规则详情] 职级下拉")]
        public async Task<Result<List<PositionDropDto>>> GetPositionDrop()
        {
            return await _workflowRuleService.GetPositionDrop();
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程规则详情] 新增规则")]
        public async Task<Result<int>> InsertWorkflowRule([FromBody] WorkflowRuleUpsert upsert)
        {
            return await _workflowRuleService.InsertWorkflowRule(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程规则详情] 删除规则")]
        public async Task<Result<int>> DeleteWorkflowRule([FromForm] string ruleId)
        {
            return await _workflowRuleService.DeleteWorkflowRule(ruleId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程规则详情] 修改规则")]
        public async Task<Result<int>> UpdateWorkflowRule([FromBody] WorkflowRuleUpsert upsert)
        {
            return await _workflowRuleService.UpdateWorkflowRule(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程规则详情] 查询规则实体")]
        public async Task<Result<WorkflowRuleDto>> GetWorkflowRuleEntity([FromForm] string ruleId)
        {
            return await _workflowRuleService.GetWorkflowRuleEntity(ruleId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程规则详情] 查询规则分页")]
        public async Task<ResultPaged<WorkflowRuleDto>> GetWorkflowRulePage([FromBody] GetWorkflowRulePage getPage)
        {
            return await _workflowRuleService.GetWorkflowRulePage(getPage);
        }
    }
}
