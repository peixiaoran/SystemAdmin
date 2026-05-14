namespace SystemAdmin.CommonSetup.Options
{
    /// <summary>
    /// 发送的邮件内容
    /// </summary>
    public class EmailMessage
    {
        /// <summary>
        /// 收件人地址列表
        /// </summary>
        public List<string> To { get; set; } = new();

        /// <summary>
        /// 抄送地址列表
        /// </summary>
        public List<string> Cc { get; set; } = new();

        /// <summary>
        /// 密送地址列表
        /// </summary>
        public List<string> Bcc { get; set; } = new();

        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// 邮件正文内容，支持 HTML
        /// </summary>
        public string Body { get; set; } = string.Empty;

        /// <summary>
        /// 是否为 HTML 格式正文
        /// </summary>
        public bool IsHtml { get; set; } = true;

        /// <summary>
        /// 附件文件路径列表，路径必须指向本地存在的文件
        /// </summary>
        public List<string> Attachments { get; set; } = new();
    }
}
