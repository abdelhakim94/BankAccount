using ExaltIt.BankAccount.Core.AccountAggregate.Events;
using ExaltIt.BankAccount.UseCases.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ExaltIt.BankAccount.UseCases.Accounts.EventHandlers;

public class TransactionAddedHandler(ITransactionNotificationService service,
  ILogger<TransactionAddedHandler> logger) : INotificationHandler<TransactionAddedEvent>
{
  private readonly ITransactionNotificationService _notificationService = service;
  private readonly ILogger<TransactionAddedHandler> _logger = logger;

  public async Task Handle(TransactionAddedEvent domainEvent, CancellationToken cancellationToken)
  {
    _logger.LogInformation("Notifiying the account holder of the transaction {0}", domainEvent.Transaction.Id);
    await _notificationService.NotifyTransaction();
  }
}
