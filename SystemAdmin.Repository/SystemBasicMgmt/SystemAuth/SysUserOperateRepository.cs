using Konscious.Security.Cryptography;
using SqlSugar;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Queries;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemAuth
{
    public class SysUserOperateRepository
    {
        private readonly SqlSugarScope _db;

        public SysUserOperateRepository(SqlSugarScope db)
        {
            _db = db;
        }

        /// <summary>
        /// 员工登录前查询是否有效
        /// </summary>
        /// <param name="sysLogin"></param>
        /// <returns></returns>
        public async Task<UserInfoEntity> LoginGetUserInfo(SysLogin sysLogin)
        {
            return await _db.Queryable<UserInfoEntity>()
                            .With(SqlWith.NoLock)
                            .LeftJoin<UserRoleEntity>((user, userrole) => user.UserId == userrole.UserId)
                            .LeftJoin<RoleInfoEntity>((user, userrole, role) => userrole.RoleId == role.RoleId)
                            .Where((user, userrole, role) => user.LoginNo == sysLogin.LoginNo && user.IsEmployed == 1)
                            .FirstAsync();
        }

        /// <summary>
        /// 查询员工锁定次数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserLockEntity> GetUserLockErrorNumberNow(long userId)
        {
            return await _db.Queryable<UserLockEntity>()
                            .With(SqlWith.NoLock)
                            .Where(user => user.UserId == userId)
                            .FirstAsync();
        }

        /// <summary>
        /// 将员工设置为冻结状态
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> UpdateUserFreeze(long userId)
        {
            return await _db.Updateable<UserInfoEntity>()
                            .SetColumns(user => user.IsFreeze == 1)
                            .Where(user => user.UserId == userId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 记录员工登录日志
        /// </summary>
        /// <param name="userLoginLogEntity"></param>
        /// <returns></returns>
        public async Task<int> AddUserLoginLogInfo(UserLogOutEntity userLoginLogEntity)
        {
            return await _db.Insertable(userLoginLogEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增员工密码错误记录
        /// </summary>
        /// <param name="userLockEntity"></param>
        /// <returns></returns>
        public async Task<int> AddUserLock(UserLockEntity userLockEntity)
        {
            return await _db.Insertable(userLockEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 增加员工密码错误记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="numberErrors"></param>
        /// <returns></returns>
        public async Task<int> AutoUserLockErrorNumber(long userId, int numberErrors)
        {
            return await _db.Updateable<UserLockEntity>()
                            .SetColumns(userlock => userlock.NumberErrors == numberErrors)
                            .Where(userlock => userlock.UserId == userId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 清空员工锁定记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> EmptyUserLock(long userId)
        {
            return await _db.Deleteable<UserLockEntity>()
                            .Where(userlock => userlock.UserId == userId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 记录员工登出日志
        /// </summary>
        /// <param name="userLoginLogEntity"></param>
        /// <returns></returns>
        public async Task<int> AddUserLogOutInfo(UserLogOutEntity userLoginLogEntity)
        {
            return await _db.Insertable(userLoginLogEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 记录员工登出日志
        /// </summary>
        /// <param name="userLoginLogEntity"></param>
        /// <returns></returns>
        public async Task<int> AddUserLogOutInfoList(List<UserLogOutEntity> userLoginLogEntity)
        {
            return await _db.Insertable(userLoginLogEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 登出前获取员工信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserInfoEntity> GetUserInfoForUserLogOut(long userId)
        {
            return await _db.Queryable<UserInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Where(user => user.UserId == userId)
                            .FirstAsync();
        }

        /// <summary>
        /// 查询员工信息
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public async Task<UserInfoEntity> GetUserInfo(string userNo)
        {
            return await _db.Queryable<UserInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Where(user => user.UserNo == userNo && user.IsEmployed == 1)
                            .FirstAsync();
        }

        /// <summary>
        /// 员工账号解锁
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passWord"></param>
        /// <param name="salt"></param>
        /// <param name="expirationTime"></param>
        /// <returns></returns>
        public async Task<int> UnlockUserFreeze(long userId, string passWord, string salt, string expirationTime)
        {
            return await _db.Updateable<UserInfoEntity>()
                            .SetColumns(userinfo => new UserInfoEntity
                            {
                                PassWord = passWord,
                                PwdSalt = salt,
                                ExpirationTime = expirationTime,
                                IsFreeze = 0, // 解锁时将 IsFreeze 设置为 0
                            }).Where(user => user.UserId == userId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除员工锁定信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> DeleleUserLockLog(long userId)
        {
            return await _db.Deleteable<UserLockEntity>()
                            .Where(user => user.UserId == userId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 密码过期修改密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passWord"></param>
        /// <param name="pwdSalt"></param>
        /// <param name="expirationTime"></param>
        /// <returns></returns>
        public async Task<int> PwdExpirationUpdate(long userId, string passWord, string pwdSalt, string expirationTime)
        {
            return await _db.Updateable<UserInfoEntity>()
                            .SetColumns(user => new UserInfoEntity
                            {
                                PassWord = passWord,
                                PwdSalt = pwdSalt,
                                ExpirationTime = expirationTime
                            }).Where(user => user.UserId == userId)
                            .ExecuteCommandAsync();
        }
    }
}
