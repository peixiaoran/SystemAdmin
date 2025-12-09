using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries;
using SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Queries;
using SystemAdmin.Service.SystemBasicMgmt.SystemUserConfig;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.SystemBasicMgmt.SystemUserConfig
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/SystemBasicMgmt/SystemUserConfig/[controller]/[action]")]
    [ApiController]
    public class UserPartTime : ControllerBase
    {
        private readonly UserPartTimeService _userPartTimeService;
        public UserPartTime(UserPartTimeService userPartTimeService)
        {
            _userPartTimeService = userPartTimeService;
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工兼任] 新增员工兼任")]
        public async Task<Result<int>> InsertUserPartTime([FromBody] UserPartTimeInsert userPartTimeInsert)
        {
            return await _userPartTimeService.InsertUserPartTime(userPartTimeInsert);
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工兼任] 删除员工兼任")]
        public async Task<Result<int>> DeleteUserPartTime([FromBody] UserPartTimeUpdateDel userPartTimeUpdateDel)
        {
            return await _userPartTimeService.DeleteUserPartTime(userPartTimeUpdateDel);
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工兼任] 修改员工兼任")]
        public async Task<Result<int>> UpdateUserPartTime([FromBody] UserPartTimeUpdateDel userPartTimeUpdateDel)
        {
            return await _userPartTimeService.UpdateUserPartTime(userPartTimeUpdateDel);
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工兼任] 查询员工分页")]
        public async Task<ResultPaged<UserPartTimeViewDto>> GetUserPartTimeView([FromBody] GetUserInfoPage getUserPage)
        {
            return await _userPartTimeService.GetUserPartTimeView(getUserPage);
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工兼任] 查询员工兼任分页")]
        public async Task<ResultPaged<UserPartTimeDto>> GetUserPartTimePage(GetUserPartTimePage getUserPartTimePage)
        {
            return await _userPartTimeService.GetUserPartTimePage(getUserPartTimePage);
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工兼任] 查询员工兼任实体")]
        public async Task<Result<UserPartTimeDto>> GetUserPartTimeEntity(GetUserPartTimeEntity getUserPartTimeEntity)
        {
            return await _userPartTimeService.GetUserPartTimeEntity(getUserPartTimeEntity);
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工兼任] 职业下拉框")]
        public async Task<Result<List<UserLaborDropDto>>> GetLaborDropDown()
        {
            return await _userPartTimeService.GetLaborDropDown();
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工兼任] 部门下拉框")]
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDropDown()
        {
            return await _userPartTimeService.GetDepartmentDropDown();
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工兼任] 职级下拉框")]
        public async Task<Result<List<UserPositionDropDto>>> GetUserPositionDropDown()
        {
            return await _userPartTimeService.GetUserPositionDropDown();
        }
    }
}
