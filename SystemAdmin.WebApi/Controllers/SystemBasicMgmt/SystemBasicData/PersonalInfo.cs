using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Service.SystemBasicMgmt.SystemBasicData;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.SystemBasicMgmt.SystemBasicData
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/SystemBasicMgmt/SystemBasicData/[controller]/[action]")]
    [ApiController]
    public class PersonalInfo : ControllerBase
    {
        private readonly PersonalInfoService _personalInfoService;
        public PersonalInfo(PersonalInfoService personalInfoService)
        {
            _personalInfoService = personalInfoService;
        }


        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[个人信息] 职业下拉")]
        public async Task<Result<List<UserLaborDropDto>>> GetLaborDrop()
        {
            var ss = await _personalInfoService.GetLaborDrop();
            return ss;
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[个人信息] 部门下拉")]
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDrop()
        {
            return await _personalInfoService.GetDepartmentDrop();
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[个人信息] 职级下拉")]
        public async Task<Result<List<PositionInfoDropDto>>> GetPositionInfoDrop()
        {
            return await _personalInfoService.GetPositionInfoDrop();
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[个人信息] 角色下拉")]
        public async Task<Result<List<RoleInfoDropDto>>> GetRoleDrop()
        {
            return await _personalInfoService.GetRoleDrop();
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[个人信息] 上传员工头像")]
        public async Task<Result<string>> UploadAvatar([FromForm] string userId, IFormFile file)
        {
            return await _personalInfoService.UploadAvatar(userId, file);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[个人信息] 查询个人信息实体")]
        public async Task<Result<PersonalInfoDto>> GetPersonalInfoEntity()
        {
            return await _personalInfoService.GetPersonalInfoEntity();
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[个人信息] 修改个人信息")]
        public async Task<Result<int>> UpdatePersonalInfo([FromBody] PersonalInfoUpsert upsert)
        {
            return await _personalInfoService.UpdatePersonalInfo(upsert);
        }
    }
}
