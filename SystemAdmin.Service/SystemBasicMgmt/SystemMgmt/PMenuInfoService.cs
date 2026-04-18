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
    public class PMenuInfoService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<PMenuInfoService> _logger;
        private readonly SqlSugarScope _db;
        private readonly PMenuRepository _pMenuRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.SystemMgmt.PMenu";

        public PMenuInfoService(CurrentUser loginuser, ILogger<PMenuInfoService> logger, SqlSugarScope db, PMenuRepository pMenuRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _pMenuRepository = pMenuRepository;
            _localization = localization;
        }

        /// <summary>
        /// 模块下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<ModuleDropDto>>> GetModuleDrop()
        {
            try
            {
                var drop = await _pMenuRepository.GetModuleDrop();
                return Result<List<ModuleDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<ModuleDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 新增一级菜单
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertPMenu(MenuInfoUpsert upsert)
        {
            try
            {
                var entity = new MenuInfoEntity
                {
                    MenuId = SnowFlakeSingle.Instance.NextId(),
                    ParentMenuId = 0,
                    ModuleId = long.Parse(upsert.ModuleId),
                    MenuNameCn = upsert.MenuNameCn,
                    MenuNameEn = upsert.MenuNameEn,
                    MenuCode = upsert.MenuCode,
                    MenuType = MenuType.PrimaryMenu.ToEnumString(),
                    RoutePath = upsert.RoutePath,
                    MenuIcon = upsert.MenuIcon,
                    SortOrder = upsert.SortOrder,
                    IsVisible = upsert.IsVisible,
                    Path = upsert.Path,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now,
                    Remark = upsert.Remark
                };

                await _db.BeginTranAsync();
                int count = await _pMenuRepository.InsertPMenu(entity);
                await _db.CommitTranAsync();

                return Result<int>.Ok(count, _localization.ReturnMsg($"{_this}InsertSuccess"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
            }
        }

        /// <summary>
        /// 删除一级菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeletePMenu(string menuId)
        {
            try
            {
                await _db.BeginTranAsync();
                // 删除一级菜单
                var delPMenuCount = await _pMenuRepository.DeletePMenu(long.Parse(menuId));
                // 删除角色一级菜单
                var delRolePMenuCount = await _pMenuRepository.DeleteRolePMenu(long.Parse(menuId));
                // 查询二级菜单Ids
                var sMenuIds = await _pMenuRepository.GetSMenuIds(long.Parse(menuId));
                // 删除二级菜单
                var delSMenuCount = await _pMenuRepository.DeleteSMenu(long.Parse(menuId));
                // 删除角色二级菜单
                var delRoleSMenuCount = await _pMenuRepository.DeleteRoleSMenu(sMenuIds);
                await _db.CommitTranAsync();

                return Result<int>.Ok(delPMenuCount, _localization.ReturnMsg($"{_this}DeleteSuccess"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DeleteSuccess"));
            }
        }

        /// <summary>
        /// 修改一级菜单
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdatePMenu(MenuInfoUpsert upsert)
        {
            try
            {
                var entity = new MenuInfoEntity
                {
                    MenuId = long.Parse(upsert.MenuId),
                    ModuleId = long.Parse(upsert.ModuleId),
                    MenuNameCn = upsert.MenuNameCn,
                    MenuNameEn = upsert.MenuNameEn,
                    RoutePath = upsert.RoutePath,
                    MenuIcon = upsert.MenuIcon,
                    SortOrder = upsert.SortOrder,
                    IsVisible = upsert.IsVisible,
                    Path = upsert.Path,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now,
                    Redirect = upsert.Redirect,
                    Remark = upsert.Remark
                };

                await _db.BeginTranAsync();
                int count = await _pMenuRepository.UpdatePMenu(entity);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}UpdateSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdateFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdateFailed"));
            }
        }

        /// <summary>
        /// 查询菜单实体
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<Result<MenuInfoDto>> GetPMenuEntity(string menuId)
        {
            try
            {
                var entity = await _pMenuRepository.GetPMenuEntity(long.Parse(menuId));
                return Result<MenuInfoDto>.Ok(entity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<MenuInfoDto>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询一级菜单分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<MenuInfoDto>> GetPMenuPage(GetMenuInfoPage getPage)
        {
            try
            {
                var page = await _pMenuRepository.GetPMenuPage(getPage);
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
