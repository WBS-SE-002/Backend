using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TravelApi.Endpoints;
using TravelApi.Services;
using TravelApi.Infrastructure;
using TravelApi.Models;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var envKey = Environment.GetEnvironmentVariable("KEY");
if (string.IsNullOrWhiteSpace(envKey))
{
    throw new InvalidOperationException("JWT key is missing. Set 'Key' in your .env file.");
}
var envIssuer = Environment.GetEnvironmentVariable("ISSUER");
var envAudience = Environment.GetEnvironmentVariable("AUDIENCE");
var envExpiry = Environment.GetEnvironmentVariable("EXPIRY_MINUTES");
var envClientUrl = Environment.GetEnvironmentVariable("CLIENT_URL");

if (string.IsNullOrWhiteSpace(envClientUrl))
{
    throw new InvalidOperationException("CLIENT_URL is missing. Set 'CLIENT_URL' in your .env file.");
}

var jwtConfigItems = new List<KeyValuePair<string, string?>>();
if (!string.IsNullOrWhiteSpace(envKey)) jwtConfigItems.Add(new KeyValuePair<string, string?>("Jwt:Key", envKey));
if (!string.IsNullOrWhiteSpace(envIssuer)) jwtConfigItems.Add(new KeyValuePair<string, string?>("Jwt:Issuer", envIssuer));
if (!string.IsNullOrWhiteSpace(envAudience)) jwtConfigItems.Add(new KeyValuePair<string, string?>("Jwt:Audience", envAudience));
if (!string.IsNullOrWhiteSpace(envExpiry)) jwtConfigItems.Add(new KeyValuePair<string, string?>("Jwt:ExpiryMinutes", envExpiry));
builder.Configuration.AddInMemoryCollection(jwtConfigItems);

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(envClientUrl!)
              .AllowAnyHeader()
              .AllowAnyMethod();

    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

//cors has to go before auth
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapAuthEndpoints();
app.MapUserEndpoints();
app.MapPostEndpoints();

app.Run();