using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using SqlSugar;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using SystemAdmin.Common.Excel;
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
        private readonly string _thisExcel = "SystemBasicMgmt.SystemBasicData.UserExcel_";

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
        /// 上传员工头像（新增员工）
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<Result<string>> UploadAvatar(IFormFile file)
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
                var allowed = new[] { ".png", ".jpg", ".jpeg" };
                var ext = Path.GetExtension(file.FileName)?.ToLowerInvariant();

                if (!allowed.Contains(ext))
                {
                    return Result<string>.Failure(400, _localization.ReturnMsg($"{_this}AvatarInvalidImageFormat"));
                }

                // 4. 上传到 MinIO
                var avatarUrl = await _minioService.UploadAsync(file.FileName, file.OpenReadStream(), file.ContentType);

                return Result<string>.Ok(avatarUrl, _localization.ReturnMsg($"{_this}UploadSuccess"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Avatar upload failed: {Message}", ex.Message);
                return Result<string>.Failure(500, _localization.ReturnMsg($"{_this}UploadFailed"));
            }
        }

        /// <summary>
        /// 上传员工头像（修改）
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<Result<string>> UploadAvatar(string userId, IFormFile file)
        {
            try
            {
                await _db.BeginTranAsync();
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
                var allowed = new[] { ".png", ".jpg", ".jpeg" };
                var ext = Path.GetExtension(file.FileName)?.ToLowerInvariant();

                if (!allowed.Contains(ext))
                {
                    return Result<string>.Failure(400, _localization.ReturnMsg($"{_this}AvatarInvalidImageFormat"));
                }

                // 4. 上传到 MinIO
                var avatarUrl = await _minioService.UploadAsync(file.FileName, file.OpenReadStream(), file.ContentType);

                var updateAvatarCount = await _userInfoRepository.UpdateUserAvatar(long.Parse(userId), avatarUrl);
                await _db.CommitTranAsync();

                return updateAvatarCount >= 1
                        ? Result<string>.Ok(avatarUrl, _localization.ReturnMsg($"{_this}UploadSuccess"))
                        : Result<string>.Failure(500, _localization.ReturnMsg($"{_this}UploadFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, "Avatar upload failed: {Message}", ex.Message);
                return Result<string>.Failure(500, _localization.ReturnMsg($"{_this}UploadFailed"));
            }
        }

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertUserInfo(UserInfoUpsert upsert)
        {
            try
            {
                if (string.IsNullOrEmpty(upsert.LoginNo) || string.IsNullOrEmpty(upsert.PassWord))
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}ValidationLoginNoPassWrodNotNull"));
                }
                if (await _userInfoRepository.UserNoIsExist(upsert.UserNo, upsert.LoginNo))
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DuplicatejobRepeatable"));
                }
                if (!ValidatePassword(upsert.PassWord))
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}ValidationPassWrodError"));
                }
                byte[] salt = GenerateSalt();
                string saltString = Convert.ToBase64String(salt);
                long userId = SnowFlakeSingle.Instance.NextId();

                // 新增员工信息
                var entity = new UserInfoEntity()
                {
                    UserId = userId,
                    DepartmentId = long.Parse(upsert.DepartmentId),
                    PositionId = long.Parse(upsert.PositionId),
                    UserNo = upsert.UserNo,
                    UserNameCn = upsert.UserNameCn,
                    UserNameEn = upsert.UserNameEn,
                    Gender = upsert.Gender,
                    HireDate = upsert.HireDate,
                    Nationality = upsert.Nationality,
                    LaborId = long.Parse(upsert.LaborId),
                    Email = upsert.Email,
                    PhoneNumber = upsert.PhoneNumber,
                    LoginNo = upsert.LoginNo,
                    PassWord = HashPasswordWithArgon2id(upsert.PassWord, salt),
                    PwdSalt = saltString,
                    AvatarAddress = upsert.AvatarAddress,
                    IsApproval = upsert.IsApproval,
                    IsRealtimeNotification = upsert.IsRealtimeNotification,
                    IsScheduledNotification = upsert.IsScheduledNotification,
                    IsEmployed = upsert.IsEmployed,
                    IsFreeze = upsert.IsFreeze,
                    ExpirationDays = upsert.ExpirationDays,
                    ExpirationTime = DateTime.Now.AddDays(upsert.ExpirationDays),
                    Remark = upsert.Remark,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now
                };

                await _db.BeginTranAsync();
                int insertUserCount = await _userInfoRepository.InsertUserInfo(entity);

                // 新增员工权限
                var insertUserRole = new UserRoleEntity()
                {
                    UserId = userId,
                    RoleId = long.Parse(upsert.RoleId),
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now
                };
                int insertUserRoleCount = await _userInfoRepository.InsertUserRole(insertUserRole);
                await _db.CommitTranAsync();

                return insertUserCount >= 1 && insertUserRoleCount >= 1
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
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteUserInfo(UserInfoUpsert upsert)
        {
            try
            {
                await _db.BeginTranAsync();
                // 删除员工信息
                int delUserCount = await _userInfoRepository.DeleteUserInfo(long.Parse(upsert.UserId));
                // 删除员工权限
                int delUserRoleCount = await _userInfoRepository.DeleteUserRoleInfo(long.Parse(upsert.UserId));
                // 删除员工代理
                int delUserAgentCount = await _userInfoRepository.DeleteUserAgent(long.Parse(upsert.UserId));
                // 删除员工兼任
                int delUserPartTimeCount = await _userInfoRepository.DeleteUserPartTime(long.Parse(upsert.UserId));
                // 删除员工表单绑定
                int delUserFormCount = await _userInfoRepository.DeleteUserForm(long.Parse(upsert.UserId));
                // 删除员工账号锁定记录
                int delUserLockCount = await _userInfoRepository.DeleteUserLock(long.Parse(upsert.UserId));
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
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateUserInfo(UserInfoUpsert upsert)
        {
            try
            {
                string updatePassWord = string.Empty;
                string updateSaltString = string.Empty;

                if (!string.IsNullOrEmpty(upsert.PassWord))
                {
                    if (!ValidatePassword(upsert.PassWord))
                    {
                        await _db.RollbackTranAsync();
                        return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}ValidationPassWrodError"));
                    }
                    else
                    {
                        byte[] salt = GenerateSalt();
                        updateSaltString = Convert.ToBase64String(salt);
                        updatePassWord = HashPasswordWithArgon2id(upsert.PassWord, salt);
                    }
                }
                else
                {
                    UserInfoEntity user = await _userInfoRepository.GetUserPasswordAndSalt(long.Parse(upsert.UserId));
                    updateSaltString = user.PwdSalt;
                    updatePassWord = user.PassWord;
                }

                // 修改员工信息
                var entity = new UserInfoEntity
                {
                    UserId = long.Parse(upsert.UserId),
                    UserNo = upsert.UserNo,
                    DepartmentId = long.Parse(upsert.DepartmentId),
                    PositionId = long.Parse(upsert.PositionId),
                    UserNameCn = upsert.UserNameCn,
                    UserNameEn = upsert.UserNameEn,
                    Gender = upsert.Gender,
                    HireDate = upsert.HireDate,
                    Nationality = upsert.Nationality,
                    LaborId = long.Parse(upsert.LaborId),
                    Email = upsert.Email,
                    PhoneNumber = upsert.PhoneNumber,
                    LoginNo = upsert.LoginNo,
                    PassWord = updatePassWord,
                    PwdSalt = updateSaltString,
                    AvatarAddress = upsert.AvatarAddress,
                    IsEmployed = upsert.IsEmployed,
                    IsApproval = upsert.IsApproval,
                    IsRealtimeNotification = upsert.IsRealtimeNotification,
                    IsScheduledNotification = upsert.IsScheduledNotification,
                    IsFreeze = upsert.IsFreeze,
                    ExpirationDays = upsert.ExpirationDays,
                    ExpirationTime = DateTime.Now.AddDays(upsert.ExpirationDays),
                    Remark = upsert.Remark,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now
                };

                await _db.BeginTranAsync();
                int updateUserCount = await _userInfoRepository.UpdateUserInfo(entity);

                // 修改员工角色
                var updateUserRole = new UserRoleEntity()
                {
                    UserId = long.Parse(upsert.UserId),
                    RoleId = long.Parse(upsert.RoleId),
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now
                };
                int updateUserRoleCount = await _userInfoRepository.UpdateUserRoleInfo(updateUserRole);
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
        /// <param name="getEntity"></param>
        /// <returns></returns>
        public async Task<Result<UserInfoEntityDto>> GetUserInfoEntity(GetUserInfoEntity getEntity)
        {
            try
            {
                var entity = await _userInfoRepository.GetUserInfoEntity(long.Parse(getEntity.UserId));
                return Result<UserInfoEntityDto>.Ok(entity, "");
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
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserInfoPageDto>> GetUserInfoPage(GetUserInfoPage getPage)
        {
            try
            {
                return await _userInfoRepository.GetUserInfoPage(getPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<UserInfoPageDto>.Failure(500, ex.Message.ToString());
            }
        }
        /// <summary>
        /// 国籍字典下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<NationalityDropDto>>> GetNationalityDropDown()
        {
            try
            {
                var drop = await _userInfoRepository.GetNationalityDropDown();
                return Result<List<NationalityDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<NationalityDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 职业下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<UserLaborDropDto>>> GetLaborDropDown()
        {
            try
            {
                var drop = await _userInfoRepository.GetLaborDropDown();
                return Result<List<UserLaborDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserLaborDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 部门树下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDropDown()
        {
            try
            {
                var drop = await _userInfoRepository.GetDepartmentDropDown();
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
        public async Task<Result<List<UserPositionDropDto>>> GetUserPositionDropDown()
        {
            try
            {
                var drop = await _userInfoRepository.GetUserPositionDropDown();
                return Result<List<UserPositionDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserPositionDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 角色下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<RoleInfoDropDto>>> GetRoleDropDown()
        {
            try
            {
                var drop = await _userInfoRepository.GetRoleDropDown();
                return Result<List<RoleInfoDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<RoleInfoDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 导出员工Excel表格
        /// </summary>
        /// <param name="getUserExcel"></param>
        /// <returns></returns>
        public async Task<byte[]> GetUserInfoExcel(GetUserInfoExcel getUserExcel)
        {
            try
            {
                DataTable dt = await _userInfoRepository.GetUserInfoExcel(getUserExcel);

                ExcelPackage.License.SetNonCommercialPersonal("Your Name");

                using var package = new ExcelPackage();

                var ws = package.Workbook.Worksheets.Add(_localization.ReturnMsg($"{_thisExcel}UserInfo"));

                var headers = new Dictionary<string, string>
                {
                    { "UserNo", _localization.ReturnMsg($"{_thisExcel}UserNo") },
                    { "UserNameCn", _localization.ReturnMsg($"{_thisExcel}UserNameCn") },
                    { "UserNameEn", _localization.ReturnMsg($"{_thisExcel}UserNameEn") },
                    { "DepartmentName", _localization.ReturnMsg($"{_thisExcel}DepartmentName") },
                    { "PositionName", _localization.ReturnMsg($"{_thisExcel}PositionName") },
                    { "HireDate", _localization.ReturnMsg($"{_thisExcel}HireDate") },
                    { "GenderName", _localization.ReturnMsg($"{_thisExcel}GenderName") },
                    { "NationalityName", _localization.ReturnMsg($"{_thisExcel}NationalityName") },
                    { "Email", _localization.ReturnMsg($"{_thisExcel}Email") },
                    { "PhoneNumber", _localization.ReturnMsg($"{_thisExcel}PhoneNumber") },
                    { "IsEmployedName", _localization.ReturnMsg($"{_thisExcel}IsEmployedName") },
                    { "IsApprovalName", _localization.ReturnMsg($"{_thisExcel}IsApprovalName") },
                    { "IsFreezeName", _localization.ReturnMsg($"{_thisExcel}IsFreezeName") }
                };

                ExcelStyleHelper.ApplyStandardStyle(ws, dt, headers, true);
                package.Workbook.CalcMode = ExcelCalcMode.Manual;

                return package.GetAsByteArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Array.Empty<byte>();
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
