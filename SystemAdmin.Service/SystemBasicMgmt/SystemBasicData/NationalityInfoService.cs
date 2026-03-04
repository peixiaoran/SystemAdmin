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
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertNationalityInfo(NationalityInfoUpsert upsert)
        {
            try
            {
                await _db.BeginTranAsync();
                NationalityInfoEntity insertNation = new NationalityInfoEntity()
                {
                    NationId = SnowFlakeSingle.Instance.NextId(),
                    NationNameCn = upsert.NationNameCn,
                    NationNameEn = upsert.NationNameEn,
                    Remark = upsert.Remark,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now,
                };
                int count = await _nationRepository.InsertNationalityInfo(insertNation);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}InsertSuccess"))
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
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteNationalityInfo(NationalityInfoUpsert upsert)
        {
            try
            {
                await _db.BeginTranAsync();
                int count = await _nationRepository.DeleteNationalityInfo(long.Parse(upsert.NationId));
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
        /// 修改国籍
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateNationalityInfo(NationalityInfoUpsert upsert)
        {
            try
            {
                await _db.BeginTranAsync();
                NationalityInfoEntity entity = new NationalityInfoEntity()
                {
                    NationId = long.Parse(upsert.NationId),
                    NationNameCn = upsert.NationNameCn,
                    NationNameEn = upsert.NationNameEn,
                    Remark = upsert.Remark,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now,
                };
                int count = await _nationRepository.UpdateNationalityInfo(entity);
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
        /// 查询国籍实体
        /// </summary>
        /// <param name="getEntity"></param>
        /// <returns></returns>
        public async Task<Result<NationalityInfoDto>> GetNationalityEntity(GetNationalityInfoEntity getEntity)
        {
            try
            {
                var entity = await _nationRepository.GetNationalityEntity(long.Parse(getEntity.NationId));
                return Result<NationalityInfoDto>.Ok(entity, "");
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
                var list = await _nationRepository.GetNationalityInfoList();
                return Result<List<NationalityInfoDto>>.Ok(list, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<NationalityInfoDto>>.Failure(500, ex.Message);
            }
        }
    }
}
