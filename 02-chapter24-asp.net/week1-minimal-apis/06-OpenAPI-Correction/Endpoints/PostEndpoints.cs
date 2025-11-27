using BlogApi.Dtos.Posts;
using BlogApi.Filters;
using BlogApi.Services;

namespace BlogApi.Endpoints;

public static class PostEndpoints
{
  public static void MapPostEndpoints(this IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("/posts").WithTags("Posts");

    // GET /posts
    group.MapGet("/", async (IPostService postService) =>
    {
      var posts = await postService.ListAsync();
      var postDtos = posts.Select(p => new PostResponseDto(p.Id, p.UserId, p.Title, p.Content, p.PublishedAt));
      return TypedResults.Ok(postDtos);
    }).Produces<IEnumerable<PostResponseDto>>();

    // POST /posts
    group.MapPost("/", async (CreatePostDto createPostDto, IPostService postService, HttpContext context) =>
    {
      try
      {
        var post = await postService.CreateAsync(createPostDto.UserId, createPostDto.Title, createPostDto.Content);
        var postDto = new PostResponseDto(post.Id, post.UserId, post.Title, post.Content, post.PublishedAt);

        var location = $"{context.Request.Scheme}://{context.Request.Host}/posts/{post.Id}";
        return TypedResults.Created(location, postDto);
      }
      catch (ArgumentException)
      {
        return Results.Problem(detail: "Invalid User Id", statusCode: StatusCodes.Status400BadRequest);
      }
    }).WithValidation<CreatePostDto>()
    .Produces<PostResponseDto>()
    .ProducesProblem(StatusCodes.Status400BadRequest);

    // GET /posts/{id:guid}
    group.MapGet("/{id:guid}", async (Guid id, IPostService postService) =>
    {
      var post = await postService.GetAsync(id);
      if (post is null)
        return Results.Problem(detail: "Post not found", statusCode: StatusCodes.Status404NotFound);

      var postDto = new PostResponseDto(post.Id, post.UserId, post.Title, post.Content, post.PublishedAt);
      return TypedResults.Ok(postDto);
    })
    .Produces<PostResponseDto>()
    .ProducesProblem(StatusCodes.Status404NotFound);

    // PATCH /posts/{id:guid}
    group.MapPatch("/{id:guid}", async (Guid id, UpdatePostDto updatePostDto, IPostService postService) =>
    {
      var post = await postService.UpdateAsync(id, updatePostDto.Title, updatePostDto.Content);
      if (post is null)
        return Results.Problem(detail: "Post not found", statusCode: StatusCodes.Status404NotFound);

      var postDto = new PostResponseDto(post.Id, post.UserId, post.Title, post.Content, post.PublishedAt);
      return Results.Ok(postDto);
    }).WithValidation<UpdatePostDto>()
    .Produces<PostResponseDto>()
    .ProducesProblem(StatusCodes.Status404NotFound);

    // DELETE /posts/{id:guid}
    group.MapDelete("/{id:guid}", async (Guid id, IPostService postService) =>
    {
      var deleted = await postService.DeleteAsync(id);
      return deleted ? Results.NoContent() : Results.Problem(detail: "Post not found", statusCode: StatusCodes.Status404NotFound);
    }).Produces(StatusCodes.Status204NoContent)
    .ProducesProblem(StatusCodes.Status404NotFound);
  }
}