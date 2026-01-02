using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace SystemAdmin.Hosting.DependencyInjection
{
    public static class SerilogSetupExtensions
    {
        public static IHostBuilder AddSerilogSetup(this IHostBuilder host)
        {
            host.UseSerilog((context, services, logger) =>
            {
                var logRoot = Path.Combine(
                    context.HostingEnvironment.ContentRootPath,
                    "Logs",
                    DateTime.Now.ToString("yyyy-MM-dd"));

                Directory.CreateDirectory(logRoot);

                logger
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .Enrich.FromLogContext()

                    // Information
                    .WriteTo.Logger(lc => lc
                        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
                        .WriteTo.File(Path.Combine(logRoot, "info.log"))
                    )

                    // Warning
                    .WriteTo.Logger(lc => lc
                        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
                        .WriteTo.File(Path.Combine(logRoot, "warning.log"))
                    )

                    // Error
                    .WriteTo.Logger(lc => lc
                        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
                        .WriteTo.File(Path.Combine(logRoot, "error.log"))
                    )

                    // Critical / Fatal
                    .WriteTo.Logger(lc => lc
                        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal)
                        .WriteTo.File(Path.Combine(logRoot, "fatal.log"))
                    );
            });
            return host;
        }
    }
}
