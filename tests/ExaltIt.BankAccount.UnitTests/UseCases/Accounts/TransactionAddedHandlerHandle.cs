using ExaltIt.BankAccount.Core.AccountAggregate;
using NSubstitute;
using Xunit;
using ExaltIt.BankAccount.Core.AccountAggregate.Events;
using ExaltIt.BankAccount.UseCases.Interfaces;
using Microsoft.Extensions.Logging;
using ExaltIt.BankAccount.UseCases.Accounts.EventHandlers;

namespace ExaltIt.BankAccount.UnitTests.UseCases.Accounts;

public class TransactionAddedHandlerHandle
{
  [Fact]
  public async Task Handle_ValidTransactionAddedEvent_CallsNotificationServiceAndLogsInformation()
  {
    // Arrange
    var transaction = new Transaction(TransactionType.Deposit, 100);
    var transactionAddedEvent = new TransactionAddedEvent(transaction);

    var notificationService = Substitute.For<ITransactionNotificationService>();
    var logger = Substitute.For<ILogger<TransactionAddedHandler>>();

    var handler = new TransactionAddedHandler(notificationService, logger);

    // Act
    await handler.Handle(transactionAddedEvent, CancellationToken.None);

    // Assert
    await notificationService.Received(1).NotifyTransaction();
  }
}
