
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;


using BudgetApi.Api.Endpoints;
using BudgetApi.Application.Services;
using BudgetApi.Application.Interfaces;
using BudgetApi.Infrastructure;
using BudgetApi.Infrastructure.Data;
using BudgetApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
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
builder.Services.AddScoped<DbSeeder>();

builder.Services.AddScoped<ITransactionService, TransactionServiceEf>();
builder.Services.AddScoped<IReportService, ReportServiceEf>();
builder.Services.AddScoped<IAuthService, AuthServiceEf>();

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (builder.Configuration.GetValue("ApplyMigrations", true))
{
  using var scope = app.Services.CreateScope();
  var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
  await db.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.MapScalarApiReference();
  using (var scope = app.Services.CreateScope())
  {
    var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
    await seeder.SeedAsync();
  }
}

app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseAuthentication();
app.UseAuthorization();

app.MapAuthEndpoints();
app.MapReportEndpoints();
app.MapTransactionEndpoints();

app.MapGet("/db-check", async (ApplicationDbContext db) =>
{
  try
  {
    var result = await db.Database.SqlQueryRaw<DateTime>("SELECT GETDATE() as Value").FirstAsync();
    return Results.Ok($"DB connection OK. Server time: {result}");
  }
  catch (Exception ex)
  {
    return Results.Problem($"DB connection failed: {ex.Message}");
  }
});

app.Run();
