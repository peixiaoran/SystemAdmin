using Mapster;
using SqlSugar;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Queries;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemConfig
{
    public class CurrencyInfoRepository
    {
        private readonly SqlSugarScope _db;

        public CurrencyInfoRepository(SqlSugarScope db)
        {
            _db = db;
        }

        /// <summary>
        /// 新增币别信息
        /// </summary>
        /// <param name="currencyEntity"></param>
        /// <returns></returns>
        public async Task<int> InsertCurrencyInfo(CurrencyInfoEntity currencyEntity)
        {
            return await _db.Insertable(currencyEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除币别信息
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        public async Task<int> DeleteCurrencyInfo(long currencyId)
        {
            return await _db.Deleteable<CurrencyInfoEntity>()
                            .Where(currency => currency.CurrencyId == currencyId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改币别信息
        /// </summary>
        /// <param name="currencyEntity"></param>
        /// <returns></returns>
        public async Task<int> UpdateCurrencyInfo(CurrencyInfoEntity currencyEntity)
        {
            return await _db.Updateable(currencyEntity)
                            .IgnoreColumns(currency => new
                            {
                                currency.CurrencyId,
                                currency.CreatedBy,
                                currency.CreatedDate,
                            }).Where(currency => currency.CurrencyId == currencyEntity.CurrencyId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询币别实体
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        public async Task<CurrencyInfoDto> GetCurrencyInfoEntity(long currencyId)
        {
            var currencyEntity = await _db.Queryable<CurrencyInfoEntity>()
                                          .With(SqlWith.NoLock)
                                          .Where(currency => currency.CurrencyId == currencyId)
                                          .FirstAsync();
            return currencyEntity.Adapt<CurrencyInfoDto>();
        }

        /// <summary>
        /// 查询币别分页
        /// </summary>
        /// <param name="getCurrencyPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<CurrencyInfoDto>> GetCurrencyInfoPage(GetCurrencyInfoPage getCurrencyPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<CurrencyInfoEntity>()
                           .With(SqlWith.NoLock);

            // 币别代码
            if (!string.IsNullOrEmpty(getCurrencyPage.CurrencyCode))
            {
                query = query.Where(currency => currency.CurrencyCode.Contains(getCurrencyPage.CurrencyCode));
            }

            // 排序
            query = query.OrderBy(currency => currency.SortOrder);

            var currencyPage = await query.Select((currency) => new CurrencyInfoDto
            {
                CurrencyId = currency.CurrencyId,
                CurrencyCode = currency.CurrencyCode,
                CurrencyNameCn = currency.CurrencyNameCn,
                CurrencyNameEn = currency.CurrencyNameEn,
            }).ToPageListAsync(getCurrencyPage.PageIndex, getCurrencyPage.PageSize, totalCount);
            return ResultPaged<CurrencyInfoDto>.Ok(currencyPage, totalCount);
        }
    }
}
