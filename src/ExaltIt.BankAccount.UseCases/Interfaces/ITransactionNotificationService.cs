using Ardalis.Result;

namespace ExaltIt.BankAccount.UseCases.Interfaces;

public interface ITransactionNotificationService
{
  Task<Result> NotifyTransaction();
}
