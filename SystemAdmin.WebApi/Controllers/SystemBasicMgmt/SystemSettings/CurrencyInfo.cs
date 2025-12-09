using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Queries;
using SystemAdmin.Service.SystemBasicMgmt.SystemSettings;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.SystemBasicMgmt.SystemSettings
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/SystemBasicMgmt/SystemSettings/[controller]/[action]")]
    [ApiController]
    public class CurrencyInfo : ControllerBase
    {
        private readonly CurrencyInfoService _currencyInfoService;

        public CurrencyInfo(CurrencyInfoService currencyInfoService)
        {
            _currencyInfoService = currencyInfoService;
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[币别信息] 新增币别信息")]
        public async Task<Result<int>> InsertCurrencyInfo([FromBody] CurrencyInfoUpsert currencyUpsert)
        {
            return await _currencyInfoService.InsertCurrencyInfo(currencyUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[币别信息] 删除币别信息")]
        public async Task<Result<int>> DeleteCurrencyInfo([FromBody] CurrencyInfoUpsert currencyUpsert)
        {
            return await _currencyInfoService.DeleteCurrencyInfo(currencyUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[币别信息] 修改币别信息")]
        public async Task<Result<int>> UpdateCurrencyInfo([FromBody] CurrencyInfoUpsert currencyUpsert)
        {
            return await _currencyInfoService.UpdateCurrencyInfo(currencyUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[币别信息] 查询币别实体")]
        public async Task<Result<CurrencyInfoDto>> GetCurrencyInfoEntity([FromBody] GetCurrencyInfoEntity getCurrencyEntity)
        {
            return await _currencyInfoService.GetCurrencyInfoEntity(getCurrencyEntity);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[币别信息] 查询币别分页")]
        public async Task<ResultPaged<CurrencyInfoDto>> GetCurrencyInfoPage([FromBody] GetCurrencyInfoPage getCurrencyPage)
        {
            return await _currencyInfoService.GetCurrencyInfoPage(getCurrencyPage);
        }
    }
}
