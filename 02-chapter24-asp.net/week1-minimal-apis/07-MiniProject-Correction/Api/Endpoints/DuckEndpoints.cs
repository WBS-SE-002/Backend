using DuckPondApi.Dtos.Ducks;
using DuckPondApi.Dtos.Users;
using DuckPondApi.Application.Services;
using DuckPondApi.Application.Interfaces;
using DuckPondApi.Api.Filters;

namespace DuckPondApi.Api.Endpoints;

public static class DuckEndpoints
{
  public static void MapDuckEndpoints(this IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("/ducks").WithTags("Ducks");

    // GET /ducks
    group.MapGet("/", async (IDuckService duckService) =>
    {
      var ducks = await duckService.ListAsync();
      var duckDtos = ducks.Select(d => new DuckResponseDto(d.Id, d.UserId, d.Name, d.Quote, d.Image, d.CreatedAt));

      return TypedResults.Ok(duckDtos);
    }).Produces<IEnumerable<DuckResponseDto>>();

    // GET /ducks/{id:guid}
    group.MapGet("/{id:guid}", async (Guid id, IDuckService duckService) =>
    {
      var duck = await duckService.GetAsync(id);
      if (duck is null)
        return Results.Problem(detail: "Duck not found", statusCode: StatusCodes.Status404NotFound);

      var duckDto = new DuckResponseDto(duck.Id, duck.UserId, duck.Name, duck.Quote, duck.Image, duck.CreatedAt);
      return TypedResults.Ok(duckDto);
    })
    .Produces<DuckResponseDto>()
    .ProducesProblem(StatusCodes.Status404NotFound);

    // POST /ducks
    group.MapPost("/", async (CreateDuckDto createDuckDto, IDuckService duckService, HttpContext context) =>
    {
      try
      {
        var duck = await duckService.CreateAsync(createDuckDto.UserId, createDuckDto.Name, createDuckDto.Quote, createDuckDto.Image);
        var duckDto = new DuckResponseDto(duck.Id, duck.UserId, duck.Name, duck.Quote, duck.Image, duck.CreatedAt);

        var location = $"{context.Request.Scheme}://{context.Request.Host}/ducks/{duck.Id}";
        return TypedResults.Created(location, duckDto);
      }
      catch (ArgumentException)
      {
        return Results.Problem(detail: "Invalid User Id", statusCode: StatusCodes.Status400BadRequest);
      }
    })
    .Produces<DuckResponseDto>(StatusCodes.Status201Created)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .WithValidation<CreateDuckDto>();

    // PATCH /ducks/{id:guid}
    group.MapPatch("/{id:guid}", async (Guid id, UpdateDuckDto updateDuckDto, IDuckService duckService) =>
    {
      var duck = await duckService.UpdateAsync(id, updateDuckDto.Name, updateDuckDto.Quote, updateDuckDto.Image);
      if (duck is null)
        return Results.Problem(detail: "Duck not found", statusCode: StatusCodes.Status404NotFound);

      var duckDto = new DuckResponseDto(duck.Id, duck.UserId, duck.Name, duck.Quote, duck.Image, duck.CreatedAt);
      return TypedResults.Ok(duckDto);
    })
    .WithValidation<UpdateDuckDto>()
    .Produces<DuckResponseDto>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status404NotFound);

    // DELETE /ducks/{id:guid}
    group.MapDelete("/{id:guid}", async (Guid id, IDuckService duckService) =>
    {
      var deleted = await duckService.DeleteAsync(id);
      return deleted ? Results.NoContent() : Results.Problem(detail: "Duck not found", statusCode: StatusCodes.Status404NotFound);
    })
    .Produces(StatusCodes.Status204NoContent)
    .ProducesProblem(StatusCodes.Status404NotFound);
  }



}