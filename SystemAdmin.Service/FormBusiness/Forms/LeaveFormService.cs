using Mapster;
using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.Enum;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Commands;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Entity;
using SystemAdmin.Repository.FormBusiness.Enum;
using SystemAdmin.Repository.FormBusiness.Forms;
using SystemAdmin.Repository.FormBusiness.WorkflowLifecycle;
using SystemAdmin.Service.FormBusiness.FormBasicInfo;

namespace SystemAdmin.Service.FormBusiness.Forms
{
    public class LeaveFormService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<ControlInfoService> _logger;
        private readonly SqlSugarScope _db;
        private readonly FormAuthRepository _formAuthRepository;
        private readonly ApproverSelectionHandler _workflowStart;
        private readonly LeaveFormRepository _leaveFormRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness.Forms.LeaveForm";

        public LeaveFormService(CurrentUser loginuser, ILogger<ControlInfoService> logger, SqlSugarScope db, FormAuthRepository formAuthRepository, ApproverSelectionHandler workflowStart, LeaveFormRepository leaveFormRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _formAuthRepository = formAuthRepository;
            _workflowStart = workflowStart;
            _leaveFormRepository = leaveFormRepository;
            _localization = localization;
        }

        /// <summary>
        /// 初始化请假表单
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<Result<LeaveFormDto>> InitLeaveForm(string formTypeId)
        {
            try
            {
                bool isCanApply = await _formAuthRepository.HasUserApplyFormType(_loginuser.UserId, long.Parse(formTypeId), FormOp.Apply);
                if (!isCanApply)
                {
                    return Result<LeaveFormDto>.Failure(404, "");
                }

                await _db.BeginTranAsync();
                // 初始化表单
                var initFormInfo = await _formAuthRepository.InitFormInfo(_loginuser.UserId, long.Parse(formTypeId));
                // 初始化请假表
                var userInfo = await _leaveFormRepository.GetUserInfo(_loginuser.UserId);
                LeaveFormEntity initleaveFormEntity = new LeaveFormEntity()
                {
                    FormId = initFormInfo.FormId,
                    FormNo = initFormInfo.FormNo,
                    ApplicantTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    ApplicantUserNo = userInfo.UserNo,
                    ApplicantUserName = userInfo.UserName,
                    ApplicantDeptId = userInfo.DetpId,
                    ApplicantDeptName = userInfo.DetpName,
                    LeaveTypeCode = "",
                    LeaveReason = "",
                    LeaveHours = 0,
                    LeaveHandoverUserName = "",
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                await _leaveFormRepository.InitLeaveForm(initleaveFormEntity);
                await _db.CommitTranAsync();

                var leaveFormDto = initleaveFormEntity.Adapt<LeaveFormDto>();
                leaveFormDto.ImportanceCode = ImportanceType.Normal.ToEnumString();
                leaveFormDto.FormTypeId = long.Parse(formTypeId); //表单类别Id
                return Result<LeaveFormDto>.Ok(leaveFormDto);
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<LeaveFormDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 保存请假表单
        /// </summary>
        /// <param name="leaveFormSave"></param>
        /// <returns></returns>
        public async Task<Result<int>> SaveLeaveForm(LeaveFormSave leaveFormSave)
        {
            try
            {
                bool isCanApply = await _formAuthRepository.HasUserApplyFormType(_loginuser.UserId, long.Parse(leaveFormSave.FormTypeId), FormOp.Apply);
                if (!isCanApply)
                {
                    return Result<int>.Failure(404, "");
                }

                await _db.BeginTranAsync();
                // 保存主表单
                var saveFormInfo = await _formAuthRepository.SaveFormInfo(long.Parse(leaveFormSave.FormId), leaveFormSave.Description, FormStatus.PendingSubmission.ToEnumString(), leaveFormSave.ImportanceCode, _loginuser.UserId);
                LeaveFormEntity saveLeaveFormEntity = new LeaveFormEntity()
                {
                    FormId = long.Parse(leaveFormSave.FormId),
                    FormNo = leaveFormSave.FormNo,
                    ApplicantTime = leaveFormSave.ApplicantTime,
                    ApplicantUserNo = leaveFormSave.ApplicantUserNo,
                    ApplicantUserName = leaveFormSave.ApplicantUserName,
                    ApplicantDeptId = leaveFormSave.ApplicantDeptId,
                    ApplicantDeptName = leaveFormSave.ApplicantDeptName,
                    LeaveTypeCode = leaveFormSave.LeaveTypeCode,
                    LeaveReason = leaveFormSave.LeaveReason,
                    LeaveStartTime = leaveFormSave.LeaveStartTime,
                    LeaveEndTime = leaveFormSave.LeaveEndTime,
                    LeaveHours = leaveFormSave.LeaveHours,
                    LeaveHandoverUserName = leaveFormSave.LeaveHandoverUserName,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                // 保存请假表单
                int saveLeaveFormCount = await _leaveFormRepository.SaveLeaveForm(saveLeaveFormEntity);
                await _db.CommitTranAsync();
                return saveFormInfo >= 1 && saveLeaveFormCount >= 1
                       ? Result<int>.Ok(saveLeaveFormCount, _localization.ReturnMsg($"{_this}SaveSuccess"))
                       : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}SaveFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询请假表单详情
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<Result<LeaveFormDto>> GetLeaveForm(string formId)
        {
            try
            {
                var leaveForm = await _leaveFormRepository.GetLeaveForm(long.Parse(formId));
                return Result<LeaveFormDto>.Ok(leaveForm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<LeaveFormDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 请假类别下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<LeaveTypeDropDto>>> GetLeaveTypeDropDown()
        {
            var leaveTypeDrop = await _leaveFormRepository.GetLeaveTypeDropDown();
            return Result<List<LeaveTypeDropDto>>.Ok(leaveTypeDrop);
        }

        /// <summary>
        /// 重要程度下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<ImportanceDropDto>>> GetImportanceDropDown()
        {
            var leaveTypeDrop = await _formAuthRepository.GetImportanceDropDown();
            return Result<List<ImportanceDropDto>>.Ok(leaveTypeDrop);
        }
    }
}
