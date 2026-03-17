using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Queries;
using SystemAdmin.Service.SystemBasicMgmt.SystemMgmt;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.SystemBasicMgmt.SystemMgmt
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/SystemBasicMgmt/SystemMgmt/[controller]/[action]")]
    [ApiController]
    public class ModuleInfo : ControllerBase
    {
        private readonly ModuleInfoService _sysMenuService;
        public ModuleInfo(ModuleInfoService sysMenuService)
        {
            _sysMenuService = sysMenuService;
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[模块信息] 新增模块")]
        public async Task<Result<int>> InsertModule([FromBody] ModuleInfoUpsert upsert)
        {
            return await _sysMenuService.InsertModule(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[模块信息] 删除模块")]
        public async Task<Result<int>> DeleteModule([FromForm] string moduleId)
        {
            return await _sysMenuService.DeleteModule(moduleId);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[模块信息] 修改模块")]
        public async Task<Result<int>> UpdateModule([FromBody] ModuleInfoUpsert upsert)
        {
            return await _sysMenuService.UpdateModule(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[模块信息] 查询模块实体")]
        public async Task<Result<ModuleInfoDto>> GetModuleEntity([FromForm] string moduleId)
        {
            return await _sysMenuService.GetModuleEntity(moduleId);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[模块信息] 查询模块分页")]
        public async Task<ResultPaged<ModuleInfoDto>> GetModulePage([FromBody] GetModuleInfoPage getPage)
        {
            return await _sysMenuService.GetModulePage(getPage);
        }
    }
}
