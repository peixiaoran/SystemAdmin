using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Queries;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemMgmt
{
    public class RoleRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public RoleRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <returns></returns>
        public async Task<int> InsertRole(RoleInfoEntity roleEntity)
        {
            return await _db.Insertable(roleEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询角色是否绑定员工
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<bool> GetUserRoleIsExist(long roleId)
        {
            return await _db.Queryable<UserRoleEntity>()
                            .With(SqlWith.NoLock)
                            .Where(role => role.RoleId == roleId)
                            .AnyAsync();
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<int> DeleteRole(long roleId)
        {
            return await _db.Deleteable<RoleInfoEntity>()
                            .Where(role => role.RoleId == roleId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除角色模块绑定
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<int> DeleteRoleModule(long roleId)
        {
            return await _db.Deleteable<RoleModuleEntity>()
                            .Where(role => role.RoleId == roleId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除角色菜单绑定
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<int> DeleteRoleMenu(long roleId)
        {
            return await _db.Deleteable<RoleMenuEntity>()
                            .Where(role => role.RoleId == roleId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <returns></returns>
        public async Task<int> UpdateRole(RoleInfoEntity roleEntity)
        {
            return await _db.Updateable(roleEntity)
                            .IgnoreColumns(role => new
                            {
                                role.RoleCode,
                                role.CreatedBy,
                                role.CreatedDate,
                            }).Where(role => role.RoleId == roleEntity.RoleId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询角色实体
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<RoleInfoDto> GetRoleEntity(long roleId)
        {
            var roleEntity = await _db.Queryable<RoleInfoEntity>()
                                      .With(SqlWith.NoLock)
                                      .Where(role => role.RoleId == roleId)
                                      .FirstAsync();
            return roleEntity.Adapt<RoleInfoDto>();
        }

        /// <summary>
        /// 查询角色分页
        /// </summary>
        /// <param name="getRolePage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<RoleInfoDto>> GetRolePage(GetRoleInfoPage getRolePage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<RoleInfoEntity>()
                           .With(SqlWith.NoLock);

            // 角色编码
            if (!string.IsNullOrEmpty(getRolePage.RoleCode))
            {
                query = query.Where(role => role.RoleCode.Contains(getRolePage.RoleCode));
            }
            // 角色名称
            if (!string.IsNullOrEmpty(getRolePage.RoleName))
            {
                query = query.Where(role =>
                    role.RoleNameCn.Contains(getRolePage.RoleName) ||
                    role.RoleNameEn.Contains(getRolePage.RoleName));
            }

            var rolePage = await query.ToPageListAsync(getRolePage.PageIndex, getRolePage.PageSize, totalCount);
            return ResultPaged<RoleInfoDto>.Ok(rolePage.Adapt<List<RoleInfoDto>>(), totalCount, "");
        }

        /// <summary>
        /// 查询角色模块绑定列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<List<RoleModuleDto>> GetRoleModuleList(long roleId)
        {
            var roleModuleList = await _db.Queryable<ModuleInfoEntity>()
                                          .With(SqlWith.NoLock)
                                          .LeftJoin<RoleModuleEntity>((module, rolemodule) => module.ModuleId == rolemodule.ModuleId && rolemodule.RoleId == roleId)
                                          .Select((module, rolemodule) => new RoleModuleDto
                                          {
                                              ModuleId = module.ModuleId,
                                              ModuleName = _lang.Locale == "zh-CN"
                                                           ? module.ModuleNameCn
                                                           : module.ModuleNameEn,
                                              IsChecked = SqlFunc.IsNull(rolemodule.RoleId, 0) > 0,
                                              IsEnabled = module.IsEnabled == 1
                                          }).ToListAsync();
            return roleModuleList;
        }

        /// <summary>
        /// 角色菜单绑定树
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<List<RoleMenuDto>> GetRoleMenuTree(long roleId, long moduleId)
        {
            return await _db.Queryable<MenuInfoEntity>()
                            .With(SqlWith.NoLock)
                            .LeftJoin<RoleMenuEntity>((menu, rolemenu) => menu.MenuId == rolemenu.MenuId && rolemenu.RoleId ==roleId)
                            .Where((menu,rolemenu)=> menu.ModuleId == moduleId )
                            .OrderBy((menu, rolemenu) => menu.SortOrder)
                            .Select((menu, rolemenu) => new RoleMenuDto
                            {
                                MenuId = menu.MenuId,
                                ParentMenuId = menu.ParentMenuId,
                                MenuName = _lang.Locale == "zh-CN"
                                           ? menu.MenuNameCn
                                           : menu.MenuNameEn,
                                IsChecked = SqlFunc.IsNull(rolemenu.RoleId, 0) > 0,
                                IsEnabled = menu.IsEnabled == 1
                            }).ToTreeAsync(menu => menu.MenuChildren, menu => menu.ParentMenuId, 0);
        }

        /// <summary>
        /// 查询模块下的所有菜单Ids
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <returns></returns>
        public async Task<List<long>> GetMenuIds(List<long> moduleIds)
        {
            return await _db.Queryable<MenuInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Where(menu => moduleIds.Contains(menu.ModuleId))
                            .Select(menu => menu.MenuId)
                            .ToListAsync();
        }

        /// <summary>
        /// 删除角色模块绑定
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<int> DeleleRoleModule(long roleId)
        {
            return await _db.Deleteable<RoleModuleEntity>()
                            .Where(rolemodule => rolemodule.RoleId == roleId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除角色模块下面的角色菜单绑定
        /// </summary>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        public async Task<int> DeleleRoleMenu(long roleId, List<long> menuIds)
        {
            return await _db.Deleteable<RoleMenuEntity>()
                            .Where(rolemenu => rolemenu.RoleId == roleId && menuIds.Contains(rolemenu.MenuId))
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增角色模块绑定
        /// </summary>
        /// <param name="roleModuleList"></param>
        /// <returns></returns>
        public async Task<int> InsertRoleModule(List<RoleModuleEntity> roleModuleList)
        {
            return await _db.Insertable(roleModuleList).ExecuteCommandAsync();
        }

        /// <summary>
        /// 角色模块下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleModuleDropDto>> GetRoleModuleDropDown(long roleId)
        {
            return await _db.Queryable<RoleModuleEntity>()
                            .With(SqlWith.NoLock)
                            .LeftJoin<ModuleInfoEntity>((rolemodule, module) => rolemodule.ModuleId == module.ModuleId)
                            .Where((rolemodule, module) => rolemodule.RoleId == roleId)
                            .OrderBy((rolemodule, module) => module.SortOrder)
                            .Select((rolemodule, module) => new RoleModuleDropDto
                            {
                                ModuleId = module.ModuleId,
                                ModuleName = _lang.Locale == "zh-CN"
                                             ? module.ModuleNameCn
                                             : module.ModuleNameEn,
                                Disabled = module.IsEnabled == 0
                            }).ToListAsync();
        }

        /// <summary>
        /// 查询模块下的所有菜单Ids
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<List<long>> GetModuleMenuIds(long moduleId)
        {
            return await _db.Queryable<MenuInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Where(menu => menu.ModuleId == moduleId)
                            .Select(menu => menu.MenuId)
                            .ToListAsync();
        }

        /// <summary>
        /// 删除角色菜单绑定
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<int> DeleteRoleMenu(long roleId, List<long> menuIds)
        {
            return await _db.Deleteable<RoleMenuEntity>()
                            .Where(rolemodule => rolemodule.RoleId == roleId && menuIds.Contains(rolemodule.MenuId))
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增角色菜单绑定
        /// </summary>
        /// <param name="roleMenuList"></param>
        /// <returns></returns>
        public async Task<int> InsertRoleMenu(List<RoleMenuEntity> roleMenuList)
        {
            return await _db.Insertable(roleMenuList).ExecuteCommandAsync();
        }
    }
}
