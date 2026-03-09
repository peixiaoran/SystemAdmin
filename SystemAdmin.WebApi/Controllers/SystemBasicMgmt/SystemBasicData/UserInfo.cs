using Microsoft.AspNetCore.Mvc;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Service.SystemBasicMgmt.SystemBasicData;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.SystemBasicMgmt.SystemBasicData
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/SystemBasicMgmt/SystemBasicData/[controller]/[action]")]
    [ApiController]
    public class UserInfo : ControllerBase
    {
        private readonly UserInfoService _userInfoService;
        private readonly LocalizationService _localization;
        private readonly string _thisExcel = "SystemBasicMgmt.SystemBasicData.UserExcel_";
        public UserInfo(UserInfoService userInfoService, LocalizationService localization)
        {
            _localization = localization;
            _userInfoService = userInfoService;
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[员工信息] 上传员工头像（新增员工）")]
        public async Task<Result<string>> UploadAvatarInsert([FromForm]IFormFile file)
        {
            return await _userInfoService.UploadAvatar(file);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[员工信息] 上传员工头像（修改员工）")]
        public async Task<Result<string>> UploadAvatarUpdate([FromForm]string userId, IFormFile file)
        {
            return await _userInfoService.UploadAvatar(userId, file);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[员工信息] 新增员工信息")]
        public async Task<Result<int>> InsertUserInfo([FromBody] UserInfoUpsert upsert)
        {
            return await _userInfoService.InsertUserInfo(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[员工信息] 删除员工信息")]
        public async Task<Result<int>> DeleteUserInfo([FromBody] UserInfoUpsert upsert)
        {
            return await _userInfoService.DeleteUserInfo(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[员工信息] 修改员工信息")]
        public async Task<Result<int>> UpdateUserInfo([FromBody] UserInfoUpsert upsert)
        {
            return await _userInfoService.UpdateUserInfo(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[员工信息] 查询员工实体")]
        public async Task<Result<UserInfoEntityDto>> GetUserInfoEntity([FromBody] GetUserInfoEntity getEntity)
        {
            return await _userInfoService.GetUserInfoEntity(getEntity);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[员工信息] 查询员工分页")]
        public async Task<ResultPaged<UserInfoPageDto>> GetUserInfoPage([FromBody] GetUserInfoPage getPage)
        {
            return await _userInfoService.GetUserInfoPage(getPage);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[员工信息] 国籍下拉")]
        public async Task<Result<List<NationalityDropDto>>> GetNationalityDropDown()
        {
            return await _userInfoService.GetNationalityDropDown();
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[员工信息] 职业下拉")]
        public async Task<Result<List<UserLaborDropDto>>> GetLaborDropDown()
        {
            return await _userInfoService.GetLaborDropDown();
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[员工信息] 部门下拉")]
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDropDown()
        {
            return await _userInfoService.GetDepartmentDropDown();
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[员工信息] 职级下拉")]
        public async Task<Result<List<UserPositionDropDto>>> GetUserPositionDropDown()
        {
            return await _userInfoService.GetUserPositionDropDown();
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[员工信息] 角色下拉")]
        public async Task<Result<List<RoleInfoDropDto>>> GetRoleDropDown()
        {
            return await _userInfoService.GetRoleDropDown();
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[员工信息] 导出员工Excel表格")]
        public async Task<IActionResult> ExportUserExcel([FromBody] GetUserInfoExcel getUserExcel)
        {
            var bytes = await _userInfoService.GetUserInfoExcel(getUserExcel);
            return File(
                bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _localization.ReturnMsg($"{_thisExcel}UserInfo") + ".xlsx"
            );
        }
    }
}
