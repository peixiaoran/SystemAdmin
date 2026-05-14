using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SystemAdmin.CommonSetup.Options;

namespace SystemAdmin.CommonSetup.DependencyInjection
{
    public static class AppUrlExtensions
    {
        public static IServiceCollection AddAppUrlSetup(this IServiceCollection services, IConfiguration configuration)
        {
            // 绑定配置
            services.Configure<AppUrlOptions>(configuration.GetSection("AppUrl"));

            return services;
        }
    }
}
