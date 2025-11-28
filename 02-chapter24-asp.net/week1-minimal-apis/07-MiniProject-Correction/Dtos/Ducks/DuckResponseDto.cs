namespace DuckPondApi.Dtos.Ducks;

public record DuckResponseDto(
  Guid Id,
  Guid UserId,
  string Name,
  string Quote,
  string Image,
  DateTimeOffset CreatedAt
);
