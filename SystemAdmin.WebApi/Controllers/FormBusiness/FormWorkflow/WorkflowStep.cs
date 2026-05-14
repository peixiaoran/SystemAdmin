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
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程步骤详情] 表单组别下拉")]
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDrop()
        {
            return await _workflowStepService.GetFormGroupDrop();
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程步骤详情] 表单类别下拉")]
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDrop([FromForm] string formGroupId)
        {
            return await _workflowStepService.GetFormTypeDrop(formGroupId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程步骤详情] 步骤指派规则下拉")]
        public async Task<Result<List<AssignmentDropDto>>> GetAssignmentDrop()
        {
            return await _workflowStepService.GetAssignmentDrop();
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程步骤详情] 步骤审批方式下拉")]
        public async Task<Result<List<ReviewModeDropDto>>> GetReviewModeDrop()
        {
            return await _workflowStepService.GetReviewModeDrop();
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程步骤详情] 部门级别下拉")]
        public async Task<Result<List<DepartmentLevelDropDto>>> GetDepartmentLevelDrop()
        {
            return await _workflowStepService.GetDepartmentLevelDrop();
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程步骤详情] 员工职级下拉")]
        public async Task<Result<List<PositionDropDto>>> GetPositionDrop()
        {
            return await _workflowStepService.GetPositionDrop();
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程步骤详情] 部门树下拉")]
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDrop()
        {
            return await _workflowStepService.GetDepartmentDrop();
        }

		[HttpPost]
		[Tags("表单业务管理-表单流程配置")]
		[EndpointSummary("[流程步骤详情] 查询员工信息分页")]
		public async Task<ResultPaged<UserInfoDto>> GetUserInfoPage(GetUserInfoPage getPage)
		{
			return await _workflowStepService.GetUserInfoPage(getPage);
		}

		[HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程步骤详情] 新增步骤")]
        public async Task<Result<int>> InsertWorkflowStep([FromBody] WorkflowStepUpsert upsert)
        {
            return await _workflowStepService.InsertWorkflowStep(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程步骤详情] 删除步骤")]
        public async Task<Result<int>> DeleteWorkflowStep([FromForm] string stepId)
        {
            return await _workflowStepService.DeleteWorkflowStep(stepId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程步骤详情] 修改步骤")]
        public async Task<Result<int>> UpdateWorkflowStep([FromBody] WorkflowStepUpsert upsert)
        {
            return await _workflowStepService.UpdateWorkflowStep(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程步骤详情] 查询步骤列表")]
        public async Task<Result<List<WorkflowStepListDto>>> GetWorkflowStepList([FromForm] string formTypeId)
        {
            return await _workflowStepService.GetWorkflowStepList(formTypeId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单流程配置")]
        [EndpointSummary("[流程步骤详情] 查询步骤实体")]
        public async Task<Result<WorkflowStepDto>> GetWorkflowStepEntity([FromForm] string stepId)
        {
            return await _workflowStepService.GetWorkflowStepEntity(stepId);
        }
    }
}
