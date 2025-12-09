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
        public async Task<Result<int>> InsertModule([FromBody] ModuleInfoUpsert moduleUpsert)
        {
            return await _sysMenuService.InsertModule(moduleUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[模块信息] 删除模块")]
        public async Task<Result<int>> DeleteModule([FromBody] ModuleInfoUpsert moduleUpsert)
        {
            return await _sysMenuService.DeleteModule(moduleUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[模块信息] 修改模块")]
        public async Task<Result<int>> UpdateModule([FromBody] ModuleInfoUpsert moduleUpsert)
        {
            return await _sysMenuService.UpdateModule(moduleUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[模块信息] 查询模块实体")]
        public async Task<Result<ModuleInfoDto>> GetModuleEntity([FromBody] GetModuleInfoEntity getModuleEntity)
        {
            return await _sysMenuService.GetModuleEntity(getModuleEntity);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[模块信息] 查询模块分页")]
        public async Task<ResultPaged<ModuleInfoDto>> GetModulePage([FromBody] GetModuleInfoPage getModulePage)
        {
            return await _sysMenuService.GetModulePage(getModulePage);
        }
    }
}
