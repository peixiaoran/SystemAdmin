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
    public class CustomerInfoService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<CustomerInfoService> _logger;
        private readonly SqlSugarScope _db;
        private readonly CustomerInfoRepository _customerInfoRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "CustMat_CustMatBasicInfo_CustomerInfo_";

        public CustomerInfoService(CurrentUser loginuser, ILogger<CustomerInfoService> logger, SqlSugarScope db, CustomerInfoRepository customerInfoRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _customerInfoRepository = customerInfoRepository;
            _localization = localization;
        }

        /// <summary>
        /// 新增客户信息
        /// </summary>
        /// <param name="customerInfoUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertCustomerInfo(CustomerInfoUpsert customerInfoUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                CustomerInfoEntity insertCustomerEntity = new CustomerInfoEntity()
                {
                    CustomerId = SnowFlakeSingle.Instance.NextId(),
                    CustomerCode = customerInfoUpsert.CustomerCode,
                    CustomerNameCn = customerInfoUpsert.CustomerNameCn,
                    CustomerNameEn = customerInfoUpsert.CustomerNameEn,
                    Description = customerInfoUpsert.Description,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                var insertCustomerCount = await _customerInfoRepository.InsertCustomerInfo(insertCustomerEntity);
                await _db.CommitTranAsync();

                return insertCustomerCount >= 1
                        ? Result<int>.Ok(insertCustomerCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
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
        /// 删除客户信息
        /// </summary>
        /// <param name="customerInfoUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteCustomerInfo(CustomerInfoUpsert customerInfoUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var delCustomerCount = await _customerInfoRepository.DeleteCustomerInfo(long.Parse(customerInfoUpsert.CustomerId));
                await _db.CommitTranAsync();

                return delCustomerCount >= 1
                        ? Result<int>.Ok(delCustomerCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
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
        /// 修改客户信息
        /// </summary>
        /// <param name="customerInfoUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateCustomerInfo(CustomerInfoUpsert customerInfoUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                CustomerInfoEntity updateCustomerEntity = new CustomerInfoEntity()
                {
                    CustomerId = long.Parse(customerInfoUpsert.CustomerId),
                    CustomerCode = customerInfoUpsert.CustomerCode,
                    CustomerNameCn = customerInfoUpsert.CustomerNameCn,
                    CustomerNameEn = customerInfoUpsert.CustomerNameEn,
                    Description = customerInfoUpsert.Description,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                var updateCustomerCount = await _customerInfoRepository.UpdateCustomerInfo(updateCustomerEntity);
                await _db.CommitTranAsync();

                return updateCustomerCount >= 1
                        ? Result<int>.Ok(updateCustomerCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// 查询客户信息实体
        /// </summary>
        /// <param name="getCustomerInfoEntity"></param>
        /// <returns></returns>
        public async Task<Result<CustomerInfoDto>> GetCustomerInfoEntity(GetCustomerInfoEntity getCustomerInfoEntity)
        {
            try
            {
                var customerInfoEntity = await _customerInfoRepository.GetCustomerInfoEntity(long.Parse(getCustomerInfoEntity.CustomerId));
                return Result<CustomerInfoDto>.Ok(customerInfoEntity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<CustomerInfoDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询客户信息分页
        /// </summary>
        /// <param name="getCustomerInfoPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<CustomerInfoDto>> GetCustomerInfoPage(GetCustomerInfoPage getCustomerInfoPage)
        {
            try
            {
                return await _customerInfoRepository.GetCustomerInfoPage(getCustomerInfoPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<CustomerInfoDto>.Failure(500, ex.Message);
            }
        }
    }
}
