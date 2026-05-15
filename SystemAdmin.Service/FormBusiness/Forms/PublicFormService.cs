using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Dto;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Entity;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Upsert;
using SystemAdmin.Model.FormBusiness.Workflow.FormReviewFlow.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Dto;
using SystemAdmin.Repository.FormBusiness.Workflow;

namespace SystemAdmin.Service.FormBusiness.Forms
{
    public class PublicFormService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<PublicFormService> _logger;
        private readonly JwtTokenService _jwt;
        private readonly SqlSugarScope _db;
        private readonly FileUploadOptions _attachmentUpload;
        private readonly MinioService _minioService;
        private readonly FormManager _formmanger;
        private readonly FormPermissionChecker _formchecker;
        private readonly FormReviewAction _formction;
        private readonly FormReviewFlow _formflow;
        private readonly LocalizationService _localization;
        private readonly string _form = "FormBusiness.Forms.";

        public PublicFormService(CurrentUser loginuser, ILogger<PublicFormService> logger, SqlSugarScope db, IOptions<FileUploadOptions> attachmentUpload, MinioService minioService, JwtTokenService jwt, FormManager formmanger, FormPermissionChecker formchecker, FormReviewAction formaction, FormReviewFlow formflow, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _attachmentUpload = attachmentUpload.Value;
            _minioService = minioService;
            _jwt = jwt;
            _formmanger = formmanger;
            _formchecker = formchecker;
            _formction = formaction;
            _formflow = formflow;
            _localization = localization;
        }

        /// <summary>
        /// 查询表单通知Token信息
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <param name="tokenValue"></param>
        /// <returns></returns>
        public async Task<Result<SysUserLoginReturnDto>> GetFormNotificationToken(HttpResponse httpResponse, string tokenValue)
        {
            var entity = await _formmanger.GetFormNotificationTokenWithUser(tokenValue);
            if (entity == null)
            {
                return Result<SysUserLoginReturnDto>.Failure(400, _localization.ReturnMsg($"{_form}NotCanView"));
            }
            else
            {
                _jwt.SetAuthCookie(httpResponse, userId: entity.UserId, userNo: entity.UserNo);

                // 返回登录成功信息（JWT Cookie 已在其他层处理）
                return Result<SysUserLoginReturnDto>.Ok(
                    new SysUserLoginReturnDto
                    {
                        UserNo = entity.UserNo,
                        UserNameCn = entity.UserNameCn,
                        UserNameEn = entity.UserNameEn,
                        AvatarAddress = entity.AvatarAddress
                    }, ""
                );
            }
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="attachments"></param>
        /// <returns></returns>
        public async Task<Result<List<FormAttachmentDto>>> UploadAttachment(string formId, List<IFormFile> attachments)
        {
            try
            {
                if (attachments == null || attachments.Count == 0)
                {
                    return Result<List<FormAttachmentDto>>.Failure(400, _localization.ReturnMsg($"{_form}AttachmentNotNull"));
                }

                long maxAttachmentSize = _attachmentUpload.MaxSizeMB * 1024L * 1024L;
                var formAttachmentList = new List<FormAttachmentDto>();

                await _db.BeginTranAsync();
                foreach (var attachment in attachments)
                {
                    if (attachment == null || attachment.Length == 0)
                    {
                        return Result<List<FormAttachmentDto>>.Failure(400, _localization.ReturnMsg($"{_form}AttachmentNotNull"));
                    }
                    if (attachment.Length > maxAttachmentSize)
                    {
                        return Result<List<FormAttachmentDto>>.Failure(400, _localization.ReturnMsg($"{_form}AttachmentSizeLimit"));
                    }

                    var attachmentExt = Path.GetExtension(attachment.FileName)?.ToLowerInvariant();
                    if (string.IsNullOrWhiteSpace(attachmentExt) || !_attachmentUpload.AllowExtensions.Contains(attachmentExt))
                    {
                        return Result<List<FormAttachmentDto>>.Failure(400, _localization.ReturnMsg($"{_form}AttachmentExtensionNotAllow"));
                    }

                    using var stream = attachment.OpenReadStream();
                    var avatarUrl = await _minioService.UploadFile(attachment.FileName, stream, attachment.ContentType);

                    int attachmentSizeKb = (int)(attachment.Length / 1024);

                    var attachmentItem = new FormAttachmentEntity
                    {
                        AttachmentId = SnowFlakeSingle.Instance.NextId(),
                        FormId = long.Parse(formId),
                        AttachmentName = attachment.FileName,
                        AttachmentPath = avatarUrl.ToString(),
                        AttachmentSize = attachmentSizeKb,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now
                    };
                    var attachmentItemDto = attachmentItem.Adapt<FormAttachmentDto>();
                    var count = await _formmanger.InsertAttachment(attachmentItem);
                    formAttachmentList.Add(attachmentItemDto);
                }
                await _db.CommitTranAsync();

                return Result<List<FormAttachmentDto>>.Ok(formAttachmentList, _localization.ReturnMsg($"{_form}UploadSuccess"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<List<FormAttachmentDto>>.Failure(500, _localization.ReturnMsg($"{_form}UploadFailed"));
            }
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentPath"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteAttachment(string attachmentId, string attachmentPath)
        {
            try
            {
                if (string.IsNullOrEmpty(attachmentId))
                {
                    return Result<int>.Failure(400, _localization.ReturnMsg($"{_form}AttachmentIdNotNull"));
                }

                await _db.BeginTranAsync();
                var count = await _formmanger.DeleteAttachment(long.Parse(attachmentId));
                await _db.CommitTranAsync();

                await _minioService.DeleteFile(attachmentPath);
                return Result<int>.Ok(count, "");
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, _localization.ReturnMsg($"{_form}DeleteAttachmentFailed"));
            }
        }

        /// <summary>
        /// 查询完整审批流程
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<Result<FormReview>> GetFullReviewFlow(string formId)
        {
            try
            {
                var fullflow = await _formflow.GetFullReviewFlow(long.Parse(formId));
                return Result<FormReview>.Ok(fullflow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<FormReview>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 表单核准
        /// </summary>
        /// <param name="reviewForm"></param>
        /// <returns></returns>
        public async Task<Result<bool>> FromApprove(ReviewForm reviewForm)
        {
            try
            {
                var isCan = await _formchecker.CanReview(long.Parse(reviewForm.FormId));
                if (!isCan)
                {
                    return Result<bool>.Failure(400, _localization.ReturnMsg($"{_form}NotCanReview"));
                }

                await _db.BeginTranAsync();
                var result = await _formction.FromApprove(reviewForm);
                await _db.CommitTranAsync();

                return Result<bool>.Ok(result);
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<bool>.Failure(500, ex.Message);
            }
        }
    }
}
