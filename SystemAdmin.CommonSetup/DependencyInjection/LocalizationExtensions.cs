using Microsoft.Extensions.DependencyInjection;
using SystemAdmin.CommonSetup.Security;

namespace SystemAdmin.CommonSetup.DependencyInjection
{
    /// <summary>
    /// 多语言相关的依赖注入（只负责注册）
    /// </summary>
    public static class LocalizationExtensions
    {
        /// <summary>
        /// 注册 MessageService + HttpContextAccessor
        /// </summary>
        public static IServiceCollection AddLocalizationSetup(this IServiceCollection services)
        {
            // 多语言消息服务
            services.AddSingleton<LocalizationService>();

            return services;
        }
    }
}
