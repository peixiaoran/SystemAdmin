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
    public class WorkflowBranch : ControllerBase
    {
        private readonly WorkflowBranchService _workflowBranchService;
        public WorkflowBranch(WorkflowBranchService workflowBranchService)
        {
            _workflowBranchService = workflowBranchService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单分支信息] 表单组别下拉")]
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDropDown()
        {
            return await _workflowBranchService.GetFormGroupDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单分支信息] 表单类别下拉")]
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDropDown([FromForm] string formGroupId)
        {
            return await _workflowBranchService.GetFormTypeDropDown(formGroupId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单分支信息] 新增分支")]
        public async Task<Result<int>> InsertWorkflowBranch([FromBody] WorkflowBranchUpsert upsert)
        {
            return await _workflowBranchService.InsertWorkflowBranch(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单分支信息] 删除分支")]
        public async Task<Result<int>> DeleteWorkflowBranch([FromForm] string condititonId)
        {
            return await _workflowBranchService.DeleteWorkflowBranch(condititonId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单分支信息] 修改分支")]
        public async Task<Result<int>> UpdateWorkflowBranch([FromBody] WorkflowBranchUpsert upsert)
        {
            return await _workflowBranchService.UpdateWorkflowBranch(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单分支信息] 查询分支实体")]
        public async Task<Result<WorkflowBranchDto>> GetWorkflowBranchEntity([FromForm] string branchId)
        {
            return await _workflowBranchService.GetWorkflowBranchEntity(branchId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单分支信息] 查询分支分页")]
        public async Task<ResultPaged<WorkflowBranchDto>> GetWorkflowBranchPage([FromBody] GetWorkflowBranchPage getPage)
        {
            return await _workflowBranchService.GetWorkflowBranchPage(getPage);
        }
    }
}
