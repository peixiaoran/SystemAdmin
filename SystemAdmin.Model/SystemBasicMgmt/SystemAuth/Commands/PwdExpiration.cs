namespace SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Commands
{
    /// <summary>
    /// 密码过期更新修改类
    /// </summary>
    public class PwdExpiration
    {
        /// <summary>
        /// 员工工号
        /// </summary>
        private string _userNo = string.Empty;
        public string UserNo
        {
            get => _userNo;
            set => _userNo = value?.Trim() ?? string.Empty;
        }

        /// <summary>
        /// 密码
        /// </summary>
        private string _passWord = string.Empty;
        public string PassWord
        {
            get => _passWord;
            set => _passWord = value?.Trim() ?? string.Empty;
        }

        /// <summary>
        /// 验证码
        /// </summary>
        private string _verificationCode = string.Empty;
        public string VerificationCode
        {
            get => _verificationCode;
            set => _verificationCode = value?.Trim() ?? string.Empty;
        }
    }
}
