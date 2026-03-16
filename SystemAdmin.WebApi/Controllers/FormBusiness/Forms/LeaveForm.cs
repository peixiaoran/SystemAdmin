using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Commands;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto;
using SystemAdmin.Model.FormBusiness.FormLifecycle.FormBeforeStart;
using SystemAdmin.Service.FormBusiness.Forms;

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
        public async Task<Result<string>> InitLeaveForm([FromForm] string formTypeId)
        {
            return await _leaveFormService.InitLeaveForm(formTypeId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[请假单] 查询请假单明细")]
        public async Task<Result<LeaveFormDto>> GetLeaveForm([FromForm] string formId)
        {
            return await _leaveFormService.GetLeaveForm(formId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[请假单] 保存请假单")]
        public async Task<Result<int>> SaveLeaveForm([FromBody] LeaveFormSave formSave)
        {
            return await _leaveFormService.SaveLeaveForm(formSave);
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[请假单] 请假类别下拉")]
        public async Task<Result<List<LeaveTypeDropDto>>> GetLeaveTypeDropDown()
        {
            return await _leaveFormService.GetLeaveTypeDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[请假单] 查询表单审批流程")]
        public async Task<Result<List<WorkflowApproveUser>>> GetWorkflowAllApproveUser([FromForm] string fromId)
        {
            return await _leaveFormService.GetWorkflowAllApproveUser(fromId);
        }
    }
}
