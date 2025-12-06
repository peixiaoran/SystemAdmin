using Mapster;
using SqlSugar;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemBasicData
{
    public class UserLaborRepository
    {
        private readonly SqlSugarScope _db;

        public UserLaborRepository(SqlSugarScope db)
        {
            _db = db;
        }

        /// <summary>
        /// 新增职业
        /// </summary>
        /// <param name="userLaborEntity"></param>
        /// <returns></returns>
        public async Task<int> InsertUserLabor(UserLaborEntity userLaborEntity)
        {
            return await _db.Insertable(userLaborEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除职业
        /// </summary>
        /// <param name="userLaborId"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserLabor(long userLaborId)
        {
            return await _db.Deleteable<UserLaborEntity>()
                            .Where(userlabor => userlabor.LaborId == userLaborId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改职业
        /// </summary>
        /// <param name="userLaborEntity"></param>
        /// <returns></returns>
        public async Task<int> UpdateUserLabor(UserLaborEntity userLaborEntity)
        {
            return await _db.Updateable(userLaborEntity)
                            .IgnoreColumns(userlabor => new
                            {
                                userlabor.LaborId,
                                userlabor.CreatedBy,
                                userlabor.CreatedDate,
                            }).Where(userlabor => userlabor.LaborId == userLaborEntity.LaborId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询职业实体
        /// </summary>
        /// <param name="laborId"></param>
        /// <returns></returns>
        public async Task<UserLaborDto> GetUserLaborEntity(long laborId)
        {
            var userLaborEntity = await _db.Queryable<UserLaborEntity>()
                                           .With(SqlWith.NoLock)
                                           .Where(userPos => userPos.LaborId == laborId)
                                           .FirstAsync();
            return userLaborEntity.Adapt<UserLaborDto>();
        }

        /// <summary>
        /// 查询职业分页
        /// </summary>
        /// <param name="getUserLaborPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserLaborDto>> GetUserLaborPage(GetUserLaborPage getUserLaborPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<UserLaborEntity>()
                           .With(SqlWith.NoLock);

            // 职业名称
            if (!string.IsNullOrEmpty(getUserLaborPage.LaborName))
            {
                query = query.Where(userlabor =>
                    userlabor.LaborNameCn.Contains(getUserLaborPage.LaborName) ||
                    userlabor.LaborNameEn.Contains(getUserLaborPage.LaborName));
            }

            var userLaborPage = await query.OrderBy(userlabor => userlabor.LaborNameCn).ToPageListAsync(getUserLaborPage.PageIndex, getUserLaborPage.PageSize, totalCount);
            return ResultPaged<UserLaborDto>.Ok(userLaborPage.Adapt<List<UserLaborDto>>(), totalCount, "");
        }
    }
}
