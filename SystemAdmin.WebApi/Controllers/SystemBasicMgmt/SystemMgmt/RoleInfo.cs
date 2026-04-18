using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Commands;
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
    public class RoleInfo : ControllerBase
    {
        private readonly RoleService _roleService;
        public RoleInfo(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 角色模块下拉")]
        public async Task<Result<List<RoleModuleDropDto>>> GetRoleModuleDrop([FromForm] string roleId)
        {
            return await _roleService.GetRoleModuleDrop(roleId);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 新增角色")]
        public async Task<Result<int>> InsertRole([FromBody] RoleInfoUpsert upsert)
        {
            return await _roleService.InsertRole(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 删除角色")]
        public async Task<Result<int>> DeleteRole([FromForm] string roleId)
        {
            return await _roleService.DeleteRole(roleId);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 修改角色")]
        public async Task<Result<int>> UpdateRole([FromBody] RoleInfoUpsert upsert)
        {
            return await _roleService.UpdateRole(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 查询角色实体")]
        public async Task<Result<RoleInfoDto>> GetRoleEntity([FromForm] string roleId)
        {
            return await _roleService.GetRoleEntity(roleId);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 查询角色分页")]
        public async Task<ResultPaged<RoleInfoDto>> GetRolePage([FromBody] GetRoleInfoPage getPage)
        {
            return await _roleService.GetRolePage(getPage);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 查询角色模块绑定列表")]
        public async Task<Result<List<RoleModuleDto>>> GetRoleModuleList([FromBody] GetRoleModuleList getList)
        {
            return await _roleService.GetRoleModuleList(getList);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 查询角色菜单绑定树")]
        public async Task<Result<List<RoleMenuDto>>> GetRoleMenuTree([FromBody] GetRoleMenuTree getTree)
        {
            return await _roleService.GetRoleMenuTree(getTree);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 修改角色模块绑定")]
        public async Task<Result<int>> UpdateRoleModuleList([FromBody] RoleModuleUpsert upsert)
        {
            return await _roleService.UpdateRoleModuleList(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 修改角色菜单绑定")]
        public async Task<Result<int>> UpdateRoleMenuList([FromBody] RoleMenuUpsert roleMenuUpsert)
        {
            return await _roleService.UpdateRoleMenuList(roleMenuUpsert);
        }
    }
}
