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
        private readonly UserPartTimeRepository _userPartTimeRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.UserSettings.UserPartTime";

        public UserPartTimeService(CurrentUser loginuser, ILogger<UserPartTimeService> logger, SqlSugarScope db, UserPartTimeRepository userPartTimeRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _userPartTimeRepository = userPartTimeRepository;
            _localization = localization;
        }

        /// <summary>
        /// 查询员工兼任分页
        /// </summary>
        /// <param name="getUserPartTimePage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserPartTimeDto>> GetUserPartTimePage(GetUserPartTimePage getUserPartTimePage)
        {
            try
            {
                return await _userPartTimeRepository.GetUserPartTimePage(getUserPartTimePage);
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
        /// <param name="getUserPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserPartTimeViewDto>> GetUserPartTimeView(GetUserInfoPage getUserPage)
        {
            try
            {
                return await _userPartTimeRepository.GetUserPartTimeView(getUserPage);
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
        /// <param name="userPartTimeInsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertUserPartTime(UserPartTimeInsert userPartTimeInsert)
        {
            try
            {
                UserPartTimeEntity insertUserPartTimeEntity = new UserPartTimeEntity()
                {
                    UserId = long.Parse(userPartTimeInsert.UserId),
                    PartTimeDeptId = long.Parse(userPartTimeInsert.PartTimeDeptId),
                    PartTimePositionId = long.Parse(userPartTimeInsert.PartTimePositionId),
                    PartTimeLaborId = long.Parse(userPartTimeInsert.PartTimeLaborId),
                    StartTime = userPartTimeInsert.StartTime,
                    EndTime = userPartTimeInsert.EndTime,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };

                await _db.BeginTranAsync();
                // 判断员工是否有重复（按照员工Id、兼任部门、兼任职业）
                var userpartTimeAny = await _userPartTimeRepository.GetUserPartTimeCount(long.Parse(userPartTimeInsert.UserId), long.Parse(userPartTimeInsert.PartTimeDeptId), long.Parse(userPartTimeInsert.PartTimePositionId), long.Parse(userPartTimeInsert.PartTimeLaborId));
                if (userpartTimeAny)
                {
                    var insertUserPartTimeCount = await _userPartTimeRepository.InsertUserPartTime(insertUserPartTimeEntity);
                    await _userPartTimeRepository.UpdateUserPartTime(long.Parse(userPartTimeInsert.UserId), 1);
                    await _db.CommitTranAsync();

                    return insertUserPartTimeCount >= 1
                        ? Result<int>.Ok(insertUserPartTimeCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
                }
                else
                {
                    await _db.CommitTranAsync();
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}IsExist"));
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
        /// <param name="userPartTimeUpdateDel"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteUserPartTime(UserPartTimeUpdateDel userPartTimeUpdateDel)
        {
            try
            {
                await _db.BeginTranAsync();
                // 删除员工兼任
                var delUserPartTimeCount = await _userPartTimeRepository.DeleteUserPartTime(userPartTimeUpdateDel);
                // 判断员工是否还有兼任，如果没有就修改兼任状态为0
                var userPartTimeIsExist = await _userPartTimeRepository.GetUserPartTimeIsExist(long.Parse(userPartTimeUpdateDel.Old_UserId));
                if (!userPartTimeIsExist)
                {
                    await _userPartTimeRepository.UpdateUserPartTime(long.Parse(userPartTimeUpdateDel.Old_UserId), 0);
                }
                await _db.CommitTranAsync();

                return delUserPartTimeCount >= 1
                    ? Result<int>.Ok(delUserPartTimeCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
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
        /// <param name="getUserPartTimeEntity"></param>
        /// <returns></returns>
        public async Task<Result<UserPartTimeDto>> GetUserPartTimeEntity(GetUserPartTimeEntity getUserPartTimeEntity)
        {
            try
            {
                var userparttimeEntity = await _userPartTimeRepository.GetUserPartTimeList(getUserPartTimeEntity);
                return Result<UserPartTimeDto>.Ok(userparttimeEntity, "");
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
        /// <param name="userPartTimeUpdateDel"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateUserPartTime(UserPartTimeUpdateDel userPartTimeUpdateDel)
        {
            try
            {
                // 判断员工是否有重复（按照员工Id、新兼任部门、新兼任职业）
                var userpartTimeAny = await _userPartTimeRepository.GetUserPartTimeCount(long.Parse(userPartTimeUpdateDel.UserId), long.Parse(userPartTimeUpdateDel.PartTimeDeptId), long.Parse(userPartTimeUpdateDel.PartTimePositionId), long.Parse(userPartTimeUpdateDel.PartTimeLaborId));
                if (userpartTimeAny)
                {
                    await _db.BeginTranAsync();
                    UserPartTimeEntity updateUserPartTimeEntity = new UserPartTimeEntity()
                    {
                        UserId = long.Parse(userPartTimeUpdateDel.UserId),
                        PartTimeDeptId = long.Parse(userPartTimeUpdateDel.PartTimeDeptId),
                        PartTimePositionId = long.Parse(userPartTimeUpdateDel.PartTimePositionId),
                        PartTimeLaborId = long.Parse(userPartTimeUpdateDel.PartTimeLaborId),
                        StartTime = userPartTimeUpdateDel.StartTime,
                        EndTime = userPartTimeUpdateDel.EndTime,
                        ModifiedBy = _loginuser.UserId,
                        ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    var updateUserPartTimeCount = await _userPartTimeRepository.UpdateUserPartTime(userPartTimeUpdateDel, updateUserPartTimeEntity);
                    // 判断老员工是否还有兼任，如果没有就修改兼任状态为0
                    var OldUserPartTimeIsExist = await _userPartTimeRepository.GetUserPartTimeIsExist(long.Parse(userPartTimeUpdateDel.Old_UserId));
                    if (!OldUserPartTimeIsExist)
                    {
                        await _userPartTimeRepository.UpdateUserPartTime(long.Parse(userPartTimeUpdateDel.Old_UserId), 0);
                    }
                    // 修改新员工的兼任状态为1
                    await _userPartTimeRepository.UpdateUserPartTime(long.Parse(userPartTimeUpdateDel.UserId), 1);
                    await _db.CommitTranAsync();

                    return updateUserPartTimeCount >= 1
                        ? Result<int>.Ok(updateUserPartTimeCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdateFailed"));
                }
                else
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}IsExist"));
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
        /// 职业下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<UserLaborDropDto>>> GetLaborDropDown()
        {
            try
            {
                var LaborDrop = await _userPartTimeRepository.GetLaborDropDown();
                return Result<List<UserLaborDropDto>>.Ok(LaborDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserLaborDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 部门下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDropDown()
        {
            try
            {
                var deptDrop = await _userPartTimeRepository.GetDepartmentDropDown();
                return Result<List<DepartmentDropDto>>.Ok(deptDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 职级下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<UserPositionDropDto>>> GetUserPositionDropDown()
        {
            try
            {
                var userPositionDrop = await _userPartTimeRepository.GetUserPositionDropDown();
                return Result<List<UserPositionDropDto>>.Ok(userPositionDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserPositionDropDto>>.Failure(500, ex.Message.ToString());
            }
        }
    }
}
