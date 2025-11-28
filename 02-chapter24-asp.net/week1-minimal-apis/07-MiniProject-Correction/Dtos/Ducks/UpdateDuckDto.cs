using System.ComponentModel.DataAnnotations;

namespace DuckPondApi.Dtos.Ducks;

public record UpdateDuckDto(

  [property: StringLength(255, MinimumLength = 1)]
  string? Name,


  [property: StringLength(510, MinimumLength = 1)]
  string? Quote,


  [property: StringLength(1_000, MinimumLength = 1)]
  string? Image
);