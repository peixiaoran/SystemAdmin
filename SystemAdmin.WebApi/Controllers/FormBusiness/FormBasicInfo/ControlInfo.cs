using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Commands;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Dto;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Queries;
using SystemAdmin.Service.FormBusiness.FormBasicInfo;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.FormBusiness.FormBasicInfo
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/FormBusiness/FormBasicInfo/[controller]/[action]")]
    [ApiController]
    public class ControlInfo : ControllerBase
    {
        private readonly ControlInfoService _controlInfoService;
        public ControlInfo(ControlInfoService controlInfoService)
        {
            _controlInfoService = controlInfoService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单基础信息")]
        [EndpointSummary("[控件信息] 新增控件信息")]
        public async Task<Result<int>> InsertControlInfo([FromBody] ControlInfoUpsert controlInfoUpsert)
        {
            return await _controlInfoService.InsertControlInfo(controlInfoUpsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单基础信息")]
        [EndpointSummary("[控件信息] 删除控件信息")]
        public async Task<Result<int>> DeleteControlInfo([FromBody] ControlInfoUpsert controlInfoUpsert)
        {
            return await _controlInfoService.DeleteControlInfo(controlInfoUpsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单基础信息")]
        [EndpointSummary("[控件信息] 查询控件信息分页")]
        public async Task<ResultPaged<ControlInfoDto>> GetControlInfoPage([FromBody] GetControlInfoPage getControlInfoPage)
        {
            return await _controlInfoService.GetControlInfoPage(getControlInfoPage);
        }
    }
}
