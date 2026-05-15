
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Dto;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Upsert;
using SystemAdmin.Model.FormBusiness.Workflow.FormReviewFlow.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Dto;
using SystemAdmin.Service.FormBusiness.Forms;

namespace SystemAdmin.WebApi.Controllers.FormBusiness.Forms
{
    [JwtAuthorize]
    [Route("api/FormBusiness/Forms/[controller]/[action]")]
    [ApiController]
    public class PublicForm : ControllerBase
    {
        private readonly PublicFormService _publicFormService;
        public PublicForm(PublicFormService publicFormService)
        {
            _publicFormService = publicFormService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[表单公共接口] 表单邮件Token验证")]
        [AllowAnonymous]
        public async Task<Result<SysUserLoginReturnDto>> UserLogin([FromForm] string tokenValue)
        {
            return await _publicFormService.GetFormNotificationToken(Response, tokenValue);
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[表单公共接口] 上传附件")]
        public async Task<Result<List<FormAttachmentDto>>> UploadAttachment([FromForm] string formId, List<IFormFile> files)
        {
            return await _publicFormService.UploadAttachment(formId, files);
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[表单公共接口] 删除附件")]
        public async Task<Result<int>> DeleteAttachment([FromForm] string attachmentId, [FromForm] string attachmentPath)
        {
            return await _publicFormService.DeleteAttachment(attachmentId, attachmentPath);
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[表单公共接口] 查询表单流程")]
        public async Task<Result<FormReview>> GetFullReviewFlow([FromForm] string formId)
        {
            return await _publicFormService.GetFullReviewFlow(formId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[表单公共接口] 表单核准")]
        public async Task<Result<bool>> FromApprove([FromBody] ReviewForm reviewForm)
        {
            return await _publicFormService.FromApprove(reviewForm);
        }
    }
}
