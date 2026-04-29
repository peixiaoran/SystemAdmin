using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Commands;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Dtp;
using SystemAdmin.Model.FormBusiness.Workflow.ReviewFlowManager;
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
        public async Task<Result<List<LeaveTypeDropDto>>> GetLeaveTypeDrop()
        {
            return await _leaveFormService.GetLeaveTypeDrop();
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[请假单] 初始化请假单")]
        public async Task<Result<LeaveFormDto>> InitializeLevel([FromForm] string formTypeId)
        {
            return await _leaveFormService.InitializeLevel(formTypeId);
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
        [EndpointSummary("[请假单] 查询请假单明细")]
        public async Task<Result<LeaveFormDto>> GetLeaveForm([FromForm] string formId)
        {
            return await _leaveFormService.GetLeaveForm(formId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[请假单] 请假单上传附件")]
        public async Task<Result<List<FormAttachmentDto>>> UploadAttachment([FromForm] string formId, List<IFormFile> files)
        {
            return await _leaveFormService.UploadAttachment(formId, files);
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[请假单] 请假单删除附件")]
        public async Task<Result<int>> DeleteAttachment([FromForm] string attachmentId, [FromForm] string attachmentPath)
        {
            return await _leaveFormService.DeleteAttachment(attachmentId, attachmentPath);
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[请假单] 查询请假单流程")]
        public async Task<Result<FormReview>> GetFullReviewFlow([FromForm] string formId)
        {
            return await _leaveFormService.GetFullReviewFlow(formId);
        }
    }
}
