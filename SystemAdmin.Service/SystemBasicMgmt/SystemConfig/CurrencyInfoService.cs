using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Queries;
using SystemAdmin.Repository.SystemBasicMgmt.SystemConfig;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemConfig
{
    public class CurrencyInfoService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<CurrencyInfoService> _logger;
        private readonly SqlSugarScope _db;
        private readonly CurrencyInfoRepository _currencyInfoRepo;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.SystemConfig.CurrencyInfo";

        public CurrencyInfoService(CurrentUser loginuser, ILogger<CurrencyInfoService> logger, SqlSugarScope db, CurrencyInfoRepository currencyInfoRepo, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _currencyInfoRepo = currencyInfoRepo;
            _localization = localization;
        }

        /// <summary>
        /// 新增币别信息
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertCurrencyInfo(CurrencyInfoUpsert upsert)
        {
            try
            {
                var entity = new CurrencyInfoEntity()
                {
                    CurrencyId = SnowFlakeSingle.Instance.NextId(),
                    CurrencyCode = upsert.CurrencyCode,
                    CurrencyNameCn = upsert.CurrencyNameCn,
                    CurrencyNameEn = upsert.CurrencyNameEn,
                    SortOrder = upsert.SortOrder,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now
                };

                await _db.BeginTranAsync();
                var count = await _currencyInfoRepo.InsertCurrencyInfo(entity);
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
        /// 删除币别信息
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteCurrencyInfo(CurrencyInfoUpsert upsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var count = await _currencyInfoRepo.DeleteCurrencyInfo(long.Parse(upsert.CurrencyId));
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
        /// 修改币别信息
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateCurrencyInfo(CurrencyInfoUpsert upsert)
        {
            try
            {
                var entity = new CurrencyInfoEntity()
                {
                    CurrencyId = long.Parse(upsert.CurrencyId),
                    CurrencyCode = upsert.CurrencyCode,
                    CurrencyNameCn = upsert.CurrencyNameCn,
                    CurrencyNameEn = upsert.CurrencyNameEn,
                    SortOrder = upsert.SortOrder,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now
                };

                await _db.BeginTranAsync();
                var count = await _currencyInfoRepo.UpdateCurrencyInfo(entity);
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
        /// 查询币别实体
        /// </summary>
        /// <param name="getEntity"></param>
        /// <returns></returns>
        public async Task<Result<CurrencyInfoDto>> GetCurrencyInfoEntity(GetCurrencyInfoEntity getEntity)
        {
            try
            {
                var entity = await _currencyInfoRepo.GetCurrencyInfoEntity(long.Parse(getEntity.CurrencyId));
                return Result<CurrencyInfoDto>.Ok(entity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<CurrencyInfoDto>.Failure(200, ex.Message);
            }
        }

        /// <summary>
        /// 查询币别分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<CurrencyInfoDto>> GetCurrencyInfoPage(GetCurrencyInfoPage getPage)
        {
            try
            {
                var page = await _currencyInfoRepo.GetCurrencyInfoPage(getPage);
                return page;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<CurrencyInfoDto>.Failure(500, ex.Message);
            }
        }
    }
}
