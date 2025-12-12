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
    [Route("api/FormBusiness/FormWorkflow/[controller]/[action]")]
    [ApiController]
    public class FormStep : ControllerBase
    {
        private readonly FormStepService _formStepService;
        public FormStep(FormStepService formStepService)
        {
            _formStepService = formStepService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 新增表单步骤")]
        public async Task<Result<int>> InsertFormStep(FormStepUpsert formStepUpsert)
        {
            return await _formStepService.InsertFormStep(formStepUpsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 修改表单步骤")]
        public async Task<Result<int>> UpdateFormStep(FormStepUpsert formStepUpsert)
        {
            return await _formStepService.UpdateFormStep(formStepUpsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 查询表单步骤分页")]
        public async Task<ResultPaged<FormStepDto>> GetFormStepPage(GetFormStepPage getFormStepPage)
        {
            return await _formStepService.GetFormStepPage(getFormStepPage);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 查询表单步骤实体")]
        public async Task<Result<FormStepDto>> GetFormStepEntity(GetFormStepEntity getFormStepEntity)
        {
            return await _formStepService.GetFormStepEntity(getFormStepEntity);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 表单组别下拉框")]
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDropDown()
        {
            return await _formStepService.GetFormGroupDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 表单类别下拉框")]
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDropDown(GetFormTypeDropDown getFormTypeDropDown)
        {
            return await _formStepService.GetFormTypeDropDown(getFormTypeDropDown);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 审批人选取方式下拉框")]
        public async Task<Result<List<AssignmentDropDto>>> GetAssignmentDropDown()
        {
            return await _formStepService.GetAssignmentDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 部门下拉框")]
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDropDown()
        {
            return await _formStepService.GetDepartmentDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[表单流程步骤] 部门级别下拉框")]
        public async Task<Result<List<DepartmentLevelDropDto>>> GetDepartmentLevelDropDown()
        {
            return await _formStepService.GetDepartmentLevelDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[员工信息] 职级下拉框")]
        public async Task<Result<List<UserPositionDropDto>>> GetUserPositionDropDown()
        {
            return await _formStepService.GetUserPositionDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[员工信息] 职业下拉框")]
        public async Task<Result<List<UserLaborDropDto>>> GetLaborDropDown()
        {
            return await _formStepService.GetLaborDropDown();
        }
    }
}
