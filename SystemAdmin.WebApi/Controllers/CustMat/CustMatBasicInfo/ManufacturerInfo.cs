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
    public class ManufacturerInfo : ControllerBase
    {
        private readonly ManufacturerInfoService _manufacturerInfoService;
        public ManufacturerInfo(ManufacturerInfoService manufacturerInfoService)
        {
            _manufacturerInfoService = manufacturerInfoService;
        }

        [HttpPost]
        [Tags("客户生产订单-相关基础信息")]
        [EndpointSummary("[厂商信息] 新增厂商信息")]
        public async Task<Result<int>> InsertManufacturerInfo([FromBody] ManufacturerInfoUpsert manufacturerInfoUpsert)
        {
            return await _manufacturerInfoService.InsertManufacturerInfo(manufacturerInfoUpsert);
        }

        [HttpPost]
        [Tags("客户生产订单-相关基础信息")]
        [EndpointSummary("[厂商信息] 删除厂商信息")]
        public async Task<Result<int>> DeleteManufacturerInfo([FromBody] ManufacturerInfoUpsert manufacturerInfoUpsert)
        {
            return await _manufacturerInfoService.DeleteManufacturerInfo(manufacturerInfoUpsert);
        }

        [HttpPost]
        [Tags(  "客户生产订单-相关基础信息")]
        [EndpointSummary("[厂商信息] 修改厂商信息")]
        public async Task<Result<int>> UpdateManufacturerInfo([FromBody] ManufacturerInfoUpsert manufacturerInfoUpsert)
        {
            return await _manufacturerInfoService.UpdateManufacturerInfo(manufacturerInfoUpsert);
        }

        [HttpPost]
        [Tags("客户生产订单-相关基础信息")]
        [EndpointSummary("[厂商信息] 查询厂商信息实体")]
        public async Task<Result<ManufacturerInfoDto>> GetManufacturerInfoEntity([FromBody] GetManufacturerInfoEntity getManufacturerInfoEntity)
        {
            return await _manufacturerInfoService.GetManufacturerInfoEntity(getManufacturerInfoEntity);
        }

        [HttpPost]
        [Tags("客户生产订单-相关基础信息")]
        [EndpointSummary("[厂商信息] 查询厂商信息分页")]
        public async Task<ResultPaged<ManufacturerInfoDto>> GetManufacturerInfoPage([FromBody] GetManufacturerInfoPage getManufacturerInfoPage)
        {
            return await _manufacturerInfoService.GetManufacturerInfoPage(getManufacturerInfoPage);
        }
    }
}
