using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Dto;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
using SystemAdmin.Model.FormBusiness.Forms.FormLifecycle.FormBeforeStart;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Commands;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Entity;
using SystemAdmin.Repository.FormBusiness.FormLifecycle;
using SystemAdmin.Repository.FormBusiness.Forms;
using SystemAdmin.Service.FormBusiness.FormBasicInfo;

namespace SystemAdmin.Service.FormBusiness.Forms
{
    public class LeaveFormService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<LeaveFormService> _logger;
        private readonly SqlSugarScope _db;
        private readonly FormBeforeStart _formBeforeStart;
        private readonly MinioService _minioService;
        private readonly LeaveFormRepository _leaveFormRepository;
        private readonly LocalizationService _localization;
        //private readonly string _this = "FormBusiness.Forms.LeaveForm";
        private readonly string _publicthis = "FormBusiness.Forms.";

        public LeaveFormService(CurrentUser loginuser, ILogger<LeaveFormService> logger, SqlSugarScope db, MinioService minioService, FormBeforeStart formBeforeStart, LeaveFormRepository leaveFormRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _formBeforeStart = formBeforeStart;
            _minioService = minioService;
            _leaveFormRepository = leaveFormRepository;
            _localization = localization;
        }

        /// <summary>
        /// 请假类别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<LeaveTypeDropDto>>> GetLeaveTypeDropDown()
        {
            var drop = await _leaveFormRepository.GetLeaveTypeDropDown();
            return Result<List<LeaveTypeDropDto>>.Ok(drop);
        }

        /// <summary>
        /// 初始化请假表单
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<Result<string>> InitLeaveForm(string formTypeId)
        {
            try
            {
                await _db.BeginTranAsync();
                // 初始化表单
                var initForm = await _formBeforeStart.InitFormInfo(_loginuser.UserId, long.Parse(formTypeId));

                // 添加表单待签核人
                var pendingApp = new PendingApprovalEntity
                {
                    FormId = initForm.FormId,
                    AppointmentType = AppointmentType.Primary.ToEnumString(),
                    ApproveUserId = _loginuser.UserId
                };
                var pendingAppCount = await _formBeforeStart.AddPendingApprover(pendingApp);

                var entity = new LeaveFormEntity()
                {
                    FormId = initForm.FormId,
                    FormNo = initForm.FormNo,
                    ApplicantUserId = _loginuser.UserId,
                    LeaveTypeCode = "",
                    LeaveStartTime = DateTime.Now,
                    LeaveEndTime = DateTime.Now,
                    LeaveReason = "",
                    LeaveHours = 0,
                    AgentUserNo = "",
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now
                };
                int initLeaveCount = await _leaveFormRepository.InitLeaveForm(entity);
                await _db.CommitTranAsync();

                return Result<string>.Ok(initForm.FormId.ToString(), "");
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<string>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 保存请假表单
        /// </summary>
        /// <param name="formSave"></param>
        /// <returns></returns>
        public async Task<Result<int>> SaveLeaveForm(LeaveFormSave formSave)
        {
            try
            {
                await _db.BeginTranAsync();
                var saveForm = await _formBeforeStart.SaveFormInfo(long.Parse(formSave.FormId), _loginuser.UserId);
                var entity = new LeaveFormEntity()
                {
                    FormId = long.Parse(formSave.FormId),
                    FormNo = formSave.FormNo,
                    ApplicantUserId = _loginuser.UserId,
                    LeaveTypeCode = formSave.LeaveTypeCode,
                    LeaveReason = formSave.LeaveReason,
                    LeaveStartTime = formSave.LeaveStartTime,
                    LeaveEndTime = formSave.LeaveEndTime,
                    LeaveHours = formSave.LeaveHours,
                    AgentUserNo = formSave.AgentUserNo,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now
                };
                // 保存请假表单
                int saveLeave = await _leaveFormRepository.SaveLeaveForm(entity);
                await _db.CommitTranAsync();

                return saveForm >= 1 && saveLeave >= 1
                        ? Result<int>.Ok(saveForm, _localization.ReturnMsg($"{_publicthis}SaveSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_publicthis}SaveFailed"));
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
                var leaveForm = await _leaveFormRepository.GetLeaveForm(long.Parse(formId));
                leaveForm.FileList = await _leaveFormRepository.GetLeaveFileList(long.Parse(formId));
                return Result<LeaveFormDto>.Ok(leaveForm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<LeaveFormDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询表单审批流程
        /// </summary>
        /// <param name="fromId"></param>
        /// <returns></returns>
        public async Task<Result<List<WorkflowApproveUser>>> GetWorkflowAllApproveUser(string fromId)
        {
            try
            {
                var approveUser = await _formBeforeStart.GetWorkflowAllApproveUser(long.Parse(fromId));
                return Result<List<WorkflowApproveUser>>.Ok(approveUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<WorkflowApproveUser>>.Failure(500, ex.Message);
            }
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

                    var uploadCount = await _leaveFormRepository.InsertFile(fileItem);
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

                var count = await _leaveFormRepository.DeleteFormFile(long.Parse(fileId));
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
