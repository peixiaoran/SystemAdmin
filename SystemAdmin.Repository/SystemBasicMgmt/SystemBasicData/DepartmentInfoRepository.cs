using Mapster;
using MapsterMapper;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemBasicData
{
    public class DepartmentInfoRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public DepartmentInfoRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }


        /// <summary>
        /// 部门树下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<DepartmentDropDto>> GetDepartmentDropDown()
        {
            return await _db.Queryable<DepartmentInfoEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<DepartmentLevelEntity>((dept, deptlevel) => dept.DepartmentLevelId == deptlevel.DepartmentLevelId)
                            .OrderBy(dept => dept.SortOrder)
                            .Select((dept, deptlevel) => new DepartmentDropDto
                            {
                                DepartmentId = dept.DepartmentId,
                                DepartmentName = _lang.Locale == "zh-CN"
                                                 ? dept.DepartmentNameCn
                                                 : dept.DepartmentNameEn,
                                ParentId = dept.ParentId,
                            }).ToTreeAsync(menu => menu.DepartmentChildList, menu => menu.ParentId, 0);
        }

        /// <summary>
        /// 部门级别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<DepartmentLevelDropDto>> GetDepartmentLevelDropDown()
        {
            return await _db.Queryable<DepartmentLevelEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(deptlevel => deptlevel.SortOrder)
                            .Select(deptlevel => new DepartmentLevelDropDto
                            {
                                DepartmentLevelId = deptlevel.DepartmentLevelId,
                                DepartmentLevelName = _lang.Locale == "zh-CN"
                                                      ? deptlevel.DepartmentLevelNameCn
                                                      : deptlevel.DepartmentLevelNameEn
                            }).ToListAsync();
        }

        /// <summary>
        /// 新增部门信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertDepartmentInfo(DepartmentInfoEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询所有员工信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserInfoEntity>> GetUserInfoList()
        {
            return await _db.Queryable<UserInfoEntity>()
                            .With(SqlWith.NoLock)
                            .ToListAsync();
        }

        /// <summary>
        /// 查询所有部门信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<DepartmentInfoEntity>> GetDepartmentInfoList()
        {
            return await _db.Queryable<DepartmentInfoEntity>()
                            .With(SqlWith.NoLock)
                            .ToListAsync();
        }

        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public async Task<int> DeleteDepartmentInfo(long deptId)
        {
            return await _db.Deleteable<DepartmentInfoEntity>()
                            .Where(dept => dept.DepartmentId == deptId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateDepartmentInfo(DepartmentInfoEntity entity)
        {
            return await _db.Updateable(entity)
                            .IgnoreColumns(dept => new
                            {
                                dept.DepartmentId,
                                dept.DepartmentCode,
                                dept.CreatedBy,
                                dept.CreatedDate,
                            }).Where(dept => dept.DepartmentId == entity.DepartmentId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询部门实体
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public async Task<DepartmentInfoDto> GetDepartmentInfoEntity(long deptId)
        {
            var entity = await _db.Queryable<DepartmentInfoEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(dept => dept.DepartmentId == deptId)
                                  .FirstAsync();
            return entity.Adapt<DepartmentInfoDto>();
        }

        /// <summary>
        /// 查询部门树
        /// </summary>
        /// <param name="getTree"></param>
        /// <returns></returns>
        public async Task<List<DepartmentInfoDto>> GetDepartmentInfoTree(GetDepartmentTree getTree)
        {
            var query = _db.Queryable<DepartmentInfoEntity>()
                           .With(SqlWith.NoLock);

            if (!string.IsNullOrEmpty(getTree.DepartmentCode))
                query = query.Where(dept => dept.DepartmentCode.Contains(getTree.DepartmentCode));

            if (!string.IsNullOrEmpty(getTree.DepartmentName))
                query = query.Where(dept => dept.DepartmentNameCn.Contains(getTree.DepartmentName)
                                      || dept.DepartmentNameEn.Contains(getTree.DepartmentName));

            var matchedNodes = await query.Select(dept => new DepartmentInfoEntity
            {
                DepartmentId = dept.DepartmentId,
                ParentId = dept.ParentId
            }).ToListAsync();

            if (!matchedNodes.Any()) return new List<DepartmentInfoDto>();

            var allDeptIds = matchedNodes.Select(dept => dept.DepartmentId).ToHashSet();
            var parentIds = matchedNodes.Select(dept => dept.ParentId).Where(pid => pid != 0).ToList();

            while (parentIds.Any())
            {
                var parents = await _db.Queryable<DepartmentInfoEntity>()
                                       .Where(dept => parentIds.Contains(dept.DepartmentId))
                                       .Select(dept => new DepartmentInfoEntity { DepartmentId = dept.DepartmentId, ParentId = dept.ParentId })
                                       .ToListAsync();

                foreach (var p in parents)
                {
                    allDeptIds.Add(p.DepartmentId);
                }

                parentIds = parents.Select(p => p.ParentId).Where(pid => pid != 0).ToList();
            }

            var deptList = await _db.Queryable<DepartmentInfoEntity>()
                                    .With(SqlWith.NoLock)
                                    .LeftJoin<DepartmentLevelEntity>((dept, level) => dept.DepartmentLevelId == level.DepartmentLevelId)
                                    .Where((dept, level) => allDeptIds.Contains(dept.DepartmentId))
                                    .OrderBy(dept => dept.SortOrder)
                                    .Select((dept, level) => new DepartmentInfoDto
                                    {
                                        DepartmentId = dept.DepartmentId,
                                        DepartmentCode = dept.DepartmentCode,
                                        DepartmentNameCn = dept.DepartmentNameCn,
                                        DepartmentNameEn = dept.DepartmentNameEn,
                                        ParentId = dept.ParentId,
                                        DepartmentLevelId = dept.DepartmentLevelId,
                                        DepartmentLevelName = _lang.Locale == "zh-CN"
                                                            ? level.DepartmentLevelNameCn
                                                            : level.DepartmentLevelNameEn,
                                        Description = dept.Description,
                                        SortOrder = dept.SortOrder,
                                        Landline = dept.Landline,
                                        Email = dept.Email,
                                        Address = dept.Address,
                                    }).ToListAsync();

            List<DepartmentInfoDto> BuildTree(List<DepartmentInfoDto> list, long parentId = 0)
            {
                var result = list.Where(dept => dept.ParentId == parentId)
                                 .OrderBy(dept => dept.SortOrder)
                                 .ToList();

                foreach (var item in result)
                {
                    item.DepartmentChildList = BuildTree(list, item.DepartmentId);
                }
                return result;
            }
            var deptTree = BuildTree(deptList);
            return deptTree;
        }

        /// <summary>
        /// 查询部门编码是否存在
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public async Task<bool> GetDepartCodeIsExist(string deptCode)
        {
            return await _db.Queryable<DepartmentInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Where(dept => dept.DepartmentCode == deptCode)
                            .AnyAsync();
        }
    }
}
