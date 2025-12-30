using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SystemAdmin.CommonSetup.Options;

namespace SystemAdmin.CommonSetup.DependencyInjection
{
    public static class CorsSetupExtensions
    {
        private const string SectionName = "Cors";

        public static IServiceCollection AddCorsSetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CorsOptions>(configuration.GetSection(SectionName));

            var corsOptions = configuration.GetSection(SectionName).Get<CorsOptions>() ?? new CorsOptions();

            services.AddCors(options =>
            {
                options.AddPolicy(corsOptions.PolicyName, policy =>
                {
                    // Origins
                    if (corsOptions.Origins is { Length: > 0 })
                    {
                        policy.WithOrigins(corsOptions.Origins);
                    }

                    // Headers
                    if (corsOptions.AllowAnyHeader)
                        policy.AllowAnyHeader();

                    // Methods
                    if (corsOptions.Methods is { Length: > 0 })
                        policy.WithMethods(corsOptions.Methods);
                    else
                        policy.AllowAnyMethod();

                    // Credentials
                    if (corsOptions.AllowCredentials)
                        policy.AllowCredentials();
                });
            });

            return services;
        }
    }
}
