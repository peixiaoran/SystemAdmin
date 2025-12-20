using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity
{
    /// <summary>
    /// 员工账号锁定信息实体
    /// </summary>
    [SugarTable("[Basic].[UserLock]")]
    public class UserLockEntity
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 密码错误次数
        /// </summary>
        public int NumberErrors { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string? CreatedDate { get; set; } = null;
    }
}
