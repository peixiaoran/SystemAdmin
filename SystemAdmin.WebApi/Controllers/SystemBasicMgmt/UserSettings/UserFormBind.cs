using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
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
    public class UserFormBind : ControllerBase
    {
        private readonly UserFormBindService _userFormBindService;
        public UserFormBind(UserFormBindService userFormBindService)
        {
            _userFormBindService = userFormBindService;
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工表单绑定] 查询员工分页")]
        public async Task<ResultPaged<UserFormBindDto>> GetUserInfoPage([FromBody] GetUserFormBindPage getUserFormBindPage)
        {
            return await _userFormBindService.GetUserInfoPage(getUserFormBindPage);
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工表单绑定] 查询员工表单绑定树")]
        public async Task<Result<List<UserFormBindViewTreeDto>>> GetUserFormBindViewTree([FromBody] GetUserFormBindViewTree getUserFormBindViewTree)
        {
            return await _userFormBindService.GetUserFormBindViewTree(getUserFormBindViewTree);
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工表单绑定] 更新员工表单绑定")]
        public async Task<Result<int>> UpdateUserFormBind([FromBody] UserFormBindUpsert userFormBindUpsert)
        {
            return await _userFormBindService.UpdateUserFormBind(userFormBindUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工表单绑定] 部门下拉框")]
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDropDown()
        {
            return await _userFormBindService.GetDepartmentDropDown();
        }
    }
}
