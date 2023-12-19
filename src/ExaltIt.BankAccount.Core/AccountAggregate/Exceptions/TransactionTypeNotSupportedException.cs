namespace ExaltIt.BankAccount.Core.AccountAggregate.Exceptions;

public class TransactionTypeNotSupportedException : Exception
{
  public TransactionTypeNotSupportedException() : base() { }

  public TransactionTypeNotSupportedException(string? message) : base(message) { }

  public TransactionTypeNotSupportedException(string? message, Exception? innerException) : base(message, innerException) { }
}
