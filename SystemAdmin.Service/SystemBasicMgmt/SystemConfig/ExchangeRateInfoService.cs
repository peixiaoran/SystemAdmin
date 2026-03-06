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
    public class ExchangeRateService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<ExchangeRateService> _logger;
        private readonly SqlSugarScope _db;
        private readonly ExchangeRateRepository _ExchangeRateRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.SystemConfig.ExchangeRate";

        public ExchangeRateService(CurrentUser loginuser, ILogger<ExchangeRateService> logger, SqlSugarScope db, ExchangeRateRepository ExchangeRateRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _ExchangeRateRepository = ExchangeRateRepository;
            _localization = localization;
        }

        /// <summary>
        /// 新增汇率对照信息
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertExchangeRate(ExchangeRateUpsert upsert)
        {
            try
            {
                if (upsert.CurrencyCode == upsert.ExchangeCurrencyCode)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}IsRepeat"));
                }
                else
                {
                    bool isExist = await _ExchangeRateRepository.GetExchangeRateIsExist(upsert.CurrencyCode, upsert.ExchangeCurrencyCode, upsert.YearMonth);
                    if (isExist)
                    {
                        return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}IsExist"));
                    }
                    else
                    {
                        ExchangeRateEntity entity = new ExchangeRateEntity()
                        {
                            CurrencyCode = upsert.CurrencyCode,
                            ExchangeCurrencyCode = upsert.ExchangeCurrencyCode,
                            ExchangeRate = upsert.ExchangeRate,
                            YearMonth = upsert.YearMonth,
                            Remark = upsert.Remark,
                            CreatedBy = _loginuser.UserId,
                            CreatedDate = DateTime.Now
                        };

                        await _db.BeginTranAsync();
                        var count = await _ExchangeRateRepository.InsertExchangeRate(entity);
                        await _db.CommitTranAsync();

                        return count >= 1
                                ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}InsertSuccess"))
                                : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
                    }
                }
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 删除汇率对照信息
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteExchangeRate(ExchangeRateUpsert upsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var count = await _ExchangeRateRepository.DeleteExchangeRate(upsert.CurrencyCode, upsert.ExchangeCurrencyCode, upsert.YearMonth);
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
        /// 修改汇率对照信息
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateExchangeRate(ExchangeRateUpsert upsert)
        {
            try
            {
                if (upsert.CurrencyCode == upsert.ExchangeCurrencyCode)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}IsRepeat"));
                }
                else
                {
                    ExchangeRateEntity entity = new ExchangeRateEntity()
                    {
                        CurrencyCode = upsert.CurrencyCode,
                        ExchangeCurrencyCode = upsert.ExchangeCurrencyCode,
                        ExchangeRate = upsert.ExchangeRate,
                        YearMonth = upsert.YearMonth,
                        Remark = upsert.Remark,
                        ModifiedBy = _loginuser.UserId,
                        ModifiedDate = DateTime.Now
                    };

                    await _db.BeginTranAsync();
                    var count = await _ExchangeRateRepository.UpdateExchangeRate(entity);
                    await _db.CommitTranAsync();

                    return count >= 1
                            ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}UpdateSuccess"))
                            : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdateFailed"));
                }
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询汇率对照信息分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<ExchangeRateDto>> GetExchangeRatePage(GetExchangeRatePage getPage)
        {
            try
            {
                var page = await _ExchangeRateRepository.GetExchangeRatePage(getPage);
                return page;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<ExchangeRateDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询汇率对照信息实体
        /// </summary>
        /// <param name="getEntity"></param>
        /// <returns></returns>
        public async Task<Result<ExchangeRateDto>> GetExchangeRateEntity(GetExchangeRateEntity getEntity)
        {
            try
            {
                var entity = await _ExchangeRateRepository.GetExchangeRateEntity(getEntity);
                return Result<ExchangeRateDto>.Ok(entity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<ExchangeRateDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询币别下拉选单
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<CurrencyInfoDropDto>>> GetCurrencyInfoDropDown()
        {
            try
            {
                var drop = await _ExchangeRateRepository.GetCurrencyInfoDropDown();
                return Result<List<CurrencyInfoDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<CurrencyInfoDropDto>>.Failure(500, ex.Message);
            }
        }
    }
}
