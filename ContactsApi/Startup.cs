using ContactsApi.Extensions;
using Serilog;

namespace ContactsApi
{
    public static class Startup
    {
        public static WebApplicationBuilder Configure(this WebApplicationBuilder builder) 
        {
            Serilog.Debugging.SelfLog.Enable(Console.Error);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("logs/.log",
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true,
                fileSizeLimitBytes: 100000) 
                .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.AddHealthChecks();
            builder.AppOpenTelemetryMetrics();
            builder.Services.AddAutoMapper(typeof(Program));

            builder.AddServices();

            return builder;
        }
    }
}
