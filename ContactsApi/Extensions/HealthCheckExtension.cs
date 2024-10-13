using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace ContactsApi.Extensions
{
    public static class HealthCheckExtension
    {
        public static WebApplicationBuilder AddHealthChecks(this WebApplicationBuilder builder) 
        {
            var a = builder.Configuration.GetConnectionString("NpgSql");

            builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("NpgSql"));

            return builder;
        }

        public static WebApplication AddHealthCheckMap(this WebApplication app) 
        {
            app.MapHealthChecks("/healthz", new HealthCheckOptions
            {
                ResponseWriter = async(context, report) => 
                {
                    context.Response.ContentType = "application/json";
                    var result = new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(e => new { name = e.Key, value = e.Value.Status.ToString(), desciption = e.Value.Description })
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                }
            });

            return app;
        }
    }
}
