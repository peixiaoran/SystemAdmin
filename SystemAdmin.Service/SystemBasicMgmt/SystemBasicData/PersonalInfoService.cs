using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Repository.SystemBasicMgmt.SystemBasicData;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemBasicData
{
    public class PersonalInfoService
    {
        private readonly CurrentUser _loginuser;
        private readonly MinioService _minioService;
        private readonly ILogger<PersonalInfoService> _logger;
        private readonly SqlSugarScope _db;
        private readonly PersonalInfoRepository _personalInfoRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.SystemBasicData.Personal";

        public PersonalInfoService(CurrentUser loginuser, MinioService minioService, ILogger<PersonalInfoService> logger, SqlSugarScope db, PersonalInfoRepository personalInfoRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _minioService = minioService;
            _logger = logger;
            _db = db;
            _personalInfoRepository = personalInfoRepository;
            _localization = localization;
        }

        /// <summary>
        /// 上传员工头像
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<Result<string>> UploadAvatarAsync(string userId, IFormFile file)
        {
            try
            {
                // 1. 判空
                if (file == null || file.Length == 0)
                {
                    return Result<string>.Failure(400, _localization.ReturnMsg($"{_this}AvatarFileNotNull"));
                }

                // 2. 限制最大 2MB
                const long maxSize = 2 * 1024 * 1024;
                if (file.Length > maxSize)
                {
                    return Result<string>.Failure(400, _localization.ReturnMsg($"{_this}AvatarFileTooLarge", "2MB"));
                }

                // 3. 限制图片格式
                var allowed = new[] { ".png", ".jpg", ".jpeg"};
                var ext = Path.GetExtension(file.FileName)?.ToLowerInvariant();

                if (!allowed.Contains(ext))
                {
                    return Result<string>.Failure(400, _localization.ReturnMsg($"{_this}AvatarInvalidImageFormat"));
                }

                // 4. 上传到 MinIO
                var avatarUrl = await _minioService.UploadAsync(file.FileName, file.OpenReadStream(), file.ContentType);

                // 5. 更新用户头像地址
                var updateAvatarCount = await _personalInfoRepository.UpdateUserAvatar(long.Parse(userId), avatarUrl);

                // 6. 返回
                return updateAvatarCount >= 1
                        ? Result<string>.Ok(avatarUrl, _localization.ReturnMsg($"{_this}UploadSuccess"))
                        : Result<string>.Failure(500, _localization.ReturnMsg($"{_this}UploadFailed"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Avatar upload failed: {Message}", ex.Message);
                return Result<string>.Failure(500, _localization.ReturnMsg($"{_this}UploadFailed"));
            }
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

                var updatePersonal = new UserInfoEntity
                {
                    UserId = personalUpdate.UserId,
                    PhoneNumber = personalUpdate.PhoneNumber,
                    PassWord = updatePassWord,
                    PwdSalt = updateSaltString,
                    IsRealtimeNotification = personalUpdate.IsRealtimeNotification,
                    IsScheduledNotification = personalUpdate.IsScheduledNotification,
                    AvatarAddress = personalUpdate.AvatarAddress,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                var updatePersonalCount = await _personalInfoRepository.UpdatePersonalInfo(_loginuser.UserId, updatePersonal);
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
