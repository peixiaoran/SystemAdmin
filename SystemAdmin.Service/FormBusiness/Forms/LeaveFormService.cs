using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormOperate.Dto;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Entity;
using SystemAdmin.Repository.FormBusiness.Forms;
using SystemAdmin.Repository.FormBusiness.Workflow;

namespace SystemAdmin.Service.FormBusiness.Forms
{
    public class LeaveFormService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<LeaveFormService> _logger;
        private readonly SqlSugarScope _db;
        private readonly FileUploadOptions _fileUploadOptions;
        private readonly MinioService _minioService;
        private readonly FormRepository _formRepository;
        private readonly LeaveFormRepository _leaveForm;
        private readonly LocalizationService _localization;
        //private readonly string _this = "FormBusiness.Forms.LeaveForm";
        private readonly string _publicthis = "FormBusiness.Forms.";

        public LeaveFormService(CurrentUser loginuser, ILogger<LeaveFormService> logger, SqlSugarScope db, IOptions<FileUploadOptions> fileUploadOptions, MinioService minioService, FormRepository formRepository, LeaveFormRepository leaveForm, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _fileUploadOptions = fileUploadOptions.Value;
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
        
        public async Task<Result<LeaveFormDto>> InitializeLevel(string formTypeId)
        {
            var formId = await _formRepository.InitializeFormInstance(formTypeId);

            if (formId < 0)
            {
                return Result<LeaveFormDto>.Failure(500, _localization.ReturnMsg($"{_publicthis}InitializeFailed"));
            }
            else
            {
                
            }
            return Result<LeaveFormDto>.Ok(null);
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<Result<List<FormAttachmentDto>>> UploadFile(string formId, List<IFormFile> files)
        {
            try
            {
                if (files == null || files.Count == 0)
                {
                    return Result<List<FormAttachmentDto>>.Failure(400, _localization.ReturnMsg($"{_publicthis}FileNotNull"));
                }

                long maxFileSize = _fileUploadOptions.MaxSizeMB * 1024L * 1024L;

                var formFileList = new List<FormAttachmentDto>();

                await _db.BeginTranAsync();
                foreach (var file in files)
                {
                    if (file == null || file.Length == 0)
                    {
                        return Result<List<FormAttachmentDto>>.Failure(400, _localization.ReturnMsg($"{_publicthis}FileNotNull"));
                    }

                    if (file.Length > maxFileSize)
                    {
                        return Result<List<FormAttachmentDto>>.Failure(400, _localization.ReturnMsg($"{_publicthis}FileSizeLimit"));
                    }

                    var fileExt = Path.GetExtension(file.FileName)?.ToLowerInvariant();

                    if (string.IsNullOrWhiteSpace(fileExt) || !_fileUploadOptions.AllowExtensions.Contains(fileExt))
                    {
                        return Result<List<FormAttachmentDto>>.Failure(400, _localization.ReturnMsg($"{_publicthis}FileExtensionNotAllow"));
                    }

                    using var stream = file.OpenReadStream();

                    var avatarUrl = await _minioService.UploadAsync(file.FileName, stream, file.ContentType);

                    int fileSizeKb = (int)(file.Length / 1024);

                    var fileItem = new FormAttachmentEntity
                    {
                        AttachmentId = SnowFlakeSingle.Instance.NextId(),
                        FormId = long.Parse(formId),
                        AttachmentName = file.FileName,
                        AttachmentPath = avatarUrl.ToString(),
                        AttachmentSize = fileSizeKb,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now
                    };

                    var fileItemDto = fileItem.Adapt<FormAttachmentDto>();

                    var count = await _leaveForm.InsertFile(fileItem);
                    formFileList.Add(fileItemDto);
                }
                await _db.CommitTranAsync();

                return Result<List<FormAttachmentDto>>.Ok(formFileList, _localization.ReturnMsg($"{_publicthis}UploadSuccess"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<List<FormAttachmentDto>>.Failure(500, _localization.ReturnMsg($"{_publicthis}UploadFailed"));
            }
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteFile(string fileId, string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(fileId))
                {
                    return Result<int>.Failure(400, _localization.ReturnMsg($"{_publicthis}FileIdNotNull"));
                }

                await _db.BeginTranAsync();
                var count = await _leaveForm.DeleteFormFile(long.Parse(fileId));
                await _db.CommitTranAsync();

                await _minioService.DeleteAsync(filePath);
                return Result<int>.Ok(count, "");
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, _localization.ReturnMsg($"{_publicthis}DeleteFileFailed"));
            }
        }
    }
}
