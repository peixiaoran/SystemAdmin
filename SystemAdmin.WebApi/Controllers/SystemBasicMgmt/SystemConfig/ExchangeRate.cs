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
        [EndpointSummary("[汇率对照] 新增汇率对照")]
        public async Task<Result<int>> InsertExchangeRate([FromBody] ExchangeRateUpsert exchangeRateUpsert)
        {
            return await _exchangeRateService.InsertExchangeRate(exchangeRateUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[汇率对照] 删除汇率对照")]
        public async Task<Result<int>> DeleteExchangeRate([FromBody] ExchangeRateUpsert exchangeRateUpsert)
        {
            return await _exchangeRateService.DeleteExchangeRate(exchangeRateUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[汇率对照] 修改汇率对照")]
        public async Task<Result<int>> UpdateExchangeRate([FromBody] ExchangeRateUpsert exchangeRateUpsert)
        {
            return await _exchangeRateService.UpdateExchangeRate(exchangeRateUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[汇率对照] 查询汇率对照信息分页")]
        public async Task<ResultPaged<ExchangeRateDto>> GetExchangeRatePage([FromBody] GetExchangeRatePage getExchangeRatePage)
        {
            return await _exchangeRateService.GetExchangeRatePage(getExchangeRatePage);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[汇率对照] 查询汇率对照信息实体")]
        public async Task<Result<ExchangeRateDto>> GetExchangeRateEntity([FromBody] GetExchangeRateEntity getExchangeRateEntity)
        {
            return await _exchangeRateService.GetExchangeRateEntity(getExchangeRateEntity);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[汇率对照] 币别信息下拉框")]
        public async Task<Result<List<CurrencyInfoDropDto>>> GetCurrencyInfoDropDown()
        {
            return await _exchangeRateService.GetCurrencyInfoDropDown();
        }
    }
}
