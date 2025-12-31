using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SystemAdmin.CommonSetup.DependencyInjection
{
    public static class CacheServiceCollectionExtensions
    {
        public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            // 只注册 HybridCache（内部使用本地 MemoryCache）
            services.AddHybridCache(options =>
            {
                // 全局默认过期时间（可按需调整）
                options.DefaultEntryOptions = new HybridCacheEntryOptions
                {
                    Expiration = TimeSpan.FromMinutes(5)
                };
            });

            return services;
        }
    }
}
