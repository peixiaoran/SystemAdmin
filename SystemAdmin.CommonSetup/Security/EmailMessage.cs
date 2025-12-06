using System;
using System.Collections.Generic;
using System.Text;

namespace SystemAdmin.CommonSetup.Options
{
    /// <summary>
    /// 业务层使用的邮件消息对象，用于描述一封要发送的邮件内容。
    /// 只负责承载数据，不包含任何发送逻辑。
    /// </summary>
    public class EmailMessage
    {
        /// <summary>
        /// 收件人地址列表（必填至少一个），格式：user@xxx.com
        /// </summary>
        public List<string> To { get; set; } = new();

        /// <summary>
        /// 抄送地址列表（可选）
        /// </summary>
        public List<string> Cc { get; set; } = new();

        /// <summary>
        /// 密送地址列表（可选）
        /// </summary>
        public List<string> Bcc { get; set; } = new();

        /// <summary>
        /// 邮件主题（标题）
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// 邮件正文内容，支持 HTML 或纯文本
        /// </summary>
        public string Body { get; set; } = string.Empty;

        /// <summary>
        /// 是否为 HTML 格式正文。true 表示按 HtmlBody 处理，否则按 TextBody 处理。
        /// 默认 true。
        /// </summary>
        public bool IsHtml { get; set; } = true;

        /// <summary>
        /// 附件文件路径列表，路径必须指向本地存在的文件。
        /// </summary>
        public List<string> Attachments { get; set; } = new();
    }
}
