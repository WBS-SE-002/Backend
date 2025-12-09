using Microsoft.EntityFrameworkCore;
using BudgetApi.Infrastructure;
using BudgetApi.Application.Services;
using BudgetApi.Models;
using BudgetApi.Dtos.Transactions;

namespace BudgetApi.Tests.Unit;

public class TransactionServiceTests
{
  // Helper method to create a fresh in-memory database for each test
  private static ApplicationDbContext CreateInMemoryDb()
  {
    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique DB per test
        .Options;
    return new ApplicationDbContext(options);
  }

  [Fact]
  public async Task CreateAsync_ValidUser_CreatesTransactionSuccessfully()
  {

  }
}