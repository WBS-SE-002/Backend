using MinimalApis.Dtos;
using MinimalApis.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TimeService>();

var app = builder.Build();

var appName = builder.Configuration["AppName"] ?? "Default App";
var greeting = builder.Configuration["Greeting"] ?? "Hi";

app.Use(async (context, next) =>
{
  Console.WriteLine($"Handling request: {context.Request.Path}");
  await next();
  Console.WriteLine($"Finished handling request.");
});

app.Use(async (context, next) =>
{
  if (context.Request.Path == "/forbidden")
  {
    context.Response.StatusCode = 403;
    await context.Response.WriteAsync("Forbidden");
  }
  else
  {
    await next();
  }
});

app.MapGet("/", () => "Hello World!");
app.MapGet("/config", () => $"{appName} says: {greeting}");
app.MapGet("/time", (TimeService ts) => ts.Now());

// Users endpoints
app.MapUsers();
// app.MapGet("/users", () => new[] { "Alice", "Bob" });
// app.MapGet("/users/{id:int}", (int id) => $"User {id}");
// app.MapGet("/users/profile", () => $"The whole user profile");
// // app.MapPost("/users", (UserRequestDto user) => $"Created user {user.Name}");
// app.MapPost("/users", (UserRequestDto user) =>
// {
//   return Results.Ok($"Welcome, {user.Name}!");
// });


// Posts endpoints
app.MapGet("/posts", () => new[] { "Post 1", "Post 2" });
app.MapGet("/posts/{id:int}", (int id) => $"Post {id}");
app.MapPost("/posts", (PostRequestDto post) => $"Created post {post.Title}");
app.MapGet("/posts/search", (string term) => $"You searched for posts with: {term}");

app.Run();


public class TimeService
{
  public string Now() => DateTime.Now.ToString("T");
}

// DTOs/UserRequestDto.cs
// record UserRequestDto(string Name);
// DTOs/PostRequestDto.cs
record PostRequestDto(string Title);