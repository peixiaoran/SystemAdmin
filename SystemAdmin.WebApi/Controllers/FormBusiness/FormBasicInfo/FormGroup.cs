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
    public class FormGroup : ControllerBase
    {
        private readonly FormGroupService _formGroupService;
        public FormGroup(FormGroupService formGroupService)
        {
            _formGroupService = formGroupService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单基础信息")]
        [EndpointSummary("[表单组别] 新增表单组别")]
        public async Task<Result<int>> InsertFormGroupInfo([FromBody] FormGroupUpsert upsert)
        {
            return await _formGroupService.InsertFormGroupInfo(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单基础信息")]
        [EndpointSummary("[表单组别] 删除表单组别")]
        public async Task<Result<int>> DeleteFormGroupInfo([FromBody] FormGroupUpsert upsert)
        {
            return await _formGroupService.DeleteFormGroupInfo(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单基础信息")]
        [EndpointSummary("[表单组别] 修改表单组别")]
        public async Task<Result<int>> UpdateFormGroupInfo([FromBody] FormGroupUpsert upsert)
        {
            return await _formGroupService.UpdateFormGroupInfo(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单基础信息")]
        [EndpointSummary("[表单组别] 查询表单组别实体")]
        public async Task<Result<FormGroupDto>> GetFormGroupEntity([FromBody] GetFormGroupEntity getEntity)
        {
            return await _formGroupService.GetFormGroupEntity(getEntity);
        }

        [HttpPost]
        [Tags("表单业务管理-表单基础信息")]
        [EndpointSummary("[表单组别] 查询表单组别分页")]
        public async Task<ResultPaged<FormGroupDto>> GetFormGroupPage([FromBody] GetFormGroupPage getPage)
        {
            return await _formGroupService.GetFormGroupPage(getPage);
        }
    }
}
