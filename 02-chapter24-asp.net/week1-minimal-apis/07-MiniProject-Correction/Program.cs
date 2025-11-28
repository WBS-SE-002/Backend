using Scalar.AspNetCore;
using DuckPondApi.Application.Services;
using DuckPondApi.Api.Endpoints;
using DuckPondApi.Application.Interfaces;
using DuckPondApi.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUserService, InMemoryUserService>();
builder.Services.AddSingleton<IDuckService, InMemoryDuckService>();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.MapScalarApiReference();
}

app.UseExceptionHandler();
app.UseStatusCodePages();

app.MapUserEndpoints();
app.MapDuckEndpoints();

app.Run();
