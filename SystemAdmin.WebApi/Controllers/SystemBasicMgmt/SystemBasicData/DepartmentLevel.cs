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
    public class DepartmentLevel : ControllerBase
    {
        private readonly DepartmentLevelService _departmentLevelService;
        public DepartmentLevel(DepartmentLevelService departmentLevelService)
        {
            _departmentLevelService = departmentLevelService;
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[部门级别] 新增部门级别信息")]
        public async Task<Result<int>> InsertDepartmentLevel([FromBody] DepartmentLevelUpsert deptLevelUpsert)
        {
            return await _departmentLevelService.InsertDepartmentLevel(deptLevelUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[部门级别] 删除部门级别信息")]
        public async Task<Result<int>> DeleteDepartmentLevel([FromBody] DepartmentLevelUpsert deptLevelUpsert)
        {
            return await _departmentLevelService.DeleteDepartmentLevel(deptLevelUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[部门级别] 修改部门级别信息")]
        public async Task<Result<int>> UpdateDepartmentLevel([FromBody] DepartmentLevelUpsert deptLevelUpsert)
        {
            return await _departmentLevelService.UpdateDepartmentLevel(deptLevelUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[部门级别] 查询部门级别实体")]
        public async Task<Result<DepartmentLevelDto>> GetDepartmentLevelEntity([FromBody] GetDepartmentLevelEntity getDeptLevelEntity)
        {
            return await _departmentLevelService.GetDepartmentLevelEntity(getDeptLevelEntity);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[部门级别] 查询部门级别列表")]
        public async Task<Result<List<DepartmentLevelDto>>> GetDepartmentLevelList()
        {
            return await _departmentLevelService.GetDepartmentLevelList();
        }
    }
}
