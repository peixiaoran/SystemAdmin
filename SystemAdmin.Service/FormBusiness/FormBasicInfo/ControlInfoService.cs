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
        private readonly string _this = "FormBusiness_FormBasicInfo_ControlInfo_";

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
        /// <param name="controlInfoUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertControlInfo(ControlInfoUpsert controlInfoUpsert)
        {
            try
            {
                ControlInfoEntity insertFromGroupEntity = new ControlInfoEntity()
                {
                    ControlCode = controlInfoUpsert.ControlCode,
                    ControlName = controlInfoUpsert.ControlName,
                    Description = controlInfoUpsert.Description,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                // 判断控件编码是否存在
                bool contorlInfoIsExist = await _controlInfoRepository.GetControlInfoIsExist(controlInfoUpsert.ControlCode);
                if (contorlInfoIsExist)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}IsExist"));
                }
                else
                {
                    await _db.BeginTranAsync();
                    int insertControlInfoCount = await _controlInfoRepository.InsertControlInfo(insertFromGroupEntity);
                    await _db.CommitTranAsync();
                    return insertControlInfoCount >= 1
                        ? Result<int>.Ok(insertControlInfoCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
                }
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
        /// <param name="controlInfoUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteControlInfo(ControlInfoUpsert controlInfoUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                int delControlInfoCount = await _controlInfoRepository.DeleteControlInfo(controlInfoUpsert.ControlCode);
                await _db.CommitTranAsync();

                return delControlInfoCount >= 1
                    ? Result<int>.Ok(delControlInfoCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
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
        /// <param name="getControlInfoPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<ControlInfoDto>> GetControlInfoPage(GetControlInfoPage getControlInfoPage)
        {
            try
            {
                return await _controlInfoRepository.GetControlInfoPage(getControlInfoPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<ControlInfoDto>.Failure(500, ex.Message);
            }
        }
    }
}
