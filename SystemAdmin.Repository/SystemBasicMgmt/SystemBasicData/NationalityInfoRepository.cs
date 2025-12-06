using Mapster;
using SqlSugar;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemBasicData
{
    public class NationalityInfoRepository
    {
        private readonly SqlSugarScope _db;

        public NationalityInfoRepository(SqlSugarScope db)
        {
            _db = db;
        }

        /// <summary>
        /// 新增国籍
        /// </summary>
        /// <param name="nationInfoEntity"></param>
        /// <returns></returns>
        public async Task<int> InsertNationalityInfo(NationalityInfoEntity nationInfoEntity)
        {
            return await _db.Insertable(nationInfoEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除国籍
        /// </summary>
        /// <param name="nationId"></param>
        /// <returns></returns>
        public async Task<int> DeleteNationalityInfo(long nationId)
        {
            return await _db.Deleteable<NationalityInfoEntity>()
                            .Where(Nationality => Nationality.NationId == nationId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改国籍
        /// </summary>
        /// <param name="nationInfoEntity"></param>
        /// <returns></returns>
        public async Task<int> UpdateNationalityInfo(NationalityInfoEntity nationInfoEntity)
        {
            return await _db.Updateable(nationInfoEntity)
                            .IgnoreColumns(Nationality => new
                            {
                                Nationality.NationId,
                                Nationality.CreatedBy,
                                Nationality.CreatedDate,
                            }).Where(Nationality => Nationality.NationId == nationInfoEntity.NationId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询国籍实体
        /// </summary>
        /// <param name="nationId"></param>
        /// <returns></returns>
        public async Task<NationalityInfoDto> GetNationalityEntity(long nationId)
        {
            var userNationalityEntity = await _db.Queryable<NationalityInfoEntity>()
                                                 .With(SqlWith.NoLock)
                                                 .Where(userPos => userPos.NationId == nationId)
                                                 .FirstAsync();
            return userNationalityEntity.Adapt<NationalityInfoDto>();
        }

        /// <summary>
        /// 查询国籍列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<NationalityInfoDto>> GetNationalityInfoList()
        {
            var nationalityList = await _db.Queryable<NationalityInfoEntity>()
                                           .With(SqlWith.NoLock)
                                           .ToListAsync();
            return nationalityList.Adapt<List<NationalityInfoDto>>();
        }
    }
}
