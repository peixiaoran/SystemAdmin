using Microsoft.Extensions.Logging;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormOperate.Dto;
using SystemAdmin.Repository.FormBusiness.FormOperate;

namespace SystemAdmin.Service.FormBusiness.FormOperate
{
    public class PendingApprovalService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<PendingApprovalService> _logger;
        private readonly PendingApprovalRepository _pendingApprovalRepository;

        public PendingApprovalService(CurrentUser loginuser, ILogger<PendingApprovalService> logger, PendingApprovalRepository pendingApprovalRepository)
        {
            _loginuser = loginuser;
            _logger = logger;
            _pendingApprovalRepository = pendingApprovalRepository;
        }

        /// <summary>
        /// 请假组别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDropDown()
        {
            var drop = await _pendingApprovalRepository.GetFormGroupDropDown();
            return Result<List<FormGroupDropDto>>.Ok(drop);
        }

        /// <summary>
        /// 请假类别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDropDown()
        {
            var drop = await _pendingApprovalRepository.GetFormTypeDropDown();
            return Result<List<FormTypeDropDto>>.Ok(drop);
        }

        /// <summary>
        /// 表单状态下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<FormStatusDropDto>>> GetFormStatusDropDown()
        {
            var drop = await _pendingApprovalRepository.GetFormStatusDropDown();
            return Result<List<FormStatusDropDto>>.Ok(drop);
        }
    }
}
