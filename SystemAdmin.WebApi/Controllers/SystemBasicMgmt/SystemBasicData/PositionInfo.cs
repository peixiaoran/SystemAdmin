using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Service.SystemBasicMgmt.SystemBasicData;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.SystemBasicMgmt.SystemBasicData
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/SystemBasicMgmt/SystemBasicData/[controller]/[action]")]
    [ApiController]
    public class PositionInfo : ControllerBase
    {
        private readonly PositionInfoService _userPositionService;
        public PositionInfo(PositionInfoService userPositionService)
        {
            _userPositionService = userPositionService;
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[职级信息] 查询职级实体")]
        public async Task<Result<PositionInfoDto>> GetPositionInfoEntity([FromForm] string positionId)
        {
            return await _userPositionService.GetPositionInfoEntity(positionId);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[职级信息] 查询职级列表")]
        public async Task<Result<List<PositionInfoDto>>> GetPositionInfoList()
        {
            return await _userPositionService.GetPositionInfoList();
        }
    }
}
