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
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertManufacturerInfo(ManufacturerInfoUpsert upsert)
        {
            try
            {
                var entity = new ManufacturerInfoEntity()
                {
                    ManufacturerId = SnowFlakeSingle.Instance.NextId(),
                    ManufacturerCode = upsert.ManufacturerCode,
                    ManufacturerNameCn = upsert.ManufacturerNameCn,
                    ManufacturerNameEn = upsert.ManufacturerNameEn,
                    Description = upsert.Description,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };

                await _db.BeginTranAsync();
                var count = await _manufacturerInfoRepository.InsertManufacturerInfo(entity);
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
        /// 删除厂商信息
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteManufacturerInfo(ManufacturerInfoUpsert upsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var delManufacturerCount = await _manufacturerInfoRepository.DeleteManufacturerInfo(long.Parse(upsert.ManufacturerId));
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
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateManufacturerInfo(ManufacturerInfoUpsert upsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var entity = new ManufacturerInfoEntity()
                {
                    ManufacturerId = long.Parse(upsert.ManufacturerId),
                    ManufacturerCode = upsert.ManufacturerCode,
                    ManufacturerNameCn = upsert.ManufacturerNameCn,
                    ManufacturerNameEn = upsert.ManufacturerNameEn,
                    Email= upsert.Email,
                    Fax = upsert.Fax,
                    Description = upsert.Description,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                var count = await _manufacturerInfoRepository.UpdateManufacturerInfo(entity);
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
        /// 查询厂商信息实体
        /// </summary>
        /// <param name="getEntity"></param>
        /// <returns></returns>
        public async Task<Result<ManufacturerInfoDto>> GetManufacturerInfoEntity(GetManufacturerInfoEntity getEntity)
        {
            try
            {
                var manufacturerInfoEntity = await _manufacturerInfoRepository.GetManufacturerInfoEntity(long.Parse(getEntity.ManufacturerId));
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
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<ManufacturerInfoDto>> GetManufacturerInfoPage(GetManufacturerInfoPage getPage)
        {
            try
            {
                return await _manufacturerInfoRepository.GetManufacturerInfoPage(getPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<ManufacturerInfoDto>.Failure(500, ex.Message);
            }
        }
    }
}
