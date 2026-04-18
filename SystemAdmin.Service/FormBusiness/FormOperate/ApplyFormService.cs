using Microsoft.Extensions.Logging;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormOperate.Dto;
using SystemAdmin.Model.FormBusiness.FormOperate.Queries;
using SystemAdmin.Repository.FormBusiness.FormOperate;

namespace SystemAdmin.Service.FormBusiness.FormOperate
{
    public class ApplyFormService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<ApplyFormService> _logger;
        private readonly ApplyFormRepository _applyFormRepository;

        public ApplyFormService(CurrentUser loginuser, ILogger<ApplyFormService> logger, ApplyFormRepository applyFormRepository)
        {
            _loginuser = loginuser;
            _logger = logger;
            _applyFormRepository = applyFormRepository;
        }

        /// <summary>
        /// 表单组别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDrop()
        {
            try
            {
                var drop = await _applyFormRepository.GetFormGroupDrop();
                return Result<List<FormGroupDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormGroupDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询申请表单分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<ApplyFormInfoDto>> GetApplyFormPage(getPage getPage)
        {
            try
            {
                return await _applyFormRepository.GetApplyFormPage(getPage, _loginuser.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<ApplyFormInfoDto>.Failure(500, ex.Message);
            }
        }
    }
}
