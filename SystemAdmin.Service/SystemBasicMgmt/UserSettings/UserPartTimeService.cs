using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Commands;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Dto;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Entity;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Queries;
using SystemAdmin.Repository.SystemBasicMgmt.UserSettings;

namespace SystemAdmin.Service.SystemBasicMgmt.UserSettings
{
    public class UserPartTimeService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<UserPartTimeService> _logger;
        private readonly SqlSugarScope _db;
        private readonly UserPartTimeRepository _userPartTimeRepo;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.UserSettings.UserPartTime";

        public UserPartTimeService(CurrentUser loginuser, ILogger<UserPartTimeService> logger, SqlSugarScope db, UserPartTimeRepository userPartTimeRepo, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _userPartTimeRepo = userPartTimeRepo
            _localization = localization;
        }


        /// <summary>
        /// 职业下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<UserLaborDropDto>>> GetLaborDrop()
        {
            try
            {
                var drop = await _userPartTimeRepo.GetLaborDrop();
                return Result<List<UserLaborDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserLaborDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 部门下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDrop()
        {
            try
            {
                var drop = await _userPartTimeRepo.GetDepartmentDrop();
                return Result<List<DepartmentDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 职级下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<PositionDropDto>>> GetPositionDrop()
        {
            try
            {
                var drop = await _userPartTimeRepo.GetPositionDrop();
                return Result<List<PositionDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<PositionDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询员工兼任分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserPartTimeDto>> GetUserPartTimePage(GetUserPartTimePage getPage)
        {
            try
            {
                return await _userPartTimeRepo.GetUserPartTimePage(getPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<UserPartTimeDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询员工分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserPartTimeViewDto>> GetUserPartTimeView(GetUserInfoPage getPage)
        {
            try
            {
                return await _userPartTimeRepo.GetUserPartTimeView(getPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<UserPartTimeViewDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 新增员工兼任
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertUserPartTime(UserPartTimeInsert upsert)
        {
            try
            {
                // 判断员工是否有重复（按照员工Id、兼任部门）
                var isPartTime = await _userPartTimeRepo.GetUserPartTimeCount(long.Parse(upsert.UserId), long.Parse(upsert.PartTimeDeptId), long.Parse(upsert.PartTimePositionId));
                if (isPartTime)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}IsExist"));
                }
                else
                {
                    var entity = new UserPartTimeEntity()
                    {
                        UserId = long.Parse(upsert.UserId),
                        PartTimeDeptId = long.Parse(upsert.PartTimeDeptId),
                        PartTimePositionId = long.Parse(upsert.PartTimePositionId),
                        StartTime = upsert.StartTime,
                        EndTime = upsert.EndTime,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now
                    };

                    await _db.BeginTranAsync();
                    var insertUserPartTimeCount = await _userPartTimeRepo.InsertUserPartTime(entity);
                    await _userPartTimeRepo.UpdateUserPartTime(long.Parse(upsert.UserId), 1);
                    await _db.CommitTranAsync();

                    return insertUserPartTimeCount >= 1
                            ? Result<int>.Ok(insertUserPartTimeCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
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
        /// 删除员工兼任
        /// </summary>
        /// <param name="upsertdel"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteUserPartTime(UserPartTimeUpdateDel upsertdel)
        {
            try
            {
                await _db.BeginTranAsync();
                // 删除员工兼任
                var count = await _userPartTimeRepo.DeleteUserPartTime(upsertdel);
                // 判断员工是否还有兼任，如果没有就修改兼任状态为0
                var isPartTime = await _userPartTimeRepo.GetUserPartTimeIsExist(long.Parse(upsertdel.Old_UserId));
                if (!isPartTime)
                {
                    await _userPartTimeRepo.UpdateUserPartTime(long.Parse(upsertdel.Old_UserId), 0);
                }
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
        /// 查询员工兼任实体
        /// </summary>
        /// <param name="getEntity"></param>
        /// <returns></returns>
        public async Task<Result<UserPartTimeDto>> GetUserPartTimeEntity(GetUserPartTimeEntity getEntity)
        {
            try
            {
                var entity = await _userPartTimeRepo.GetUserPartTimeList(getEntity);
                return Result<UserPartTimeDto>.Ok(entity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<UserPartTimeDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 修改员工兼任
        /// </summary>
        /// <param name="upsertdel"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateUserPartTime(UserPartTimeUpdateDel upsertdel)
        {
            try
            {
                // 判断员工是否有重复（按照员工Id、新兼任部门、新兼任职级）
                var isPartTime = await _userPartTimeRepo.GetUserPartTimeCount(long.Parse(upsertdel.UserId), long.Parse(upsertdel.PartTimeDeptId), long.Parse(upsertdel.PartTimePositionId));
                if (isPartTime)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}IsExist"));
                }
                else
                {
                    var entity = new UserPartTimeEntity()
                    {
                        UserId = long.Parse(upsertdel.UserId),
                        PartTimeDeptId = long.Parse(upsertdel.PartTimeDeptId),
                        PartTimePositionId = long.Parse(upsertdel.PartTimePositionId),
                        StartTime = upsertdel.StartTime,
                        EndTime = upsertdel.EndTime,
                        ModifiedBy = _loginuser.UserId,
                        ModifiedDate = DateTime.Now
                    };

                    await _db.BeginTranAsync();
                    var updateUserPartCount = await _userPartTimeRepo.UpdateUserPartTime(upsertdel, entity);
                    // 判断老员工是否还有兼任，如果没有就修改兼任状态为0
                    var oldUserPartIsExist = await _userPartTimeRepo.GetUserPartTimeIsExist(long.Parse(upsertdel.Old_UserId));
                    if (!oldUserPartIsExist)
                    {
                        await _userPartTimeRepo.UpdateUserPartTime(long.Parse(upsertdel.Old_UserId), 0);
                    }
                    // 修改新员工的兼任状态为1
                    await _userPartTimeRepo.UpdateUserPartTime(long.Parse(upsertdel.UserId), 1);
                    await _db.CommitTranAsync();

                    return updateUserPartCount >= 1
                            ? Result<int>.Ok(updateUserPartCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
                            : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdateFailed"));
                }
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }
    }
}
