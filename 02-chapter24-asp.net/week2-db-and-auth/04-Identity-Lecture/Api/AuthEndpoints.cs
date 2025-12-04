using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BlogApi.Dtos.Auth;
using BlogApi.Services;

namespace BlogApi.Endpoints;

public static class AuthEndpoints
{
  public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("/auth").WithTags("Auth");

    // POST /register
    group.MapPost("/register", async (RegisterRequestDto req, IAuthService authService) =>
    {
      var (success, errors) = await authService.RegisterAsync(req);
      return success ? TypedResults.Ok() : Results.BadRequest(errors);
    }).WithValidation<RegisterRequestDto>()
      .Produces(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status400BadRequest);

    // POST /login
    group.MapPost("/login", async (LoginRequestDto req, IAuthService authService) =>
    {
      var result = await authService.LoginAsync(req);
      return result is not null ? Results.Ok(result) : Results.Unauthorized();
    })
    .WithValidation<LoginRequestDto>()
    .Produces<AuthResponseDto>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status401Unauthorized);

    // GET /me
    group.MapGet("/me", [Authorize] async (ClaimsPrincipal user, IAuthService authService) =>
    {
      var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
      if (userId is null)
        return Results.Unauthorized();

      var currentUser = await authService.GetCurrentUserAsync(userId);
      return currentUser is not null ? Results.Ok(currentUser) : Results.NotFound();
    });
  }
}