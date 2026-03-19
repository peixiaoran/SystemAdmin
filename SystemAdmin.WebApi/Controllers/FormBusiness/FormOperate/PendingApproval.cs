using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.FormBusiness.FormOperate.Dto;
using SystemAdmin.Service.FormBusiness.FormOperate;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.FormBusiness.FormOperate
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/FormBusiness/FormOperate/[controller]/[action]")]
    [ApiController]
    public class PendingApproval : ControllerBase
    {
        private readonly PendingApprovalService _pendingApprovalService;
        public PendingApproval(PendingApprovalService pendingApprovalService)
        {
            _pendingApprovalService = pendingApprovalService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[待签表单列表] 表单组别下拉")]
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDropDown()
        {
            return await _pendingApprovalService.GetFormGroupDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[待签表单列表] 表单类别下拉")]
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDropDown()
        {
            return await _pendingApprovalService.GetFormTypeDropDown();
        }

        [HttpPost]
        [Tags("表单业务管理-表单作业模块")]
        [EndpointSummary("[待签表单列表] 表单状态下拉")]
        public async Task<Result<List<FormStatusDropDto>>> GetFormStatusDropDown()
        {
            return await _pendingApprovalService.GetFormStatusDropDown();
        }
    }
}
