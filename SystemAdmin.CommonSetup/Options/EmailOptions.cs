using System;
using System.Collections.Generic;
using System.Text;

namespace SystemAdmin.CommonSetup.Options
{
    /// <summary>
    /// 邮件发送配置，对应 appsettings: EmailSettings
    /// </summary>
    public class EmailOptions
    {
        /// <summary>
        /// SMTP 服务器地址，例如：smtp.qq.com、smtp.office365.com
        /// </summary>
        public string SmtpServer { get; set; } = string.Empty;

        /// <summary>
        /// SMTP 端口号，一般：25、465、587 等
        /// </summary>
        public int Port { get; set; } = 587;

        /// <summary>
        /// 是否启用 SSL 直连（通常 465 端口使用）
        /// </summary>
        public bool UseSsl { get; set; } = true;

        /// <summary>
        /// 是否启用 StartTLS（通常 587 端口使用）
        /// </summary>
        public bool UseStartTls { get; set; } = true;

        /// <summary>
        /// SMTP 登录用户名，一般为邮箱地址
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// SMTP 登录密码或授权码
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 默认发件人邮箱地址，例如：no-reply@xxx.com
        /// </summary>
        public string From { get; set; } = string.Empty;

        /// <summary>
        /// 默认发件人显示名称，例如：Systems Admin
        /// </summary>
        public string DisplayName { get; set; } = "Systems Admin";

        /// <summary>
        /// 邮件发送超时时间（毫秒），包括连接和发送过程
        /// </summary>
        public int Timeout { get; set; } = 10000;
    }
}
