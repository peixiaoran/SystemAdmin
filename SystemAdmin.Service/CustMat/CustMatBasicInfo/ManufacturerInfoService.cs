using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.CustMat.CustMatBasicInfo.Commands;
using SystemAdmin.Model.CustMat.CustMatBasicInfo.Dto;
using SystemAdmin.Model.CustMat.CustMatBasicInfo.Entity;
using SystemAdmin.Model.CustMat.CustMatBasicInfo.Queries;
using SystemAdmin.Repository.CustMat.CustMatBasicInfo;

namespace SystemAdmin.Service.CustMat.CustMatBasicInfo
{
    public class ManufacturerInfoService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<ManufacturerInfoService> _logger;
        private readonly SqlSugarScope _db;
        private readonly ManufacturerInfoRepository _manufacturerInfoRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "CustMat_CustMatBasicInfo_ManufacturerInfo_";

        public ManufacturerInfoService(CurrentUser loginuser, ILogger<ManufacturerInfoService> logger, SqlSugarScope db, ManufacturerInfoRepository manufacturerInfoRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _manufacturerInfoRepository = manufacturerInfoRepository;
            _localization = localization;
        }

        /// <summary>
        /// 新增厂商信息
        /// </summary>
        /// <param name="manufacturerInfoUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertManufacturerInfo(ManufacturerInfoUpsert manufacturerInfoUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                ManufacturerInfoEntity insertManufacturerEntity = new ManufacturerInfoEntity()
                {
                    ManufacturerId = SnowFlakeSingle.Instance.NextId(),
                    ManufacturerCode = manufacturerInfoUpsert.ManufacturerCode,
                    ManufacturerNameCn = manufacturerInfoUpsert.ManufacturerNameCn,
                    ManufacturerNameEn = manufacturerInfoUpsert.ManufacturerNameEn,
                    Description = manufacturerInfoUpsert.Description,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                var insertManufacturerCount = await _manufacturerInfoRepository.InsertManufacturerInfo(insertManufacturerEntity);
                await _db.CommitTranAsync();

                return insertManufacturerCount >= 1
                        ? Result<int>.Ok(insertManufacturerCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
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
        /// 删除厂商信息
        /// </summary>
        /// <param name="manufacturerInfoUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteManufacturerInfo(ManufacturerInfoUpsert manufacturerInfoUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var delManufacturerCount = await _manufacturerInfoRepository.DeleteManufacturerInfo(long.Parse(manufacturerInfoUpsert.ManufacturerId));
                await _db.CommitTranAsync();

                return delManufacturerCount >= 1
                        ? Result<int>.Ok(delManufacturerCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
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
        /// 修改厂商信息
        /// </summary>
        /// <param name="manufacturerInfoUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateManufacturerInfo(ManufacturerInfoUpsert manufacturerInfoUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                ManufacturerInfoEntity updateManufacturerEntity = new ManufacturerInfoEntity()
                {
                    ManufacturerId = long.Parse(manufacturerInfoUpsert.ManufacturerId),
                    ManufacturerCode = manufacturerInfoUpsert.ManufacturerCode,
                    ManufacturerNameCn = manufacturerInfoUpsert.ManufacturerNameCn,
                    ManufacturerNameEn = manufacturerInfoUpsert.ManufacturerNameEn,
                    Email= manufacturerInfoUpsert.Email,
                    Fax = manufacturerInfoUpsert.Fax,
                    Description = manufacturerInfoUpsert.Description,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                var updateManufacturerCount = await _manufacturerInfoRepository.UpdateManufacturerInfo(updateManufacturerEntity);
                await _db.CommitTranAsync();

                return updateManufacturerCount >= 1
                        ? Result<int>.Ok(updateManufacturerCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// 查询厂商信息实体
        /// </summary>
        /// <param name="getManufacturerInfoEntity"></param>
        /// <returns></returns>
        public async Task<Result<ManufacturerInfoDto>> GetManufacturerInfoEntity(GetManufacturerInfoEntity getManufacturerInfoEntity)
        {
            try
            {
                var manufacturerInfoEntity = await _manufacturerInfoRepository.GetManufacturerInfoEntity(long.Parse(getManufacturerInfoEntity.ManufacturerId));
                return Result<ManufacturerInfoDto>.Ok(manufacturerInfoEntity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<ManufacturerInfoDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询厂商信息分页
        /// </summary>
        /// <param name="getManufacturerInfoPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<ManufacturerInfoDto>> GetManufacturerInfoPage(GetManufacturerInfoPage getManufacturerInfoPage)
        {
            try
            {
                return await _manufacturerInfoRepository.GetManufacturerInfoPage(getManufacturerInfoPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<ManufacturerInfoDto>.Failure(500, ex.Message);
            }
        }
    }
}
