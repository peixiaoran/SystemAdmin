namespace SystemAdmin.CommonSetup.Options
{
    /// <summary>
    /// 发送邮件配置选项
    /// </summary>
    public class EmailOptions
    {
        public string SmtpServer { get; set; } = default!;

        public string UserName { get; set; } = default!;

        public string Password { get; set; } = default!; // SMTP 授权码

        public string From { get; set; } = default!;

        public string DisplayName { get; set; } = "System Admin";

        public int Timeout { get; set; } = 10000;
    }
}
