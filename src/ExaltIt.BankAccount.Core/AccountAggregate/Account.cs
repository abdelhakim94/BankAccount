using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using ExaltIt.BankAccount.Core.AccountAggregate.Events;

namespace ExaltIt.BankAccount.Core.AccountAggregate;

public class Account(string number) : EntityBase<Guid>, IAggregateRoot
{
  private const decimal INITIAL_BALANCE = 0;
  private readonly IList<Transaction> _transactions = new List<Transaction>();

  public string Number { get; } = Guard.Against.NullOrWhiteSpace(number);
  public IEnumerable<Transaction> Transactions => _transactions.AsReadOnly();
  public decimal Balance => _transactions.Aggregate(INITIAL_BALANCE, (b, t) => t.ComputeBalance(b));

  public void AddTransaction(Transaction transaction)
  {
    Guard.Against.Null(transaction, nameof(transaction));
    _transactions.Add(transaction);
    RegisterDomainEvent(new TransactionAddedEvent(transaction));
  }
}
