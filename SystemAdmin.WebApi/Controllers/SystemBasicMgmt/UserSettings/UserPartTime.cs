using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Commands;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Dto;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Queries;
using SystemAdmin.Service.SystemBasicMgmt.UserSettings;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.SystemBasicMgmt.UserSettings
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/SystemBasicMgmt/UserSettings/[controller]/[action]")]
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
        public async Task<Result<int>> InsertUserPartTime([FromBody] UserPartTimeInsert upsert)
        {
            return await _userPartTimeService.InsertUserPartTime(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工兼任] 删除员工兼任")]
        public async Task<Result<int>> DeleteUserPartTime([FromBody] UserPartTimeUpdateDel upsertdel)
        {
            return await _userPartTimeService.DeleteUserPartTime(upsertdel);
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工兼任] 修改员工兼任")]
        public async Task<Result<int>> UpdateUserPartTime([FromBody] UserPartTimeUpdateDel upsertdel)
        {
            return await _userPartTimeService.UpdateUserPartTime(upsertdel);
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工兼任] 查询员工分页")]
        public async Task<ResultPaged<UserPartTimeViewDto>> GetUserPartTimeView([FromBody] GetUserInfoPage getPage)
        {
            return await _userPartTimeService.GetUserPartTimeView(getPage);
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工兼任] 查询员工兼任分页")]
        public async Task<ResultPaged<UserPartTimeDto>> GetUserPartTimePage(GetUserPartTimePage getPage)
        {
            return await _userPartTimeService.GetUserPartTimePage(getPage);
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工兼任] 查询员工兼任实体")]
        public async Task<Result<UserPartTimeDto>> GetUserPartTimeEntity(GetUserPartTimeEntity getEntity)
        {
            return await _userPartTimeService.GetUserPartTimeEntity(getEntity);
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工兼任] 职业下拉")]
        public async Task<Result<List<UserLaborDropDto>>> GetLaborDropDown()
        {
            return await _userPartTimeService.GetLaborDropDown();
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工兼任] 部门下拉")]
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDropDown()
        {
            return await _userPartTimeService.GetDepartmentDropDown();
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工兼任] 职级下拉")]
        public async Task<Result<List<UserPositionDropDto>>> GetUserPositionDropDown()
        {
            return await _userPartTimeService.GetUserPositionDropDown();
        }
    }
}
