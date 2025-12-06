using Konscious.Security.Cryptography;
using SqlSugar;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Entity;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemBasicData
{
    public class PersonalInfoRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public PersonalInfoRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 查询个人信息实体
        /// </summary>
        /// <param name="loginUserId"></param>
        /// <returns></returns>
        public async Task<PersonalInfoDto> GetPersonalInfoEntity(long loginUserId)
        {
            return await _db.Queryable<UserInfoView>()
                            .With(SqlWith.NoLock)
                            .Where(parsonal => parsonal.UserId == loginUserId)
                            .Select((parsonal) => new PersonalInfoDto
                            {
                                UserId = parsonal.UserId,
                                UserNo = parsonal.UserNo,
                                UserNameCn = parsonal.UserNameCn,
                                UserNameEn = parsonal.UserNameEn,
                                Email = parsonal.Email,
                                PhoneNumber = parsonal.PhoneNumber,
                                LoginNo = parsonal.LoginNo,
                                DepartmentId = parsonal.DepartmentId,
                                DepartmentName = _lang.Locale == "zh-CN"
                                                 ? parsonal.DepartmentNameCn
                                                 : parsonal.DepartmentNameEn,
                                DepartmentLevelId = parsonal.DepartmentLevelId,
                                DepartmentLevelName = _lang.Locale == "zh-CN"
                                                 ? parsonal.DepartmentLevelNameCn
                                                 : parsonal.DepartmentLevelNameEn,
                                RoleId = parsonal.RoleId,
                                PositionId = parsonal.PositionId,
                                PositionName = _lang.Locale == "zh-CN"
                                                 ? parsonal.PositionNameCn
                                                 : parsonal.PositionNameEn,
                                Gender = parsonal.Gender,
                                GenderName = _lang.Locale == "zh-CN"
                                                 ? parsonal.GenderNameCn
                                                 : parsonal.GenderNameEn,
                                RoleName = _lang.Locale == "zh-CN"
                                                 ? parsonal.RoleNameCn
                                                 : parsonal.RoleNameEn,
                                LaborId = parsonal.LaborId,
                                LaborName = _lang.Locale == "zh-CN"
                                                 ? parsonal.LaborNameCn
                                                 : parsonal.LaborNameEn,
                                HireDate = Convert.ToDateTime(parsonal.HireDate).ToString("yyyy-MM-dd"),
                                AvatarAddress = parsonal.AvatarAddress,
                                IsEmployed = parsonal.IsEmployed,
                                IsEmployedName = _lang.Locale == "zh-CN"
                                                 ? parsonal.IsEmployedNameCn
                                                 : parsonal.IsEmployedNameEn,
                                IsApproval = parsonal.IsApproval,
                                IsRealtimeNotification = parsonal.IsRealtimeNotification,
                                IsScheduledNotification = parsonal.IsScheduledNotification,
                                IsAgent = parsonal.IsAgent,
                                IsPartTime = parsonal.IsPartTime,
                                IsFreeze = parsonal.IsFreeze,
                                Remark = parsonal.Remark,
                            }).FirstAsync();
        }

        /// <summary>
        /// 个人信息修改
        /// </summary>
        /// <param name="loginUserId"></param>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public async Task<int> UpdatePersonalInfo(long loginUserId, UserInfoEntity userEntity)
        {
            return await _db.Updateable(userEntity)
                            .UpdateColumns(user => new
                            {
                                user.UserNameCn,
                                user.UserNameEn,
                                user.Email,
                                user.PhoneNumber,
                                user.PassWord,
                                user.PwdSalt,
                                user.AvatarAddress,
                                user.IsRealtimeNotification,
                                user.IsScheduledNotification
                            }).Where(personal => personal.UserId == loginUserId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 性别字典下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<GenderDropDto>> GetGenderDropDown()
        {
            return await _db.Queryable<DictionaryInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Where(genderdic => genderdic.DicType == "Gender")
                            .Select(genderdic => new GenderDropDto
                            {
                                GenderCode = genderdic.DicCode,
                                GenderName = _lang.Locale == "zh-CN"
                                             ? genderdic.DicNameCn
                                             : genderdic.DicNameEn
                            }).ToListAsync();
        }

        /// <summary>
        /// 职业下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserLaborDropDto>> GetLaborDropDown()
        {
            return await _db.Queryable<UserLaborEntity>()
                            .With(SqlWith.NoLock)
                            .Select(userlabor => new UserLaborDropDto
                            {
                                LaborId = userlabor.LaborId,
                                LaborName = _lang.Locale == "zh-CN"
                                            ? userlabor.LaborNameCn
                                            : userlabor.LaborNameEn
                            }).ToListAsync();
        }

        /// <summary>
        /// 部门树下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<DepartmentDropDto>> GetDepartmentDropDown()
        {
            return await _db.Queryable<DepartmentInfoEntity>()
                            .With(SqlWith.NoLock)
                            .LeftJoin<DepartmentLevelEntity>((dept, deptlevel) => dept.DepartmentLevelId == deptlevel.DepartmentLevelId)
                            .OrderBy(dept => dept.SortOrder)
                            .Select((dept, deptlevel) => new DepartmentDropDto
                            {
                                DepartmentId = dept.DepartmentId,
                                DepartmentName = _lang.Locale == "zh-CN"
                                                 ? dept.DepartmentNameCn
                                                 : dept.DepartmentNameEn,
                                ParentId = dept.ParentId,
                                Disabled = dept.IsEnabled == 0
                            }).ToTreeAsync(menu => menu.DepartmentChildList, menu => menu.ParentId, 0);
        }

        /// <summary>
        /// 职级下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserPositionDropDto>> GetUserPositionDropDown()
        {
            return await _db.Queryable<UserPositionEntity>()
                            .With(SqlWith.NoLock).OrderBy(userpos => userpos.CreatedDate)
                            .Select((userpos) => new UserPositionDropDto
                            {
                                PositionId = userpos.PositionId,
                                PositionName = _lang.Locale == "zh-CN"
                                               ? userpos.PositionNameCn
                                               : userpos.PositionNameEn
                            }).ToListAsync();
        }

        /// <summary>
        /// 角色下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleInfoDropDto>> GetRoleDropDown()
        {
            return await _db.Queryable<RoleInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Select(role => new RoleInfoDropDto
                            {
                                RoleId = role.RoleId,
                                RoleName = _lang.Locale == "zh-CN"
                                           ? role.RoleNameCn
                                           : role.RoleNameEn,
                                Disabled = role.IsEnabled == 0
                            }).ToListAsync();
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

        /// <summary>
        /// 查询员工密码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserInfoEntity> GetUserPasswordAndSalt(long userId)
        {
            return await _db.Queryable<UserInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Where(u => u.UserId == userId)
                            .Select(u => new UserInfoEntity
                            {
                                PassWord = u.PassWord,
                                PwdSalt = u.PwdSalt
                            }).FirstAsync();
        }
    }
}
