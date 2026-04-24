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
        private readonly ModuleRepository _moduleRepo;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.SystemMgmt.Module";

        public ModuleInfoService(CurrentUser loginuser, ILogger<ModuleInfoService> logger, SqlSugarScope db, ModuleRepository moduleRepo, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _moduleRepo = moduleRepo;
            _localization = localization;
        }

        /// <summary>
        /// 新增模块
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertModule(ModuleInfoUpsert upsert)
        {
            try
            {
                var entity = new ModuleInfoEntity()
                {
                    ModuleId = SnowFlakeSingle.Instance.NextId(),
                    ModuleNameCn = upsert.ModuleNameCn,
                    ModuleNameEn = upsert.ModuleNameEn,
                    ModuleCode = upsert.ModuleCode,
                    ModuleIcon = upsert.ModuleIcon,
                    SortOrder = upsert.SortOrder,
                    IsVisible = upsert.IsVisible,
                    Path = upsert.Path,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now,
                    RemarkCh = upsert.RemarkCh,
                    RemarkEn = upsert.RemarkEn
                };

                await _db.BeginTranAsync();
                int count = await _moduleRepo.InsertModule(entity);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}InsertSuccess"))
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
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteModule(string moduleId)
        {
            try
            {
                await _db.BeginTranAsync();
                // 删除模块
                var delModuleCount = await _moduleRepo.DeleteModule(long.Parse(moduleId));
                // 删除角色模块
                var delRoleModuleCount = await _moduleRepo.DeleteRoleModule(long.Parse(moduleId));
                // 获取删除菜单Ids
                var delMenuIds = await _moduleRepo.GetModuleMenusIds(long.Parse(moduleId));
                // 删除模块下的菜单
                var delMenuCount = await _moduleRepo.DeleteMenu(long.Parse(moduleId));
                // 删除角色菜单绑定
                var delRoleMenuCount = await _moduleRepo.DeleteRoleMenuId(delMenuIds);
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
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateModule(ModuleInfoUpsert upsert)
        {
            try
            {
                var entity = new ModuleInfoEntity()
                {
                    ModuleId = long.Parse(upsert.ModuleId),
                    ModuleNameCn = upsert.ModuleNameCn,
                    ModuleNameEn = upsert.ModuleNameEn,
                    ModuleIcon = upsert.ModuleIcon,
                    SortOrder = upsert.SortOrder,
                    IsVisible = upsert.IsVisible,
                    Path = upsert.Path,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now,
                    RemarkCh = upsert.RemarkCh,
                    RemarkEn = upsert.RemarkEn
                };

                await _db.BeginTranAsync();
                int count = await _moduleRepo.UpdateModule(entity);
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
        /// 查询模块实体
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<Result<ModuleInfoDto>> GetModuleEntity(string moduleId)
        {
            try
            {
                ModuleInfoDto entity = await _moduleRepo.GetModuleEntity(long.Parse(moduleId));
                return Result<ModuleInfoDto>.Ok(entity, "");
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
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<ModuleInfoDto>> GetModulePage(GetModuleInfoPage getPage)
        {
            try
            {
                return await _moduleRepo.GetModulePage(getPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<ModuleInfoDto>.Failure(500, ex.Message);
            }
        }
    }
}
