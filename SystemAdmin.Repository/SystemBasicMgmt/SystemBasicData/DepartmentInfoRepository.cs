
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
        private readonly IMapper _mapper;
        private readonly Language _lang;

        public DepartmentInfoRepository(SqlSugarScope db, IMapper mapper, Language lang)
        {
            _db = db;
            _mapper = mapper;
            _lang = lang;
        }

        /// <summary>
        /// 新增部门信息
        /// </summary>
        /// <param name="deptEntity"></param>
        /// <returns></returns>
        public async Task<int> InsertDepartmentInfo(DepartmentInfoEntity deptEntity)
        {
            return await _db.Insertable(deptEntity).ExecuteCommandAsync();
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
        /// <param name="deptEntity"></param>
        /// <returns></returns>
        public async Task<int> UpdateDepartmentInfo(DepartmentInfoEntity deptEntity)
        {
            return await _db.Updateable(deptEntity)
                            .IgnoreColumns(dept => new
                            {
                                dept.DepartmentId,
                                dept.CreatedBy,
                                dept.CreatedDate,
                            }).Where(dept => dept.DepartmentId == deptEntity.DepartmentId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询部门实体
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public async Task<DepartmentInfoDto> GetDepartmentInfoEntity(long deptId)
        {
            var deptEntity = await _db.Queryable<DepartmentInfoEntity>()
                                      .With(SqlWith.NoLock)
                                      .Where(dept => dept.DepartmentId == deptId)
                                      .FirstAsync();
            return _mapper.Map<DepartmentInfoDto>(deptEntity);
        }

        /// <summary>
        /// 查询部门树
        /// </summary>
        /// <param name="getDeptTree"></param>
        /// <returns></returns>
        public async Task<List<DepartmentInfoDto>> GetDepartmentInfoTree(GetDepartmentTree getDeptTree)
        {
            var query = _db.Queryable<DepartmentInfoEntity>()
                           .With(SqlWith.NoLock);

            if (!string.IsNullOrEmpty(getDeptTree.DepartmentCode))
                query = query.Where(dept => dept.DepartmentCode.Contains(getDeptTree.DepartmentCode));

            if (!string.IsNullOrEmpty(getDeptTree.DepartmentName))
                query = query.Where(dept => dept.DepartmentNameCn.Contains(getDeptTree.DepartmentName)
                                      || dept.DepartmentNameEn.Contains(getDeptTree.DepartmentName));

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
        /// 部门树下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<DepartmentDropDto>> GetDepartmentDropDown()
        {
            return await _db.Queryable<DepartmentInfoEntity>()
                            .With(SqlWith.NoLock)
                            .LeftJoin<DepartmentLevelEntity>((dept, deptlevel) => dept.DepartmentLevelId == deptlevel.DepartmentLevelId)
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
        /// 部门级别下拉框
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
    }
}
