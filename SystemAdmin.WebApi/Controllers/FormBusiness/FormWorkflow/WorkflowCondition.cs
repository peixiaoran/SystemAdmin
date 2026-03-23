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
    public class WorkflowCondition
    {
        private readonly WorkflowConditionService _workflowConditionService;
        public WorkflowCondition(WorkflowConditionService workflowConditionService)
        {
            _workflowConditionService = workflowConditionService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程条件] 表单组别下拉")]
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDropDown()
        {
            return await _workflowConditionService.GetFormGroupDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程条件] 表单类别下拉")]
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDropDown([FromForm] string formGroupId)
        {
            return await _workflowConditionService.GetFormTypeDropDown(formGroupId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程条件] 新增流程条件")]
        public async Task<Result<int>> InsertWorkflowCondition([FromBody] WorkflowConditionUpsert upsert)
        {
            return await _workflowConditionService.InsertWorkflowCondition(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程条件] 删除流程条件")]
        public async Task<Result<int>> DeleteWorkflowCondition([FromForm] string condititonId)
        {
            return await _workflowConditionService.DeleteWorkflowCondition(condititonId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程条件] 修改流程条件")]
        public async Task<Result<int>> UpdateWorkflowCondition([FromBody] WorkflowConditionUpsert upsert)
        {
            return await _workflowConditionService.UpdateWorkflowCondition(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程条件] 查询流程条件分页")]
        public async Task<ResultPaged<WorkflowConditionDto>> GetWorkflowConditionPage([FromBody] GetWorkflowConditionPage getPage)
        {
            return await _workflowConditionService.GetWorkflowConditionPage(getPage);
        }
    }
}
