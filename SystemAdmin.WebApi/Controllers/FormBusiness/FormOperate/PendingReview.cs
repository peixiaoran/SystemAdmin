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
    public class PendingReview : ControllerBase
    {
        private readonly PendingReviewService _pendingReviewService;
        public PendingReview(PendingReviewService PendingSubAppService)
        {
            _pendingReviewService = PendingSubAppService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[待签表单列表] 表单组别下拉")]
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDrop()
        {
            return await _pendingReviewService.GetFormGroupDrop();
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[待签表单列表] 表单类别下拉")]
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDrop([FromForm] string formGroupId)
        {
            return await _pendingReviewService.GetFormTypeDrop(formGroupId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[待签表单列表] 表单状态下拉")]
        public async Task<Result<List<FormStatusDropDto>>> GetFormStatusDrop()
        {
            return await _pendingReviewService.GetFormStatusDrop();
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[待审表单列表] 查询待送审分页")]
        public async Task<ResultPaged<PendingReviewDto>> GetPendingSubmissionPage([FromBody] GetPendingSubAppPage getpage)
        {
            return await _pendingReviewService.GetPendingSubmissionPage(getpage);
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[待审表单列表] 查询待审批分页")]
        public async Task<ResultPaged<PendingReviewDto>> GetPendingReviewPage([FromBody] GetPendingSubAppPage getpage)
        {
            return await _pendingReviewService.GetPendingReviewPage(getpage);
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[待审表单列表] 查询表单待审批人列表")]
        public async Task<Result<List<FormPendingReviewDto>>> GetFormPendingReview([FromForm] string formId)
        {
            return await _pendingReviewService.GetFormPendingReview(formId);
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[待签表单列表] 作废表单")]
        public async Task<Result<int>> VoidedForm([FromForm] string formId)
        {
            return await _pendingReviewService.VoidedForm(formId);
        }
    }
}
