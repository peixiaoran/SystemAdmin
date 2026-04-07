using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.FormBusiness.FormOperate.Dto;
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
        public async Task<Result<LeaveFormDto>> InitializeLevel(string formTypeId)
        {
            return await _leaveFormService.InitializeLevel(formTypeId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[请假单] 查询请假单明细")]
        public async Task<Result<LeaveFormDto>> GetLeaveFormDto(string formId)
        {
            return await _leaveFormService.GetLeaveFormDto(formId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[请假单] 保存请假单")]
        public async Task<Result<int>> SaveLeaveForm([FromBody] LeaveFormSave save)
        {
            return await _leaveFormService.SaveLeaveForm(save);
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[请假单] 上传附件")]
        public async Task<Result<List<FormAttachmentDto>>> UploadFile([FromForm] string formId, List<IFormFile> files)
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
    }
}
