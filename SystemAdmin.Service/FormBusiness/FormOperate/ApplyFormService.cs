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
        /// 查询申请表单分页
        /// </summary>
        /// <param name="getApplyFormPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<ApplyFormInfoDto>> GetApplyFormPage(getApplyFormPage getApplyFormPage)
        {
            try
            {
                return await _applyFormRepository.GetApplyFormPage(getApplyFormPage, _loginuser.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<ApplyFormInfoDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 表单组别下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDropDown()
        {
            try
            {
                var formGroupDropDown = await _applyFormRepository.GetFormGroupDropDown();
                return Result<List<FormGroupDropDto>>.Ok(formGroupDropDown);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormGroupDropDto>>.Failure(500, ex.Message);
            }
        }
    }
}
