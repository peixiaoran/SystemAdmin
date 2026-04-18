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
    public class WorkflowBranchStep : ControllerBase
    {
        private readonly WorkflowBranchStepService _workflowBranchStepService;
        public WorkflowBranchStep(WorkflowBranchStepService workflowBranchStepService)
        {
            _workflowBranchStepService = workflowBranchStepService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单分支步骤] 表单组别下拉")]
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDrop()
        {
            return await _workflowBranchStepService.GetFormGroupDrop();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单分支步骤] 表单类别下拉")]
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDrop([FromForm] string formGroupId)
        {
            return await _workflowBranchStepService.GetFormTypeDrop(formGroupId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单分支步骤] 分支下拉")]
        public async Task<Result<List<WorkflowBranchDropDto>>> GetWorkflowBranchDrop([FromForm] string formTypeId)
        {
            return await _workflowBranchStepService.GetWorkflowBranchDrop(formTypeId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单分支步骤] 新增分支步骤")]
        public async Task<Result<int>> InsertWorkflowBranchStep([FromBody] WorkflowBranchStepUpsert upsert)
        {
            return await _workflowBranchStepService.InsertWorkflowBranchStep(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单分支步骤] 删除分支步骤")]
        public async Task<Result<int>> DeleteWorkflowBranchStep([FromForm] string branchId, [FromForm] string stepId)
        {
            return await _workflowBranchStepService.DeleteWorkflowBranchStep(branchId, stepId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单分支步骤] 修改分支步骤")]
        public async Task<Result<int>> UpdateWorkflowBranchStep([FromBody] WorkflowBranchStepUpsert upsert)
        {
            return await _workflowBranchStepService.UpdateWorkflowBranchStep(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单分支步骤] 查询分支步骤实体")]
        public async Task<Result<WorkflowBranchStepDto>> GetWorkflowBranchStepEntity([FromForm] string branchId, [FromForm] string stepId)
        {
            return await _workflowBranchStepService.GetWorkflowBranchStepEntity(branchId, stepId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单分支步骤] 查询分支步骤分页")]
        public async Task<ResultPaged<WorkflowBranchStepDto>> GetWorkflowBranchStepPage([FromBody] GetWorkflowBranchStepPage getPage)
        {
            return await _workflowBranchStepService.GetWorkflowBranchStepPage(getPage);
        }
    }
}
