using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
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
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertExchangeRate(ExchangeRateEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
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
                            .Where(rate => rate.CurrencyCode == currencyCode && rate.ExchangeCurrencyCode == exchangeCurrencyCode && rate.YearMonth == yearMonth)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改汇率信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateExchangeRate(ExchangeRateEntity entity)
        {
            return await _db.Updateable(entity)
                            .IgnoreColumns(rate => new
                            {
                                rate.CreatedBy,
                                rate.CreatedDate,
                            }).Where(rate => rate.ExchangeCurrencyCode == entity.ExchangeCurrencyCode && rate.YearMonth == entity.YearMonth)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询汇率对照信息分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<ExchangeRateDto>> GetExchangeRatePage(GetExchangeRatePage getPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<ExchangeRateEntity>()
                           .With(SqlWith.NoLock);

            // 本位币别
            if (!string.IsNullOrEmpty(getPage.CurrencyCode))
            {
                query = query.Where(exchangerate => exchangerate.CurrencyCode == getPage.CurrencyCode);
            }
            // 年月
            if (!string.IsNullOrEmpty(getPage.YearMonth))
            {
                query = query.Where(exchangerate => exchangerate.YearMonth == getPage.YearMonth);
            }

            // 排序
            query = query.OrderByDescending(exchangerate => exchangerate.CreatedDate);

            var exchangeratePage = await query.ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<ExchangeRateDto>.Ok(exchangeratePage.Adapt<List<ExchangeRateDto>>(), totalCount, "");
        }

        /// <summary>
        /// 查询汇率对照信息实体
        /// </summary>
        /// <param name="getEntity"></param>
        /// <returns></returns>
        public async Task<ExchangeRateDto> GetExchangeRateEntity(GetExchangeRateEntity getEntity)
        {
            var entity = await _db.Queryable<ExchangeRateEntity>()
                                              .With(SqlWith.NoLock)
                                              .Where(rate => rate.CurrencyCode == getEntity.CurrencyCode && rate.ExchangeCurrencyCode == getEntity.ExchangeCurrencyCode && getEntity.YearMonth == getEntity.YearMonth)
                                              .FirstAsync();
            return entity.Adapt<ExchangeRateDto>();
        }

        /// <summary>
        /// 币别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<CurrencyInfoDropDto>> GetCurrencyInfoDropDown()
        {
            return await _db.Queryable<CurrencyInfoEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(currency => currency.SortOrder)
                            .Select(currency => new CurrencyInfoDropDto
                            {
                                CurrencyCode = currency.CurrencyCode,
                                CurrencyName = _lang.Locale == "zh-CN"
                                               ? currency.CurrencyNameCn
                                               : currency.CurrencyNameEn,
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
                            .Where(rate => rate.CurrencyCode == currencyCode && rate.ExchangeCurrencyCode == exchangeCurrencyCode && rate.YearMonth == yearMonth)
                            .AnyAsync();
        }
    }
}
