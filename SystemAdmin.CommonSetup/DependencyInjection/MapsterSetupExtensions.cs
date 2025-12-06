using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SystemAdmin.CommonSetup.DependencyInjection
{
    public static class MapsterSetupExtensions
    {
        public static IServiceCollection AddMapsterSetup(this IServiceCollection services)
        {
            // 创建 Mapster 配置实例
            var config = TypeAdapterConfig.GlobalSettings;

            // 注册你自定义的配置
            //MapsterRegister.Register(config);

            // 注入 IMapper（MapsterMapper 实现）
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
