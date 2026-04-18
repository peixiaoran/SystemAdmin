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
    public class DepartmentInfo : ControllerBase
    {
        private readonly DepartmentInfoService _departmentInfoService;
        public DepartmentInfo(DepartmentInfoService departmentInfoService)
        {
            _departmentInfoService = departmentInfoService;
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[员工信息] 部门下拉")]
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDrop()
        {
            return await _departmentInfoService.GetDepartmentDrop();
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[部门信息] 部门级别下拉")]
        public async Task<Result<List<DepartmentLevelDropDto>>> GetDepartmentLevelDrop()
        {
            return await _departmentInfoService.GetDepartmentLevelDrop();
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[部门信息] 新增部门信息")]
        public async Task<Result<int>> InsertDepartmentInfo([FromBody] DepartmentInfoUpsert upsert)
        {
            return await _departmentInfoService.InsertDepartmentInfo(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[部门信息] 删除部门信息")]
        public async Task<Result<int>> DeleteDepartmentInfo([FromForm] string deptId)
        {
            return await _departmentInfoService.DeleteDepartmentInfo(deptId);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[部门信息] 修改部门信息")]
        public async Task<Result<int>> UpdateDepartmentInfo([FromBody] DepartmentInfoUpsert upsert)
        {
            return await _departmentInfoService.UpdateDepartmentInfo(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[部门信息] 查询部门实体")]
        public async Task<Result<DepartmentInfoDto>> GetDepartmentInfoEntity([FromForm] string deptId)
        {
            return await _departmentInfoService.GetDepartmentInfoEntity(deptId);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[部门信息] 查询部门树")]
        public async Task<Result<List<DepartmentInfoDto>>> GetDepartmentInfoTree([FromBody] GetDepartmentTree getPage)
        {
            return await _departmentInfoService.GetDepartmentInfoTree(getPage);
        }
    }
}
