using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Commands;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Queries;
using SystemAdmin.Repository.FormBusiness.FormWorkflow;

namespace SystemAdmin.Service.FormBusiness.FormWorkflow
{
    public class FormReviewLimitService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<FormReviewLimitService> _logger;
        private readonly SqlSugarScope _db;
        private readonly FormReviewLimitRepository _FormReviewLimitRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness.FormWorkflow.FormReviewLimit";

        public FormReviewLimitService(CurrentUser loginuser, ILogger<FormReviewLimitService> logger, SqlSugarScope db, FormReviewLimitRepository FormReviewLimitRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _FormReviewLimitRepository = FormReviewLimitRepository;
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
                var drop = await _FormReviewLimitRepository.GetFormGroupDrop();
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
        /// <param name="formGroupId"></param>
        /// <returns></returns>
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDrop(string formGroupId)
        {
            try
            {
                var drop = await _FormReviewLimitRepository.GetFormTypeDrop(long.Parse(formGroupId));
                return Result<List<FormTypeDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormTypeDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 新增签核层级上限
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertFormReviewLimit(FormReviewLimitUpsert upsert)
        {
            try
            {
                // 判断表单职级是否有配置最高上限配置
                var isRepat = await _FormReviewLimitRepository.FormReviewLimitRepeat(upsert.FormTypeId, upsert.PositionId);
                if (isRepat)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}Repeat"));
                }
                var entity = new FormReviewLimitEntity()
                {
                    FormTypeId = upsert.FormTypeId,
                    PositionId = upsert.PositionId,
                    MaxPositionId = upsert.MaxPositionId,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now
                };

                await _db.BeginTranAsync();
                var count = await _FormReviewLimitRepository.InsertFormReviewLimit(entity);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}InsertSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 删除签核层级上限
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteFormReviewLimit(string formTypeId, string positionId)
        {
            try
            {
                await _db.BeginTranAsync();
                var count = await _FormReviewLimitRepository.DeleteFormReviewLimit(long.Parse(formTypeId), long.Parse(positionId));
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}DeleteSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DeleteFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 修改签核层级上限
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateFormReviewLimit(FormReviewLimitUpsert upsert)
        {
            try
            {
                var entity = new FormReviewLimitEntity()
                {
                    FormTypeId = upsert.FormTypeId,
                    PositionId = upsert.PositionId,
                    MaxPositionId = upsert.MaxPositionId,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now
                };

                await _db.BeginTranAsync();
                var count = await _FormReviewLimitRepository.UpdateFormReviewLimit(entity);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}UpdateSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdateFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询签核层级上限实体
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public async Task<Result<FormReviewLimitDto>> GetFormReviewLimitEntity(string formTypeId, string positionId)
        {
            try
            {
                var entity = await _FormReviewLimitRepository.GetFormReviewLimitEntity(long.Parse(formTypeId), long.Parse(positionId));
                return Result<FormReviewLimitDto>.Ok(entity);
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<FormReviewLimitDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询签核层级上限分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<FormReviewLimitDto>> GetFormReviewLimitPage(GetFormReviewLimitPage getPage)
        {
            try
            {
                return await _FormReviewLimitRepository.GetFormReviewLimitPage(getPage);
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return ResultPaged<FormReviewLimitDto>.Failure(500, ex.Message);
            }
        }
    }
}
