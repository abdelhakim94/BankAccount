using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ExaltIt.BankAccount.Core.AccountAggregate;

namespace ExaltIt.BankAccount.Infrastructure.Data;

public static class SeedData
{
  public static readonly Account Account1 = new("FR6214508000506166136129K69")
  {
    Id = Guid.Parse("08c65b2e-ef89-45b0-a78c-f91950a2531a")
  };
  public static readonly Account Account2 = new("FR4612739000705575819336T69")
  {
    Id = Guid.Parse("270b5d07-06cb-4cfb-accb-3d94cf9d9b01")
  };

  public static void Initialize(IServiceProvider serviceProvider)
  {
    using var dbContext = new AppDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null);
    // Look for any accounts.
    if (dbContext.Accounts.Any())
    {
      return;   // DB has been seeded
    }

    PopulateTestData(dbContext);
  }

  public static void PopulateTestData(AppDbContext dbContext)
  {
    foreach (var item in dbContext.Accounts)
    {
      dbContext.Remove(item);
    }
    dbContext.SaveChanges();

    dbContext.Accounts.Add(Account1);
    dbContext.Accounts.Add(Account2);

    dbContext.SaveChanges();
  }
}
