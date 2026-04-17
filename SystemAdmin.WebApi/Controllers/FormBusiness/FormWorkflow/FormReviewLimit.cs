using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Commands;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Queries;
using SystemAdmin.Service.FormBusiness.FormWorkflow;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.FormBusiness.FormWorkflow
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/FormBusiness/FormWorkFlow/[controller]/[action]")]
    [ApiController]
    public class FormReviewLimit : ControllerBase
    {
        private readonly FormReviewLimitService _FormReviewLimitService;
        public FormReviewLimit(FormReviewLimitService FormReviewLimitService)
        {
            _FormReviewLimitService = FormReviewLimitService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[签核最高上限] 表单组别下拉")]
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDropDown()
        {
            return await _FormReviewLimitService.GetFormGroupDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[签核最高上限] 表单类别下拉")]
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDropDown([FromForm] string formGroupId)
        {
            return await _FormReviewLimitService.GetFormTypeDropDown(formGroupId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[签核最高上限] 新增最高上限")]
        public async Task<Result<int>> InsertFormReviewLimit([FromBody] FormReviewLimitUpsert upsert)
        {
            return await _FormReviewLimitService.InsertFormReviewLimit(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[签核最高上限] 删除最高上限")]
        public async Task<Result<int>> DeleteFormReviewLimit([FromForm] string formTypeId, [FromForm] string positionId)
        {
            return await _FormReviewLimitService.DeleteFormReviewLimit(formTypeId, positionId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[签核最高上限] 修改最高上限")]
        public async Task<Result<int>> UpdateFormReviewLimit([FromBody] FormReviewLimitUpsert upsert)
        {
            return await _FormReviewLimitService.UpdateFormReviewLimit(upsert);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[签核最高上限] 查询最高上限实体")]
        public async Task<Result<FormReviewLimitDto>> GetFormReviewLimitEntity([FromForm] string formTypeId, [FromForm] string positionId)
        {
            return await _FormReviewLimitService.GetFormReviewLimitEntity(formTypeId, positionId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单相关配置")]
        [EndpointSummary("[签核最高上限] 查询最高上限分页")]
        public async Task<ResultPaged<FormReviewLimitDto>> GetFormReviewLimitPage([FromBody] GetFormReviewLimitPage getPage)
        {
            return await _FormReviewLimitService.GetFormReviewLimitPage(getPage);
        }
    }
}
