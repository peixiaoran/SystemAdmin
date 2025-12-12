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
    public class PartNumberInfo : ControllerBase
    {
        private readonly PartNumberInfoService _partNumberService;
        public PartNumberInfo(PartNumberInfoService partNumberService)
        {
            _partNumberService = partNumberService;
        }

        [HttpPost]
        [Tags("客户生产订单-相关基础信息")]
        [EndpointSummary("[料号信息] 新增料号信息")]
        public async Task<Result<int>> InsertPartNumberInfo([FromBody] PartNumberInfoUpsert partNumberUpsert)
        {
            return await _partNumberService.InsertPartNumberInfo(partNumberUpsert);
        }

        [HttpPost]
        [Tags("客户生产订单-相关基础信息")]
        [EndpointSummary("[料号信息] 删除料号信息")]
        public async Task<Result<int>> DeletePartNumberInfo([FromBody] PartNumberInfoUpsert partNumberUpsert)
        {
            return await _partNumberService.DeletePartNumberInfo(partNumberUpsert);
        }

        [HttpPost]
        [Tags("客户生产订单-相关基础信息")]
        [EndpointSummary("[料号信息] 修改料号信息")]
        public async Task<Result<int>> UpdatePartNumberInfo([FromBody] PartNumberInfoUpsert partNumberUpsert)
        {
            return await _partNumberService.UpdatePartNumberInfo(partNumberUpsert);
        }

        [HttpPost]
        [Tags("客户生产订单-相关基础信息")]
        [EndpointSummary("[料号信息] 查询料号信息实体")]
        public async Task<Result<PartNumberInfoDto>> GetPartNumberInfoEntity([FromBody] GetPartNumberInfoEntity getPartNumberInfoEntity)
        {
            return await _partNumberService.GetPartNumberInfoEntity(getPartNumberInfoEntity);
        }

        [HttpPost]
        [Tags("客户生产订单-相关基础信息")]
        [EndpointSummary("[料号信息] 查询料号信息分页")]
        public async Task<ResultPaged<PartNumberInfoDto>> GetPartNumberInfoPage([FromBody] GetPartNumberInfoPage getPartNumberInfoPage)
        {
            return await _partNumberService.GetPartNumberInfoPage(getPartNumberInfoPage);
        }
    }
}
