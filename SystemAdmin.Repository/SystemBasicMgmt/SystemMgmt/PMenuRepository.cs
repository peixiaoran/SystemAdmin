using Mapster;
using SqlSugar;
using SystemAdmin.Tool;
using SystemAdmin.CommonSetup.DependencyInjection.Localization;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Queries;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemMgmt
{
    public class PMenuRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public PMenuRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 新增一级菜单
        /// </summary>
        /// <param name="menuEntity"></param>
        /// <returns></returns>
        public async Task<int> InsertPMenu(MenuInfoEntity menuEntity)
        {
            return await _db.Insertable(menuEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除一级菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<int> DeletePMenu(long menuId)
        {
            return await _db.Deleteable<MenuInfoEntity>()
                            .Where(pmenu => pmenu.MenuId == menuId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除一级菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<int> DeleteRolePMenu(long menuId)
        {
            return await _db.Deleteable<RoleMenuEntity>()
                            .Where(pmenu => pmenu.MenuId == menuId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询一级菜单子二级菜单Ids
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<List<long>> GetSMenuIds(long menuId)
        {
            return await _db.Queryable<MenuInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Where(smenu => smenu.ParentMenuId == menuId)
                            .Select(smenu => smenu.MenuId)
                            .ToListAsync();
        }

        /// <summary>
        /// 删除二级菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<int> DeleteSMenu(long menuId)
        {
            return await _db.Deleteable<MenuInfoEntity>()
                            .Where(smenu => smenu.ParentMenuId == menuId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除角色二级菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<int> DeleteRoleSMenu(List<long> sMenuIds)
        {
            return await _db.Deleteable<RoleMenuEntity>()
                            .Where(smenu => sMenuIds.Contains(smenu.MenuId))
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改一级菜单
        /// </summary>
        /// <param name="menuEntity"></param>
        /// <returns></returns>
        public async Task<int> UpdatePMenu(MenuInfoEntity menuEntity)
        {
            return await _db.Updateable(menuEntity)
                            .IgnoreColumns(pmenu => new
                            {
                                pmenu.MenuCode,
                                pmenu.MenuType,
                                pmenu.CreatedBy,
                                pmenu.CreatedDate,
                            }).Where(pmenu => pmenu.MenuId == menuEntity.MenuId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询一级菜单实体
        /// </summary>
        /// <param name="pmenuId"></param>
        /// <returns></returns>
        public async Task<MenuInfoDto> GetPMenuEntity(long pmenuId)
        {
            var menuEntity = await _db.Queryable<MenuInfoEntity>()
                                      .With(SqlWith.NoLock)
                                      .Where(pmenu => pmenu.MenuType == 2 && pmenu.MenuId == pmenuId)
                                      .FirstAsync();
            return menuEntity.Adapt<MenuInfoDto>();
        }

        /// <summary>
        /// 查询一级菜单分页
        /// </summary>
        /// <param name="getMenuPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<MenuInfoDto>> GetPMenuPage(GetMenuInfoPage getMenuPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<MenuInfoEntity>()
                           .With(SqlWith.NoLock)
                           .LeftJoin<DictionaryInfoEntity>((pmenu, dic) => dic.DicType == "MenuType" && pmenu.MenuType == dic.DicCode)
                           .LeftJoin<UserInfoEntity>((pmenu, dic, user) => pmenu.CreatedBy == user.UserId)
                           .Where(pmenu => pmenu.MenuType == 2);

            // 一级菜单编码
            if (!string.IsNullOrEmpty(getMenuPage.MenuCode))
            {
                query = query.Where((pmenu, dic, user) => pmenu.MenuCode.Contains(getMenuPage.MenuCode));
            }
            // 一级菜单名称
            if (!string.IsNullOrEmpty(getMenuPage.MenuName))
            {
                query = query.Where((pmenu, dic, user) =>
                    pmenu.MenuNameCn.Contains(getMenuPage.MenuName) ||
                    pmenu.MenuNameEn.Contains(getMenuPage.MenuName));
            }
            // 所属模块Id
            if (!string.IsNullOrEmpty(getMenuPage.ModuleId))
            {
                query = query.Where((pmenu, dic, user) => pmenu.ModuleId == long.Parse(getMenuPage.ModuleId));
            }

            var pmenuPage = await query.OrderBy(pmenu => pmenu.SortOrder)
                                       .Select((pmenu, dic, user) => new MenuInfoDto
                                       {
                                           MenuId = pmenu.MenuId,
                                           MenuCode = pmenu.MenuCode,
                                           MenuNameCn = pmenu.MenuNameCn,
                                           MenuNameEn = pmenu.MenuNameEn,
                                           MenuType = pmenu.MenuType,
                                           MenuTypeName = _lang.Locale == "zh-CN"
                                                          ? dic.DicNameCn
                                                          : dic.DicNameEn,
                                           MenuIcon = pmenu.MenuIcon,
                                           IsEnabled = pmenu.IsEnabled,
                                           IsVisible = pmenu.IsVisible,
                                           Path = pmenu.Path,
                                           CreatedName = SqlFunc.IsNull(user.UserNameCn, "") + " " + SqlFunc.IsNull(user.UserNameEn, ""),
                                           CreatedDate = pmenu.CreatedDate,
                                           Remark = pmenu.Remark,
                                       }).ToPageListAsync(getMenuPage.PageIndex, getMenuPage.PageSize, totalCount);
            return ResultPaged<MenuInfoDto>.Ok(pmenuPage, totalCount, "");
        }

        /// <summary>
        /// 模块下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<ModuleDropDto>> GetModuleDropDown()
        {
            return await _db.Queryable<ModuleInfoEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(module => module.SortOrder)
                            .Select(module => new ModuleDropDto
                            {
                                ModuleId = module.ModuleId,
                                ModuleName = _lang.Locale == "zh-CN"
                                             ? module.ModuleNameCn
                                             : module.ModuleNameEn,
                                Disabled = module.IsEnabled == 0
                            }).ToListAsync();
        }
    }
}
