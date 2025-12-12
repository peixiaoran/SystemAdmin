using Azure;
using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Localization.Resources;
using SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Queries;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Entity;
using SystemAdmin.Repository.SystemBasicMgmt.SystemAuth;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemAuth
{
    public class SysUserOperateService
    {
        private readonly ILogger<SysUserOperateService> _logger;

        private readonly CurrentUser _loginuser;
        private readonly JwtTokenService _jwt;
        private readonly SqlSugarScope _db;
        private readonly SysUserOperateRepository _sysUserOperateRepository;
        private readonly MailKitEmailSender _mailKitEmail;
        private readonly LocalizationService _localization;
        //private readonly HybridCache _cache;
        private readonly string _this = "SystemBasicMgmt_Auth_SysLogOut_";
        private const int CodeLength = 6;
        private static readonly Random _random = new Random();

        public SysUserOperateService(CurrentUser loginuser, JwtTokenService jwt, ILogger<SysUserOperateService> logger, SqlSugarScope db, SysUserOperateRepository sysUserOperateRepository, MailKitEmailSender mailKitEmail, LocalizationService localization)
        {
            _loginuser = loginuser;
            _jwt = jwt;
            _logger = logger;
            _db = db;
            _sysUserOperateRepository = sysUserOperateRepository;
            _mailKitEmail = mailKitEmail;
            _localization = localization;
        }

        /// <summary>
        /// 员工登录
        /// </summary>
        /// <param name="sysLogin"></param>
        /// <returns></returns>
        public async Task<Result<SysUserLoginReturnDto>> UserLogin(HttpResponse httpResponse, SysLogin sysLogin)
        {
            try
            {
                await _db.BeginTranAsync();

                var ip = GetLocalIPv4();
                var now = DateTime.Now;
                var nowStr = now.ToString("yyyy-MM-dd HH:mm:ss");

                // 查询用户
                var user = await _sysUserOperateRepository.LoginGetUserInfo(sysLogin);

                if (user == null)
                {
                    // 员工不存在
                    await _sysUserOperateRepository.AddUserLoginLogInfo(new UserLogOutEntity
                    {
                        UserId = 0,
                        StatusId = 3,
                        IP = ip,
                        LoginDate = nowStr
                    });

                    await _db.CommitTranAsync();
                    return Result<SysUserLoginReturnDto>.Failure(500, _localization.ReturnMsg($"{_this}UserNotFound")
                    );
                }

                // 账号已冻结
                if (user.IsFreeze != 0)
                {
                    await _db.CommitTranAsync();
                    return Result<SysUserLoginReturnDto>.Failure(220,_localization.ReturnMsg($"{_this}LoginLock"));
                }

                // 校验密码
                var inputHash = HashPasswordWithArgon2id(sysLogin.PassWord,Convert.FromBase64String(user.PwdSalt));

                if (inputHash != user.PassWord)
                {
                    // 记录密码错误
                    await _sysUserOperateRepository.AddUserLoginLogInfo(new UserLogOutEntity
                    {
                        UserId = user.UserId,
                        StatusId = 2,
                        IP = ip,
                        LoginDate = nowStr
                    });

                    // 获取并累加错误次数
                    var lockInfo = await _sysUserOperateRepository.GetUserLockErrorNumberNow(user.UserId);
                    var newErrors = (lockInfo?.NumberErrors ?? 0) + 1;

                    if (lockInfo == null)
                    {
                        await _sysUserOperateRepository.AddUserLock(new UserLockEntity
                        {
                            UserId = user.UserId,
                            NumberErrors = 1,
                            CreatedDate = nowStr
                        });
                    }
                    else
                    {
                        await _sysUserOperateRepository.AutoUserLockErrorNumber(user.UserId, newErrors);
                    }

                    // 达到阈值 → 冻结账号
                    if (newErrors >= 5)
                    {
                        await _sysUserOperateRepository.UpdateUserFreeze(user.UserId);
                        await _db.CommitTranAsync();

                        return Result<SysUserLoginReturnDto>.Failure(220, _localization.ReturnMsg($"{_this}LoginLock"));
                    }

                    // 未达阈值
                    var remain = 5 - newErrors;
                    await _db.CommitTranAsync();

                    return Result<SysUserLoginReturnDto>.Failure(500, _localization.ReturnMsg($"{_this}LoginFailed", remain)
                    );
                }

                // 密码是否过期
                if (Convert.ToDateTime(user.ExpirationTime) < now)
                {
                    await _db.CommitTranAsync();
                    return Result<SysUserLoginReturnDto>.Failure(210, _localization.ReturnMsg($"{_this}PasswordExpiration"));
                }

                // 登录成功日志
                await _sysUserOperateRepository.AddUserLoginLogInfo(new UserLogOutEntity
                {
                    UserId = user.UserId,
                    StatusId = 1,
                    IP = ip,
                    LoginDate = nowStr
                });

                // 清空锁定记录
                await _sysUserOperateRepository.EmptyUserLock(user.UserId);

                await _db.CommitTranAsync();

                // 返回登录成功信息（JWT Cookie 已在其他层处理）
                return Result<SysUserLoginReturnDto>.Ok(
                    new SysUserLoginReturnDto
                    {
                        UserNo = user.UserNo,
                        UserNameCn = user.UserNameCn,
                        UserNameEn = user.UserNameEn,
                        AvatarAddress = user.AvatarAddress
                    },
                    _localization.ReturnMsg($"{_this}LoginSuccess")
                );
            }
            catch(Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<SysUserLoginReturnDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 员工登出
        /// </summary>
        /// <returns></returns>
        public async Task<Result<int>> UserLogOut()
        {
            try
            {
                await _db.BeginTranAsync();
                var user = await _sysUserOperateRepository.GetUserInfoForUserLogOut(_loginuser.UserId);

                // 判断员工是否存在
                if (user == null)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UserNotFound"));
                }
                var logOutLog = new UserLogOutEntity
                {
                    UserId = user.UserId,
                    StatusId = 4, // 状态码 4 = 登出
                    IP = GetLocalIPv4(),
                    LoginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                var insertLogOutCount = await _sysUserOperateRepository.AddUserLogOutInfo(logOutLog);
                await _db.CommitTranAsync();

                return Result<int>.Ok(insertLogOutCount, _localization.ReturnMsg($"{_this}LogOutSuccess"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 解锁账号发送验证码
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public async Task<Result<string>> UnLockSendVcCode(string userNo)
        {
            try
            {
                var user = await _sysUserOperateRepository.GetUserInfo(userNo);

                // 判断员工是否在职
                if (user == null || string.IsNullOrWhiteSpace(user.Email))
                {
                    return Result<string>.Failure(500, _localization.ReturnMsg($"{_this}EmailNotFound"));
                }

                // 判断员工是否被冻结
                if (user.IsFreeze == 0)
                {
                    return Result<string>.Failure(500, _localization.ReturnMsg($"{_this}UserNotFreeze"));
                }

                //await _cache.RemoveAsync(userNo); // 先清除旧缓存（HybridCache 不支持直接覆盖）

                // 正常发送验证码流程
                var code = GenerateRandomCode();
                //await _cache.SetAsync(userNo, code);

                // 发送邮件
                EmailMessage emailMsg = new EmailMessage
                {
                    To = new List<string> { user.Email },
                    Subject = _localization.ReturnMsg($"{_this}EmailAccountUnlock"),
                    Body = _localization.ReturnMsg($"{_this}UnlockSendVcCodeBody", code)
                };
                await _mailKitEmail.SendAsync(emailMsg);

                return Result<string>.Ok("1", _localization.ReturnMsg($"{_this}SendVcCodeSuccess"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<string>.Failure(500, _localization.ReturnMsg($"{_this}SendVcCodeFailed", ex.Message));
            }
        }

        /// <summary>
        /// 解锁账号（重置密码）
        /// </summary>
        /// <param name="userUnlock"></param>
        /// <returns></returns>
        public async Task<Result<int>> UserUnLock(UserUnlock userUnlock)
        {
            try
            {
                // 判断是否频繁发送：通过第一次调用返回非 null 判定
                //var cacheValue = await _cache.GetOrCreateAsync(
                //      userUnlock.UserNo,
                //      ct => new ValueTask<string>("")
                //);
                if (string.IsNullOrEmpty("11111"))
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}VcCodeExpired"));
                }

                if ("11111" != userUnlock.VerificationCode)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}VcCodeError"));
                }

                var user = await _sysUserOperateRepository.GetUserInfo(userUnlock.UserNo);
                if (user == null)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UserNotFound"));
                }

                // 验证密码格式
                if (!ValidatePassword(userUnlock.PassWord))
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}ValidationPassWrodError"));
                }

                // 密码不能与老密码相同
                await _db.BeginTranAsync();
                var oldHash = HashPasswordWithArgon2id(userUnlock.PassWord, Convert.FromBase64String(user.PwdSalt));
                if (oldHash == user.PassWord)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}NotEqualOldPassWord"));
                }
                else
                {
                    // 加密密码
                    byte[] salt = GenerateSalt();
                    string saltString = Convert.ToBase64String(salt);
                    string passwordHash = HashPasswordWithArgon2id(userUnlock.PassWord, salt);

                    int unlockFreezeCount = await _sysUserOperateRepository.UnlockUserFreeze(user.UserId, passwordHash, saltString, DateTime.Now.AddDays(user.ExpirationDays).ToString("yyyy-MM-dd HH:mm:ss"));
                    await _sysUserOperateRepository.DeleleUserLockLog(user.UserId);
                    await _db.CommitTranAsync();

                    //await _cache.RemoveAsync(userUnlock.UserNo); // 验证通过，清除缓存

                    return unlockFreezeCount >= 1
                        ? Result<int>.Ok(unlockFreezeCount, _localization.ReturnMsg($"{_this}UnlockSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UnlockFailed"));
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
        /// 密码过期发送验证码
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public async Task<Result<string>> UnExpirationSendVcCode(string userNo)
        {
            try
            {
                var user = await _sysUserOperateRepository.GetUserInfo(userNo);

                // 判断员工是否在职
                if (user == null || string.IsNullOrWhiteSpace(user.Email))
                {
                    return Result<string>.Failure(500, _localization.ReturnMsg($"{_this}EmailNotFound"));
                }

                // 判断账号密码是否过期
                if (Convert.ToDateTime(user.ExpirationTime) > DateTime.Now)
                {
                    return Result<string>.Failure(500, _localization.ReturnMsg($"{_this}PasswordNotExpiration"));
                }

                //await _cache.RemoveAsync(userNo); // 先清除旧缓存（HybridCache 不支持直接覆盖）

                // 正常发送验证码流程
                var code = GenerateRandomCode();
                //await _cache.SetAsync(userNo, code);

                // 发送邮件
                EmailMessage emailMsg = new EmailMessage
                {
                    To = new List<string> { user.Email },
                    Subject = _localization.ReturnMsg($"{_this}EmailAccountPasswordEx"),
                    Body = _localization.ReturnMsg($"{_this}PasswordExSendVcCodeBody", code)
                };
                await _mailKitEmail.SendAsync(emailMsg);

                return Result<string>.Ok("SendVcCode Success", _localization.ReturnMsg($"{_this}SendVcCodeSuccess"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<string>.Failure(500, _localization.ReturnMsg($"{_this}SendVcCodeFailed", ex.Message));
            }
        }

        /// <summary>
        ///  密码过期（重置密码）
        /// </summary>
        /// <param name="pwdExpirationUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> PwdExpirationUpdate(PwdExpiration pwdExpirationUpsert)
        {
            try
            {
                // 查询员工信息
                var user = await _sysUserOperateRepository.GetUserInfo(pwdExpirationUpsert.UserNo);

                // 判断员工是否在职
                if (user == null)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UserNotFound"));
                }

                // 获取缓存中的验证码（注意不是创建）
                // 查询存入缓存中的验证码
                //var cachedCode = await _cache.GetOrCreateAsync(
                //    pwdExpirationUpsert.UserNo,
                //    ct => new ValueTask<string>("")
                //);

                // 如果缓存中没有验证码，说明验证码已过期或未发送
                if (string.IsNullOrEmpty("11111"))
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}VcCodeExpired"));
                }

                // 验证验证码是否匹配
                if (!string.Equals("11111", pwdExpirationUpsert.VerificationCode))
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}VcCodeError"));
                }

                // 验证密码格式
                if (!ValidatePassword(pwdExpirationUpsert.PassWord))
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}ValidationPassWrodError"));
                }

                // 密码不能与老密码相同
                await _db.BeginTranAsync();
                var oldHash = HashPasswordWithArgon2id(pwdExpirationUpsert.PassWord, Convert.FromBase64String(user.PwdSalt));
                if (oldHash == user.PassWord)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}NotEqualOldPassWord"));
                }
                else
                {
                    // 加密密码
                    byte[] salt = GenerateSalt();
                    string saltString = Convert.ToBase64String(salt);
                    string passwordHash = HashPasswordWithArgon2id(pwdExpirationUpsert.PassWord, salt);

                    // 更新员工密码
                    int updateCount = await _sysUserOperateRepository.PwdExpirationUpdate(user.UserId, passwordHash, saltString, DateTime.Now.AddDays(user.ExpirationDays).ToString("yyyy-MM-dd HH:mm:ss"));
                    // 清空员工锁定记录
                    await _sysUserOperateRepository.EmptyUserLock(user.UserId);
                    await _db.CommitTranAsync();

                    // 清除验证码缓存
                    //await _cache.RemoveAsync(pwdExpirationUpsert.UserNo);

                    return updateCount >= 1
                        ? Result<int>.Ok(updateCount, _localization.ReturnMsg($"{_this}UpdatePassWrodSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdatePassWrodFailed"));
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
        /// 生成6位随机验证码
        /// </summary>
        /// <returns></returns>
        private static string GenerateRandomCode()
        {
            return string.Create(CodeLength, _random, (span, r) =>
            {
                for (int i = 0; i < span.Length; i++)
                {
                    span[i] = (char)(r.Next(0, 10) + '0');
                }
            });
        }

        /// <summary>
        /// 获取本地IPv4地址
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIPv4()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus != OperationalStatus.Up)
                    continue;

                var ipProps = ni.GetIPProperties();
                foreach (UnicastIPAddressInformation ip in ipProps.UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(ip.Address))
                    {
                        return ip.Address.ToString();
                    }
                }
            }
            return "127.0.0.1"; // fallback
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
        /// 员工密码加密（Argon2Id）
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
