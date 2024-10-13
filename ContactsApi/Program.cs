using ContactsApi;
using ContactsApi.Extensions;
using ContactsApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddHealthCheckMap();

app.MapControllers();

app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();
