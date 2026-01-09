using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries;
using SystemAdmin.Repository.SystemBasicMgmt.SystemBasicData;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemBasicData
{
    public class NationalityInfoService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<NationalityInfoService> _logger;
        private readonly SqlSugarScope _db;
        private readonly NationalityInfoRepository _nationRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.SystemBasicData.NationalityInfo";

        public NationalityInfoService(CurrentUser loginuser, ILogger<NationalityInfoService> logger, SqlSugarScope db, NationalityInfoRepository nationRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _nationRepository = nationRepository;
            _localization = localization;
        }

        /// <summary>
        /// 新增国籍
        /// </summary>
        /// <param name="nationUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertNationalityInfo(NationalityInfoUpsert nationUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                NationalityInfoEntity insertNation = new NationalityInfoEntity()
                {
                    NationId = SnowFlakeSingle.Instance.NextId(),
                    NationNameCn = nationUpsert.NationNameCn,
                    NationNameEn = nationUpsert.NationNameEn,
                    Remark = nationUpsert.Remark,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                int insertNationCount = await _nationRepository.InsertNationalityInfo(insertNation);
                await _db.CommitTranAsync();

                return insertNationCount >= 1
                        ? Result<int>.Ok(insertNationCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 删除国籍
        /// </summary>
        /// <param name="nationUpset"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteNationalityInfo(NationalityInfoUpsert nationUpset)
        {
            try
            {
                await _db.BeginTranAsync();
                int delNationCount = await _nationRepository.DeleteNationalityInfo(long.Parse(nationUpset.NationId));
                await _db.CommitTranAsync();

                return delNationCount >= 1
                        ? Result<int>.Ok(delNationCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
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
        /// 修改国籍
        /// </summary>
        /// <param name="nationUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateNationalityInfo(NationalityInfoUpsert nationUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                NationalityInfoEntity updateNation = new NationalityInfoEntity()
                {
                    NationId = long.Parse(nationUpsert.NationId),
                    NationNameCn = nationUpsert.NationNameCn,
                    NationNameEn = nationUpsert.NationNameEn,
                    Remark = nationUpsert.Remark,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                int updateNationCount = await _nationRepository.UpdateNationalityInfo(updateNation);
                await _db.CommitTranAsync();

                return updateNationCount >= 1
                        ? Result<int>.Ok(updateNationCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// 查询国籍实体
        /// </summary>
        /// <param name="getNationEntity"></param>
        /// <returns></returns>
        public async Task<Result<NationalityInfoDto>> GetNationalityEntity(GetNationalityInfoEntity getNationEntity)
        {
            try
            {
                var nationEntity = await _nationRepository.GetNationalityEntity(long.Parse(getNationEntity.NationId));
                return Result<NationalityInfoDto>.Ok(nationEntity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<NationalityInfoDto>.Failure(500, ex.Message);
            }
        }
        
        /// <summary>
        /// 查询国籍列表
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<NationalityInfoDto>>> GetNationalityInfoList()
        {
            try
            {
                var nationList = await _nationRepository.GetNationalityInfoList();
                return Result<List<NationalityInfoDto>>.Ok(nationList, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<NationalityInfoDto>>.Failure(500, ex.Message);
            }
        }
    }
}
