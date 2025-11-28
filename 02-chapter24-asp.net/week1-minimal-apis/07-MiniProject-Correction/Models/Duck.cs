namespace DuckPondApi.Models;

public class Duck
{
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public string Name { get; set; } = string.Empty;
  public string Quote { get; set; } = string.Empty;
  public string Image { get; set; } = string.Empty;

  public DateTimeOffset CreatedAt { get; set; }

}