using Ardalis.Result;
using Ardalis.SharedKernel;
using ExaltIt.BankAccount.Core.AccountAggregate;
using ExaltIt.BankAccount.UseCases.Accounts.Specifications;
using ExaltIt.BankAccount.UseCases.Accounts.AddTransaction;
using NSubstitute;
using Xunit;

namespace ExaltIt.BankAccount.UnitTests.UseCases.Accounts;

public class AddTransactionHandlerHandle
{
  [Fact]
  public async Task Handle_ValidRequest_ReturnsSuccessResult()
  {
    // Arrange
    var accountId = Guid.NewGuid();
    var addTransactionCommand = new AddTransactionCommand(accountId, TransactionType.Deposit, 10);

    var repository = Substitute.For<IRepository<Account>>();
    var handler = new AddTransactionHandler(repository);

    var account = new Account(accountId.ToString());
    repository.FirstOrDefaultAsync(Arg.Any<AccountByIdWithTransactionsSpec>(), Arg.Any<CancellationToken>())
        .Returns(Task.FromResult<Account?>(account));

    // Act
    var result = await handler.Handle(addTransactionCommand, CancellationToken.None);

    // Assert
    Assert.True(result.Status == ResultStatus.Ok);
  }

  [Fact]
  public async Task Handle_AccountNotFound_ReturnsNotFoundResult()
  {
    // Arrange
    var accountId = Guid.NewGuid();
    var addTransactionCommand = new AddTransactionCommand(accountId, TransactionType.Deposit, 10);

    var repository = Substitute.For<IRepository<Account>>();
    var handler = new AddTransactionHandler(repository);

    repository.FirstOrDefaultAsync(Arg.Any<AccountByIdWithTransactionsSpec>(), Arg.Any<CancellationToken>())
        .Returns(Task.FromResult<Account?>(null));

    // Act
    var result = await handler.Handle(addTransactionCommand, CancellationToken.None);

    // Assert
    Assert.True(result.Status == ResultStatus.NotFound);
  }
}
