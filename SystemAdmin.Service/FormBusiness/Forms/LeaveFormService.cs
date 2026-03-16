using Mapster;
using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormLifecycle.FormBeforeStart;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
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
        private readonly ILogger<ControlInfoService> _logger;
        private readonly SqlSugarScope _db;
        private readonly FormBeforeStart _formBeforeStart;
        private readonly LeaveFormRepository _leaveFormRepository;
        private readonly LocalizationService _localization;
        //private readonly string _this = "FormBusiness.Forms.LeaveForm";
        private readonly string _publicthis = "FormBusiness.Forms.";

        public LeaveFormService(CurrentUser loginuser, ILogger<ControlInfoService> logger, SqlSugarScope db, FormBeforeStart formBeforeStart, LeaveFormRepository leaveFormRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _formBeforeStart = formBeforeStart;
            _leaveFormRepository = leaveFormRepository;
            _localization = localization;
        }

        /// <summary>
        /// 请假类别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<LeaveTypeDropDto>>> GetLeaveTypeDropDown()
        {
            var leaveTypeDrop = await _leaveFormRepository.GetLeaveTypeDropDown();
            return Result<List<LeaveTypeDropDto>>.Ok(leaveTypeDrop);
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
                bool isApply = await _formBeforeStart.HasUserApplyFormType(_loginuser.UserId, long.Parse(formTypeId));
                if (!isApply)
                {
                    return Result<LeaveFormDto>.Failure(403, "");
                }

                await _db.BeginTranAsync();
                // 初始化表单
                var initForm = await _formBeforeStart.InitFormInfo(_loginuser.UserId, long.Parse(formTypeId));

                // 添加表单待签核人
                var pendingApp = new PendingApproversEntity
                {
                    FormId = initForm.FormId,
                    ApproveUserId = _loginuser.UserId
                };
                var pendingAppCount = await _formBeforeStart.AddPendingApprover(pendingApp);

                var entity = new LeaveFormEntity()
                {
                    FormId = initForm.FormId,
                    FormNo = initForm.FormNo,
                    ApplicantUserId = _loginuser.UserId,
                    LeaveTypeCode = "",
                    LeaveReason = "",
                    LeaveHours = 0,
                    AgentUserNo = "",
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now
                };
                await _leaveFormRepository.InitLeaveForm(entity);
                await _db.CommitTranAsync();

                var leaveFormDto = entity.Adapt<LeaveFormDto>();
                leaveFormDto.FormTypeId = long.Parse(formTypeId);
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
        /// <param name="formSave"></param>
        /// <returns></returns>
        public async Task<Result<int>> SaveLeaveForm(LeaveFormSave formSave)
        {
            try
            {
                // 判断是否有权限申请该表单类型
                bool isCanApply = await _formBeforeStart.HasUserApplyFormType(_loginuser.UserId, long.Parse(formSave.FormTypeId));
                if (!isCanApply)
                {
                    return Result<int>.Failure(403, _localization.ReturnMsg($"{_publicthis}NotPermissionApply"));
                }

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
        /// 查询表单审批流程
        /// </summary>
        /// <param name="fromId"></param>
        /// <returns></returns>
        public async Task<Result<List<WorkflowApproveUser>>> GetWorkflowAllApproveUser(string fromId)
        {
            try
            {
                var appUser = await _formBeforeStart.GetWorkflowAllApproveUser(long.Parse(fromId));
                return Result<List<WorkflowApproveUser>>.Ok(appUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<WorkflowApproveUser>>.Failure(500, ex.Message);
            }
        }
    }
}
