using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Commands;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto;
using SystemAdmin.Service.FormBusiness.Forms;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.FormBusiness.Forms
{
    [JwtAuthorize]
    [Route("api/FormBusiness/Forms/[controller]/[action]")]
    [ApiController]
    public class LeaveForm : ControllerBase
    {
        private readonly LeaveFormService _leaveFormService;
        public LeaveForm(LeaveFormService leaveFormService)
        {
            _leaveFormService = leaveFormService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[请假单] 初始化请假单")]
        public async Task<Result<LeaveFormDto>> InitLeaveForm([FromForm] string formTypeId)
        {
            return await _leaveFormService.InitLeaveForm(formTypeId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[请假单] 保存请假单")]
        public async Task<Result<int>> SaveLeaveForm([FromBody] LeaveFormSave leaveFormSave)
        {
            return await _leaveFormService.SaveLeaveForm(leaveFormSave);
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[请假单] 查询请假单详情")]
        public async Task<Result<LeaveFormDto>> GetLeaveForm([FromForm] string formId)
        {
            return await _leaveFormService.GetLeaveForm(formId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[请假单] 请假类别下拉框")]
        public async Task<Result<List<LeaveTypeDropDto>>> GetLeaveTypeDropDown()
        {
            return await _leaveFormService.GetLeaveTypeDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[请假单] 重要程度下拉框")]
        public async Task<Result<List<ImportanceDropDto>>> GetImportanceDropDown()
        {
            return await _leaveFormService.GetImportanceDropDown();
        }
    }
}
