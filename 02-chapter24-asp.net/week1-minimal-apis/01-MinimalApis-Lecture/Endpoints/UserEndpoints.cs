using MinimalApis.Dtos;

namespace MinimalApis.Endpoints;

public static class UserEndpoints
{
  public static RouteGroupBuilder MapUsers(this IEndpointRouteBuilder routes)
  {
    var group = routes.MapGroup("/users");
    group.MapGet("/", () => new[] { "Alice", "Bob" });
    group.MapPost("/", (UserRequestDto user) => Results.Ok(user));
    return group;
  }
}