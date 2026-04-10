using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.FormBusiness.FormWorkflow
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/FormBusiness/FormWorkFlow/[controller]/[action]")]
    [ApiController]
    public class FormApprovalLimit : ControllerBase
    {
    }
}
