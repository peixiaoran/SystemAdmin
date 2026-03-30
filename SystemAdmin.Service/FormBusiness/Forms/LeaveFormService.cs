using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Dto;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto;
using SystemAdmin.Repository.FormBusiness.Forms;

namespace SystemAdmin.Service.FormBusiness.Forms
{
    public class LeaveFormService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<LeaveFormService> _logger;
        private readonly SqlSugarScope _db;
        private readonly MinioService _minioService;
        private readonly LeaveFormRepository _leaveForm;
        private readonly LocalizationService _localization;
        //private readonly string _this = "FormBusiness.Forms.LeaveForm";
        private readonly string _publicthis = "FormBusiness.Forms.";

        public LeaveFormService(CurrentUser loginuser, ILogger<LeaveFormService> logger, SqlSugarScope db, MinioService minioService, LeaveFormRepository leaveForm, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _minioService = minioService;
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
        /// 上传附件
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<Result<List<FormFileDto>>> UploadFile(string formId, List<IFormFile> files)
        {
            try
            {
                if (files == null || files.Count == 0)
                {
                    return Result<List<FormFileDto>>.Failure(400, _localization.ReturnMsg($"{_publicthis}FileNotNull"));
                }

                const long maxFileSize = 25 * 1024 * 1024;

                var formFileList = new List<FormFileDto>();

                foreach (var file in files)
                {
                    if (file == null || file.Length == 0)
                    {
                        return Result<List<FormFileDto>>.Failure(400, _localization.ReturnMsg($"{_publicthis}FileNotNull"));
                    }

                    if (file.Length > maxFileSize)
                    {
                        return Result<List<FormFileDto>>.Failure(400, _localization.ReturnMsg($"{_publicthis}FileSizeLimit"));
                    }

                    using var stream = file.OpenReadStream();

                    var avatarUrl = await _minioService.UploadAsync(file.FileName, stream, file.ContentType);

                    int fileSizeKb = (int)(file.Length / 1024);

                    var fileItem = new FormFileEntity()
                    {
                        FileId = SnowFlakeSingle.Instance.NextId(),
                        FormId = long.Parse(formId),
                        FileName = file.FileName,
                        FilePath = avatarUrl.ToString(),
                        FileSize = fileSizeKb,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now,
                    };

                    var fileItemDto = fileItem.Adapt<FormFileDto>();

                    var uploadCount = await _leaveForm.InsertFile(fileItem);
                    formFileList.Add(fileItemDto);
                }

                return Result<List<FormFileDto>>.Ok(formFileList, _localization.ReturnMsg($"{_publicthis}UploadSuccess"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormFileDto>>.Failure(500, _localization.ReturnMsg($"{_publicthis}UploadFailed"));
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

                var count = await _leaveForm.DeleteFormFile(long.Parse(fileId));
                await _minioService.DeleteAsync(filePath);
                return Result<int>.Ok(count, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, _localization.ReturnMsg($"{_publicthis}DeleteFileFailed"));
            }
        }
    }
}
