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
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertUserLabor(UserLaborEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除职业
        /// </summary>
        /// <param name="userLaborId"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserLabor(long userLaborId)
        {
            return await _db.Deleteable<UserLaborEntity>()
                            .Where(labor => labor.LaborId == userLaborId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改职业
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateUserLabor(UserLaborEntity entity)
        {
            return await _db.Updateable(entity)
                            .IgnoreColumns(labor => new
                            {
                                labor.LaborId,
                                labor.CreatedBy,
                                labor.CreatedDate,
                            }).Where(labor => labor.LaborId == entity.LaborId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询职业实体
        /// </summary>
        /// <param name="laborId"></param>
        /// <returns></returns>
        public async Task<UserLaborDto> GetUserLaborEntity(long laborId)
        {
            var entity = await _db.Queryable<UserLaborEntity>()
                                           .With(SqlWith.NoLock)
                                           .Where(userPos => userPos.LaborId == laborId)
                                           .FirstAsync();
            return entity.Adapt<UserLaborDto>();
        }

        /// <summary>
        /// 查询职业分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserLaborDto>> GetUserLaborPage(GetUserLaborPage getPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<UserLaborEntity>()
                           .With(SqlWith.NoLock);

            // 职业名称
            if (!string.IsNullOrEmpty(getPage.LaborName))
            {
                query = query.Where(labor =>
                    labor.LaborNameCn.Contains(getPage.LaborName) ||
                    labor.LaborNameEn.Contains(getPage.LaborName));
            }

            // 排序
            query = query.OrderByDescending(labor => labor.CreatedDate);

            var page = await query.ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<UserLaborDto>.Ok(page.Adapt<List<UserLaborDto>>(), totalCount, "");
        }
    }
}
