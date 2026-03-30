using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.FormBusiness.FormAudit.Dto;
using SystemAdmin.Model.FormBusiness.FormAudit.Queries;
using SystemAdmin.Service.FormBusiness.FormAudit;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.FormBusiness.FormAudit
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/FormBusiness/FormBasicInfo/[controller]/[action]")]
    [ApiController]
    public class FormSequence : ControllerBase
    {
        private readonly FormSequenceService _formCountingService;
        public FormSequence(FormSequenceService formCountingService)
        {
            _formCountingService = formCountingService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单审计模块")]
        [EndpointSummary("[表单计数] 查询表单计数分页")]
        public async Task<ResultPaged<FormSequenceDto>> GetFormCountingPage([FromBody] GetFormSequencePage getPage)
        {
            return await _formCountingService.GetFormCountingPage(getPage);
        }
    }
}
