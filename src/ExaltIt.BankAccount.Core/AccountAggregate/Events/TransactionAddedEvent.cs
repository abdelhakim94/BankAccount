using Ardalis.SharedKernel;

namespace ExaltIt.BankAccount.Core.AccountAggregate.Events;

public sealed class TransactionAddedEvent(Transaction transaction) : DomainEventBase
{
  public Transaction Transaction { get; } = transaction;
}
