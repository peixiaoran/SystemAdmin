using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Queries;
using SystemAdmin.Service.SystemBasicMgmt.SystemConfig;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.SystemBasicMgmt.SystemConfig
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/SystemBasicMgmt/SystemConfig/[controller]/[action]")]
    [ApiController]
    public class ExchangeRate : ControllerBase
    {
        private readonly ExchangeRateService _exchangeRateService;

        public ExchangeRate(ExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[汇率对照] 币别下拉")]
        public async Task<Result<List<CurrencyInfoDropDto>>> GetCurrencyInfoDrop()
        {
            return await _exchangeRateService.GetCurrencyInfoDrop();
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[汇率对照] 新增汇率对照")]
        public async Task<Result<int>> InsertExchangeRate([FromBody] ExchangeRateUpsert upsert)
        {
            return await _exchangeRateService.InsertExchangeRate(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[汇率对照] 删除汇率对照")]
        public async Task<Result<int>> DeleteExchangeRate([FromBody] ExchangeRateUpsert upsert)
        {
            return await _exchangeRateService.DeleteExchangeRate(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[汇率对照] 修改汇率对照")]
        public async Task<Result<int>> UpdateExchangeRate([FromBody] ExchangeRateUpsert upsert)
        {
            return await _exchangeRateService.UpdateExchangeRate(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[汇率对照] 查询汇率对照信息分页")]
        public async Task<ResultPaged<ExchangeRateDto>> GetExchangeRatePage([FromBody] GetExchangeRatePage getPage)
        {
            return await _exchangeRateService.GetExchangeRatePage(getPage);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[汇率对照] 查询汇率对照信息实体")]
        public async Task<Result<ExchangeRateDto>> GetExchangeRateEntity([FromBody] GetExchangeRateEntity getEntity)
        {
            return await _exchangeRateService.GetExchangeRateEntity(getEntity);
        }
    }
}
