using System.Text;
using CarStockApi.Auth;
using CarStockApi.Database;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
// Register the db connection class with .NET's Dependency Injection system
builder.Services.AddSingleton<IDbConnectionFactory, DatabaseConnectionFactory>();

// Register the Token service with .NET's DI system
builder.Services.AddSingleton<ITokenService, TokenService>();

// Check secretKey for authenticating the token
var jwtSecretKey = builder.Configuration["Jwt:SecretKey"]
    ?? throw new InvalidOperationException("JWT SecretKey is not configured.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidIssuer = builder.Configuration["Jwt:Issuer"],

           ValidateAudience = true,
           ValidAudience = builder.Configuration["Jwt:Audience"],

           ValidateLifetime = true,

           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
       };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();

app.Run();