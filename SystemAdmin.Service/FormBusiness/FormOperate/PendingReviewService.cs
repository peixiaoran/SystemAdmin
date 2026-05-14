using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormOperate.Dto;
using SystemAdmin.Model.FormBusiness.FormOperate.Queries;
using SystemAdmin.Repository.FormBusiness.FormOperate;
using SystemAdmin.Repository.FormBusiness.Workflow;

namespace SystemAdmin.Service.FormBusiness.FormOperate
{
    public class PendingReviewService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<PendingReviewService> _logger;
        private readonly SqlSugarScope _db;
        private readonly FormPermissionChecker _formChecker;
        private readonly PendingReviewRepository _pendingReviewRepo;
        private readonly FormReviewFlow _reviewFlow;
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness.FormOperate.PendingSubApp";

        public PendingReviewService(CurrentUser loginuser, ILogger<PendingReviewService> logger, SqlSugarScope db, FormPermissionChecker formChecker, PendingReviewRepository pendingReviewRepo, FormReviewFlow reviewFlow, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _formChecker = formChecker;
            _pendingReviewRepo = pendingReviewRepo;
            _reviewFlow = reviewFlow;
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
                var drop = await _pendingReviewRepo.GetFormGroupDrop();
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
                var drop = await _pendingReviewRepo.GetFormTypeDrop(long.Parse(formGroupId));
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
                var drop = await _pendingReviewRepo.GetFormStatusDrop();
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
        public async Task<ResultPaged<PendingReviewDto>> GetPendingSubmissionPage(GetPendingSubAppPage getpage)
        {
            try
            {
                return await _pendingReviewRepo.GetPendingSubmissionPage(getpage, _loginuser.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<PendingReviewDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询待审批分页
        /// </summary>
        /// <returns></returns>
        public async Task<ResultPaged<PendingReviewDto>> GetPendingReviewPage(GetPendingSubAppPage getpage)
        {
            try
            {
                return await _pendingReviewRepo.GetPendingReviewPage(getpage, _loginuser.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<PendingReviewDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询表单待审批人列表
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<Result<List<FormPendingReviewDto>>> GetFormPendingReview(string formId)
        {
            try
            {
                var list = await _pendingReviewRepo.GetFormPendingReviewUser(long.Parse(formId));
                return Result<List<FormPendingReviewDto>>.Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormPendingReviewDto>>.Failure(500, ex.Message);
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
                var isCan = await _formChecker.CanVoided(long.Parse(formId));
                if (!isCan)
                {
                    return Result<int>.Ok(500, _localization.ReturnMsg($"{_this}NotCanVoided"));
                }
                var count = await _pendingReviewRepo.VoidedForm(long.Parse(formId), _loginuser.UserId);
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
