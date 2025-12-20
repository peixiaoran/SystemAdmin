using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Queries;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemConfig
{
    public class ExchangeRateRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public ExchangeRateRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 新增汇率信息
        /// </summary>
        /// <param name="exchangeRateEntity"></param>
        /// <returns></returns>
        public async Task<int> InsertExchangeRate(ExchangeRateEntity exchangeRateEntity)
        {
            return await _db.Insertable(exchangeRateEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除汇率信息
        /// </summary>
        /// <param name="currencyCode"></param>
        /// <param name="exchangeCurrencyCode"></param>
        /// <param name="yearMonth"></param>
        /// <returns></returns>
        public async Task<int> DeleteExchangeRate(string currencyCode, string exchangeCurrencyCode, string yearMonth)
        {
            return await _db.Deleteable<ExchangeRateEntity>()
                            .Where(exchangerate => exchangerate.CurrencyCode == currencyCode && exchangerate.ExchangeCurrencyCode == exchangeCurrencyCode && exchangerate.YearMonth == yearMonth)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询汇率对照信息分页
        /// </summary>
        /// <param name="ExchangeRatePage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<ExchangeRateDto>> GetExchangeRatePage(GetExchangeRatePage ExchangeRatePage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<ExchangeRateEntity>()
                           .With(SqlWith.NoLock)
                           .OrderByDescending(exchangerate => exchangerate.CreatedDate);

            // 本位币别
            if (!string.IsNullOrEmpty(ExchangeRatePage.CurrencyCode))
            {
                query = query.Where(exchangerate => exchangerate.CurrencyCode == ExchangeRatePage.CurrencyCode);
            }
            // 年月
            if (!string.IsNullOrEmpty(ExchangeRatePage.YearMonth))
            {
                query = query.Where(exchangerate => exchangerate.YearMonth == ExchangeRatePage.YearMonth);
            }

            var exchangeratePage = await query.ToPageListAsync(ExchangeRatePage.PageIndex, ExchangeRatePage.PageSize, totalCount);
            return ResultPaged<ExchangeRateDto>.Ok(exchangeratePage.Adapt<List<ExchangeRateDto>>(), totalCount, "");
        }

        /// <summary>
        /// 查询汇率对照信息实体
        /// </summary>
        /// <param name="getExchangeRateEntity"></param>
        /// <returns></returns>
        public async Task<ExchangeRateDto> GetExchangeRateEntity(GetExchangeRateEntity getExchangeRateEntity)
        {
            var exchangeRateEntity = await _db.Queryable<ExchangeRateEntity>()
                                              .With(SqlWith.NoLock)
                                              .Where(exchangerate => exchangerate.CurrencyCode == getExchangeRateEntity.CurrencyCode && exchangerate.ExchangeCurrencyCode == getExchangeRateEntity.ExchangeCurrencyCode && getExchangeRateEntity.YearMonth == getExchangeRateEntity.YearMonth)
                                              .FirstAsync();
            return exchangeRateEntity.Adapt<ExchangeRateDto>();
        }

        /// <summary>
        /// 币别下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<CurrencyInfoDropDto>> GetCurrencyInfoDropDown()
        {
            return await _db.Queryable<CurrencyInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Select(currency => new CurrencyInfoDropDto
                            {
                                CurrencyCode = currency.CurrencyCode,
                                CurrencyName = _lang.Locale == "zh-CN"
                                               ? currency.CurrencyNameCn
                                               : currency.CurrencyNameEn,
                                Disabled = currency.IsEnabled == 0
                            }).ToListAsync();
        }

        /// <summary>
        /// 查询汇率对照是否存在
        /// </summary>
        /// <param name="currencyCode"></param>
        /// <param name="exchangeCurrencyCode"></param>
        /// <param name="yearMonth"></param>
        /// <returns></returns>
        public async Task<bool> GetExchangeRateIsExist(string currencyCode, string exchangeCurrencyCode, string yearMonth)
        {
            return await _db.Queryable<ExchangeRateEntity>()
                            .With(SqlWith.NoLock)
                            .Where(exchangerate => exchangerate.CurrencyCode == currencyCode && exchangerate.ExchangeCurrencyCode == exchangeCurrencyCode && exchangerate.YearMonth == yearMonth)
                            .AnyAsync();
        }
    }
}
