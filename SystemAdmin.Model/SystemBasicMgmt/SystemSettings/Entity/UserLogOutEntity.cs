using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Entity
{
    /// <summary>
    /// 员工登录日志实体类
    /// </summary>
    [SugarTable("[Basic].[UserLogOut]")]
    public class UserLogOutEntity
    {
        /// <summary>
        /// 登录账号Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 来源IP
        /// </summary>
        public string IP { get; set; } = string.Empty;

        /// <summary>
        /// 操作状态（1：登录成功，2：密码错误，3：未找到工号，4：登出）
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public string LoginDate { get; set; } = string.Empty;
    }
}
