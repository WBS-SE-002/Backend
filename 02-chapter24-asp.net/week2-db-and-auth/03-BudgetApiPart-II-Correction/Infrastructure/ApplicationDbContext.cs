using Microsoft.EntityFrameworkCore;
using BudgetApi.Models;

namespace BudgetApi.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
  public DbSet<Transaction> Transactions => Set<Transaction>();

}