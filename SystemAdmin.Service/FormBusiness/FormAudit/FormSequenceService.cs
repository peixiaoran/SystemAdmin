using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormAudit.Dto;
using SystemAdmin.Model.FormBusiness.FormAudit.Queries;
using SystemAdmin.Repository.FormBusiness.FormAudit;
using SystemAdmin.Service.FormBusiness.FormBasicInfo;

namespace SystemAdmin.Service.FormBusiness.FormAudit
{
    public class FormSequenceService
    {
        private readonly ILogger<FormTypeService> _logger;
        private readonly FormSequenceRepository _formSequenceRepo;

        public FormSequenceService(CurrentUser loginuser, ILogger<FormTypeService> logger, SqlSugarScope db, FormSequenceRepository formSequenceRepo, LocalizationService localization)
        {
            _logger = logger;
            _formSequenceRepo = formSequenceRepo;
        }

        /// <summary>
        /// 查询表单计数信息分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<FormSequenceDto>> GetFormCountingPage(GetFormSequencePage getPage)
        {
            try
            {
                return await _formSequenceRepo.GetFormCountingPage(getPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<FormSequenceDto>.Failure(500, ex.Message);
            }
        }
    }
}
