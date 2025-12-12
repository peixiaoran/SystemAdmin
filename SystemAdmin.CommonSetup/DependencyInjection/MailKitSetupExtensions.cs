using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;

namespace SystemAdmin.CommonSetup.DependencyInjection
{
    public static class MailKitSetupExtensions
    {
        /// <summary>
        /// 注册基于 MailKit 的邮件发送组件。
        /// 包含 EmailOptions 配置绑定和 MailKitEmailSender 类型注册。
        /// </summary>
        /// <param name="services">依赖注入服务集合</param>
        /// <param name="configuration">配置对象，内含 appsettings.json + 环境变量 等多个配置源</param>
        public static IServiceCollection AddMailKitSetup(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<EmailOptions>(configuration.GetSection("EmailSettings"));
            services.PostConfigure<EmailOptions>(options =>
            {
                if (options.Port <= 0)
                {
                    options.Port = 587;
                }

                if (string.IsNullOrWhiteSpace(options.DisplayName))
                {
                    options.DisplayName = "Systems Admin";
                }

                if (options.Timeout <= 0)
                {
                    options.Timeout = 10000;
                }
            });

            services.AddTransient<MailKitEmailSender>();

            return services;
        }
    }
}
