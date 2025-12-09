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
    public class SMenuInfo : ControllerBase
    {
        private readonly SMenuInfoService _sMenuInfoService;
        public SMenuInfo(SMenuInfoService sMenuInfoService)
        {
            _sMenuInfoService = sMenuInfoService;
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[二级菜单信息] 新增二级菜单")]
        public async Task<Result<int>> InsertSMenu([FromBody] MenuInfoUpsert menuUpsert)
        {
            return await _sMenuInfoService.InsertSMenu(menuUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[二级菜单信息] 删除二级菜单")]
        public async Task<Result<int>> DeleteSMenu([FromBody] MenuInfoUpsert menuUpsert)
        {
            return await _sMenuInfoService.DeleteSMenu(menuUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[二级菜单信息] 修改二级菜单")]
        public async Task<Result<int>> UpdateSMenu([FromBody] MenuInfoUpsert menuUpsert)
        {
            return await _sMenuInfoService.UpdateSMenu(menuUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[二级菜单信息] 查询二级菜单实体")]
        public async Task<Result<MenuInfoDto>> GetSMenuEntity([FromBody] GetMenuInfoEntity getMenuEntity)
        {
            return await _sMenuInfoService.GetSMenuEntity(getMenuEntity);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[二级菜单信息] 查询二级菜单分页")]
        public async Task<ResultPaged<MenuInfoDto>> GetSMenuPage([FromBody] GetMenuInfoPage getMenuPage)
        {
            return await _sMenuInfoService.GetSMenuPage(getMenuPage);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[二级菜单信息] 模块下拉框")]
        public async Task<Result<List<ModuleDropDto>>> GetModuleDropDown()
        {
            return await _sMenuInfoService.GetModuleDropDown();
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[二级菜单信息] 一级菜单下拉框")]
        public async Task<Result<List<MenuDropDto>>> GetPMenuDropDown([FromBody] GetPMenuDropDown getPMenuDropDown)
        {
            return await _sMenuInfoService.GetPMenuDropDown(getPMenuDropDown);
        }
    }
}
