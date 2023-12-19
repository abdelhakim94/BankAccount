using ExaltIt.BankAccount.Core.AccountAggregate;
using ExaltIt.BankAccount.UseCases.Accounts.Specifications;
using Xunit;

namespace ExaltIt.BankAccount.UnitTests.UseCases.Accounts;

public class AccountByIdWithTransactionSpecificationsConstructor
{
  [Fact]
  public void Spec_CollectionWithAccount_ReturnsAccount()
  {
    // Act
    var accountId1 = Guid.NewGuid();
    var accountId2 = Guid.NewGuid();

    var account1 = new Account("FR6214508000506166136129K69") { Id = accountId1 };
    var account2 = new Account("FR6214508000506166136129K70") { Id = accountId2 };

    var accounts = new List<Account>() { account1, account2 };

    var spec = new AccountByIdWithTransactionsSpec(accountId1);

    var filteredList = spec.Evaluate(accounts);

    Assert.Contains(account1, filteredList);
    Assert.DoesNotContain(account2, filteredList);
  }
}
