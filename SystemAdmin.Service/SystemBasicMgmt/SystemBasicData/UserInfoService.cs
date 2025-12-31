using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Repository.SystemBasicMgmt.SystemBasicData;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemBasicData
{
    public class UserInfoService
    {
        private readonly CurrentUser _loginuser;
        private readonly MinioService _minioService;
        private readonly ILogger<UserInfoService> _logger;
        private readonly SqlSugarScope _db;
        private readonly UserInfoRepository _userInfoRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.SystemBasicData.UserInfo";

        public UserInfoService(CurrentUser loginuser, MinioService minioService, ILogger<UserInfoService> logger, SqlSugarScope db, UserInfoRepository userInfoRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _minioService = minioService;
            _logger = logger;
            _db = db;
            _userInfoRepository = userInfoRepository;
            _localization = localization;
        }

        /// <summary>
        /// 上传头像图片文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<Result<string>> UploadAvatarAsync(IFormFile file)
        {
            try
            {
                // 1. 判空
                if (file == null || file.Length == 0)
                {
                    return Result<string>.Failure(400, _localization.ReturnMsg($"{_this}AvatarFileNotNull"));
                }

                // 2. 限制最大 2MB（头像不需要太大）
                const long maxSize = 2 * 1024 * 1024;
                if (file.Length > maxSize)
                {
                    return Result<string>.Failure(400, _localization.ReturnMsg($"{_this}AvatarFileTooLarge", "2MB"));
                }

                // 3. 限制图片格式
                var allowed = new[] { ".png", ".jpg", ".jpeg", ".webp" };
                var ext = Path.GetExtension(file.FileName)?.ToLowerInvariant();

                if (!allowed.Contains(ext))
                {
                    return Result<string>.Failure(400, _localization.ReturnMsg($"{_this}AvatarInvalidImageFormat"));
                }

                // 4. 上传到 MinIO
                var avatarUrl = await _minioService.UploadAsync(file.FileName, file.OpenReadStream(), file.ContentType);

                // 5. 返回成功
                return Result<string>.Ok(avatarUrl, _localization.ReturnMsg($"{_this}UploadSuccess"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Avatar upload failed: {Message}", ex.Message);

                // 禁止把内部报错直接暴露给前端（安全问题）
                return Result<string>.Failure(500, _localization.ReturnMsg($"{_this}UploadFailed"));
            }
        }

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="userUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertUserInfo(UserInfoUpsert userUpsert)
        {
            try
            {
                if (string.IsNullOrEmpty(userUpsert.LoginNo) || string.IsNullOrEmpty(userUpsert.PassWord))
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}ValidationLoginNoPassWrodNotNull"));
                }
                if (await _userInfoRepository.UserNoIsExist(userUpsert.UserNo, userUpsert.LoginNo))
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DuplicatejobRepeatable"));
                }
                if (!ValidatePassword(userUpsert.PassWord))
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}ValidationPassWrodError"));
                }
                byte[] salt = GenerateSalt();
                string saltString = Convert.ToBase64String(salt);
                long userId = SnowFlakeSingle.Instance.NextId();

                await _db.BeginTranAsync();
                // 新增员工信息
                UserInfoEntity insertUserEntity = new UserInfoEntity()
                {
                    UserId = userId,
                    DepartmentId = long.Parse(userUpsert.DepartmentId),
                    PositionId = long.Parse(userUpsert.PositionId),
                    UserNo = userUpsert.UserNo,
                    UserNameCn = userUpsert.UserNameCn,
                    UserNameEn = userUpsert.UserNameEn,
                    Gender = userUpsert.Gender,
                    HireDate = userUpsert.HireDate,
                    Nationality = userUpsert.Nationality,
                    LaborId = long.Parse(userUpsert.LaborId),
                    Email = userUpsert.Email,
                    PhoneNumber = userUpsert.PhoneNumber,
                    LoginNo = userUpsert.LoginNo,
                    PassWord = HashPasswordWithArgon2id(userUpsert.PassWord, salt),
                    PwdSalt = saltString,
                    AvatarAddress = userUpsert.AvatarAddress,
                    IsApproval = userUpsert.IsApproval,
                    IsRealtimeNotification = userUpsert.IsRealtimeNotification,
                    IsScheduledNotification = userUpsert.IsScheduledNotification,
                    IsEmployed = userUpsert.IsEmployed,
                    IsFreeze = userUpsert.IsFreeze,
                    ExpirationDays = userUpsert.ExpirationDays,
                    ExpirationTime = DateTime.Now.AddDays(userUpsert.ExpirationDays).ToString("yyyy-MM-dd HH:mm:ss"),
                    Remark = userUpsert.Remark,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                int insertUserCount = await _userInfoRepository.InsertUserInfo(insertUserEntity);

                // 新增员工权限
                UserRoleEntity insertUserRoleEntity = new UserRoleEntity()
                {
                    UserId = userId,
                    RoleId = long.Parse(userUpsert.RoleId),
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                int InsertUserRoleCount = await _userInfoRepository.InsertUserRole(insertUserRoleEntity);
                await _db.CommitTranAsync();

                return insertUserCount >= 1 && InsertUserRoleCount >= 1
                        ? Result<int>.Ok(insertUserCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="userUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteUserInfo(UserInfoUpsert userUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                // 删除员工信息
                int delUserCount = await _userInfoRepository.DeleteUserInfo(long.Parse(userUpsert.UserId));
                // 删除员工权限
                int delUserRoleCount = await _userInfoRepository.DeleteUserRoleInfo(long.Parse(userUpsert.UserId));
                // 删除员工代理
                int delUserAgentCount = await _userInfoRepository.DeleteUserAgent(long.Parse(userUpsert.UserId));
                // 删除员工兼任
                int delUserPartTimeCount = await _userInfoRepository.DeleteUserPartTime(long.Parse(userUpsert.UserId));
                // 删除员工表单绑定
                int delUserFormBindCount = await _userInfoRepository.DeleteUserFormBind(long.Parse(userUpsert.UserId));
                // 删除员工账号锁定记录
                int delUserLockCount = await _userInfoRepository.DeleteUserLock(long.Parse(userUpsert.UserId));
                await _db.CommitTranAsync();

                return delUserCount >= 1 && delUserRoleCount >= 1
                        ? Result<int>.Ok(delUserCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DeleteFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="userUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateUserInfo(UserInfoUpsert userUpsert)
        {
            try
            {
                string updatePassWord = string.Empty;
                string updateSaltString = string.Empty;

                await _db.BeginTranAsync();
                if (!string.IsNullOrEmpty(userUpsert.PassWord))
                {
                    if (!ValidatePassword(userUpsert.PassWord))
                    {
                        await _db.RollbackTranAsync();
                        return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}ValidationPassWrodError"));
                    }
                    else
                    {
                        byte[] salt = GenerateSalt();
                        updateSaltString = Convert.ToBase64String(salt);
                        updatePassWord = HashPasswordWithArgon2id(userUpsert.PassWord, salt);
                    }
                }
                else
                {
                    UserInfoEntity user = await _userInfoRepository.GetUserPasswordAndSalt(long.Parse(userUpsert.UserId));
                    updateSaltString = user.PwdSalt;
                    updatePassWord = user.PassWord;
                }
                
                // 修改员工信息
                var updateUserEntity = new UserInfoEntity
                {
                    UserId = long.Parse(userUpsert.UserId),
                    UserNo = userUpsert.UserNo,
                    DepartmentId = long.Parse(userUpsert.DepartmentId),
                    PositionId = long.Parse(userUpsert.PositionId),
                    UserNameCn = userUpsert.UserNameCn,
                    UserNameEn = userUpsert.UserNameEn,
                    Gender = userUpsert.Gender,
                    HireDate = userUpsert.HireDate,
                    Nationality = userUpsert.Nationality,
                    LaborId = long.Parse(userUpsert.LaborId),
                    Email = userUpsert.Email,
                    PhoneNumber = userUpsert.PhoneNumber,
                    LoginNo = userUpsert.LoginNo,
                    PassWord = updatePassWord,
                    PwdSalt = updateSaltString,
                    AvatarAddress = userUpsert.AvatarAddress,
                    IsEmployed = userUpsert.IsEmployed,
                    IsApproval = userUpsert.IsApproval,
                    IsRealtimeNotification = userUpsert.IsRealtimeNotification,
                    IsScheduledNotification = userUpsert.IsScheduledNotification,
                    IsFreeze = userUpsert.IsFreeze,
                    ExpirationDays = userUpsert.ExpirationDays,
                    ExpirationTime = DateTime.Now.AddDays(userUpsert.ExpirationDays).ToString("yyyy-MM-dd HH:mm:ss"),
                    Remark = userUpsert.Remark,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                int updateUserCount = await _userInfoRepository.UpdateUserInfo(updateUserEntity);

                // 修改员工权限
                UserRoleEntity updateUserRoleEntity = new UserRoleEntity()
                {
                    UserId = long.Parse(userUpsert.UserId),
                    RoleId = long.Parse(userUpsert.RoleId),
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                int updateUserRoleCount = await _userInfoRepository.UpdateUserRoleInfo(updateUserRoleEntity);
                await _db.CommitTranAsync();

                return updateUserCount >= 1 || updateUserRoleCount >= 1
                        ? Result<int>.Ok(updateUserCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// 查询员工实体
        /// </summary>
        /// <param name="getUserEntity"></param>
        /// <returns></returns>
        public async Task<Result<UserInfoEntityDto>> GetUserInfoEntity(GetUserInfoEntity getUserEntity)
        {
            try
            {
                var userEntity = await _userInfoRepository.GetUserInfoEntity(long.Parse(getUserEntity.UserId));
                return Result<UserInfoEntityDto>.Ok(userEntity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<UserInfoEntityDto>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询员工分页
        /// </summary>
        /// <param name="getUserPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserInfoPageDto>> GetUserInfoPage(GetUserInfoPage getUserPage)
        {
            try
            {
                return await _userInfoRepository.GetUserInfoPage(getUserPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<UserInfoPageDto>.Failure(500, ex.Message.ToString());
            }
        }
        /// <summary>
        /// 国籍字典下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<NationalityDropDto>>> GetNationalityDropDown()
        {
            try
            {
                var nationDrop = await _userInfoRepository.GetNationalityDropDown();
                return Result<List<NationalityDropDto>>.Ok(nationDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<NationalityDropDto>>.Failure(500, ex.Message.ToString());
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
                var LaborDrop = await _userInfoRepository.GetLaborDropDown();
                return Result<List<UserLaborDropDto>>.Ok(LaborDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserLaborDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 部门树下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDropDown()
        {
            try
            {
                var deptDrop = await _userInfoRepository.GetDepartmentDropDown();
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
                var userPositionDrop = await _userInfoRepository.GetUserPositionDropDown();
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
                var roleDrop = await _userInfoRepository.GetRoleDropDown();
                return Result<List<RoleInfoDropDto>>.Ok(roleDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<RoleInfoDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 验证密码是否符合规范（必须为8-16位，包含小写、大写、数字和特殊字符）
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8 || password.Length > 16)
            {
                return false;
            }
            return Regex.IsMatch(password, @"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9])");
        }

        /// <summary>
        /// 生成安全随机盐（16字节）
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] GenerateSalt(int length = 16)
        {
            byte[] salt = new byte[length];
            RandomNumberGenerator.Fill(salt);
            return salt;
        }

        /// <summary>
        /// 员工密码加密（Argon2id）
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public string HashPasswordWithArgon2id(string password, byte[] salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            try
            {
                using var argon2 = new Argon2id(passwordBytes)
                {
                    Salt = salt,
                    DegreeOfParallelism = 4, // 固定并行度，而不是使用 Environment.ProcessorCount
                    MemorySize = 65536,      // 固定内存大小(64KB)
                    Iterations = 3           // 固定迭代次数
                };

                byte[] hash = argon2.GetBytes(32); // 256位 hash
                return Convert.ToBase64String(hash);
            }
            finally
            {
                // 安全擦除密码内存
                Array.Clear(passwordBytes, 0, passwordBytes.Length);
            }
        }
    }
}
