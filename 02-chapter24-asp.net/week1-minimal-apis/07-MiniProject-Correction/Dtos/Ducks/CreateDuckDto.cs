using System.ComponentModel.DataAnnotations;

namespace DuckPondApi.Dtos.Ducks;

public record CreateDuckDto(
  [property: Required]
  Guid UserId,

  [property: Required]
  [property: StringLength(255, MinimumLength = 1)]
  string Name,

  [property: Required]
  [property: StringLength(510, MinimumLength = 1)]
  string Quote,

  [property: Required]
  [property: StringLength(1_000, MinimumLength = 1)]
  string Image
);