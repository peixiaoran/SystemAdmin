using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using SystemAdmin.CommonSetup.Options;

namespace SystemAdmin.CommonSetup.Security
{
    public class MailKitEmailSender
    {
        private readonly EmailOptions _options;

        /// <summary>
        /// 构造函数，由依赖注入框架调用。
        /// IOptions&lt;EmailOptions&gt; 会从配置系统中自动绑定 EmailSettings 节点。
        /// </summary>
        /// <param name="options">邮件发送配置，来源于配置系统（appsettings + 环境变量）</param>
        /// <param name="logger">日志记录器</param>
        public MailKitEmailSender(IOptions<EmailOptions> options, ILogger<MailKitEmailSender> logger)
        {
            _options = options.Value;
        }

        /// <summary>
        /// 发送邮件（异步）。
        /// 业务层只需要构建 EmailMessage 对象并调用本方法即可完成发送。
        /// </summary>
        /// <param name="message">邮件内容对象</param>
        /// <param name="cancellationToken">取消标记，可用于超时或请求中止</param>
        public async Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default)
        {
            // 基础校验：必须至少有一个收件人
            if (message.To == null || message.To.Count == 0)
            {
                throw new ArgumentException("邮件至少需要一个收件人（To）。", nameof(message));
            }

            // 将业务层的 EmailMessage 转换为 MimeMessage（MailKit/MimeKit 使用的格式）
            var mimeMessage = BuildMimeMessage(message);

            // MailKit SmtpClient 实例
            using var client = new SmtpClient();

            // 设置超时时间（毫秒）
            client.Timeout = _options.Timeout;

            // 依据配置选择加密方式
            var secureOption = GetSecureSocketOptions();

            // 连接到 SMTP 服务器
            await client.ConnectAsync(
                _options.SmtpServer,
                _options.Port,
                secureOption,
                cancellationToken);

            // 如果配置了用户名，则进行 SMTP 身份认证
            if (!string.IsNullOrWhiteSpace(_options.UserName))
            {
                await client.AuthenticateAsync(
                    _options.UserName,
                    _options.Password,
                    cancellationToken);
            }

            // 发送邮件
            await client.SendAsync(mimeMessage, cancellationToken);

            // 正常断开连接
            await client.DisconnectAsync(true, cancellationToken);
        }

        /// <summary>
        /// 根据 EmailMessage 构建 MimeMessage 对象，
        /// 其中包括发件人、收件人、抄送、密送、主题、正文和附件等信息。
        /// </summary>
        /// <param name="message">业务层邮件对象</param>
        /// <returns>MimeMessage 实例</returns>
        private MimeMessage BuildMimeMessage(EmailMessage message)
        {
            var mimeMessage = new MimeMessage();

            // 设置发件人（使用配置中的 From 和 DisplayName）
            mimeMessage.From.Add(new MailboxAddress(
                _options.DisplayName,
                _options.From));

            // 收件人
            foreach (var to in message.To)
            {
                if (!string.IsNullOrWhiteSpace(to))
                {
                    mimeMessage.To.Add(MailboxAddress.Parse(to));
                }
            }

            // 抄送
            if (message.Cc != null)
            {
                foreach (var cc in message.Cc)
                {
                    if (!string.IsNullOrWhiteSpace(cc))
                    {
                        mimeMessage.Cc.Add(MailboxAddress.Parse(cc));
                    }
                }
            }

            // 密送
            if (message.Bcc != null)
            {
                foreach (var bcc in message.Bcc)
                {
                    if (!string.IsNullOrWhiteSpace(bcc))
                    {
                        mimeMessage.Bcc.Add(MailboxAddress.Parse(bcc));
                    }
                }
            }

            // 主题
            mimeMessage.Subject = message.Subject ?? string.Empty;

            // 正文和附件构建
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = message.IsHtml ? message.Body : null,
                TextBody = message.IsHtml ? null : message.Body
            };

            // 附件处理：只添加存在的文件
            if (message.Attachments != null)
            {
                foreach (var path in message.Attachments)
                {
                    if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
                    {
                        bodyBuilder.Attachments.Add(path);
                    }
                }
            }

            mimeMessage.Body = bodyBuilder.ToMessageBody();

            return mimeMessage;
        }

        /// <summary>
        /// 根据配置决定使用的加密方式：
        /// - UseSsl 为 true 时使用 SslOnConnect
        /// - UseStartTls 为 true 时使用 StartTls
        /// - 否则使用 Auto（由 MailKit 自行判断）
        /// </summary>
        private SecureSocketOptions GetSecureSocketOptions()
        {
            if (_options.UseSsl)
                return SecureSocketOptions.SslOnConnect;

            if (_options.UseStartTls)
                return SecureSocketOptions.StartTls;

            return SecureSocketOptions.Auto;
        }
    }
}
