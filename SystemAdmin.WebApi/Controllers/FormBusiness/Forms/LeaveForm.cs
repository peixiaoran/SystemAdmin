using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Dto;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto;
using SystemAdmin.Service.FormBusiness.Forms;
using SystemAdmin.Service.FormBusiness.Workflow;

namespace SystemAdmin.WebApi.Controllers.FormBusiness.Forms
{
    [JwtAuthorize]
    [Route("api/FormBusiness/Forms/[controller]/[action]")]
    [ApiController]
    public class LeaveForm : ControllerBase
    {
        private readonly FormService _formService;
        private readonly LeaveFormService _leaveFormService;
        public LeaveForm(FormService formService, LeaveFormService leaveFormService)
        {
            _formService = formService;
            _leaveFormService = leaveFormService;
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[请假单] 请假类别下拉")]
        public async Task<Result<List<LeaveTypeDropDto>>> GetLeaveTypeDropDown()
        {
            return await _leaveFormService.GetLeaveTypeDropDown();
        }

        //[HttpPost]
        //[Tags("表单业务管理-表单Forms")]
        //[EndpointSummary("[请假单] 查询最大表单类别表单号")]
        //public async Task<Result<string>> GetFormAutoNo([FromForm] string formTypeId)
        //{
        //    return await _formService.GetFormAutoNo(formTypeId);
        //}

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[请假单] 上传附件")]
        public async Task<Result<List<FormFileDto>>> UploadFile([FromForm] string formId, List<IFormFile> files)
        {
            return await _leaveFormService.UploadFile(formId, files);
        }

        [HttpPost]
        [Tags("表单业务管理-表单Forms")]
        [EndpointSummary("[请假单] 删除附件")]
        public async Task<Result<int>> DeleteFile([FromForm] string fileId, [FromForm] string filePath)
        {
            return await _leaveFormService.DeleteFile(fileId, filePath);
        }
    }
}
