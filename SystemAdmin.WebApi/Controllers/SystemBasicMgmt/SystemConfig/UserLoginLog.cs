using Microsoft.AspNetCore.Mvc;
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
    public class UserLoginLog : ControllerBase
    {
        private readonly UserLoginLogService _userLoginLogService;

        public UserLoginLog(UserLoginLogService userLoginLogService)
        {
            _userLoginLogService = userLoginLogService;
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[员工登录日志] 查询登录日志分页")]
        public async Task<ResultPaged<UserLogOutDto>> GetUserLoginLogPage([FromBody] GetUserLoginLogPage getUserLoginLogPage)
        {
            return await _userLoginLogService.GetUserLoginLogPage(getUserLoginLogPage);
        }
    }
}
