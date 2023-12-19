using Ardalis.Result;
using Ardalis.SharedKernel;
using ExaltIt.BankAccount.Core.AccountAggregate;
using ExaltIt.BankAccount.UseCases.Accounts.Get;
using ExaltIt.BankAccount.UseCases.Accounts.Specifications;
using NSubstitute;
using Xunit;

namespace ExaltIt.BankAccount.UnitTests.UseCases.Accounts;

public class GetAccountHandlerHandle
{
  [Fact]
  public async Task Handle_ValidGetAccountQuery_ReturnsSuccessResultWithAccountDTO()
  {
    // Arrange
    var accountId = Guid.NewGuid();
    var accountNumber = "FR6214508000506166136129K69";
    var getAccountQuery = new GetAccountQuery(accountId);

    var repository = Substitute.For<IReadRepository<Account>>();
    var handler = new GetAccountHandler(repository);

    var account = new Account(accountNumber) { Id = accountId };
    repository.FirstOrDefaultAsync(Arg.Any<AccountByIdWithTransactionsSpec>(), Arg.Any<CancellationToken>())
        .Returns(Task.FromResult<Account?>(account));

    // Act
    var result = await handler.Handle(getAccountQuery, CancellationToken.None);

    // Assert
    Assert.True(result.Status == ResultStatus.Ok);
    Assert.NotNull(result.Value);
    Assert.Equal(accountId, result.Value.Id);
    Assert.Equal(accountNumber, result.Value.Number);
    Assert.Equal(0, result.Value.Balance);
  }

  [Fact]
  public async Task Handle_AccountNotFound_ReturnsNotFoundResult()
  {
    // Arrange
    var accountId = Guid.NewGuid();
    var getAccountQuery = new GetAccountQuery(accountId);

    var repository = Substitute.For<IReadRepository<Account>>();
    var handler = new GetAccountHandler(repository);

    repository.FirstOrDefaultAsync(Arg.Any<AccountByIdWithTransactionsSpec>(), Arg.Any<CancellationToken>())
        .Returns(Task.FromResult<Account?>(null));

    // Act
    var result = await handler.Handle(getAccountQuery, CancellationToken.None);

    // Assert
    Assert.True(result.Status == ResultStatus.NotFound);
  }
}
