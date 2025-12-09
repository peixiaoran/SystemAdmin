using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Queries;
using SystemAdmin.Repository.SystemBasicMgmt.SystemSettings;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemSettings
{
    public class ExchangeRateService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<ExchangeRateService> _logger;
        private readonly SqlSugarScope _db;
        private readonly ExchangeRateRepository _ExchangeRateRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt_SystemSettings_ExchangeRate_";

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
        /// <param name="exchangeRateUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertExchangeRate(ExchangeRateUpsert exchangeRateUpsert)
        {
            try
            {
                if (exchangeRateUpsert.CurrencyCode == exchangeRateUpsert.ExchangeCurrencyCode)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}IsRepeat"));
                }
                else
                {
                    await _db.BeginTranAsync();
                    bool IsExist = await _ExchangeRateRepository.GetExchangeRateIsExist(exchangeRateUpsert.CurrencyCode, exchangeRateUpsert.ExchangeCurrencyCode, exchangeRateUpsert.YearMonth);
                    if (IsExist)
                    {
                        await _db.RollbackTranAsync();
                        return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}IsExist"));
                    }
                    else
                    {
                        ExchangeRateEntity ExchangeRateEntity = new ExchangeRateEntity()
                        {
                            CurrencyCode = exchangeRateUpsert.CurrencyCode,
                            ExchangeCurrencyCode = exchangeRateUpsert.ExchangeCurrencyCode,
                            ExchangeRate = exchangeRateUpsert.ExchangeRate,
                            YearMonth = exchangeRateUpsert.YearMonth,
                            Remark = exchangeRateUpsert.Remark,
                            CreatedBy = _loginuser.UserId,
                            CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        };
                        var insertExchangeRateCount = await _ExchangeRateRepository.InsertExchangeRate(ExchangeRateEntity);
                        await _db.CommitTranAsync();

                        return insertExchangeRateCount >= 1
                                ? Result<int>.Ok(insertExchangeRateCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
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
        /// <param name="exchangeRateUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteExchangeRate(ExchangeRateUpsert exchangeRateUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var delExchangeRateCount = await _ExchangeRateRepository.DeleteExchangeRate(exchangeRateUpsert.CurrencyCode, exchangeRateUpsert.ExchangeCurrencyCode, exchangeRateUpsert.YearMonth);
                await _db.CommitTranAsync();

                return delExchangeRateCount >= 1
                        ? Result<int>.Ok(delExchangeRateCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
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
        /// <param name="exchangeRateUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateExchangeRate(ExchangeRateUpsert exchangeRateUpsert)
        {
            try
            {
                if (exchangeRateUpsert.CurrencyCode == exchangeRateUpsert.ExchangeCurrencyCode)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}IsRepeat"));
                }
                else
                {
                    await _db.BeginTranAsync();
                    await _ExchangeRateRepository.DeleteExchangeRate(exchangeRateUpsert.CurrencyCode, exchangeRateUpsert.ExchangeCurrencyCode, exchangeRateUpsert.YearMonth);
                    ExchangeRateEntity ExchangeRateEntity = new ExchangeRateEntity()
                    {
                        CurrencyCode = exchangeRateUpsert.CurrencyCode,
                        ExchangeCurrencyCode = exchangeRateUpsert.ExchangeCurrencyCode,
                        ExchangeRate = exchangeRateUpsert.ExchangeRate,
                        YearMonth = exchangeRateUpsert.YearMonth,
                        Remark = exchangeRateUpsert.Remark,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        ModifiedBy = _loginuser.UserId,
                        ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    var insertExchangeRateCount = await _ExchangeRateRepository.InsertExchangeRate(ExchangeRateEntity);
                    await _db.CommitTranAsync();

                    return insertExchangeRateCount >= 1
                            ? Result<int>.Ok(insertExchangeRateCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// <param name="getExchangeRatePage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<ExchangeRateDto>> GetExchangeRatePage(GetExchangeRatePage getExchangeRatePage)
        {
            try
            {
                var exchangeRatePage = await _ExchangeRateRepository.GetExchangeRatePage(getExchangeRatePage);
                return exchangeRatePage;
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
        /// <param name="getExchangeRateEntity"></param>
        /// <returns></returns>
        public async Task<Result<ExchangeRateDto>> GetExchangeRateEntity(GetExchangeRateEntity getExchangeRateEntity)
        {
            try
            {
                var exchangeRateEntity = await _ExchangeRateRepository.GetExchangeRateEntity(getExchangeRateEntity);
                return Result<ExchangeRateDto>.Ok(exchangeRateEntity, "");
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
                var currencyDrop = await _ExchangeRateRepository.GetCurrencyInfoDropDown();
                return Result<List<CurrencyInfoDropDto>>.Ok(currencyDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<CurrencyInfoDropDto>>.Failure(500, ex.Message);
            }
        }
    }
}
