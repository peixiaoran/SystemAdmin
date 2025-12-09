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
    public class PMenuInfo : ControllerBase
    {
        private readonly PMenuInfoService _pMenuInfoService;
        public PMenuInfo(PMenuInfoService pMenuInfoService)
        {
            _pMenuInfoService = pMenuInfoService;
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[一级菜单信息] 新增一级菜单")]
        public async Task<Result<int>> InsertPMenu([FromBody] MenuInfoUpsert menuUpsert)
        {
            return await _pMenuInfoService.InsertPMenu(menuUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[一级菜单信息] 删除一级菜单")]
        public async Task<Result<int>> DeletePMenu([FromBody] MenuInfoUpsert menuUpsert)
        {
            return await _pMenuInfoService.DeletePMenu(menuUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[一级菜单信息] 修改一级菜单")]
        public async Task<Result<int>> UpdatePMenu([FromBody] MenuInfoUpsert menuUpsert)
        {
            return await _pMenuInfoService.UpdatePMenu(menuUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[一级菜单信息] 查询一级菜单实体")]
        public async Task<Result<MenuInfoDto>> GetPMenuEntity([FromBody] GetMenuInfoEntity getMenuEntity)
        {
            return await _pMenuInfoService.GetPMenuEntity(getMenuEntity);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[一级菜单信息] 查询一级菜单分页")]
        public async Task<ResultPaged<MenuInfoDto>> GetPMenuPage([FromBody] GetMenuInfoPage getMenuPage)
        {
            return await _pMenuInfoService.GetPMenuPage(getMenuPage);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[一级菜单信息] 模块下拉框")]
        public async Task<Result<List<ModuleDropDto>>> GetModuleDropDown()
        {
            return await _pMenuInfoService.GetModuleDropDown();
        }
    }
}
