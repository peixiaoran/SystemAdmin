using Mapster;
using SqlSugar;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Queries;
using SystemAdmin.CommonSetup.Options;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemMgmt
{
    public class SMenuRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public SMenuRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 新增二级菜单
        /// </summary>
        /// <param name="menuEntity"></param>
        /// <returns></returns>
        public async Task<int> InsertSMenu(MenuInfoEntity menuEntity)
        {
            return await _db.Insertable(menuEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除二级菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<int> DeleteSMenu(long menuId)
        {
            return await _db.Deleteable<MenuInfoEntity>()
                            .Where(smenu => smenu.MenuId == menuId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除角色二级菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<int> DeleteRoleSMenu(long menuId)
        {
            return await _db.Deleteable<RoleMenuEntity>()
                            .Where(smenu => smenu.MenuId == menuId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改二级菜单
        /// </summary>
        /// <param name="menuEntity"></param>
        /// <returns></returns>
        public async Task<int> UpdateSMenu(MenuInfoEntity menuEntity)
        {
            return await _db.Updateable(menuEntity)
                            .IgnoreColumns(smenu => new
                            {
                                smenu.MenuCode,
                                smenu.MenuType,
                                smenu.CreatedBy,
                                smenu.CreatedDate,
                            }).Where(smenu => smenu.MenuId == menuEntity.MenuId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询二级菜单实体
        /// </summary>
        /// <param name="smenuId"></param>
        /// <returns></returns>
        public async Task<MenuInfoDto> GetSMenuEntity(long smenuId)
        {
            var smenuEntity = await _db.Queryable<MenuInfoEntity>()
                                       .With(SqlWith.NoLock)
                                       .Where(smenu => smenu.MenuType == 3 && smenu.MenuId == smenuId)
                                       .FirstAsync();
            return smenuEntity.Adapt<MenuInfoDto>();
        }

        /// <summary>
        /// 查询二级菜单分页
        /// </summary>
        /// <param name="getMenuPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<MenuInfoDto>> GetSMenuPage(GetMenuInfoPage getMenuPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<MenuInfoEntity>()
                           .With(SqlWith.NoLock)
                           .LeftJoin<DictionaryInfoEntity>((pmenu, dic) => dic.DicType == "MenuType" && pmenu.MenuType == dic.DicCode)
                           .LeftJoin<UserInfoEntity>((pmenu, dic, user) => pmenu.CreatedBy == user.UserId)
                           .Where((pmenu, dic, user) => pmenu.MenuType == 3);

            // 二级菜单编码
            if (!string.IsNullOrEmpty(getMenuPage.MenuCode))
            {
                query = query.Where((pmenu, dic, user) => pmenu.MenuCode.Contains(getMenuPage.MenuCode));
            }
            // 二级菜单名称
            if (!string.IsNullOrEmpty(getMenuPage.MenuName))
            {
                query = query.Where((pmenu, dic, user) => pmenu.MenuNameCn.Contains(getMenuPage.MenuName) || pmenu.MenuNameEn.Contains(getMenuPage.MenuName));
            }
            // 所属模块
            if (long.Parse(getMenuPage.ModuleId) > 0)
            {
                query = query.Where((pmenu, dic, user) => pmenu.ModuleId == long.Parse(getMenuPage.ModuleId));
            }
            // 所属一级菜单
            if (long.Parse(getMenuPage.ParentMenuId) > 0)
            {
                query = query.Where((pmenu, dic, user) => pmenu.ParentMenuId == long.Parse(getMenuPage.ParentMenuId));
            }

            var smenuPage = await query.OrderBy((pmenu, dic, user) => pmenu.SortOrder)
                                       .Select((pmenu, dic, user) => new MenuInfoDto
                                       {
                                           MenuId = pmenu.MenuId,
                                           MenuCode = pmenu.MenuCode,
                                           MenuNameCn = pmenu.MenuNameCn,
                                           MenuNameEn = pmenu.MenuNameEn,
                                           MenuType = pmenu.MenuType,
                                           MenuTypeName = _lang.Locale == "zh-cn"
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
            return ResultPaged<MenuInfoDto>.Ok(smenuPage.Adapt<List<MenuInfoDto>>(), totalCount, "");
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
                                ModuleName = _lang.Locale == "zh-cn"
                                             ? module.ModuleNameCn
                                             : module.ModuleNameEn,
                                Disabled = module.IsEnabled == 0
                            }).ToListAsync();
        }

        /// <summary>
        /// 一级菜单下拉框
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<List<MenuDropDto>> GetPMenuDropDown(long moduleId)
        {
            return await _db.Queryable<MenuInfoEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(pmenu => pmenu.SortOrder)
                            .Where(pmenu => pmenu.MenuType == 2 && pmenu.ModuleId == moduleId)
                            .Select(pmenu => new MenuDropDto
                            {
                                MenuId = pmenu.MenuId,
                                MenuName = _lang.Locale == "zh-cn"
                                           ? pmenu.MenuNameCn
                                           : pmenu.MenuNameEn,
                                Disabled = pmenu.IsEnabled == 0
                            }).ToListAsync();
        }
    }
}
