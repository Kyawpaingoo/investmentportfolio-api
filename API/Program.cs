using System.Configuration;
using Data;
using Infra;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InvestmentPortfolioDBContext>(options => options.UseNpgsql(ConnectionString.GetConnectionString()), ServiceLifetime.Scoped);
builder.Services.AddHttpClient();

var options = new ApplicationInsightsServiceOptions();
builder.Services.AddApplicationInsightsTelemetry(options);
builder.Services.AddSingleton<TelemetryClient>();

builder.Services.AddControllers();
builder.Services.AddResponseCompression(otps =>
{
    otps.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors(x => 
    x.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true)
        .AllowCredentials());
app.UseAuthentication();
app.MapControllers();
app.Run();        