using Ardalis.Result;
using Ardalis.SharedKernel;
using ExaltIt.BankAccount.UseCases.Accounts.DTOs;

namespace ExaltIt.BankAccount.UseCases.Accounts.Get;

public record GetAccountQuery(Guid accountId) : IQuery<Result<AccountWithAllTransactionsDTO>>;
