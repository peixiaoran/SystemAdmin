namespace SystemAdmin.Model.SystemBasicMgmt.Auth.Queries
{
    /// <summary>
    /// 员工登录请求参数
    /// </summary>
    public class SysLogin
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginNo { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; } = string.Empty;
    }
}
