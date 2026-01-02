using Mapster;
using SqlSugar;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Queries;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemMgmt
{
    public class ModuleRepository
    {
        private readonly SqlSugarScope _db;

        public ModuleRepository(SqlSugarScope db)
        {
            _db = db;
        }

        /// <summary>
        /// 新增模块
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <returns></returns>
        public async Task<int> InsertModule(ModuleInfoEntity moduleEntity)
        {
            return await _db.Insertable(moduleEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<int> DeleteModule(long moduleId)
        {
            return await _db.Deleteable<ModuleInfoEntity>()
                            .Where(module => module.ModuleId == moduleId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除角色模块
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<int> DeleteRoleModule(long moduleId)
        {
            return await _db.Deleteable<RoleModuleEntity>()
                            .Where(module => module.ModuleId == moduleId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询模块下的菜单Ids
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<List<long>> GetModuleMenusIds(long moduleId)
        {
            return await _db.Queryable<MenuInfoEntity>()
                            .Where(menu => menu.ModuleId == moduleId)
                            .Select(menu => menu.MenuId)
                            .ToListAsync();
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<int> DeleteMenu(long moduleId)
        {
            return await _db.Deleteable<MenuInfoEntity>()
                            .Where(menu => menu.ModuleId == moduleId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除角色菜单
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <returns></returns>
        public async Task<int> DeleteRoleMenuId(List<long> moduleIds)
        {
            return await _db.Deleteable<RoleMenuEntity>()
                            .Where(module => moduleIds.Contains(module.MenuId))
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <returns></returns>
        public async Task<int> UpdateModule(ModuleInfoEntity moduleEntity)
        {
            return await _db.Updateable(moduleEntity)
                            .IgnoreColumns(module => new
                            {
                                module.ModuleCode,
                                module.CreatedBy,
                                module.CreatedDate,
                            }).Where(module => module.ModuleId == moduleEntity.ModuleId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询模块实体
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<ModuleInfoDto> GetModuleEntity(long moduleId)
        {
            var moduleEntity = await _db.Queryable<ModuleInfoEntity>()
                                        .With(SqlWith.NoLock)
                                        .Where(module => module.ModuleId == moduleId)
                                        .FirstAsync();
            return moduleEntity.Adapt<ModuleInfoDto>();
        }

        /// <summary>
        /// 查询模块分页
        /// </summary>
        /// <param name="getModulePage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<ModuleInfoDto>> GetModulePage(GetModuleInfoPage getModulePage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<ModuleInfoEntity>()
                           .With(SqlWith.NoLock);

            // 模块编码
            if (!string.IsNullOrEmpty(getModulePage.ModuleCode))
            {
                query = query.Where(module => module.ModuleCode.Contains(getModulePage.ModuleCode));
            }
            // 模块名称
            if (!string.IsNullOrEmpty(getModulePage.ModuleName))
            {
                query = query.Where(module => module.ModuleNameCn.Contains(getModulePage.ModuleName));
            }

            // 排序
            query = query.OrderBy(module => module.SortOrder);

            var modulePage = await query.Select((module) => new ModuleInfoDto
                                        {
                                            ModuleId = module.ModuleId,
                                            ModuleCode = module.ModuleCode,
                                            ModuleNameCn = module.ModuleNameCn,
                                            ModuleNameEn = module.ModuleNameEn,
                                            ModuleIcon = module.ModuleIcon,
                                            IsEnabled = module.IsEnabled,
                                            IsVisible = module.IsVisible,
                                            Path = module.Path,
                                            RemarkCh = module.RemarkCh,
                                            RemarkEn = module.RemarkEn,
                                        }).ToPageListAsync(getModulePage.PageIndex, getModulePage.PageSize, totalCount);
            return ResultPaged<ModuleInfoDto>.Ok(modulePage, totalCount, "");
        }
    }
}
