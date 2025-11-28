using DuckPondApi.Application.Interfaces;
using DuckPondApi.Models;

namespace DuckPondApi.Application.Services;

public class InMemoryDuckService(IUserService userService) : IDuckService
{
  private readonly Dictionary<Guid, Duck> _ducks = [];
  private readonly IUserService _userService = userService;

  public async Task<Duck?> GetAsync(Guid id)
  {
    _ducks.TryGetValue(id, out Duck? duck);

    return duck;
  }
  public async Task<IReadOnlyList<Duck>> ListAsync()
  {
    return _ducks.Values.ToList();
  }

  public async Task<IReadOnlyList<Duck>> ListByUserAsync(Guid userId)
  {
    var userPosts = _ducks.Values.Where(p => p.UserId == userId).ToList();
    return userPosts;
  }

  public async Task<Duck> CreateAsync(Guid userId, string name, string quote, string image)
  {
    var user = await _userService.GetAsync(userId);

    if (user is null) throw new ArgumentException("User not found", nameof(userId));

    var duck = new Duck
    {
      Id = Guid.NewGuid(),
      UserId = userId,
      Name = name,
      Quote = quote,
      Image = image,
      CreatedAt = DateTimeOffset.UtcNow
    };

    _ducks[duck.Id] = duck;

    return duck;
  }

  public async Task<Duck?> UpdateAsync(Guid id, string? name, string? quote, string? image)
  {
    if (!_ducks.TryGetValue(id, out var duck)) return null;

    if (name is not null) duck.Name = name;
    if (quote is not null) duck.Quote = quote;
    if (image is not null) duck.Image = image;

    return duck;
  }

  public async Task<bool> DeleteAsync(Guid id)
  {
    return _ducks.Remove(id);
  }
}
