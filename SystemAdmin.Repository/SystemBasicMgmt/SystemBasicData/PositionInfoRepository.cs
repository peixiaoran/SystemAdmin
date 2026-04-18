using Mapster;
using Minio.DataModel.Notification;
using SqlSugar;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemBasicData
{
    public class PositionInfoRepository
    {
        private readonly SqlSugarScope _db;

        public PositionInfoRepository(SqlSugarScope db)
        {
            _db = db;
        }

        /// <summary>
        /// 查询职级实体
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public async Task<PositionInfoDto> GetPositionInfoEntity(long positionId)
        {
            var entity = await _db.Queryable<PositionInfoEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(position => position.PositionId == positionId)
                                  .FirstAsync();
            return entity.Adapt<PositionInfoDto>();
        }

        /// <summary>
        /// 查询职级列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<PositionInfoDto>> GetPositionInfoList()
        {
            var list = await _db.Queryable<PositionInfoEntity>()
                                .With(SqlWith.NoLock)
                                .OrderBy(position => position.SortOrder)
                                .ToListAsync();
            return list.Adapt<List<PositionInfoDto>>();
        }
    }
}
