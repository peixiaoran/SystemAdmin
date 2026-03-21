using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.FormBusiness.FormOperate.Dto;
using SystemAdmin.Model.FormBusiness.FormOperate.Queries;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Commands;
using SystemAdmin.Service.FormBusiness.FormOperate;
using SystemAdmin.Service.FormBusiness.Forms;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.FormBusiness.FormOperate
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/FormBusiness/FormOperate/[controller]/[action]")]
    [ApiController]
    public class PendingSubApp : ControllerBase
    {
        private readonly PendingSubAppService _PendingSubAppService;
        public PendingSubApp(PendingSubAppService PendingSubAppService)
        {
            _PendingSubAppService = PendingSubAppService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[待签表单列表] 表单组别下拉")]
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDropDown()
        {
            return await _PendingSubAppService.GetFormGroupDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[待签表单列表] 表单类别下拉")]
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDropDown([FromForm] string formGroupId)
        {
            return await _PendingSubAppService.GetFormTypeDropDown(formGroupId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[待签表单列表] 表单状态下拉")]
        public async Task<Result<List<FormStatusDropDto>>> GetFormStatusDropDown()
        {
            return await _PendingSubAppService.GetFormStatusDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[待签表单列表] 查询待送审分页")]
        public async Task<ResultPaged<PendingSubAppDto>> GetPendingSubmissionPage(GetPendingSubAppPage getpage)
        {
            return await _PendingSubAppService.GetPendingSubmissionPage(getpage);
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[待签表单列表] 查询待签核分页")]
        public async Task<ResultPaged<PendingSubAppDto>> GetPendingApprovalPage(GetPendingSubAppPage getpage)
        {
            return await _PendingSubAppService.GetPendingApprovalPage(getpage);
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[待签表单列表] 作废表单")]
        public async Task<Result<int>> VoidedForm([FromForm] string formId)
        {
            return await _PendingSubAppService.VoidedForm(formId);
        }
    }
}
