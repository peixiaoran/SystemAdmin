using Microsoft.AspNetCore.Http;
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
    public class PartNumberInfoService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<PartNumberInfoService> _logger;
        private readonly SqlSugarScope _db;
        private readonly PartNumberInfoRepository _partNumberInfoRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "CustMat_CustMatBasicInfo_PartNumberInfo_";

        public PartNumberInfoService(CurrentUser loginuser, ILogger<PartNumberInfoService> logger, SqlSugarScope db, PartNumberInfoRepository partNumberInfoRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _partNumberInfoRepository = partNumberInfoRepository;
            _localization = localization;
        }

        /// <summary>
        /// 新增料号信息
        /// </summary>
        /// <param name="partNumberInfoUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertPartNumberInfo(PartNumberInfoUpsert partNumberInfoUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                PartNumberInfoEntity insertPartNumberEntity = new PartNumberInfoEntity()
                {
                    PartNumberId = SnowFlakeSingle.Instance.NextId(),
                    PartNumberNo = partNumberInfoUpsert.PartNumberNo,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                var insertPartNumberCount = await _partNumberInfoRepository.InsertPartNumberInfo(insertPartNumberEntity);
                await _db.CommitTranAsync();

                return insertPartNumberCount >= 1
                    ? Result<int>.Ok(insertPartNumberCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
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
        /// 删除料号信息
        /// </summary>
        /// <param name="partNumberInfoUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeletePartNumberInfo(PartNumberInfoUpsert partNumberInfoUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var delPartNumberCount = await _partNumberInfoRepository.DeletePartNumberInfo(long.Parse(partNumberInfoUpsert.PartNumberId));
                await _db.CommitTranAsync();

                return delPartNumberCount >= 1
                    ? Result<int>.Ok(delPartNumberCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
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
        /// 修改料号信息
        /// </summary>
        /// <param name="partNumberInfoUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdatePartNumberInfo(PartNumberInfoUpsert partNumberInfoUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                PartNumberInfoEntity updatePartNumberEntity = new PartNumberInfoEntity()
                {
                    PartNumberId = long.Parse(partNumberInfoUpsert.PartNumberId),
                    ManufacturerId = partNumberInfoUpsert.ManufacturerId,
                    PartNumberNo = partNumberInfoUpsert.PartNumberNo,
                    ProductName = partNumberInfoUpsert.ProductName,
                    Specifications = partNumberInfoUpsert.Specifications,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                var updatePartNumberCount = await _partNumberInfoRepository.UpdatePartNumberInfo(updatePartNumberEntity);
                await _db.CommitTranAsync();

                return updatePartNumberCount >= 1
                    ? Result<int>.Ok(updatePartNumberCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// 查询料号信息实体
        /// </summary>
        /// <param name="getPartNumberInfoEntity"></param>
        /// <returns></returns>
        public async Task<Result<PartNumberInfoDto>> GetPartNumberInfoEntity(GetPartNumberInfoEntity getPartNumberInfoEntity)
        {
            try
            {
                var partNumberInfoEntity = await _partNumberInfoRepository.GetPartNumberInfoEntity(long.Parse(getPartNumberInfoEntity.PartNumberId));
                return Result<PartNumberInfoDto>.Ok(partNumberInfoEntity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<PartNumberInfoDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询料号信息分页
        /// </summary>
        /// <param name="getPartNumberInfoPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<PartNumberInfoDto>> GetPartNumberInfoPage(GetPartNumberInfoPage getPartNumberInfoPage)
        {
            try
            {
                return await _partNumberInfoRepository.GetPartNumberInfoPage(getPartNumberInfoPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<PartNumberInfoDto>.Failure(500, ex.Message);
            }
        }
    }
}
