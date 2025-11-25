using BlogApi.Dtos.Posts;
using BlogApi.Dtos.Users;
using BlogApi.Services;

namespace BlogApi.Endpoints;

public static class UserEndpoints
{
  public static void MapUserEndpoints(this IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("/users");

    // GET /users
    group.MapGet("/", async (IUserService userService) =>
    {
      var users = await userService.ListAsync();
      var userDtos = users.Select(u => new UserResponseDto(u.Id, u.Name, u.Email, u.CreatedAt));
      return Results.Ok(userDtos);
    });

    // POST /users
    group.MapPost("/", async (CreateUserDto createUserDto, IUserService userService, HttpContext context) =>
    {
      var user = await userService.CreateAsync(createUserDto.Name, createUserDto.Email);
      var userDto = new UserResponseDto(user.Id, user.Name, user.Email, user.CreatedAt);

      var location = $"{context.Request.Scheme}://{context.Request.Host}/users/{user.Id}";
      return Results.Created(location, userDto);
    });

    // GET users/{id:guid}
    group.MapGet("/{id:guid}", async (Guid id, IUserService userService) =>
    {
      var user = await userService.GetAsync(id);

      if (user is null) return Results.NotFound();

      var userDto = new UserResponseDto(user.Id, user.Name, user.Email, user.CreatedAt);

      return Results.Ok(userDto);
    });

    // PATCH /users/{id:guid}
    group.MapPatch("/{id:guid}", async (Guid id, UpdateUserDto updateUserDto, IUserService userService) =>
    {
      var user = await userService.UpdateAsync(id, updateUserDto.Name, updateUserDto.Email);
      if (user is null)
        return Results.NotFound();

      var userDto = new UserResponseDto(user.Id, user.Name, user.Email, user.CreatedAt);
      return Results.Ok(userDto);
    });

    // DELETE /users/{id:guid}
    group.MapDelete("/{id:guid}", async (Guid id, IUserService userService, IPostService postService) =>
    {
      var found = await userService.DeleteAsync(id);
      if (!found)
        return Results.NotFound();

      var postsFromUser = await postService.ListByUserAsync(id);

      foreach (var post in postsFromUser) await postService.DeleteAsync(post.Id);

      return Results.NoContent();

    });

    // GET /users/{id:guid}/posts
    group.MapGet("/{id:guid}/posts", async (Guid id, IUserService userService, IPostService postService) =>
    {
      var user = await userService.GetAsync(id);
      if (user is null)
        return Results.NotFound();

      var posts = await postService.ListByUserAsync(id);
      var postDtos = posts.Select(p => new PostResponseDto(p.Id, p.UserId, p.Title, p.Content, p.PublishedAt));
      return Results.Ok(postDtos);
    });
  }
}