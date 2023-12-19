using Ardalis.Result;
using Ardalis.SharedKernel;
using ExaltIt.BankAccount.UseCases.Accounts.DTOs;

namespace ExaltIt.BankAccount.UseCases.Accounts.List;

public record ListAccountsQuery() : IQuery<Result<IEnumerable<AccountDTO>>>;
