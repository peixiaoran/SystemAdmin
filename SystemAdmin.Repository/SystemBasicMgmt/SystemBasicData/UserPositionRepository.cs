using Mapster;
using SqlSugar;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemBasicData
{
    public class UserPositionRepository
    {
        private readonly SqlSugarScope _db;

        public UserPositionRepository(SqlSugarScope db)
        {
            _db = db;
        }

        /// <summary>
        /// 查询职级实体
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public async Task<UserPositionDto> GetUserPositionEntity(long positionId)
        {
            var userPositionEntity = await _db.Queryable<UserPositionEntity>()
                                              .With(SqlWith.NoLock)
                                              .Where(userpos => userpos.PositionId == positionId)
                                              .FirstAsync();
            return userPositionEntity.Adapt<UserPositionDto>();
        }

        /// <summary>
        /// 查询职级列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserPositionDto>> GetUserPositionList()
        {
            var userPositionList = await _db.Queryable<UserPositionEntity>()
                                            .With(SqlWith.NoLock)
                                            .OrderBy(userpos => userpos.PositionOrderBy)
                                            .Select((userpos) => new UserPositionDto
                                            {
                                                PositionId = userpos.PositionId,
                                                PositionNo = userpos.PositionNo,
                                                PositionNameCn = userpos.PositionNameCn,
                                                PositionNameEn = userpos.PositionNameEn,
                                                Description = userpos.Description,
                                            }).ToListAsync();
            return userPositionList;
        }
    }
}
