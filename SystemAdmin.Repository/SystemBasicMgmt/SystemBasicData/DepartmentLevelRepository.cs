using Mapster;
using SqlSugar;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemBasicData
{
    public class DepartmentLevelRepository
    {
        private readonly SqlSugarScope _db;

        public DepartmentLevelRepository(SqlSugarScope db)
        {
            _db = db;
        }

        /// <summary>
        /// 新增部门级别信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertDepartmentLevel(DepartmentLevelEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除部门级别信息
        /// </summary>
        /// <param name="deptLevelId"></param>
        /// <returns></returns>
        public async Task<int> DeleteDepartmentLevel(long deptLevelId)
        {
            return await _db.Deleteable<DepartmentLevelEntity>()
                            .Where(deptlevel => deptlevel.DepartmentLevelId == deptLevelId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改部门级别信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateDepartmentLevel(DepartmentLevelEntity entity)
        {
            return await _db.Updateable(entity)
                            .IgnoreColumns(deptlevel => new
                            {
                                deptlevel.DepartmentLevelId,
                                deptlevel.CreatedBy,
                                deptlevel.CreatedDate,
                            }).Where(deptlevel => deptlevel.DepartmentLevelId == entity.DepartmentLevelId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询部门级别实体
        /// </summary>
        /// <param name="deptLevelId"></param>
        /// <returns></returns>
        public async Task<DepartmentLevelDto> GetDepartmentLevelEntity(long deptLevelId)
        {
            var entity = await _db.Queryable<DepartmentLevelEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(deptlevel => deptlevel.DepartmentLevelId == deptLevelId)
                                  .FirstAsync();
            return entity.Adapt<DepartmentLevelDto>();
        }

        /// <summary>
        /// 查询部门级别列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<DepartmentLevelDto>> GetDepartmentLevelList()
        {
            var deptLevelList = await _db.Queryable<DepartmentLevelEntity>()
                                         .With(SqlWith.NoLock)
                                         .OrderBy(deptlevel => deptlevel.SortOrder).ToListAsync();
            return deptLevelList.Adapt<List<DepartmentLevelDto>>();
        }
    }
}
