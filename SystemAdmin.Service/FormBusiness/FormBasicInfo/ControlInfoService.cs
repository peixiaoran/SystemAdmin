using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Commands;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Dto;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Queries;
using SystemAdmin.Repository.FormBusiness.FormBasicInfo;

namespace SystemAdmin.Service.FormBusiness.FormBasicInfo
{
    public class ControlInfoService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<ControlInfoService> _logger;
        private readonly SqlSugarScope _db;
        private readonly ControlInfoRepository _controlInfoRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness.FormBasicInfo.ControlInfo";

        public ControlInfoService(CurrentUser loginuser, ILogger<ControlInfoService> logger, SqlSugarScope db, ControlInfoRepository controlInfoRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _controlInfoRepository = controlInfoRepository;
            _localization = localization;
        }

        /// <summary>
        /// 新增控件
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertControlInfo(ControlInfoUpsert upsert)
        {
            try
            {
                var entity = new ControlInfoEntity()
                {
                    ControlCode = upsert.ControlCode,
                    ControlName = upsert.ControlName,
                    Description = upsert.Description,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };

                await _db.BeginTranAsync();
                int count = await _controlInfoRepository.InsertControlInfo(entity);
                await _db.CommitTranAsync();
                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}InsertSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 删除控件
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteControlInfo(ControlInfoUpsert upsert)
        {
            try
            {
                await _db.BeginTranAsync();
                int count = await _controlInfoRepository.DeleteControlInfo(upsert.ControlCode);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}DeleteSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DeleteFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询控件信息分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<ControlInfoDto>> GetControlInfoPage(GetControlInfoPage getPage)
        {
            try
            {
                return await _controlInfoRepository.GetControlInfoPage(getPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<ControlInfoDto>.Failure(500, ex.Message);
            }
        }
    }
}
