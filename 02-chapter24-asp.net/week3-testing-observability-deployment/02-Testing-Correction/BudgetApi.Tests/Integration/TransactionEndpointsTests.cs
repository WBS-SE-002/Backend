// BudgetApi.Tests/Integration/UserEndpointsTests.cs
using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using BudgetApi.Infrastructure;
using BudgetApi.Dtos.Transactions;
using BudgetApi.Models;

namespace BudgetApi.Tests.Integration;

public class TransactionEndpointsTests : IClassFixture<TestHost>
{
  private readonly HttpClient _client;

  public TransactionEndpointsTests(TestHost factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task POST_transactions_Returns401Error_WhenHeadersMissing()
  {

  }
  [Fact]
  public async Task POST_transactions_ReturnsTransaction_WithValidHeaders()
  {

  }


}