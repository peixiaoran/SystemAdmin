using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Queries;
using SystemAdmin.Repository.SystemBasicMgmt.SystemConfig;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemConfig
{
    public class DictionaryInfoService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<DictionaryInfoService> _logger;
        private readonly SqlSugarScope _db;
        private readonly DictionaryInfoRepository _dictionaryRepo;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.SystemConfig.DictionaryInfo";

        public DictionaryInfoService(CurrentUser loginuser, ILogger<DictionaryInfoService> logger, SqlSugarScope db, DictionaryInfoRepository dictionaryRepo, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _dictionaryRepo = dictionaryRepo;
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
                var drop = await _dictionaryRepo.GetModuleDrop();
                return Result<List<ModuleDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<ModuleDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 字典类型下拉
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<Result<List<DicTypeDropDto>>> GetDicTypeDrop(string moduleId)
        {
            try
            {
                var drop = await _dictionaryRepo.GetDicTypeDrop(long.Parse(moduleId));
                return Result<List<DicTypeDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DicTypeDropDto>>.Failure(500, "");
            }
        }

        /// <summary>
        /// 新增字典信息
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertDictionaryInfo(DictionaryInfoUpsert upsert)
        {
            try
            {
                var dicTypeCodeExist = await _dictionaryRepo.GetDictionaryInfoIsExist(upsert.DicType, upsert.DicCode);
                if (dicTypeCodeExist)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DicTypeCodeIsExist"));
                }
                else
                {
                    var entity = new DictionaryInfoEntity()
                    {
                        DicId = SnowFlakeSingle.Instance.NextId(),
                        ModuleId = long.Parse(upsert.ModuleId),
                        DicType = upsert.DicType,
                        DicCode = upsert.DicCode,
                        DicNameCn = upsert.DicNameCn,
                        DicNameEn = upsert.DicNameEn,
                        SortOrder = upsert.SortOrder,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now
                    };

                    await _db.BeginTranAsync();
                    var count = await _dictionaryRepo.InsertDictionaryInfo(entity);
                    await _db.CommitTranAsync();

                    return count >= 1
                            ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}InsertSuccess"))
                            : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
                }
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 删除字典信息
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteDictionaryInfo(DictionaryInfoUpsert upsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var count = await _dictionaryRepo.DeleteDictionaryInfo(upsert);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}DeleteSuccess"))
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
        /// 修改字典信息
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateDictionaryInfo(DictionaryInfoUpsert upsert)
        {
            try
            {
                var entity = new DictionaryInfoEntity()
                {
                    DicId = long.Parse(upsert.DicId),
                    ModuleId = long.Parse(upsert.ModuleId),
                    DicType = upsert.DicType,
                    DicCode = upsert.DicCode,
                    DicNameCn = upsert.DicNameCn,
                    DicNameEn = upsert.DicNameEn,
                    SortOrder = upsert.SortOrder,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now
                };

                await _db.BeginTranAsync();
                var count = await _dictionaryRepo.UpdateDictionaryInfo(entity);
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
        /// 查询字典实体
        /// </summary>
        /// <param name="getEntity"></param>
        /// <returns></returns>
        public async Task<Result<DictionaryInfoDto>> GetDictionaryInfoEntity(GetDictionaryInfoEntity getEntity)
        {
            try
            {
                var entity = await _dictionaryRepo.GetDictionaryInfoEntity(long.Parse(getEntity.DicId));
                return Result<DictionaryInfoDto>.Ok(entity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<DictionaryInfoDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询字典分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<DictionaryInfoDto>> GetDictionaryInfoPage(GetDictionaryInfoPage getPage)
        {
            try
            {
                var page = await _dictionaryRepo.GetDictionaryInfoPage(getPage);
                return page;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<DictionaryInfoDto>.Failure(500, ex.Message);
            }
        }
    }
}
