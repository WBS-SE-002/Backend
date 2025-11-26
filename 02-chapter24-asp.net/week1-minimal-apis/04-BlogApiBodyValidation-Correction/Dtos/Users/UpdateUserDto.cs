using System.ComponentModel.DataAnnotations;

namespace BlogApi.Dtos.Users;

public record UpdateUserDto(
  [property: StringLength(255, MinimumLength = 1)]
  string? Name,
  [property: EmailAddress]
  string? Email);