using BlogApi.Models;

namespace BlogApi.Services;

public class InMemoryUserService : IUserService
{
  private readonly Dictionary<Guid, User> _users = new();

  // public Task<User?> GetAsync(Guid id)
  // {
  //   _users.TryGetValue(id, out var user);

  //   return Task.FromResult(user);
  // }
  async public Task<User?> GetAsync(Guid id)
  {
    _users.TryGetValue(id, out var user);

    return user;
  }

  async public Task<IReadOnlyList<User>> ListAsync()
  {
    return _users.Values.ToList();
  }

  async public Task<User> CreateAsync(string name, string email)
  {
    var user = new User
    {
      Id = Guid.NewGuid(),
      Name = name,
      Email = email,
      CreatedAt = DateTimeOffset.UtcNow
    };

    _users[user.Id] = user;

    return user;
  }

  async public Task<User?> UpdateAsync(Guid id, string? name, string? email)
  {
    if (!_users.TryGetValue(id, out var user)) return null;

    if (name is not null) user.Name = name;
    if (email is not null) user.Email = email;

    return user;

  }

  async public Task<bool> DeleteAsync(Guid id)
  {
    return _users.Remove(id);
  }


}