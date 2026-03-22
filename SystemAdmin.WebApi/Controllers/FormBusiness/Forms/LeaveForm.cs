using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Dto;
using SystemAdmin.Model.FormBusiness.Forms.FormLifecycle.FormBeforeStart;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Commands;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto;
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
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[请假单] 请假类别下拉")]
        public async Task<Result<List<LeaveTypeDropDto>>> GetLeaveTypeDropDown()
        {
            return await _leaveFormService.GetLeaveTypeDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[请假单] 初始化请假单")]
        public async Task<Result<string>> InitLeaveForm([FromForm] string formTypeId)
        {
            return await _leaveFormService.InitLeaveForm(formTypeId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[请假单] 保存请假单")]
        public async Task<Result<int>> SaveLeaveForm([FromBody] LeaveFormSave formSave)
        {
            return await _leaveFormService.SaveLeaveForm(formSave);
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[请假单] 查询请假单明细")]
        public async Task<Result<LeaveFormDto>> GetLeaveForm([FromForm] string formId)
        {
            return await _leaveFormService.GetLeaveForm(formId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[请假单] 上传附件")]
        public async Task<Result<List<FormFileDto>>> UploadFile([FromForm] string formId, List<IFormFile> files)
        {
            return await _leaveFormService.UploadFile(formId, files);
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[请假单] 删除附件")]
        public async Task<Result<int>> DeleteFile([FromForm] string fileId, [FromForm] string filePath)
        {
            return await _leaveFormService.DeleteFile(fileId, filePath);
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[请假单] 查询表单审批流程")]
        public async Task<Result<List<WorkflowApproveUser>>> GetWorkflowAllApproveUser([FromForm] string fromId)
        {
            return await _leaveFormService.GetWorkflowAllApproveUser(fromId);
        }
    }
}
