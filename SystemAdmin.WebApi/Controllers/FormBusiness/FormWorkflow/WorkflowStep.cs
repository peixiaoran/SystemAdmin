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
    public class WorkflowStep : ControllerBase
    {
        private readonly WorkflowStepService _workflowStepService;
        public WorkflowStep(WorkflowStepService workflowStepService)
        {
            _workflowStepService = workflowStepService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 表单组别下拉")]
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDropDown()
        {
            return await _workflowStepService.GetFormGroupDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 表单类别下拉")]
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDropDown([FromForm] string formGroupId)
        {
            return await _workflowStepService.GetFormTypeDropDown(formGroupId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 步骤指派规则下拉")]
        public async Task<Result<List<AssignmentDropDto>>> GetAssignmentDropDown()
        {
            return await _workflowStepService.GetAssignmentDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 步骤签核方式下拉")]
        public async Task<Result<List<ApproveModeDropDto>>> GetApproveModeDropDown()
        {
            return await _workflowStepService.GetApproveModeDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 部门级别下拉")]
        public async Task<Result<List<DepartmentLevelDropDto>>> GetDepartmentLevelDropDown()
        {
            return await _workflowStepService.GetDepartmentLevelDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 员工职级下拉")]
        public async Task<Result<List<UserPositionDropDto>>> GetUserPositionDropDown()
        {
            return await _workflowStepService.GetUserPositionDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 部门树下拉")]
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDropDown()
        {
            return await _workflowStepService.GetDepartmentDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 新增步骤")]
        public async Task<Result<int>> InsertWorkflowStep([FromBody] WorkflowStepUpsert upsert)
        {
            return await _workflowStepService.InsertWorkflowStep(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 删除步骤")]
        public async Task<Result<int>> DeleteWorkflowStep([FromForm] string stepId)
        {
            return await _workflowStepService.DeleteWorkflowStep(stepId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 修改步骤")]
        public async Task<Result<int>> UpdateWorkflowStep([FromBody] WorkflowStepUpsert upsert)
        {
            return await _workflowStepService.UpdateWorkflowStep(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 查询步骤及流程分支列表")]
        public async Task<Result<List<WorkflowStepListDto>>> GetWorkflowStepList([FromForm] string formTypeId)
        {
            return await _workflowStepService.GetWorkflowStepList(formTypeId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 查询步骤实体")]
        public async Task<Result<WorkflowStepDto>> GetWorkflowStepEntity([FromForm] string stepId)
        {
            return await _workflowStepService.GetWorkflowStepEntity(stepId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 查询员工信息分页")]
        public async Task<ResultPaged<UserInfoDto>> GetUserInfoPage(GetUserInfoPage getPage)
        {
            return await _workflowStepService.GetUserInfoPage(getPage);
        }
    }
}
