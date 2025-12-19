using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SystemAdmin.CommonSetup.DependencyInjection
{
    public static class CorsSetupExtensions
    {
        private const string PolicyName = "DefaultCors";

        public static IServiceCollection AddCorsSetup(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("DefaultCors", policy =>
                {
                    policy.WithOrigins(
                            "https://localhost:3001"   // ✅ 你的前端
                        )
                        .AllowAnyHeader()
                        .WithMethods("POST", "OPTIONS")
                        .AllowCredentials();
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCorsSetup(this IApplicationBuilder app)
        {
            app.UseCors(PolicyName);
            return app;
        }
    }
}
