using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using ExaltIt.BankAccount.Core.AccountAggregate.Exceptions;

namespace ExaltIt.BankAccount.Core.AccountAggregate;

public class Transaction(TransactionType type, decimal amount) : EntityBase<Guid>
{
  public TransactionType Type { get; } = type;
  public decimal Amount { get; } = Guard.Against.Default(amount);

  public decimal ComputeBalance(decimal initialBalance) => Type.Name switch
  {
    nameof(TransactionType.Deposit) => initialBalance + amount,
    nameof(TransactionType.Withdrawal) => initialBalance - amount,
    _ => throw new TransactionTypeNotSupportedException($"The transaction type {Type} is not supported")
  };
}
