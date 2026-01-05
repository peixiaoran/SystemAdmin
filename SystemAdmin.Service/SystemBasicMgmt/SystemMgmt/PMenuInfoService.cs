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
        /// 新增一级菜单
        /// </summary>
        /// <param name="menuUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertPMenu(MenuInfoUpsert menuUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                MenuInfoEntity insertPMenu = new MenuInfoEntity
                {
                    MenuId = SnowFlakeSingle.Instance.NextId(),
                    ParentMenuId = 0,
                    ModuleId = long.Parse(menuUpsert.ModuleId),
                    MenuNameCn = menuUpsert.MenuNameCn,
                    MenuNameEn = menuUpsert.MenuNameEn,
                    MenuCode = menuUpsert.MenuCode,
                    MenuType = MenuType.PrimaryMenu.ToEnumString(),
                    RoutePath = menuUpsert.RoutePath,
                    MenuIcon = menuUpsert.MenuIcon,
                    SortOrder = menuUpsert.SortOrder,
                    IsVisible = menuUpsert.IsVisible,
                    Path = menuUpsert.Path,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Remark = menuUpsert.Remark
                };
                int insertPMenuCount = await _pMenuRepository.InsertPMenu(insertPMenu);
                await _db.CommitTranAsync();

                return Result<int>.Ok(insertPMenuCount, _localization.ReturnMsg($"{_this}InsertSuccess"));
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
        /// <param name="menuUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeletePMenu(MenuInfoUpsert menuUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                // 删除一级菜单
                var delPMenuCount = await _pMenuRepository.DeletePMenu(long.Parse(menuUpsert.MenuId));
                // 删除角色一级菜单
                var delRolePMenuCount = await _pMenuRepository.DeleteRolePMenu(long.Parse(menuUpsert.MenuId));
                // 查询二级菜单Ids
                var sMenuIds = await _pMenuRepository.GetSMenuIds(long.Parse(menuUpsert.MenuId));
                // 删除二级菜单
                var delSMenuCount = await _pMenuRepository.DeleteSMenu(long.Parse(menuUpsert.MenuId));
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
        /// <param name="menuUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdatePMenu(MenuInfoUpsert menuUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var updatePMenu = new MenuInfoEntity
                {
                    MenuId = long.Parse(menuUpsert.MenuId),
                    ModuleId = long.Parse(menuUpsert.ModuleId),
                    MenuNameCn = menuUpsert.MenuNameCn,
                    MenuNameEn = menuUpsert.MenuNameEn,
                    RoutePath = menuUpsert.RoutePath,
                    MenuIcon = menuUpsert.MenuIcon,
                    SortOrder = menuUpsert.SortOrder,
                    IsVisible = menuUpsert.IsVisible,
                    Path = menuUpsert.Path,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Redirect = menuUpsert.Redirect,
                    Remark = menuUpsert.Remark
                };
                int updatePMenuCount = await _pMenuRepository.UpdatePMenu(updatePMenu);
                await _db.CommitTranAsync();

                return updatePMenuCount >= 1
                    ? Result<int>.Ok(updatePMenuCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// <param name="getMenuEntity"></param>
        /// <returns></returns>
        public async Task<Result<MenuInfoDto>> GetPMenuEntity(GetMenuInfoEntity getMenuEntity)
        {
            try
            {
                var pmenuEntity = await _pMenuRepository.GetPMenuEntity(long.Parse(getMenuEntity.MenuId));
                return Result<MenuInfoDto>.Ok(pmenuEntity, "");
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
        /// <param name="getMenuPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<MenuInfoDto>> GetPMenuPage(GetMenuInfoPage getMenuPage)
        {
            try
            {
                var pmenuPage = await _pMenuRepository.GetPMenuPage(getMenuPage);
                return pmenuPage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<MenuInfoDto>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 模块下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<ModuleDropDto>>> GetModuleDropDown()
        {
            try
            {
                var moduleDrop = await _pMenuRepository.GetModuleDropDown();
                return Result<List<ModuleDropDto>>.Ok(moduleDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<ModuleDropDto>>.Failure(500, ex.Message.ToString());
            }
        }
    }
}
