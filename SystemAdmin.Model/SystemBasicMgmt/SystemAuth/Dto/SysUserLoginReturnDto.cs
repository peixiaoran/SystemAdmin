namespace SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Dto
{
    /// <summary>
    /// 员工登录返回Dto
    /// </summary>
    public class SysUserLoginReturnDto
    {
        /// <summary>
        /// 员工工号
        /// </summary>
        public string UserNo { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名（中文）
        /// </summary>
        public string UserNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名（英文）
        /// </summary>
        public string UserNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 员工头像地址
        /// </summary>
        public string AvatarAddress { get; set; } = string.Empty;
    }
}
