using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SystemAdmin.CommonSetup.Security;

namespace SystemAdmin.CommonSetup.DependencyInjection
{
    /// <summary>
    /// 多语言相关的依赖注入（只负责注册）
    /// </summary>
    public static class LocalizationSetupExtensions
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
