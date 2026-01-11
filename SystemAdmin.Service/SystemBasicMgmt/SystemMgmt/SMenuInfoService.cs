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
        /// 新增菜单
        /// </summary>
        /// <param name="menuUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertSMenu(MenuInfoUpsert menuUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var insertSMenu = new MenuInfoEntity
                {
                    MenuId = SnowFlakeSingle.Instance.NextId(),
                    MenuCode = menuUpsert.MenuCode,
                    MenuNameCn = menuUpsert.MenuNameCn,
                    MenuNameEn = menuUpsert.MenuNameEn,
                    ParentMenuId = long.Parse(menuUpsert.ParentMenuId),
                    ModuleId = long.Parse(menuUpsert.ModuleId),
                    MenuType = MenuType.SecondaryMenu.ToEnumString(),
                    RoutePath = menuUpsert.RoutePath,
                    MenuIcon = menuUpsert.MenuIcon,
                    SortOrder = menuUpsert.SortOrder,
                    IsVisible = menuUpsert.IsVisible,
                    Path = menuUpsert.Path,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Redirect = menuUpsert.Redirect,
                    Remark = menuUpsert.Remark
                };

                int insertSMenuCount = await _sMenuRepository.InsertSMenu(insertSMenu);
                await _db.CommitTranAsync();

                return insertSMenuCount >= 1
                        ? Result<int>.Ok(insertSMenuCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// <param name="menuUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteSMenu(MenuInfoUpsert menuUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                // 删除二级菜单
                var delSMenuCount = await _sMenuRepository.DeleteSMenu(long.Parse(menuUpsert.MenuId));
                // 删除角色二级菜单
                var delRoleSMenuCount = await _sMenuRepository.DeleteRoleSMenu(long.Parse(menuUpsert.MenuId));

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
        /// <param name="menuUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateSMenu(MenuInfoUpsert menuUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var updateSMenu = new MenuInfoEntity
                {
                    MenuId = long.Parse(menuUpsert.MenuId),
                    ModuleId = long.Parse(menuUpsert.ModuleId),
                    ParentMenuId = long.Parse(menuUpsert.ParentMenuId),
                    MenuCode = menuUpsert.MenuCode,
                    MenuNameCn = menuUpsert.MenuNameCn,
                    MenuNameEn = menuUpsert.MenuNameEn,
                    Path = menuUpsert.Path,
                    MenuIcon = menuUpsert.MenuIcon,
                    SortOrder = menuUpsert.SortOrder,
                    IsVisible = menuUpsert.IsVisible,
                    RoutePath = menuUpsert.RoutePath,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Redirect = menuUpsert.Redirect,
                    Remark = menuUpsert.Remark
                };

                int updateSMenuCount = await _sMenuRepository.UpdateSMenu(updateSMenu);
                await _db.CommitTranAsync();

                return updateSMenuCount >= 1
                        ? Result<int>.Ok(updateSMenuCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// <param name="getMenuEntity"></param>
        /// <returns></returns>
        public async Task<Result<MenuInfoDto>> GetSMenuEntity(GetMenuInfoEntity getMenuEntity)
        {
            try
            {
                var smenuEntity = await _sMenuRepository.GetSMenuEntity(long.Parse(getMenuEntity.MenuId));
                return Result<MenuInfoDto>.Ok(smenuEntity, "");
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
        /// <param name="getMenuPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<MenuInfoDto>> GetSMenuPage(GetMenuInfoPage getMenuPage)
        {
            try
            {
                var smenuPage = await _sMenuRepository.GetSMenuPage(getMenuPage);
                return smenuPage;
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
                var moduleDrop = await _sMenuRepository.GetModuleDropDown();
                return Result<List<ModuleDropDto>>.Ok(moduleDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<ModuleDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 一级菜单下拉框
        /// </summary>
        /// <param name="getPMenuDropDown"></param>
        /// <returns></returns>
        public async Task<Result<List<MenuDropDto>>> GetPMenuDropDown(GetPMenuDropDown getPMenuDropDown)
        {
            try
            {
                var pmenuDrop = await _sMenuRepository.GetPMenuDropDown(long.Parse(getPMenuDropDown.ModuleId));
                return Result<List<MenuDropDto>>.Ok(pmenuDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<MenuDropDto>>.Failure(500, ex.Message.ToString());
            }
        }
    }
}
