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
        private readonly UserLaborRepository _userLaborRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.SystemBasicData.UserLabor";

        public UserLaborService(CurrentUser loginuser, ILogger<UserLaborService> logger, SqlSugarScope db, UserLaborRepository userLaborRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _userLaborRepository = userLaborRepository;
            _localization = localization;
        }

        /// <summary>
        /// 新增职业
        /// </summary>
        /// <param name="userLaborUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertUserLabor(UserLaborUpsert userLaborUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                UserLaborEntity insertUserLaborEntity = new UserLaborEntity()
                {
                    LaborId = SnowFlakeSingle.Instance.NextId(),
                    LaborNameCn = userLaborUpsert.LaborNameCn,
                    LaborNameEn = userLaborUpsert.LaborNameEn,
                    Description = userLaborUpsert.Description,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                var insertUserLaborCount = await _userLaborRepository.InsertUserLabor(insertUserLaborEntity);
                await _db.CommitTranAsync();

                return insertUserLaborCount >= 1
                    ? Result<int>.Ok(insertUserLaborCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
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
        /// <param name="userLaborUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteUserLabor(UserLaborUpsert userLaborUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var delUserLaborCount = await _userLaborRepository.DeleteUserLabor(long.Parse(userLaborUpsert.LaborId));
                await _db.CommitTranAsync();

                return delUserLaborCount >= 1
                    ? Result<int>.Ok(delUserLaborCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
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
        /// <param name="userLaborUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateUserLabor(UserLaborUpsert userLaborUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                UserLaborEntity updateUserLaborEntity = new UserLaborEntity()
                {
                    LaborId = long.Parse(userLaborUpsert.LaborId),
                    LaborNameCn = userLaborUpsert.LaborNameCn,
                    LaborNameEn = userLaborUpsert.LaborNameEn,
                    Description = userLaborUpsert.Description,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                var updateUserLaborCount = await _userLaborRepository.UpdateUserLabor(updateUserLaborEntity);
                await _db.CommitTranAsync();

                return updateUserLaborCount >= 1
                    ? Result<int>.Ok(updateUserLaborCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// <param name="getUserLaborEntity"></param>
        /// <returns></returns>
        public async Task<Result<UserLaborDto>> GetUserLaborEntity(GetUserLaborEntity getUserLaborEntity)
        {
            try
            {
                var userLaborEntity = await _userLaborRepository.GetUserLaborEntity(long.Parse(getUserLaborEntity.LaborId));
                return Result<UserLaborDto>.Ok(userLaborEntity, "");
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
        /// <param name="getUserLaborPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserLaborDto>> GetUserLaborPage(GetUserLaborPage getUserLaborPage)
        {
            try
            {
                return await _userLaborRepository.GetUserLaborPage(getUserLaborPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<UserLaborDto>.Failure(500, ex.Message);
            }
        }
    }
}
