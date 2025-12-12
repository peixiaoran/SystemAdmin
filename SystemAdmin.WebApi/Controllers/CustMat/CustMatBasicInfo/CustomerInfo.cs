using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.CustMat.CustMatBasicInfo.Commands;
using SystemAdmin.Model.CustMat.CustMatBasicInfo.Dto;
using SystemAdmin.Model.CustMat.CustMatBasicInfo.Queries;
using SystemAdmin.Service.CustMat.CustMatBasicInfo;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.CustMat.CustMatBasicInfo
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/CustMat/CustMatBasicInfo/[controller]/[action]")]
    [ApiController]
    public class CustomerInfo : ControllerBase
    {
        private readonly CustomerInfoService _customerInfoService;
        public CustomerInfo(CustomerInfoService customerInfoService)
        {
            _customerInfoService = customerInfoService;
        }

        [HttpPost]
        [Tags("客户生产订单-相关基础信息")]
        [EndpointSummary("[客户信息] 新增客户信息")]
        public async Task<Result<int>> InsertCustomerInfo([FromBody] CustomerInfoUpsert customerInfoUpsert)
        {
            return await _customerInfoService.InsertCustomerInfo(customerInfoUpsert);
        }

        [HttpPost]
        [Tags("客户生产订单-相关基础信息")]
        [EndpointSummary("[客户信息] 删除客户信息")]
        public async Task<Result<int>> DeleteCustomerInfo([FromBody] CustomerInfoUpsert customerInfoUpsert)
        {
            return await _customerInfoService.DeleteCustomerInfo(customerInfoUpsert);
        }

        [HttpPost]
        [Tags("客户生产订单-相关基础信息")]
        [EndpointSummary("[客户信息] 修改客户信息")]
        public async Task<Result<int>> UpdateCustomerInfo([FromBody] CustomerInfoUpsert customerInfoUpsert)
        {
            return await _customerInfoService.UpdateCustomerInfo(customerInfoUpsert);
        }

        [HttpPost]
        [Tags("客户生产订单-相关基础信息")]
        [EndpointSummary("[客户信息] 查询客户信息实体")]
        public async Task<Result<CustomerInfoDto>> GetCustomerInfoEntity([FromBody] GetCustomerInfoEntity getCustomerInfoEntity)
        {
            return await _customerInfoService.GetCustomerInfoEntity(getCustomerInfoEntity);
        }

        [HttpPost]
        [Tags("客户生产订单-相关基础信息")]
        [EndpointSummary("[客户信息] 查询客户信息分页")]
        public async Task<ResultPaged<CustomerInfoDto>> GetCustomerInfoPage([FromBody] GetCustomerInfoPage getCustomerInfoPage)
        {
            return await _customerInfoService.GetCustomerInfoPage(getCustomerInfoPage);
        }
    }
}
