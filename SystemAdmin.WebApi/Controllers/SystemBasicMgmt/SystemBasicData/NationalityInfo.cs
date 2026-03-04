using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries;
using SystemAdmin.Service.SystemBasicMgmt.SystemBasicData;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.SystemBasicMgmt.SystemBasicData
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/SystemBasicMgmt/SystemBasicData/[controller]/[action]")]
    [ApiController]
    public class NationalityInfo : ControllerBase
    {
        private readonly NationalityInfoService _nationalityService;
        public NationalityInfo(NationalityInfoService nationalityService)
        {
            _nationalityService = nationalityService;
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[国籍信息] 新增国籍信息")]
        public async Task<Result<int>> InsertNationalityInfo([FromBody] NationalityInfoUpsert upsert)
        {
            return await _nationalityService.InsertNationalityInfo(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[国籍信息] 删除国籍信息")]
        public async Task<Result<int>> DeleteNationalityInfo([FromBody] NationalityInfoUpsert upsert)
        {
            return await _nationalityService.DeleteNationalityInfo(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[国籍信息] 修改国籍信息")]
        public async Task<Result<int>> UpdateNationalityInfo([FromBody] NationalityInfoUpsert upsert)
        {
            return await _nationalityService.UpdateNationalityInfo(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[国籍信息] 查询国籍实体")]
        public async Task<Result<NationalityInfoDto>> GetNationalityEntity([FromBody] GetNationalityInfoEntity getEntity)
        {
            return await _nationalityService.GetNationalityEntity(getEntity);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[国籍信息] 查询国籍列表")]
        public async Task<Result<List<NationalityInfoDto>>> GetNationalityInfoList()
        {
            return await _nationalityService.GetNationalityInfoList();
        }
    }
}
