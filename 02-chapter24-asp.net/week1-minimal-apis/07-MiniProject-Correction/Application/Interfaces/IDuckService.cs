using DuckPondApi.Models;

namespace DuckPondApi.Application.Services;

public interface IDuckService
{
  Task<Duck?> GetAsync(Guid id);
  Task<IReadOnlyList<Duck>> ListAsync();
  Task<IReadOnlyList<Duck>> ListByUserAsync(Guid userId);
  Task<Duck> CreateAsync(Guid userId, string name, string quote, string image);
  Task<Duck?> UpdateAsync(Guid id, string? name, string? quote, string? image);
  Task<bool> DeleteAsync(Guid id);
}