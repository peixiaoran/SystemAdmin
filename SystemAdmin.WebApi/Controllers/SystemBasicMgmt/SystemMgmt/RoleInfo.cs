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
        [EndpointSummary("[角色信息] 新增角色")]
        public async Task<Result<int>> InsertRole([FromBody] RoleInfoUpsert roleUpsert)
        {
            return await _roleService.InsertRole(roleUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 删除角色")]
        public async Task<Result<int>> DeleteRole([FromBody] RoleInfoUpsert roleUpsert)
        {
            return await _roleService.DeleteRole(roleUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 修改角色")]
        public async Task<Result<int>> UpdateRole([FromBody] RoleInfoUpsert roleUpsert)
        {
            return await _roleService.UpdateRole(roleUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 查询角色实体")]
        public async Task<Result<RoleInfoDto>> GetRoleEntity([FromBody] GetRoleInfoEntity getRoleEntity)
        {
            return await _roleService.GetRoleEntity(getRoleEntity);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 查询角色分页")]
        public async Task<ResultPaged<RoleInfoDto>> GetRolePage([FromBody] GetRoleInfoPage getRolePage)
        {
            return await _roleService.GetRolePage(getRolePage);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 查询角色模块绑定列表")]
        public async Task<Result<List<RoleModuleDto>>> GetRoleModuleList(GetRoleModuleList getRoleModuleList)
        {
            return await _roleService.GetRoleModuleList(getRoleModuleList);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 查询角色菜单绑定树")]
        public async Task<Result<List<RoleMenuDto>>> GetRoleMenuTree(GetRoleMenuTree getRoleMenuTree)
        {
            return await _roleService.GetRoleMenuTree(getRoleMenuTree);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 修改角色模块绑定")]
        public async Task<Result<int>> UpdateRoleModuleList(RoleModuleUpsert roleModuleUpsert)
        {
            return await _roleService.UpdateRoleModuleList(roleModuleUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 角色模块下拉框")]
        public async Task<Result<List<RoleModuleDropDto>>> GetRoleModuleDropDown(GetRoleModuleDropDown getRoleModuleDrop)
        {
            return await _roleService.GetRoleModuleDropDown(getRoleModuleDrop);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[角色信息] 修改角色菜单绑定")]
        public async Task<Result<int>> UpdateRoleMenuList(RoleMenuUpsert roleMenuUpsert)
        {
            return await _roleService.UpdateRoleMenuList(roleMenuUpsert);
        }
    }
}
