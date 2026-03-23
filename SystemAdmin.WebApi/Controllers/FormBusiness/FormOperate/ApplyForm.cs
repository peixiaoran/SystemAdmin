using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.FormBusiness.FormOperate.Dto;
using SystemAdmin.Model.FormBusiness.FormOperate.Queries;
using SystemAdmin.Service.FormBusiness.FormOperate;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.FormBusiness.FormOperate
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/FormBusiness/FormOperate/[controller]/[action]")]
    [ApiController]
    public class ApplyForm : ControllerBase
    {
        private readonly ApplyFormService _applyFormService;
        public ApplyForm(ApplyFormService applyFormService)
        {
            _applyFormService = applyFormService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[申请表单作业] 表单组别下拉")]
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDropDown()
        {
            return await _applyFormService.GetFormGroupDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[申请表单作业] 查询申请表单分页")]
        public async Task<ResultPaged<ApplyFormInfoDto>> GetApplyFormPage([FromBody] getPage getPage)
        {
            return await _applyFormService.GetApplyFormPage(getPage);
        }
    }
}
