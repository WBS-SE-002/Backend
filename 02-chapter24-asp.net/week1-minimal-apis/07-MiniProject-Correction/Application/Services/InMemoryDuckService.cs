using DuckPondApi.Models;
using DuckPondApi.Application.Interfaces;

namespace DuckPondApi.Application.Services;

public class InMemoryDuckService : IDuckService
{
    private readonly Dictionary<Guid, Duck> _ducks = new();
    private readonly IUserService _userService;

    public InMemoryDuckService(IUserService userService)
    {
        _userService = userService;
    }

    public Task<Duck?> GetAsync(Guid id)
    {
        _ducks.TryGetValue(id, out var duck);
        return Task.FromResult(duck);
    }

    public Task<IReadOnlyList<Duck>> ListAsync()
    {
        return Task.FromResult<IReadOnlyList<Duck>>(_ducks.Values.ToList());
    }

    public Task<IReadOnlyList<Duck>> ListByUserAsync(Guid userId)
    {
        var userDucks = _ducks.Values.Where(p => p.UserId == userId).ToList();
        return Task.FromResult<IReadOnlyList<Duck>>(userDucks);
    }

    public async Task<Duck> CreateAsync(Guid userId, string name, string quote, string image)
    {
        // Validate that the user exists
        var user = await _userService.GetAsync(userId);
        if (user is null)
            throw new ArgumentException("User not found", nameof(userId));

        var duck = new Duck
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = name,
            Quote = quote,
            Image = image,
            PublishedAt = DateTimeOffset.UtcNow
        };

        _ducks[duck.Id] = duck;
        return duck;
    }

    public Task<Duck?> UpdateAsync(Guid id, string? name, string? quote, string? image)
    {
        if (!_ducks.TryGetValue(id, out var duck))
            return Task.FromResult<Duck?>(null);

        if (name is not null)
            duck.Name = name;

        if (quote is not null)
            duck.Quote = quote;

        if (image is not null)
            duck.Image = image;

        return Task.FromResult<Duck?>(duck);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        return Task.FromResult(_ducks.Remove(id));
    }
}