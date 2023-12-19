using Ardalis.Result;
using ExaltIt.BankAccount.UseCases.Interfaces;

namespace ExaltIt.BankAccount.Infrastructure.Services;

public class TransactionNotificationService : ITransactionNotificationService
{
  public async Task<Result> NotifyTransaction()
  {
    // Do the actual work of notifying the account holder about the transaction
    await Task.Yield();
    return Result.Success();
  }
}
