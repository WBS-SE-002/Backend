using BlogApi.Models;

namespace BlogApi.Services;

public class InMemoryPostService(IUserService userService) : IPostService
{
  private readonly Dictionary<Guid, Post> _posts = new();
  private readonly IUserService _userService = userService;

  async public Task<Post?> GetAsync(Guid id)
  {
    _posts.TryGetValue(id, out Post? post);

    return post;
  }

  async public Task<IReadOnlyList<Post>> ListAsync()
  {
    return _posts.Values.ToList();
  }

  async public Task<IReadOnlyList<Post>> ListByUserAsync(Guid userId)
  {
    var userPosts = _posts.Values.Where(p => p.UserId == userId).ToList();
    return userPosts;
  }

  async public Task<Post> CreateAsync(Guid userId, string title, string content)
  {
    var user = await _userService.GetAsync(userId);

    if (user is null) throw new ArgumentException("User not found", nameof(userId));

    var post = new Post
    {
      Id = Guid.NewGuid(),
      UserId = userId,
      Title = title,
      Content = content,
      PublishedAt = DateTimeOffset.UtcNow
    };

    _posts[post.Id] = post;

    return post;
  }

  async public Task<Post?> UpdateAsync(Guid id, string? title, string? content)
  {
    if (!_posts.TryGetValue(id, out var post)) return null;

    if (title is not null) post.Title = title;
    if (content is not null) post.Content = content;

    return post;
  }

  async public Task<bool> DeleteAsync(Guid id)
  {
    return _posts.Remove(id);
  }
}