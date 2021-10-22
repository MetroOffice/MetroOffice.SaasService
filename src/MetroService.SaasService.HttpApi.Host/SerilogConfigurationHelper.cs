using Serilog;
using Serilog.Events;

namespace MetroService.SaasService.HttpApi.Host
{
    public static class SerilogConfigurationHelper
    {
        public static void Configure(string? applicationName)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", $"{applicationName}")
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
                .WriteTo.Async(c => c.Console())
                .CreateLogger();
        }
    }
}
