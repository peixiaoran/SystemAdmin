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
        /// 职业下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserLaborDropDto>> GetLaborDrop()
        {
            var query = _db.Queryable<UserLaborEntity>().With(SqlWith.NoLock);
            if (_lang.Locale == "zh-CN")
            {
                query = query.OrderBy(labor => labor.LaborNameCn);
            }
            else
            {
                query = query.OrderBy(labor => labor.LaborNameEn);
            }

            return await query.Select(labor => new UserLaborDropDto
            {
                LaborId = labor.LaborId,
                LaborName = _lang.Locale == "zh-CN"
                            ? labor.LaborNameCn
                            : labor.LaborNameEn
            }).ToListAsync();
        }

        /// <summary>
        /// 部门树下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<DepartmentDropDto>> GetDepartmentDrop()
        {
            return await _db.Queryable<DepartmentInfoEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<DepartmentLevelEntity>((dept, deptlevel) => dept.DepartmentLevelId == deptlevel.DepartmentLevelId)
                            .OrderBy(dept => dept.SortOrder)
                            .Select((dept, deptlevel) => new DepartmentDropDto
                            {
                                DepartmentId = dept.DepartmentId,
                                DepartmentName = _lang.Locale == "zh-CN"
                                                 ? dept.DepartmentNameCn
                                                 : dept.DepartmentNameEn,
                                ParentId = dept.ParentId,
                            }).ToTreeAsync(menu => menu.DepartmentChildList, menu => menu.ParentId, 0);
        }

        /// <summary>
        /// 职级下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<PositionInfoDropDto>> GetPositionInfoDrop()
        {
            return await _db.Queryable<PositionInfoEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(position => position.CreatedDate)
                            .Select((position) => new PositionInfoDropDto
                            {
                                PositionId = position.PositionId,
                                PositionName = _lang.Locale == "zh-CN"
                                               ? position.PositionNameCn
                                               : position.PositionNameEn
                            }).ToListAsync();
        }

        /// <summary>
        /// 角色下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleInfoDropDto>> GetRoleDrop()
        {
            return await _db.Queryable<RoleInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Select(role => new RoleInfoDropDto
                            {
                                RoleId = role.RoleId,
                                RoleName = _lang.Locale == "zh-CN"
                                           ? role.RoleNameCn
                                           : role.RoleNameEn,
                            }).ToListAsync();
        }

        /// <summary>
        /// 查询个人信息实体
        /// </summary>
        /// <param name="loginUserId"></param>
        /// <returns></returns>
        public async Task<PersonalInfoDto> GetPersonalInfoEntity(long loginUserId)
        {
            return await _db.Queryable<UserInfoEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<UserRoleEntity>((user, userrole) => user.UserId == userrole.UserId)
                            .InnerJoin<RoleInfoEntity>((user, userrole, role) => userrole.RoleId == role.RoleId)
                            .InnerJoin<DepartmentInfoEntity>((user, userrole, role, dept) => user.DepartmentId == dept.DepartmentId)
                            .InnerJoin<DepartmentLevelEntity>((user, userrole, role, dept, deptlevel) => dept.DepartmentLevelId == deptlevel.DepartmentLevelId)
                            .InnerJoin<PositionInfoEntity>((user, userrole, role, dept, deptlevel, position) => user.PositionId == position.PositionId).InnerJoin<UserLaborEntity>((user, userrole, role, dept, deptlevel, position, labor) => user.LaborId == labor.LaborId)
                            .InnerJoin<NationalityInfoEntity>((user, userrole, role, dept, deptlevel, position, labor, nation) => user.Nationality == nation.NationId)
                            .Where((user, userrole, role, dept, deptlevel, position, labor, nation) => user.UserId == loginUserId)
                             .Select((user, userrole, role, dept, deptlevel, position, labor, nation) => new PersonalInfoDto
                             {
                                 UserId = user.UserId,
                                 UserNo = user.UserNo,
                                 UserNameCn = user.UserNameCn,
                                 UserNameEn = user.UserNameEn,
                                 Email = user.Email,
                                 PhoneNumber = user.PhoneNumber,
                                 LoginNo = user.LoginNo,
                                 DepartmentId = user.DepartmentId,
                                 DepartmentLevelId = deptlevel.DepartmentLevelId,
                                 RoleId = role.RoleId,
                                 PositionId = position.PositionId,
                                 Gender = user.Gender,
                                 LaborId = labor.LaborId,
                                 HireDate = Convert.ToDateTime(user.HireDate).ToString("yyyy-MM-dd"),
                                 AvatarAddress = user.AvatarAddress,
                                 IsEmployed = user.IsEmployed,
                                 IsApproval = user.IsApproval,
                                 IsRealtimeNotification = user.IsRealtimeNotification,
                                 IsScheduledNotification = user.IsScheduledNotification,
                                 IsAgent = user.IsAgent,
                                 IsPartTime = user.IsPartTime,
                                 IsFreeze = user.IsFreeze,
                                 Remark = user.Remark,
                             }).FirstAsync();
        }

        /// <summary>
        /// 个人信息修改
        /// </summary>
        /// <param name="loginUserId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdatePersonalInfo(long loginUserId, UserInfoEntity entity)
        {
            return await _db.Updateable(entity)
                            .UpdateColumns(user => new
                            {
                                user.PassWord,
                                user.PwdSalt,
                                user.AvatarAddress,
                                user.IsRealtimeNotification,
                                user.IsScheduledNotification,
                                user.ModifiedBy,
                                user.ModifiedDate
                            }).Where(personal => personal.UserId == loginUserId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改员工头像
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userAvatar"></param>
        /// <returns></returns>
        public async Task<int> UpdateUserAvatar(long userId, string userAvatar)
        {
            return await _db.Updateable<UserInfoEntity>()
                            .SetColumns(user => new UserInfoEntity
                            {
                                AvatarAddress = userAvatar,
                                ModifiedBy = userId,
                                ModifiedDate = DateTime.Now
                            }).Where(user => user.UserId == userId)
                            .ExecuteCommandAsync();
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
