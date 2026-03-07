namespace SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Dto
{
    /// <summary>
    /// 员工登入登出日志Dto
    /// </summary>
    public class UserLogOutDto
    {
        /// <summary>
        /// 员工工号
        /// </summary>
        public string UserNo { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 来源IP
        /// </summary>
        public string IP { get; set; } = string.Empty;

        /// <summary>
        /// 登录状态
        /// </summary>
        public string LoginType { get; set; } = string.Empty;

        /// <summary>
        /// 登录状态名称
        /// </summary>
        public string LoginTypeName { get; set; } = string.Empty;

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime? LoginDate { get; set; }
    }
}
