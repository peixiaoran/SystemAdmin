namespace SystemAdmin.Common.Enums.SystemBasicMgmt
{
    /// <summary>
    /// 登录行为枚举
    /// </summary>
    public enum LoginBehavior
    {
        /// <summary>
        /// 登入成功
        /// </summary>
        LoginSuccessful = 1,

        /// <summary>
        /// 密码错误
        /// </summary>
        IncorrectPassword = 2,

        /// <summary>
        /// 账号不存在
        /// </summary>
        AccountNotExist = 3,

        /// <summary>
        /// 登出
        /// </summary>
        LoggedOut = 4,
    }
}
