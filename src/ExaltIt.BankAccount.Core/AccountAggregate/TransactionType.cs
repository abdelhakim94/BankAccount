using Ardalis.SmartEnum;

namespace ExaltIt.BankAccount.Core.AccountAggregate;

public class TransactionType : SmartEnum<TransactionType>
{
  public static readonly TransactionType Deposit = new(nameof(Deposit), 1);
  public static readonly TransactionType Withdrawal = new(nameof(Withdrawal), 2);

  protected TransactionType(string name, int value) : base(name, value) { }
}

