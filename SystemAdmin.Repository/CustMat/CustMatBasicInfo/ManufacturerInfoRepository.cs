using Mapster;
using SqlSugar;
using SystemAdmin.Model.CustMat.CustMatBasicInfo.Dto;
using SystemAdmin.Model.CustMat.CustMatBasicInfo.Entity;
using SystemAdmin.Model.CustMat.CustMatBasicInfo.Queries;

namespace SystemAdmin.Repository.CustMat.CustMatBasicInfo
{
    public class ManufacturerInfoRepository
    {
        private readonly SqlSugarScope _db;

        public ManufacturerInfoRepository(SqlSugarScope db)
        {
            _db = db;
        }

        /// <summary>
        /// 新增厂商信息
        /// </summary>
        /// <param name="manufacturerEntity"></param>
        /// <returns></returns>
        public async Task<int> InsertManufacturerInfo(ManufacturerInfoEntity manufacturerEntity)
        {
            return await _db.Insertable(manufacturerEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除厂商信息
        /// </summary>
        /// <param name="manufacturerId"></param>
        /// <returns></returns>
        public async Task<int> DeleteManufacturerInfo(long manufacturerId)
        {
            return await _db.Deleteable<ManufacturerInfoEntity>()
                            .Where(manufacturer => manufacturer.ManufacturerId == manufacturerId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改厂商信息
        /// </summary>
        /// <param name="manufacturerEntity"></param>
        /// <returns></returns>
        public async Task<int> UpdateManufacturerInfo(ManufacturerInfoEntity manufacturerEntity)
        {
            return await _db.Updateable(manufacturerEntity)
                            .IgnoreColumns(manufacturer => new
                            {
                                manufacturer.ManufacturerId,
                                manufacturer.CreatedBy,
                                manufacturer.CreatedDate,
                            }).Where(manufacturer => manufacturer.ManufacturerId == manufacturerEntity.ManufacturerId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询厂商实体
        /// </summary>
        /// <param name="manufacturerId"></param>
        /// <returns></returns>
        public async Task<ManufacturerInfoDto> GetManufacturerInfoEntity(long manufacturerId)
        {
            var manufacturerEntity = await _db.Queryable<ManufacturerInfoEntity>()
                                              .With(SqlWith.NoLock)
                                              .Where(manufacturer => manufacturer.ManufacturerId == manufacturerId)
                                              .FirstAsync();
            return manufacturerEntity.Adapt<ManufacturerInfoDto>();
        }

        /// <summary>
        /// 查询厂商分页
        /// </summary>
        /// <param name="getManufacturerPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<ManufacturerInfoDto>> GetManufacturerInfoPage(GetManufacturerInfoPage getManufacturerPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<ManufacturerInfoEntity>()
                           .With(SqlWith.NoLock);

            // 厂商编码
            if (!string.IsNullOrEmpty(getManufacturerPage.ManufacturerCode))
            {
                query = query.Where(manufacturer => manufacturer.ManufacturerCode.Contains(getManufacturerPage.ManufacturerCode));
            }
            // 厂商名称
            if (!string.IsNullOrEmpty(getManufacturerPage.ManufacturerName))
            {
                query = query.Where(manufacturer => 
                    manufacturer.ManufacturerNameCn.Contains(getManufacturerPage.ManufacturerName) || 
                    manufacturer.ManufacturerNameEn.Contains(getManufacturerPage.ManufacturerName));
            }

            // 排序
            query = query.OrderBy(manufacturer => manufacturer.CreatedDate);

            var manufacturerPage = await query.ToPageListAsync(getManufacturerPage.PageIndex, getManufacturerPage.PageSize, totalCount);
            return ResultPaged<ManufacturerInfoDto>.Ok(manufacturerPage.Adapt<List<ManufacturerInfoDto>>(), totalCount, "");
        }
    }
}
