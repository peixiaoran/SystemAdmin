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
    public class UserForm : ControllerBase
    {
        private readonly UserFormService _userFormBindService;
        public UserForm(UserFormService userFormBindService)
        {
            _userFormBindService = userFormBindService;
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工表单绑定] 部门下拉")]
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDrop()
        {
            return await _userFormBindService.GetDepartmentDrop();
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工表单绑定] 查询员工分页")]
        public async Task<ResultPaged<UserFormDto>> GetUserInfoPage([FromBody] GetUserFormPage getPage)
        {
            return await _userFormBindService.GetUserInfoPage(getPage);
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工表单绑定] 查询员工表单绑定树")]
        public async Task<Result<List<UserFormViewTreeDto>>> GetUserFormViewTree([FromForm] string userId)
        {
            return await _userFormBindService.GetUserFormViewTree(userId);
        }

        [HttpPost]
        [Tags("系统基础管理-员工相关配置")]
        [EndpointSummary("[员工表单绑定] 更新员工表单绑定")]
        public async Task<Result<int>> UpdateUserForm([FromBody] UserFormUpsert upsert)
        {
            return await _userFormBindService.UpdateUserForm(upsert);
        }
    }
}
