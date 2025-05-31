using System.Configuration;
using System.Text;
using API.Services;
using API.Services.AccountService;
using API.Services.AccountService.JWTService;
using API.Services.AssetService;
using Data;
using Infra;
using Infra.UnitOfWork;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InvestmentPortfolioDBContext>(options => options.UseNpgsql(ConnectionString.GetConnectionString()), ServiceLifetime.Scoped);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IUnitOfWork>(s => new UnitOfWork(
    s.GetService<InvestmentPortfolioDBContext>()
    ));

builder.Services.AddScoped<IUserService>(s => new UserService(
    s.GetService<InvestmentPortfolioDBContext>()
    ));
builder.Services.AddScoped<IAccountService>(s => new AccountService(
    s.GetService<InvestmentPortfolioDBContext>(),
    s.GetService<IJWTAuthService>()
));
builder.Services.AddScoped<IAssetService>(s => new AssetService(
    s.GetService<InvestmentPortfolioDBContext>()
));

var key = "my-super-secret-key-kpo-123456-123456!";
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.RequireHttpsMetadata = false;
    option.SaveToken = true;
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddScoped<IJWTAuthService, JWTAuthService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();

var options = new ApplicationInsightsServiceOptions();
builder.Services.AddApplicationInsightsTelemetry(options);
builder.Services.AddSingleton<TelemetryClient>();

builder.Services.AddResponseCompression(otps =>
{
    otps.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();  
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();  
}

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();
app.Run();        