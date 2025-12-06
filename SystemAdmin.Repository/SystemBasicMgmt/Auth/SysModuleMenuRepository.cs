using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.SystemBasicMgmt.Auth.Dto;
using SystemAdmin.Model.SystemBasicMgmt.Auth.Entity;
using SystemAdmin.Model.SystemBasicMgmt.Auth.Queries;

namespace SystemAdmin.Repository.SystemBasicMgmt.Auth
{
    public class SysModuleMenuRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public SysModuleMenuRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 查询一级菜单列表
        /// </summary>
        /// <param name="loginUserId"></param>
        /// <returns></returns>
        public async Task<List<SysModuleInfoDto>> GetModuleList(long loginUserId)
        {
            var moduleList = await _db.Queryable<SysUserInfoEntity>()
                                      .With(SqlWith.NoLock)
                                      .InnerJoin<SysUserRoleEntity>((user, userrole) => user.UserId == userrole.UserId)
                                      .InnerJoin<SysRoleInfoEntity>((user, userrole, role) => userrole.RoleId == role.RoleId)
                                      .InnerJoin<SysRoleModuleEntity>((user, userrole, role, rolemodule) => role.RoleId == rolemodule.RoleId)
                                      .InnerJoin<SysModuleInfoEntity>((user, userrole, role, rolemodule, module) =>
                                          rolemodule.ModuleId == module.ModuleId &&
                                          module.IsEnabled == 1 &&
                                          module.IsVisible == 1)
                                      .Where((user, userrole, role, rolemodule, module) => user.UserId == loginUserId)
                                      .OrderBy((user, userrole, role, rolemodule, module) => module.SortOrder)
                                      .Select((user, userrole, role, rolemodule, module) => new SysModuleInfoDto
                                      {
                                          ModuleId = module.ModuleId,
                                          ModuleNameCn = module.ModuleNameCn,
                                          ModuleNameEn = module.ModuleNameEn,
                                          ModuleIcon = module.ModuleIcon,
                                          Path = module.Path,
                                          RemarkCh = module.RemarkCh,
                                          RemarkEn = module.RemarkEn
                                      }).ToListAsync();

            return moduleList;
        }

        /// <summary>
        /// 查询菜单树
        /// </summary>
        /// <param name="getSysMenu"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<SysMenuInfoDto>> GetMenuTreeList(GetSysMenu getSysMenu, long userId)
        {
            var pmenuTree = await _db.Queryable<SysUserInfoEntity>()
                                     .With(SqlWith.NoLock)
                                     .LeftJoin<SysUserRoleEntity>((user, userrole) => user.UserId == userrole.UserId)
                                     .LeftJoin<SysRoleInfoEntity>((user, userrole, role) => userrole.RoleId == role.RoleId)
                                     .InnerJoin<SysRoleMenuEntity>((user, userrole, role, rolemenu) => role.RoleId == rolemenu.RoleId)
                                     .InnerJoin<SysMenuInfoEntity>((user, userrole, role, rolemenu, menu) => rolemenu.MenuId == menu.MenuId)
                                     .Where((user, userrole, role, rolemenu, menu) => user.UserId == userId && menu.ModuleId == long.Parse(getSysMenu.ModuleId) && menu.IsEnabled && menu.IsVisible)
                                     .OrderBy((user, userrole, role, rolemenu, menu) => menu.SortOrder)
                                     .Select((user, userrole, role, rolemenu, menu) => new SysMenuInfoDto
                                     {
                                         MenuId = menu.MenuId,
                                         ParentMenuId = menu.ParentMenuId,
                                         MenuCode = menu.MenuCode,
                                         MenuName = _lang.Locale == "zh-CN"
                                                         ? menu.MenuNameCn
                                                         : menu.MenuNameEn,
                                         Path = menu.Path,
                                         MenuIcon = menu.MenuIcon
                                     }).ToTreeAsync(menu => menu.MenuChildList, menu => menu.ParentMenuId, 0);
            return pmenuTree;
        }
    }
}
