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
                var logRoot = Path.Combine(AppContext.BaseDirectory, "Logs");
                Directory.CreateDirectory(logRoot);

                // 每天一个文件夹 Logs/yyyy-MM-dd/
                string dayFolder = Path.Combine(logRoot, DateTime.Now.ToString("yyyy-MM-dd"));
                Directory.CreateDirectory(dayFolder);

                logger
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("System", LogEventLevel.Warning)
                    .Enrich.FromLogContext()

                    // Console（开发使用）
                    .WriteTo.Console()

                    // 信息日志
                    .WriteTo.File(
                        Path.Combine(dayFolder, "info.log"),
                        restrictedToMinimumLevel: LogEventLevel.Information,
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 30,
                        shared: true)

                    // 错误日志
                    .WriteTo.File(
                        Path.Combine(dayFolder, "error.log"),
                        restrictedToMinimumLevel: LogEventLevel.Error,
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 30,
                        shared: true)

                    // Fatal 致命日志
                    .WriteTo.File(
                        Path.Combine(dayFolder, "fatal.log"),
                        restrictedToMinimumLevel: LogEventLevel.Fatal,
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 30,
                        shared: true)

                    // 捕获异常日志
                    .WriteTo.File(
                        Path.Combine(dayFolder, "exceptions.log"),
                        restrictedToMinimumLevel: LogEventLevel.Error,
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 30,
                        shared: true);
            });

            return host;
        }
    }
}
