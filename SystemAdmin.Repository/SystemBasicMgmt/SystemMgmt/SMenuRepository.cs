using Mapster;
using SqlSugar;
using SystemAdmin.Common.Enums.SystemBasicMgmt;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Queries;

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
        /// 模块下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<ModuleDropDto>> GetModuleDrop()
        {
            return await _db.Queryable<ModuleInfoEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(module => module.SortOrder)
                            .Select(module => new ModuleDropDto
                            {
                                ModuleId = module.ModuleId,
                                ModuleName = _lang.Locale == "zh-CN"
                                             ? module.ModuleNameCn
                                             : module.ModuleNameEn
                            }).ToListAsync();
        }

        /// <summary>
        /// 一级菜单下拉
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<List<MenuDropDto>> GetPMenuDrop(long moduleId)
        {
            return await _db.Queryable<MenuInfoEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(pmenu => pmenu.SortOrder)
                            .Where(pmenu => pmenu.MenuType == MenuType.PrimaryMenu.ToEnumString() && pmenu.ModuleId == moduleId)
                            .Select(pmenu => new MenuDropDto
                            {
                                MenuId = pmenu.MenuId,
                                MenuName = _lang.Locale == "zh-CN"
                                           ? pmenu.MenuNameCn
                                           : pmenu.MenuNameEn
                            }).ToListAsync();
        }

        /// <summary>
        /// 新增二级菜单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertSMenu(MenuInfoEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
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
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateSMenu(MenuInfoEntity entity)
        {
            return await _db.Updateable(entity)
                            .IgnoreColumns(smenu => new
                            {
                                smenu.MenuType,
                                smenu.CreatedBy,
                                smenu.CreatedDate,
                            }).Where(smenu => smenu.MenuId == entity.MenuId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询二级菜单实体
        /// </summary>
        /// <param name="smenuId"></param>
        /// <returns></returns>
        public async Task<MenuInfoDto> GetSMenuEntity(long smenuId)
        {
            var entity = await _db.Queryable<MenuInfoEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(smenu => smenu.MenuType == MenuType.SecondaryMenu.ToEnumString() && smenu.MenuId == smenuId)
                                  .FirstAsync();
            return entity.Adapt<MenuInfoDto>();
        }

        /// <summary>
        /// 查询二级菜单分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<MenuInfoDto>> GetSMenuPage(GetMenuInfoPage getPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<MenuInfoEntity>()
                           .With(SqlWith.NoLock)
                           .LeftJoin<DictionaryInfoEntity>((pmenu, dic) => dic.DicType == "MenuType" && pmenu.MenuType == dic.DicCode)
                           .Where((pmenu, dic) => pmenu.MenuType == MenuType.SecondaryMenu.ToEnumString());

            // 二级菜单编码
            if (!string.IsNullOrEmpty(getPage.MenuCode))
            {
                query = query.Where((pmenu, dic) => pmenu.MenuCode.Contains(getPage.MenuCode));
            }
            // 二级菜单名称
            if (!string.IsNullOrEmpty(getPage.MenuName))
            {
                query = query.Where((pmenu, dic) => pmenu.MenuNameCn.Contains(getPage.MenuName) || pmenu.MenuNameEn.Contains(getPage.MenuName));
            }
            // 所属模块
            if (long.Parse(getPage.ModuleId) > 0)
            {
                query = query.Where((pmenu, dic) => pmenu.ModuleId == long.Parse(getPage.ModuleId));
            }
            // 所属一级菜单
            if (long.Parse(getPage.ParentMenuId) > 0)
            {
                query = query.Where((pmenu, dic) => pmenu.ParentMenuId == long.Parse(getPage.ParentMenuId));
            }

            // 排序
            query.OrderBy((pmenu, dic) => pmenu.SortOrder);

            var page = await query.Select((pmenu, dic) => new MenuInfoDto
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
                                      SortOrder = pmenu.SortOrder,
                                      IsVisible = pmenu.IsVisible,
                                      Path = pmenu.Path,
                                      Remark = pmenu.Remark,
                                  }).ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<MenuInfoDto>.Ok(page.Adapt<List<MenuInfoDto>>(), totalCount, "");
        }
    }
}
