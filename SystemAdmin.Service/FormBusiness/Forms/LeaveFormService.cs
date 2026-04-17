using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Commands;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Entity;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Dtp;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Entity;
using SystemAdmin.Repository.FormBusiness.Forms;
using SystemAdmin.Repository.FormBusiness.Workflow;

namespace SystemAdmin.Service.FormBusiness.Forms
{
    public class LeaveFormService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<LeaveFormService> _logger;
        private readonly SqlSugarScope _db;
        private readonly FileUploadOptions _attachmentUploadOptions;
        private readonly MinioService _minioService;
        private readonly FormManager _formRepository;
        private readonly LeaveFormRepository _leaveForm;
        private readonly LocalizationService _localization;
        private readonly string _form = "FormBusiness.Forms.";

        public LeaveFormService(CurrentUser loginuser, ILogger<LeaveFormService> logger, SqlSugarScope db, IOptions<FileUploadOptions> attachmentUploadOptions, MinioService minioService, FormManager formRepository, LeaveFormRepository leaveForm, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _attachmentUploadOptions = attachmentUploadOptions.Value;
            _minioService = minioService;
            _formRepository = formRepository;
            _leaveForm = leaveForm;
            _localization = localization;
        }

        /// <summary>
        /// 请假类别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<LeaveTypeDropDto>>> GetLeaveTypeDropDown()
        {
            var drop = await _leaveForm.GetLeaveTypeDropDown();
            return Result<List<LeaveTypeDropDto>>.Ok(drop);
        }
        
        /// <summary>
        /// 初始化表单
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<Result<LeaveFormDto>> InitializeLevel(string formTypeId)
        {
            try
            {
                await _db.BeginTranAsync();
                var formId = await _formRepository.InitializeFormInstance(formTypeId);
                if (string.IsNullOrEmpty(formId))
                {
                    await _db.RollbackTranAsync();
                    return Result<LeaveFormDto>.Failure(500, _localization.ReturnMsg($"{_form}InitializeFailed"));
                }
                else
                {
                    var leaveForm = new LeaveFormEntity()
                    {
                        FormId = long.Parse(formId),
                        ApplicantUserId = _loginuser.UserId,
                        LeaveTypeCode = "",
                        LeaveReason = "",
                        LeaveStartTime = null,
                        LeaveEndTime = null,
                        LeaveDays = 0,
                        AgentUserNo = "",
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now
                    };
                    await _leaveForm.InitLeaveForm(leaveForm);
                    await _db.CommitTranAsync();

                    var leaveFormDto = await _leaveForm.GetLeaveForm(long.Parse(formId));
                    leaveFormDto.AttachmentList = await _leaveForm.GetAttachmentList(long.Parse(formId));

                    return Result<LeaveFormDto>.Ok(leaveFormDto);
                }
                
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<LeaveFormDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 保存请假单
        /// </summary>
        /// <param name="save"></param>
        /// <returns></returns>
        public async Task<Result<int>> SaveLeaveForm(LeaveFormSave save)
        {
            try
            {
                await _db.BeginTranAsync();
                var entity = new LeaveFormEntity()
                {
                    FormId = long.Parse(save.FormId),
                    ApplicantUserId = _loginuser.UserId,
                    LeaveTypeCode = save.LeaveTypeCode,
                    LeaveReason = save.LeaveReason,
                    LeaveStartTime = save.LeaveStartTime,
                    LeaveEndTime = save.LeaveEndTime,
                    LeaveDays = save.LeaveDays,
                    AgentUserNo = save.AgentUserNo,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now
                };
                var count = await _leaveForm.SaveLeaveForm(entity);
                await _formRepository.SaveFormInstance(long.Parse(save.FormId));
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_form}SaveSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_form}SaveFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询请假单明细
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<Result<LeaveFormDto>> GetLeaveForm(string formId)
        {
            try
            {
                var dto = await _leaveForm.GetLeaveForm(long.Parse(formId));
                dto.AttachmentList = await _leaveForm.GetAttachmentList(long.Parse(formId));
                return Result<LeaveFormDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<LeaveFormDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询完整签核流程
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<Result<LeaveFormDto>> GetFullApprovalFlow(string formId)
        {
            try
            {
                var dto = await _leaveForm.GetLeaveForm(long.Parse(formId));
                dto.AttachmentList = await _leaveForm.GetAttachmentList(long.Parse(formId));
                return Result<LeaveFormDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<LeaveFormDto>.Failure(500, ex.Message);
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

                long maxAttachmentSize = _attachmentUploadOptions.MaxSizeMB * 1024L * 1024L;

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

                    if (string.IsNullOrWhiteSpace(attachmentExt) || !_attachmentUploadOptions.AllowExtensions.Contains(attachmentExt))
                    {
                        return Result<List<FormAttachmentDto>>.Failure(400, _localization.ReturnMsg($"{_form}AttachmentExtensionNotAllow"));
                    }

                    using var stream = attachment.OpenReadStream();

                    var avatarUrl = await _minioService.UploadAsync(attachment.FileName, stream, attachment.ContentType);

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

                    var count = await _leaveForm.InsertAttachment(attachmentItem);
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
                var count = await _leaveForm.DeleteAttachment(long.Parse(attachmentId));
                await _db.CommitTranAsync();

                await _minioService.DeleteAsync(attachmentPath);
                return Result<int>.Ok(count, "");
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, _localization.ReturnMsg($"{_form}DeleteAttachmentFailed"));
            }
        }
    }
}
