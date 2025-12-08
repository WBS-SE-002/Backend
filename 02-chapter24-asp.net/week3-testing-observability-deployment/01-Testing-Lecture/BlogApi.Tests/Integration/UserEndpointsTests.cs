using System.Net;
using System.Net.Http.Json;
using BlogApi.Dtos.Users;

namespace BlogApi.Tests.Integration;

public class UserEndpointsTests : IClassFixture<TestHost>
{
  private readonly HttpClient _client;

  public UserEndpointsTests(TestHost factory)
  {
    _client = factory.CreateClient(); // Regular client for public endpoints
  }

  [Fact]
  public async Task GET_users_ReturnsEmptyList_WhenNoUsersExist()
  {
    // Act - Make HTTP request to public endpoint
    var response = await _client.GetAsync("/users");

    // Assert - Check response
    response.EnsureSuccessStatusCode();
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    // Verify response content
    var users = await response.Content.ReadFromJsonAsync<List<UserResponseDto>>();
    Assert.NotNull(users);
    Assert.Empty(users);
  }

  [Fact]
  public async Task GET_users_ReturnsUserList_WhenUsersExist()
  {
    // Arrange - Seed test data using HTTP requests (ensures same database)
    await SeedTestUser("Alice Johnson", "alice@unique1.com");
    await SeedTestUser("Bob Smith", "bob@unique2.com");

    // Act
    var response = await _client.GetAsync("/users");

    // Assert
    response.EnsureSuccessStatusCode();

    var users = await response.Content.ReadFromJsonAsync<List<UserResponseDto>>();
    Assert.NotNull(users);
    Assert.True(users!.Count >= 2); // May have data from other tests (shared DB)

    // Verify our test users are present
    Assert.Contains(users, u => u.Email == "alice@unique1.com");
    Assert.Contains(users, u => u.Email == "bob@unique2.com");
  }

  /// <summary>
  /// Helper method to seed test data by making HTTP requests (ensures same database)
  /// </summary>
  private async Task SeedTestUser(string name, string email)
  {
    var createUserDto = new CreateUserDto(name, email);
    var response = await _client.PostAsJsonAsync("/users", createUserDto);
    response.EnsureSuccessStatusCode(); // Ensure the user was created successfully
  }
}