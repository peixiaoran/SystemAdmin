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
    public class UserPosition : ControllerBase
    {
        private readonly UserPositionService _userPositionService;
        public UserPosition(UserPositionService userPositionService)
        {
            _userPositionService = userPositionService;
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[职级信息] 查询职级实体")]
        public async Task<Result<UserPositionDto>> GetUserPositionEntity([FromForm] string positionId)
        {
            return await _userPositionService.GetUserPositionEntity(positionId);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[职级信息] 查询职级列表")]
        public async Task<Result<List<UserPositionDto>>> GetUserPositionList()
        {
            return await _userPositionService.GetUserPositionList();
        }
    }
}
