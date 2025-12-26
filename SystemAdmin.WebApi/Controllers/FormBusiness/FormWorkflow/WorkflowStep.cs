using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.FormBusiness.FormProcessConfig.Queries;
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
        [EndpointSummary("[表单流程步骤] 新增审批步骤")]
        public async Task<Result<int>> InsertWorkflowStep(WorkflowStepUpsert workflowStepUpsert)
        {
            return await _workflowStepService.InsertWorkflowStep(workflowStepUpsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 删除审批步骤")]
        public async Task<Result<int>> DeleteWorkflowStep(WorkflowStepUpsert workflowStepUpsert)
        {
            return await _workflowStepService.DeleteWorkflowStep(workflowStepUpsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 修改审批步骤")]
        public async Task<Result<int>> UpdateWorkflowStep(WorkflowStepUpsert workflowStepUpsert)
        {
            return await _workflowStepService.UpdateWorkflowStep(workflowStepUpsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 查询审批步骤分页")]
        public async Task<ResultPaged<WorkflowStepPageDto>> GetWorkflowStepPage(GetWorkflowStepPage getWorkflowStepPage)
        {
            return await _workflowStepService.GetWorkflowStepPage(getWorkflowStepPage);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 查询审批步骤实体")]
        public async Task<Result<WorkflowStepEntityDto>> GetWorkflowStepEntity(GetWorkflowStepEntity getWorkflowStepEntity)
        {
            return await _workflowStepService.GetWorkflowStepEntity(getWorkflowStepEntity);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 表单表单组别下拉框")]
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDropDown()
        {
            return await _workflowStepService.GetFormGroupDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 表单表单类别下拉框")]
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDropDown(GetFormTypeDropDown getFormTypeDropDown)
        {
            return await _workflowStepService.GetFormTypeDropDown(getFormTypeDropDown);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 审批人选取方式下拉框")]
        public async Task<Result<List<AssignmentDropDto>>> GetAssignmentDropDown()
        {
            return await _workflowStepService.GetAssignmentDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 部门下拉框")]
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDropDown()
        {
            return await _workflowStepService.GetDepartmentDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 部门级别下拉框")]
        public async Task<Result<List<DepartmentLevelDropDto>>> GetDepartmentLevelDropDown()
        {
            return await _workflowStepService.GetDepartmentLevelDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[员工信息] 职级下拉框")]
        public async Task<Result<List<UserPositionDropDto>>> GetUserPositionDropDown()
        {
            return await _workflowStepService.GetUserPositionDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[员工信息] 职业下拉框")]
        public async Task<Result<List<UserLaborDropDto>>> GetLaborDropDown()
        {
            return await _workflowStepService.GetLaborDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[员工信息] 查询用户信息分页")]
        public async Task<ResultPaged<Model.FormBusiness.FormWorkflow.Dto.UserInfoDto>> GetUserInfoPage(GetUserInfoPage getUserInfoPage)
        {
            return await _workflowStepService.GetUserInfoPage(getUserInfoPage);
        }
    }
}
