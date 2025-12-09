using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Dto;
using SystemAdmin.Repository.SystemBasicMgmt.SystemBasicData;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemBasicData
{
    public class PersonalInfoService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<PersonalInfoService> _logger;
        private readonly SqlSugarScope _db;
        private readonly PersonalInfoRepository _personalInfoRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt_SystemBasicData_Personal_";

        public PersonalInfoService(CurrentUser loginuser, ILogger<PersonalInfoService> logger, SqlSugarScope db, PersonalInfoRepository personalInfoRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _personalInfoRepository = personalInfoRepository;
            _localization = localization;
        }

        /// <summary>
        /// 查询个人信息实体
        /// </summary>
        /// <returns></returns>
        public async Task<Result<PersonalInfoDto>> GetPersonalInfoEntity()
        {
            try
            {
                var userEntity = await _personalInfoRepository.GetPersonalInfoEntity(_loginuser.UserId);
                return Result<PersonalInfoDto>.Ok(userEntity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<PersonalInfoDto>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="personalUpdate"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdatePersonalInfo(PersonalInfoUpdate personalUpdate)
        {
            try
            {
                await _db.BeginTranAsync();
                UserInfoEntity user = await _personalInfoRepository.GetUserPasswordAndSalt(_loginuser.UserId);

                string updatePassWord = string.Empty;
                string updateSaltString = string.Empty;

                if (!string.IsNullOrEmpty(personalUpdate.PassWord))
                {
                    //验证密码是否符合规范（必须为8-16位、包含小写、大写字母和数字）
                    if (!_personalInfoRepository.ValidatePassword(personalUpdate.PassWord))
                    {
                        await _db.RollbackTranAsync();
                        return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}ValidationPassWrodError"));
                    }
                    else
                    {
                        byte[] salt = _personalInfoRepository.GenerateSalt();
                        updateSaltString = Convert.ToBase64String(salt);
                        updatePassWord = _personalInfoRepository.HashPasswordWithArgon2id(personalUpdate.PassWord, salt);
                    }
                }
                else
                {
                    updateSaltString = user.PwdSalt;
                    updatePassWord = user.PassWord;
                }

                var updatePersonalEntity = new UserInfoEntity
                {
                    UserId = personalUpdate.UserId,
                    UserNameCn = personalUpdate.UserNameCn,
                    UserNameEn = personalUpdate.UserNameEn,
                    Email = personalUpdate.Email,
                    PhoneNumber = personalUpdate.PhoneNumber,
                    PassWord = updatePassWord,
                    PwdSalt = updateSaltString,
                    IsRealtimeNotification = personalUpdate.IsRealtimeNotification,
                    IsScheduledNotification = personalUpdate.IsScheduledNotification,
                    AvatarAddress = personalUpdate.AvatarAddress
                };
                var updatePersonalCount = await _personalInfoRepository.UpdatePersonalInfo(_loginuser.UserId, updatePersonalEntity);
                await _db.CommitTranAsync();

                return updatePersonalCount >= 1
                              ? Result<int>.Ok(updatePersonalCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
                              : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdateFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 性别字典下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<GenderDropDto>>> GetGenderDropDown()
        {
            try
            {
                var genderDrop = await _personalInfoRepository.GetGenderDropDown();
                return Result<List<GenderDropDto>>.Ok(genderDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<GenderDropDto>>.Failure(500, ex.Message.ToString());
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
                var userlaborDrop = await _personalInfoRepository.GetLaborDropDown();
                return Result<List<UserLaborDropDto>>.Ok(userlaborDrop, "");
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
                var deptDrop = await _personalInfoRepository.GetDepartmentDropDown();
                return Result<List<DepartmentDropDto>>.Ok(deptDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询职级列表
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<UserPositionDropDto>>> GetUserPositionDropDown()
        {
            try
            {
                var userPositionDrop = await _personalInfoRepository.GetUserPositionDropDown();
                return Result<List<UserPositionDropDto>>.Ok(userPositionDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserPositionDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 角色下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<RoleInfoDropDto>>> GetRoleDropDown()
        {
            try
            {
                var roleDrop = await _personalInfoRepository.GetRoleDropDown();
                return Result<List<RoleInfoDropDto>>.Ok(roleDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<RoleInfoDropDto>>.Failure(500, ex.Message.ToString());
            }
        }
    }
}
