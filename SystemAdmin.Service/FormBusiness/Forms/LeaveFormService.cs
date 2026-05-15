using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Commands;
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
        private readonly FormPermissionChecker _formChecker;
        private readonly LeaveFormRepository _leaveForm;
        private readonly FormManager _formmanger;
        private readonly FormReviewFlow _formflow;
        private readonly LocalizationService _localization;
        private readonly string _form = "FormBusiness.Forms.";

        public LeaveFormService(CurrentUser loginuser, ILogger<LeaveFormService> logger, SqlSugarScope db, FormPermissionChecker formchecker,  LeaveFormRepository leaveForm, FormManager formmanger, FormReviewFlow formflow, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _leaveForm = leaveForm;
            _formChecker = formchecker;
            _formmanger = formmanger;
            _formflow = formflow;
            _localization = localization;
        }

        /// <summary>
        /// 请假类别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<LeaveTypeDropDto>>> GetLeaveTypeDrop()
        {
            var drop = await _leaveForm.GetLeaveTypeDrop();
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
                var isCan = await _formChecker.CanApply(long.Parse(formTypeId));
                if (!isCan)
                {
                    return Result<LeaveFormDto>.Failure(400, _localization.ReturnMsg($"{_form}NotCanApply"));
                }
                else
                {
                    await _db.BeginTranAsync();
                    var formId = await _formmanger.InitializeFormInstance(long.Parse(formTypeId));
                    var leaveForm = new LeaveFormEntity()
                    {
                        FormId = long.Parse(formId),
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
                    await _formmanger.MatchWorkflowRuleAsync(long.Parse(formTypeId), long.Parse(formId));
                    await _db.CommitTranAsync();

                    var leaveFormDto = await _leaveForm.GetLeaveForm(long.Parse(formId));
                    leaveFormDto.RejectStepDrop = await _formflow.GetRejectStepDrop(long.Parse(formId));
                    leaveFormDto.AttachmentList = await _formmanger.GetAttachmentList(long.Parse(formId));
                    leaveFormDto.ReviewRecordList = await _formmanger.GetReviewRecordList(long.Parse(formId));
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
                var entity = new LeaveFormEntity()
                {
                    FormId = long.Parse(save.FormId),
                    LeaveTypeCode = save.LeaveTypeCode,
                    LeaveReason = save.LeaveReason,
                    LeaveStartTime = save.LeaveStartTime,
                    LeaveEndTime = save.LeaveEndTime,
                    LeaveDays = save.LeaveDays,
                    AgentUserNo = save.AgentUserNo,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now
                };
                await _db.BeginTranAsync();
                var count = await _leaveForm.SaveLeaveForm(entity);
                await _formmanger.SaveFormInstance(long.Parse(save.FormId));
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
                var isCan = await _formChecker.CanView(long.Parse(formId));
                if (!isCan)
                {
                    return Result<LeaveFormDto>.Failure(400, _localization.ReturnMsg($"{_form}NotCanView"));
                }

                var form = await _leaveForm.GetLeaveForm(long.Parse(formId));
                form.RejectStepDrop = await _formflow.GetRejectStepDrop(long.Parse(formId));
                form.AttachmentList = await _formmanger.GetAttachmentList(long.Parse(formId));
                form.ReviewRecordList = await _formmanger.GetReviewRecordList(long.Parse(formId));
                return Result<LeaveFormDto>.Ok(form);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<LeaveFormDto>.Failure(500, ex.Message);
            }
        }
    }
}
