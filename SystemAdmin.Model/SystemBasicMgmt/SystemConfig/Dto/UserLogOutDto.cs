namespace SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Dto
{
    /// <summary>
    /// 员工登入登出日志Dto
    /// </summary>
    public class UserLogOutDto
    {
        /// <summary>
        /// 登录账号Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string UserNo { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名(中文)
        /// </summary>
        public string UserNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名(英文)
        /// </summary>
        public string UserNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 来源IP
        /// </summary>
        public string IP { get; set; } = string.Empty;

        /// <summary>
        /// 登录状态Id
        /// </summary>
        public string StatusId { get; set; } = string.Empty;

        /// <summary>
        /// 登录状态名称
        /// </summary>
        public string StatusName { get; set; } = string.Empty;

        /// <summary>
        /// 登录时间
        /// </summary>
        public string LoginDate { get; set; } = string.Empty;
    }
}
