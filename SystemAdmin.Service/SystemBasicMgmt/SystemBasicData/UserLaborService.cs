using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries;
using SystemAdmin.Repository.SystemBasicMgmt.SystemBasicData;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemBasicData
{
    public class UserLaborService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<UserLaborService> _logger;
        private readonly SqlSugarScope _db;
        private readonly UserLaborRepository _userLaborRepo;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.SystemBasicData.UserLabor";

        public UserLaborService(CurrentUser loginuser, ILogger<UserLaborService> logger, SqlSugarScope db, UserLaborRepository userLaborRepo, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _userLaborRepo = userLaborRepo;
            _localization = localization;
        }

        /// <summary>
        /// 新增职业
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertUserLabor(UserLaborUpsert upsert)
        {
            try
            {
                var entity = new UserLaborEntity()
                {
                    LaborId = SnowFlakeSingle.Instance.NextId(),
                    LaborNameCn = upsert.LaborNameCn,
                    LaborNameEn = upsert.LaborNameEn,
                    Description = upsert.Description,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now
                };

                await _db.BeginTranAsync();
                var count = await _userLaborRepo.InsertUserLabor(entity);
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
        /// 删除职业
        /// </summary>
        /// <param name="laborId"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteUserLabor(string laborId)
        {
            try
            {
                await _db.BeginTranAsync();
                var count = await _userLaborRepo.DeleteUserLabor(long.Parse(laborId));
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
        /// 修改职业
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateUserLabor(UserLaborUpsert upsert)
        {
            try
            {
                var entity = new UserLaborEntity()
                {
                    LaborId = long.Parse(upsert.LaborId),
                    LaborNameCn = upsert.LaborNameCn,
                    LaborNameEn = upsert.LaborNameEn,
                    Description = upsert.Description,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now
                };

                await _db.BeginTranAsync();
                var count = await _userLaborRepo.UpdateUserLabor(entity);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}UpdateSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdateFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询职业实体
        /// </summary>
        /// <param name="laborId"></param>
        /// <returns></returns>
        public async Task<Result<UserLaborDto>> GetUserLaborEntity(string laborId)
        {
            try
            {
                var entity = await _userLaborRepo.GetUserLaborEntity(long.Parse(laborId));
                return Result<UserLaborDto>.Ok(entity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<UserLaborDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询职业分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserLaborDto>> GetUserLaborPage(GetUserLaborPage getPage)
        {
            try
            {
                return await _userLaborRepo.GetUserLaborPage(getPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<UserLaborDto>.Failure(500, ex.Message);
            }
        }
    }
}
