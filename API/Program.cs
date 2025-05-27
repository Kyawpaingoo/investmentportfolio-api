using System.Configuration;
using API.Services;
using Data;
using Infra;
using Infra.UnitOfWork;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InvestmentPortfolioDBContext>(options => options.UseNpgsql(ConnectionString.GetConnectionString()), ServiceLifetime.Scoped);
builder.Services.AddHttpClient();
builder.Services.AddScoped<IUnitOfWork>(s => new UnitOfWork(
    s.GetService<InvestmentPortfolioDBContext>()
    ));
builder.Services.AddScoped<IUserService>(s => new UserService(
    s.GetService<InvestmentPortfolioDBContext>()
    ));
var options = new ApplicationInsightsServiceOptions();
builder.Services.AddApplicationInsightsTelemetry(options);
builder.Services.AddSingleton<TelemetryClient>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddResponseCompression(otps =>
{
    otps.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true)
        .AllowCredentials());
app.UseAuthorization();
app.MapControllers();
app.Run();        