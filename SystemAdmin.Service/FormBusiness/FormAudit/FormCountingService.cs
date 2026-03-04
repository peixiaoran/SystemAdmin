using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormAudit.Dto;
using SystemAdmin.Model.FormBusiness.FormAudit.Queries;
using SystemAdmin.Repository.FormBusiness.FormAudit;
using SystemAdmin.Service.FormBusiness.FormBasicInfo;

namespace SystemAdmin.Service.FormBusiness.FormAudit
{
    public class FormCountingService
    {
        private readonly ILogger<FormTypeService> _logger;
        private readonly FormCountingRepository _formCountingRepository;

        public FormCountingService(CurrentUser loginuser, ILogger<FormTypeService> logger, SqlSugarScope db, FormCountingRepository formTypeRepository, LocalizationService localization)
        {
            _logger = logger;
            _formCountingRepository = formTypeRepository;
        }

        /// <summary>
        /// 查询表单计数信息分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<FormCountingDto>> GetFormCountingPage(GetFormCountingPage getPage)
        {
            try
            {
                return await _formCountingRepository.GetFormCountingPage(getPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<FormCountingDto>.Failure(500, ex.Message);
            }
        }
    }
}
