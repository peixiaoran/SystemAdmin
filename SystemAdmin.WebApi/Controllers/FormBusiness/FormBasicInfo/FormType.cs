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
    public class FormType : ControllerBase
    {
        private readonly FormTypeService _formTypeService;
        public FormType(FormTypeService formTypeService)
        {
            _formTypeService = formTypeService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单基础信息")]
        [EndpointSummary("[表单类别] 新增表单类别")]
        public async Task<Result<int>> InsertFormTypeInfo([FromBody] FormTypeUpsert formTypeUpsert)
        {
            return await _formTypeService.InsertFormTypeInfo(formTypeUpsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单基础信息")]
        [EndpointSummary("[表单类别] 删除表单类别")]
        public async Task<Result<int>> DeleteFormTypeInfo([FromBody] FormTypeUpsert userLaborUpsert)
        {
            return await _formTypeService.DeleteFormTypeInfo(userLaborUpsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单基础信息")]
        [EndpointSummary("[表单类别] 修改表单类别")]
        public async Task<Result<int>> UpdateFormTypeInfo([FromBody] FormTypeUpsert formTypeUpsert)
        {
            return await _formTypeService.UpdateFormTypeInfo(formTypeUpsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单基础信息")]
        [EndpointSummary("[表单类别] 查询表单类别实体")]
        public async Task<Result<FormTypeDto>> GetFormTypeEntity([FromBody] GetFormTypeEntity getFormTypeEntity)
        {
            return await _formTypeService.GetFormTypeEntity(getFormTypeEntity);
        }

        [HttpPost]
        [Tags("表单业务管理-表单基础信息")]
        [EndpointSummary("[表单类别] 查询表单类别分页")]
        public async Task<ResultPaged<FormTypeDto>> GetFormTypePage([FromBody] GetFormTypePage getFormTypePage)
        {
            return await _formTypeService.GetFormTypePage(getFormTypePage);
        }

        [HttpPost]
        [Tags("表单业务管理-表单基础信息")]
        [EndpointSummary("[表单类别] 表单组别下拉框")]
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDropDown()
        {
            return await _formTypeService.GetFormGroupDropDown();
        }
    }
}
