using DuckPondApi.Dtos.Ducks;
using DuckPondApi.Dtos.Users;
using DuckPondApi.Application.Services;
using DuckPondApi.Application.Interfaces;
using DuckPondApi.Api.Filters;

namespace DuckPondApi.Endpoints;

public static class UserEndpoints
{
  public static void MapUserEndpoints(this IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("/users").WithTags("Users");

    // GET /users
    group.MapGet("/", async (IUserService userService) =>
    {
      var users = await userService.ListAsync();
      var userDtos = users.Select(u => new UserResponseDto(u.Id, u.Name, u.Email, u.CreatedAt));
      return TypedResults.Ok(userDtos);
    }).Produces<IEnumerable<UserResponseDto>>();

    // POST /users
    group.MapPost("/", async (CreateUserDto createUserDto, IUserService userService, HttpContext context) =>
    {
      var user = await userService.CreateAsync(createUserDto.Name, createUserDto.Email);
      var userDto = new UserResponseDto(user.Id, user.Name, user.Email, user.CreatedAt);

      var location = $"{context.Request.Scheme}://{context.Request.Host}/users/{user.Id}";
      return TypedResults.Created(location, userDto);
    }).WithValidation<CreateUserDto>()
     .Produces<UserResponseDto>(StatusCodes.Status201Created);

    // GET users/{id:guid}
    group.MapGet("/{id:guid}", async (Guid id, IUserService userService) =>
    {
      var user = await userService.GetAsync(id);

      if (user is null)
        return Results.Problem(detail: "User not found", statusCode: StatusCodes.Status404NotFound);

      var userDto = new UserResponseDto(user.Id, user.Name, user.Email, user.CreatedAt);

      return TypedResults.Ok(userDto);
    }).Produces<UserResponseDto>()
    .ProducesProblem(StatusCodes.Status404NotFound);

    // PATCH /users/{id:guid}
    group.MapPatch("/{id:guid}", async (Guid id, UpdateUserDto updateUserDto, IUserService userService) =>
    {
      var user = await userService.UpdateAsync(id, updateUserDto.Name, updateUserDto.Email);
      if (user is null)
        return Results.Problem(detail: "User not found", statusCode: StatusCodes.Status404NotFound);

      var userDto = new UserResponseDto(user.Id, user.Name, user.Email, user.CreatedAt);
      return TypedResults.Ok(userDto);
    }).WithValidation<UpdateUserDto>()
    .Produces<UserResponseDto>()
    .ProducesProblem(StatusCodes.Status404NotFound);

    // DELETE /users/{id:guid}
    group.MapDelete("/{id:guid}", async (Guid id, IUserService userService, IDuckService duckService) =>
    {
      var found = await userService.DeleteAsync(id);
      if (!found)
        return Results.Problem(detail: "User not found", statusCode: StatusCodes.Status404NotFound);

      var postsFromUser = await duckService.ListByUserAsync(id);

      foreach (var post in postsFromUser) await duckService.DeleteAsync(post.Id);

      return TypedResults.NoContent();
    })
    .Produces(StatusCodes.Status204NoContent)
    .ProducesProblem(StatusCodes.Status404NotFound);

    // GET /users/{id:guid}/posts
    group.MapGet("/{id:guid}/posts", async (Guid id, IUserService userService, IDuckService duckService) =>
    {
      var user = await userService.GetAsync(id);
      if (user is null)
        return Results.Problem(detail: "User not found", statusCode: StatusCodes.Status404NotFound);

      var posts = await duckService.ListByUserAsync(id);
      var postDtos = posts.Select(d => new DuckResponseDto(d.Id, d.UserId, d.Name, d.Quote, d.Image, d.CreatedAt));
      return TypedResults.Ok(postDtos);
    }).Produces<IEnumerable<DuckResponseDto>>()
    .ProducesProblem(StatusCodes.Status404NotFound);
  }
}