using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Queries;
using SystemAdmin.Repository.SystemBasicMgmt.SystemMgmt;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemMgmt
{
    public class ModuleInfoService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<ModuleInfoService> _logger;
        private readonly SqlSugarScope _db;
        private readonly ModuleRepository _moduleRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.SystemMgmt.Module";

        public ModuleInfoService(CurrentUser loginuser, ILogger<ModuleInfoService> logger, SqlSugarScope db, ModuleRepository moduleRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _moduleRepository = moduleRepository;
            _localization = localization;
        }

        /// <summary>
        /// 新增模块
        /// </summary>
        /// <param name="moduleUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertModule(ModuleInfoUpsert moduleUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                ModuleInfoEntity insertModuleEntity = new ModuleInfoEntity()
                {
                    ModuleId = SnowFlakeSingle.Instance.NextId(),
                    ModuleNameCn = moduleUpsert.ModuleNameCn,
                    ModuleNameEn = moduleUpsert.ModuleNameEn,
                    ModuleCode = moduleUpsert.ModuleCode,
                    ModuleIcon = moduleUpsert.ModuleIcon,
                    SortOrder = moduleUpsert.SortOrder,
                    IsEnabled = moduleUpsert.IsEnabled,
                    IsVisible = moduleUpsert.IsVisible,
                    Path = moduleUpsert.Path,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    RemarkCh = moduleUpsert.RemarkCh,
                    RemarkEn = moduleUpsert.RemarkEn
                };
                int insertModuleCount = await _moduleRepository.InsertModule(insertModuleEntity);
                await _db.CommitTranAsync();

                return insertModuleCount >= 1
                        ? Result<int>.Ok(insertModuleCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
            }
        }

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="moduleUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteModule(ModuleInfoUpsert moduleUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                // 删除模块
                var delModuleCount = await _moduleRepository.DeleteModule(long.Parse(moduleUpsert.ModuleId));
                // 删除角色模块
                var delRoleModuleCount = await _moduleRepository.DeleteRoleModule(long.Parse(moduleUpsert.ModuleId));
                // 获取删除菜单Ids
                var delMenuIds = await _moduleRepository.GetModuleMenusIds(long.Parse(moduleUpsert.ModuleId));
                // 删除模块下的菜单
                var delMenuCount = await _moduleRepository.DeleteMenu(long.Parse(moduleUpsert.ModuleId));
                // 删除角色菜单绑定
                var delRoleMenuCount = await _moduleRepository.DeleteRoleMenuId(delMenuIds);
                await _db.CommitTranAsync();

                return delModuleCount >= 1
                        ? Result<int>.Ok(delModuleCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DeleteFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="moduleUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateModule(ModuleInfoUpsert moduleUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                ModuleInfoEntity EntityInsert = new ModuleInfoEntity()
                {
                    ModuleId = long.Parse(moduleUpsert.ModuleId),
                    ModuleNameCn = moduleUpsert.ModuleNameCn,
                    ModuleNameEn = moduleUpsert.ModuleNameEn,
                    ModuleIcon = moduleUpsert.ModuleIcon,
                    SortOrder = moduleUpsert.SortOrder,
                    IsEnabled = moduleUpsert.IsEnabled,
                    IsVisible = moduleUpsert.IsVisible,
                    Path = moduleUpsert.Path,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    RemarkCh = moduleUpsert.RemarkCh,
                    RemarkEn = moduleUpsert.RemarkEn
                };
                int updateModuleCount = await _moduleRepository.UpdateModule(EntityInsert);
                await _db.CommitTranAsync();

                return updateModuleCount >= 1
                        ? Result<int>.Ok(updateModuleCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// 查询模块实体
        /// </summary>
        /// <param name="getModuleEntity"></param>
        /// <returns></returns>
        public async Task<Result<ModuleInfoDto>> GetModuleEntity(GetModuleInfoEntity getModuleEntity)
        {
            try
            {
                ModuleInfoDto moduleEntity = await _moduleRepository.GetModuleEntity(long.Parse(getModuleEntity.ModuleId));
                return Result<ModuleInfoDto>.Ok(moduleEntity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<ModuleInfoDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询模块分页
        /// </summary>
        /// <param name="getModulePage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<ModuleInfoDto>> GetModulePage(GetModuleInfoPage getModulePage)
        {
            try
            {
                return await _moduleRepository.GetModulePage(getModulePage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<ModuleInfoDto>.Failure(500, ex.Message);
            }
        }
    }
}
