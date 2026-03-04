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
        public async Task<Result<int>> InsertPMenu([FromBody] MenuInfoUpsert upsert)
        {
            return await _pMenuInfoService.InsertPMenu(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[一级菜单信息] 删除一级菜单")]
        public async Task<Result<int>> DeletePMenu([FromBody] MenuInfoUpsert upsert)
        {
            return await _pMenuInfoService.DeletePMenu(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[一级菜单信息] 修改一级菜单")]
        public async Task<Result<int>> UpdatePMenu([FromBody] MenuInfoUpsert upsert)
        {
            return await _pMenuInfoService.UpdatePMenu(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[一级菜单信息] 查询一级菜单实体")]
        public async Task<Result<MenuInfoDto>> GetPMenuEntity([FromBody] GetMenuInfoEntity getEntity)
        {
            return await _pMenuInfoService.GetPMenuEntity(getEntity);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[一级菜单信息] 查询一级菜单分页")]
        public async Task<ResultPaged<MenuInfoDto>> GetPMenuPage([FromBody] GetMenuInfoPage getPage)
        {
            return await _pMenuInfoService.GetPMenuPage(getPage);
        }

        [HttpPost]
        [Tags("系统基础管理-系统管理模块")]
        [EndpointSummary("[一级菜单信息] 模块下拉")]
        public async Task<Result<List<ModuleDropDto>>> GetModuleDropDown()
        {
            return await _pMenuInfoService.GetModuleDropDown();
        }
    }
}
