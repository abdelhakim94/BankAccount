using Ardalis.Result;
using Ardalis.SharedKernel;
using ExaltIt.BankAccount.Core.AccountAggregate;
using ExaltIt.BankAccount.UseCases.Accounts.List;
using ExaltIt.BankAccount.UseCases.Accounts.Specifications;
using NSubstitute;
using Xunit;

namespace ExaltIt.BankAccount.UnitTests.UseCases.Accounts;

public class ListAccountsHandlerHandle
{
  [Fact]
  public async Task Handle_ValidListAccountsQuery_ReturnsSuccessResultWithAccountDTOs()
  {
    // Arrange
    var repository = Substitute.For<IReadRepository<Account>>();
    var handler = new ListAccountsHandler(repository);

    var accounts = new List<Account>
    {
      new("FR6214508000506166136129K69"),
      new("FR6214508000506166136129K70"),
    };

    repository.ListAsync(Arg.Any<AccountsWithTransactionsSpec>(), Arg.Any<CancellationToken>())
        .Returns(Task.FromResult(accounts));

    var listAccountsQuery = new ListAccountsQuery();

    // Act
    var result = await handler.Handle(listAccountsQuery, CancellationToken.None);

    // Assert
    Assert.True(result.Status == ResultStatus.Ok);
    Assert.NotNull(result.Value);
    Assert.Equal(accounts.Count, result.Value.Count());

    foreach (var accountDto in result.Value)
    {
      Assert.Contains(accounts, a => a.Id == accountDto.Id && a.Number == accountDto.Number && a.Balance == accountDto.Balance);
    }
  }
}
