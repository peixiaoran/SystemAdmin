using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;

namespace SystemAdmin.CommonSetup.DependencyInjection
{
    public static class MinioSetupExtensions
    {
        /// <summary>
        /// 注册 Minio 相关依赖：MinioSettings、IMinioClient、MinioService
        /// </summary>
        public static IServiceCollection AddMinioSetup(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // 1. 绑定 MinioSettings
            services.Configure<MinioSettings>(opts =>
            {
                configuration.GetSection("Minio").Bind(opts);
            });

            // 2. 注册 IMinioClient 单例
            services.AddSingleton<IMinioClient>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<MinioSettings>>().Value;

                var client = new MinioClient()
                    .WithEndpoint(options.Endpoint)
                    .WithCredentials(options.AccessKey, options.SecretKey);

                if (options.UseSSL)
                {
                    client = client.WithSSL();
                }

                // Build() 返回 IMinioClient，正好匹配注册类型
                return client.Build();
            });

            // 3. 注册业务操作服务
            services.AddScoped<MinioService>();

            return services;
        }
    }
}
