using ExaltIt.BankAccount.Core.AccountAggregate;
using Xunit;

namespace ExaltIt.BankAccount.IntegrationTests.Data;

public class EfRepositoryAdd : BaseEfRepoTestFixture
{
  [Fact]
  public async Task Add_ValidAccount_AddsAccount()
  {
    // Arrange
    var accountNumber = "FR6214508000506166136129K69";
    var account = new Account(accountNumber);
    var repository = GetRepository();

    // Act
    await repository.AddAsync(account);

    // Assert
    var newAccount = (await repository.ListAsync()).First();

    Assert.NotEqual(Guid.Empty, newAccount.Id);
    Assert.Equal(accountNumber, newAccount.Number);
  }

  // Above is an example of how to test a repository's method.
  // Ideally I would test all the other methods...
}
