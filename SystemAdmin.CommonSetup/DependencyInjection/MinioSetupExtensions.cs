using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;
using System;
using System.Collections.Generic;
using System.Text;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;

namespace SystemAdmin.CommonSetup.DependencyInjection
{
    public static class MinioSetupExtensions
    {
        /// <summary>
        /// 注册 Minio 相关依赖：MinioSettings、MinioClient、MinioStorageService
        /// </summary>
        public static IServiceCollection AddMinioSetup(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // 1. 绑定 MinioSettings（JSON + 环境变量 Minio__XXX）
            services.Configure<MinioSettings>(opts =>
            {
                // 从 appsettings.json: "Minio" section 绑定默认值
                configuration.GetSection("Minio").Bind(opts);
            });

            // 2. 注册 MinioClient 单例（从 Options 里读取配置）
            services.AddSingleton(sp =>
            {
                var options = sp.GetRequiredService<IOptions<MinioSettings>>().Value;

                var client = new MinioClient()
                    .WithEndpoint(options.Endpoint)
                    .WithCredentials(options.AccessKey, options.SecretKey);

                if (options.UseSSL)
                {
                    client = client.WithSSL();
                }

                return client.Build();
            });

            // 3. 注册业务操作服务
            services.AddScoped<MinioService>();

            return services;
        }
    }
}
