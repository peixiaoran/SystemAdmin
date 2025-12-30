using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using SystemAdmin.CommonSetup.Options;

namespace SystemAdmin.CommonSetup.DependencyInjection
{
    public static class CorsAppExtensions
    {
        private const string SectionName = "Cors";

        public static IApplicationBuilder UseCorsSetup(this IApplicationBuilder app, IConfiguration configuration)
        {
            var corsOptions = configuration.GetSection(SectionName).Get<CorsOptions>() ?? new CorsOptions();
            return app.UseCors(corsOptions.PolicyName);
        }
    }
}
