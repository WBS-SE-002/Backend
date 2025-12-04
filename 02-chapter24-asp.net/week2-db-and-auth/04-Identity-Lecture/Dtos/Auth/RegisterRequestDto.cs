using System.ComponentModel.DataAnnotations;

namespace BlogApi.Dtos.Auth;

public class RegisterRequestDto
{
  [Required, StringLength(100, MinimumLength = 1)]
  public string Name { get; set; } = string.Empty;

  [Required, EmailAddress]
  public string Email { get; set; } = string.Empty;

  [Required, MinLength(6)]
  public string Password { get; set; } = string.Empty;
}