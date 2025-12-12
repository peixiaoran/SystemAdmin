using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Queries;
using SystemAdmin.Service.SystemBasicMgmt.SystemAuth;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.SystemBasicMgmt.SystemAuth
{
    [JwtAuthorize]
    [Route("api/SystemBasicMgmt/Auth/[controller]/[action]")]
    [ApiController]
    public class SysModuleMenu : ControllerBase
    {
        private readonly SysModuleMenuService _sysModuleMenuService;

        public SysModuleMenu(SysModuleMenuService sysModuleMenuService)
        {
            _sysModuleMenuService = sysModuleMenuService;
        }

        [HttpPost]
        [Tags("系统基础管理-系统接口")]
        [EndpointSummary("[系统接口] 查询模块")]
        [AllowAnonymous]
        public async Task<Result<List<SysModuleInfoDto>>> GetModuleList()
        {
            return await _sysModuleMenuService.GetModuleList();
        }

        [HttpPost]
        [Tags("系统基础管理-系统接口")]
        [EndpointSummary("[系统接口] 查询菜单树")]
        [AllowAnonymous]
        public async Task<Result<List<SysMenuInfoDto>>> GetMenuTreeList([FromBody] GetSysMenu getSysMenu)
        {
            return await _sysModuleMenuService.GetMenuTreeList(getSysMenu);
        }
    }
}
