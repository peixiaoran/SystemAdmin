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
        private readonly CurrencyInfoRepository _currencyInfoRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.SystemConfig.CurrencyInfo";

        public CurrencyInfoService(CurrentUser loginuser, ILogger<CurrencyInfoService> logger, SqlSugarScope db, CurrencyInfoRepository currencyInfoRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _currencyInfoRepository = currencyInfoRepository;
            _localization = localization;
        }

        /// <summary>
        /// 新增币别信息
        /// </summary>
        /// <param name="currencyUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertCurrencyInfo(CurrencyInfoUpsert currencyUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                CurrencyInfoEntity insertCurrencyEntity = new CurrencyInfoEntity()
                {
                    CurrencyId = SnowFlakeSingle.Instance.NextId(),
                    CurrencyCode = currencyUpsert.CurrencyCode,
                    CurrencyNameCn = currencyUpsert.CurrencyNameCn,
                    CurrencyNameEn = currencyUpsert.CurrencyNameEn,
                    SortOrder = currencyUpsert.SortOrder,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                var insertCurrencyCount = await _currencyInfoRepository.InsertCurrencyInfo(insertCurrencyEntity);
                await _db.CommitTranAsync();

                return insertCurrencyCount >= 1
                        ? Result<int>.Ok(insertCurrencyCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
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
        /// <param name="currencyUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteCurrencyInfo(CurrencyInfoUpsert currencyUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var delCurrencyCount = await _currencyInfoRepository.DeleteCurrencyInfo(long.Parse(currencyUpsert.CurrencyId));
                await _db.CommitTranAsync();

                return delCurrencyCount >= 1
                        ? Result<int>.Ok(delCurrencyCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
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
        /// <param name="currencyUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateCurrencyInfo(CurrencyInfoUpsert currencyUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                CurrencyInfoEntity updateCurrencyEntity = new CurrencyInfoEntity()
                {
                    CurrencyId = long.Parse(currencyUpsert.CurrencyId),
                    CurrencyCode = currencyUpsert.CurrencyCode,
                    CurrencyNameCn = currencyUpsert.CurrencyNameCn,
                    CurrencyNameEn = currencyUpsert.CurrencyNameEn,
                    SortOrder = currencyUpsert.SortOrder,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                var updateCurrencyCount = await _currencyInfoRepository.UpdateCurrencyInfo(updateCurrencyEntity);
                await _db.CommitTranAsync();

                return updateCurrencyCount >= 1
                        ? Result<int>.Ok(updateCurrencyCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// <param name="getCurrencyEntity"></param>
        /// <returns></returns>
        public async Task<Result<CurrencyInfoDto>> GetCurrencyInfoEntity(GetCurrencyInfoEntity getCurrencyEntity)
        {
            try
            {
                var currencyEntity = await _currencyInfoRepository.GetCurrencyInfoEntity(long.Parse(getCurrencyEntity.CurrencyId));
                return Result<CurrencyInfoDto>.Ok(currencyEntity, "");
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
        /// <param name="getCurrencyPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<CurrencyInfoDto>> GetCurrencyInfoPage(GetCurrencyInfoPage getCurrencyPage)
        {
            try
            {
                var currencyPage = await _currencyInfoRepository.GetCurrencyInfoPage(getCurrencyPage);
                return currencyPage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<CurrencyInfoDto>.Failure(500, ex.Message);
            }
        }
    }
}
