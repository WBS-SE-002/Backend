using System.ComponentModel.DataAnnotations;

namespace BudgetApi.Dtos.Auth;

public record RegisterRequestDto(
  [Required, StringLength(100, MinimumLength = 1)]
   string Name,

  [Required, EmailAddress]
   string Email,

  [Required, MinLength(6)]
   string Password
);