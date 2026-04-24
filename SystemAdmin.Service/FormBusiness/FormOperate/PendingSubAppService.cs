using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormOperate.Dto;
using SystemAdmin.Model.FormBusiness.FormOperate.Queries;
using SystemAdmin.Repository.FormBusiness.FormOperate;

namespace SystemAdmin.Service.FormBusiness.FormOperate
{
    public class PendingSubAppService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<PendingSubAppService> _logger;
        private readonly SqlSugarScope _db;
        private readonly PendingSubAppRepository _pendingSubReviewRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness.FormOperate.PendingSubApp";

        public PendingSubAppService(CurrentUser loginuser, ILogger<PendingSubAppService> logger, SqlSugarScope db, PendingSubAppRepository PendingSubAppRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _pendingSubReviewRepository = PendingSubAppRepository;
            _localization = localization;
        }

        /// <summary>
        /// 表单组别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDrop()
        {
            try
            {
                var drop = await _pendingSubReviewRepository.GetFormGroupDrop();
                return Result<List<FormGroupDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormGroupDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 表单类别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDrop(string formGroupId)
        {
            try
            {
                var drop = await _pendingSubReviewRepository.GetFormTypeDrop(long.Parse(formGroupId));
                return Result<List<FormTypeDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormTypeDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 表单状态下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<FormStatusDropDto>>> GetFormStatusDrop()
        {
            try
            {
                var drop = await _pendingSubReviewRepository.GetFormStatusDrop();
                return Result<List<FormStatusDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormStatusDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询待送审分页
        /// </summary>
        /// <returns></returns>
        public async Task<ResultPaged<PendingSubAppDto>> GetPendingSubmissionPage(GetPendingSubAppPage getpage)
        {
            try
            {
                return await _pendingSubReviewRepository.GetPendingSubmissionPage(getpage, _loginuser.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<PendingSubAppDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询待签核分页
        /// </summary>
        /// <returns></returns>
        public async Task<ResultPaged<PendingSubAppDto>> GetPendingApprovalPage(GetPendingSubAppPage getpage)
        {
            try
            {
                return await _pendingSubReviewRepository.GetPendingApprovalPage(getpage, _loginuser.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<PendingSubAppDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 作废表单
        /// </summary>
        /// <returns></returns>
        public async Task<Result<int>> VoidedForm(string formId)
        {
            try
            {
                await _db.BeginTranAsync();
                var isCan = await _pendingSubReviewRepository.IsVoidedForm(long.Parse(formId));
                if (!isCan)
                {
                    return Result<int>.Ok(500, _localization.ReturnMsg($"{_this}NotVoided"));
                }
                var count = await _pendingSubReviewRepository.VoidedForm(long.Parse(formId), _loginuser.UserId);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}VoidedSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}VoidedFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }
    }
}
