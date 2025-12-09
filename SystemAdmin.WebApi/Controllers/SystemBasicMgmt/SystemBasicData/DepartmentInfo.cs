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
        [EndpointSummary("[部门信息] 新增部门信息")]
        public async Task<Result<int>> InsertDepartmentInfo([FromBody] DepartmentInfoUpsert deptUpsert)
        {
            return await _departmentInfoService.InsertDepartmentInfo(deptUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[部门信息] 删除部门信息")]
        public async Task<Result<int>> DeleteDepartmentInfo([FromBody] DepartmentInfoUpsert deptUpsert)
        {
            return await _departmentInfoService.DeleteDepartmentInfo(deptUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[部门信息] 修改部门信息")]
        public async Task<Result<int>> UpdateDepartmentInfo([FromBody] DepartmentInfoUpsert deptUpsert)
        {
            return await _departmentInfoService.UpdateDepartmentInfo(deptUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[部门信息] 查询部门实体")]
        public async Task<Result<DepartmentInfoDto>> GetDepartmentInfoEntity([FromBody] GetDepartmentInfoEntity getDeptEntity)
        {
            return await _departmentInfoService.GetDepartmentInfoEntity(getDeptEntity);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[部门信息] 查询部门树")]
        public async Task<Result<List<DepartmentInfoDto>>> GetDepartmentInfoTree([FromBody] GetDepartmentTree getDeptPage)
        {
            return await _departmentInfoService.GetDepartmentInfoTree(getDeptPage);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[员工信息] 部门下拉框")]
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDropDown()
        {
            return await _departmentInfoService.GetDepartmentDropDown();
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[部门信息] 部门级别下拉框")]
        public async Task<Result<List<DepartmentLevelDropDto>>> GetDepartmentLevelDropDown()
        {
            return await _departmentInfoService.GetDepartmentLevelDropDown();
        }
    }
}
