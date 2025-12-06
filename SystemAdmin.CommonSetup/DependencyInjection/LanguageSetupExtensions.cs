using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;

namespace SystemAdmin.CommonSetup.DependencyInjection
{
    public static class LanguageSetupExtensions
    {
        public static IServiceCollection AddLanguage(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            // 每个请求生成一个 RequestLanguage 实例
            services.AddScoped(sp =>
            {
                var accessor = sp.GetRequiredService<LanguageAccessor>();
                return accessor.GetRequestLanguage();
            });

            services.AddScoped<LanguageAccessor>();

            return services;
        }
    }
}
