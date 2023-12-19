using Ardalis.Result;
using ExaltIt.BankAccount.Core.AccountAggregate;

namespace ExaltIt.BankAccount.UseCases.Accounts.AddTransaction;

public record AddTransactionCommand(Guid AccountId, TransactionType Type, decimal Amount)
  : Ardalis.SharedKernel.ICommand<Result>;
