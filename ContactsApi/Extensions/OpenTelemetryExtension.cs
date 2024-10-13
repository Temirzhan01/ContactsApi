using OpenTelemetry.Metrics;

namespace ContactsApi.Extensions
{
    public static class OpenTelemetryExtension
    {
        public static WebApplicationBuilder AppOpenTelemetryMetrics(this WebApplicationBuilder builder) 
        {
            builder.Services.AddOpenTelemetry().WithMetrics(metricsBuilder =>
            {
                metricsBuilder.AddHttpClientInstrumentation();
                metricsBuilder.AddAspNetCoreInstrumentation();
                metricsBuilder.AddPrometheusExporter(exp => { exp.ScrapeEndpointPath = "/metrics"; });
            });

            return builder;
        }
    }
}
