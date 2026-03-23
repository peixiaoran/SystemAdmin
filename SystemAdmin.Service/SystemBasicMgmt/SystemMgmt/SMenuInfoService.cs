using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.Common.Enums.SystemBasicMgmt;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Queries;
using SystemAdmin.Repository.SystemBasicMgmt.SystemMgmt;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemMgmt
{
    public class SMenuInfoService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<SMenuInfoService> _logger;
        private readonly SqlSugarScope _db;
        private readonly SMenuRepository _sMenuRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.SystemMgmt.SMenu";

        public SMenuInfoService(CurrentUser loginuser, ILogger<SMenuInfoService> logger, SqlSugarScope db, SMenuRepository sMenuRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _sMenuRepository = sMenuRepository;
            _localization = localization;
        }

        /// <summary>
        /// 模块下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<ModuleDropDto>>> GetModuleDropDown()
        {
            try
            {
                var drop = await _sMenuRepository.GetModuleDropDown();
                return Result<List<ModuleDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<ModuleDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 一级菜单下拉
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<Result<List<MenuDropDto>>> GetPMenuDropDown(string moduleId)
        {
            try
            {
                var drop = await _sMenuRepository.GetPMenuDropDown(long.Parse(moduleId));
                return Result<List<MenuDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<MenuDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertSMenu(MenuInfoUpsert upsert)
        {
            try
            {
                var entity = new MenuInfoEntity
                {
                    MenuId = SnowFlakeSingle.Instance.NextId(),
                    MenuCode = upsert.MenuCode,
                    MenuNameCn = upsert.MenuNameCn,
                    MenuNameEn = upsert.MenuNameEn,
                    ParentMenuId = long.Parse(upsert.ParentMenuId),
                    ModuleId = long.Parse(upsert.ModuleId),
                    MenuType = MenuType.SecondaryMenu.ToEnumString(),
                    RoutePath = upsert.RoutePath,
                    MenuIcon = upsert.MenuIcon,
                    SortOrder = upsert.SortOrder,
                    IsVisible = upsert.IsVisible,
                    Path = upsert.Path,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now,
                    Redirect = upsert.Redirect,
                    Remark = upsert.Remark
                };

                await _db.BeginTranAsync();
                int count = await _sMenuRepository.InsertSMenu(entity);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}UpdateSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdateFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 删除二级菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteSMenu(string menuId)
        {
            try
            {
                await _db.BeginTranAsync();
                // 删除二级菜单
                var delSMenuCount = await _sMenuRepository.DeleteSMenu(long.Parse(menuId));
                // 删除角色二级菜单
                var delRoleSMenuCount = await _sMenuRepository.DeleteRoleSMenu(long.Parse(menuId));

                await _db.CommitTranAsync();

                return delSMenuCount >= 1
                        ? Result<int>.Ok(delSMenuCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DeleteFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 修改二级菜单
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateSMenu(MenuInfoUpsert upsert)
        {
            try
            {
                var entity = new MenuInfoEntity
                {
                    MenuId = long.Parse(upsert.MenuId),
                    ModuleId = long.Parse(upsert.ModuleId),
                    ParentMenuId = long.Parse(upsert.ParentMenuId),
                    MenuCode = upsert.MenuCode,
                    MenuNameCn = upsert.MenuNameCn,
                    MenuNameEn = upsert.MenuNameEn,
                    Path = upsert.Path,
                    MenuIcon = upsert.MenuIcon,
                    SortOrder = upsert.SortOrder,
                    IsVisible = upsert.IsVisible,
                    RoutePath = upsert.RoutePath,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now,
                    Redirect = upsert.Redirect,
                    Remark = upsert.Remark
                };

                await _db.BeginTranAsync();
                int count = await _sMenuRepository.UpdateSMenu(entity);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}UpdateSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdateFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询菜单实体
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<Result<MenuInfoDto>> GetSMenuEntity(string menuId)
        {
            try
            {
                var entity = await _sMenuRepository.GetSMenuEntity(long.Parse(menuId));
                return Result<MenuInfoDto>.Ok(entity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<MenuInfoDto>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询菜单分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<MenuInfoDto>> GetSMenuPage(GetMenuInfoPage getPage)
        {
            try
            {
                var page = await _sMenuRepository.GetSMenuPage(getPage);
                return page;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<MenuInfoDto>.Failure(500, ex.Message.ToString());
            }
        }
    }
}
