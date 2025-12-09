using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Queries;
using SystemAdmin.Repository.SystemBasicMgmt.SystemSettings;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemSettings
{
    public class DictionaryInfoService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<DictionaryInfoService> _logger;
        private readonly SqlSugarScope _db;
        private readonly DictionaryInfoRepository _dictionaryRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt_SystemSettings_DictionaryInfo_";

        public DictionaryInfoService(CurrentUser loginuser, ILogger<DictionaryInfoService> logger, SqlSugarScope db, DictionaryInfoRepository dictionaryRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _dictionaryRepository = dictionaryRepository;
            _localization = localization;
        }

        /// <summary>
        /// 新增字典信息
        /// </summary>
        /// <param name="dicUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertDictionaryInfo(DictionaryInfoUpsert dicUpsert)
        {
            try
            {
                var dicTypeCodeExist = await _dictionaryRepository.GetDictionaryInfoIsExist(dicUpsert.DicType, dicUpsert.DicCode);

                if (dicTypeCodeExist)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DicTypeCodeIsExist"));
                }
                else
                {
                    await _db.BeginTranAsync();
                    DictionaryInfoEntity insertDicEntity = new DictionaryInfoEntity()
                    {
                        DicId = SnowFlakeSingle.Instance.NextId(),
                        ModuleId = long.Parse(dicUpsert.ModuleId),
                        DicType = dicUpsert.DicType,
                        DicCode = dicUpsert.DicCode,
                        DicNameCn = dicUpsert.DicNameCn,
                        DicNameEn = dicUpsert.DicNameEn,
                        SortOrder = dicUpsert.SortOrder,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    var insertDicCount = await _dictionaryRepository.InsertDictionaryInfo(insertDicEntity);
                    await _db.CommitTranAsync();

                    return insertDicCount >= 1
                            ? Result<int>.Ok(insertDicCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
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
        /// <param name="dicUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteDictionaryInfo(DictionaryInfoUpsert dicUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var delDicCount = await _dictionaryRepository.DeleteDictionaryInfo(dicUpsert);
                await _db.CommitTranAsync();

                return delDicCount >= 1
                        ? Result<int>.Ok(delDicCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
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
        /// <param name="dicUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateDictionaryInfo(DictionaryInfoUpsert dicUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                DictionaryInfoEntity updateDicEntity = new DictionaryInfoEntity()
                {
                    DicId = long.Parse(dicUpsert.DicId),
                    ModuleId = long.Parse(dicUpsert.ModuleId),
                    DicType = dicUpsert.DicType,
                    DicCode = dicUpsert.DicCode,
                    DicNameCn = dicUpsert.DicNameCn,
                    DicNameEn = dicUpsert.DicNameEn,
                    SortOrder = dicUpsert.SortOrder,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                var updateDicCount = await _dictionaryRepository.UpdateDictionaryInfo(updateDicEntity);
                await _db.CommitTranAsync();

                return updateDicCount >= 1
                        ? Result<int>.Ok(updateDicCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// <param name="getDicEntity"></param>
        /// <returns></returns>
        public async Task<Result<DictionaryInfoDto>> GetDictionaryInfoEntity(GetDictionaryInfoEntity getDicEntity)
        {
            try
            {
                var dicEntity = await _dictionaryRepository.GetDictionaryInfoEntity(long.Parse(getDicEntity.DicId));
                return Result<DictionaryInfoDto>.Ok(dicEntity, "");
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
        /// <param name="getDicPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<DictionaryInfoDto>> GetDictionaryInfoPage(GetDictionaryInfoPage getDicPage)
        {
            try
            {
                var dicPage = await _dictionaryRepository.GetDictionaryInfoPage(getDicPage);
                return dicPage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<DictionaryInfoDto>.Failure(500, ex.Message);
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
                var moduleDrop = await _dictionaryRepository.GetModuleDropDown();
                return Result<List<ModuleDropDto>>.Ok(moduleDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<ModuleDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 字典类型下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DicTypeDropDto>>> GetDicTypeDropDown(GetDicTypeDropDown getDicTypeDropDown)
        {
            try
            {
                var dicDrop = await _dictionaryRepository.GetDicTypeDropDown(long.Parse(getDicTypeDropDown.ModuleId));
                return Result<List<DicTypeDropDto>>.Ok(dicDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DicTypeDropDto>>.Failure(500, "");
            }
        }
    }
}
