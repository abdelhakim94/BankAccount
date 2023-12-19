using Ardalis.Result;
using Ardalis.SharedKernel;
using ExaltIt.BankAccount.Core.AccountAggregate;
using ExaltIt.BankAccount.UseCases.Accounts.DTOs;
using ExaltIt.BankAccount.UseCases.Accounts.Specifications;

namespace ExaltIt.BankAccount.UseCases.Accounts.List;

public class ListAccountsHandler(IReadRepository<Account> _repository)
  : IQueryHandler<ListAccountsQuery, Result<IEnumerable<AccountDTO>>>
{
  public async Task<Result<IEnumerable<AccountDTO>>> Handle(ListAccountsQuery request, CancellationToken cancellationToken)
  {
    var spec = new AccountsWithTransactionsSpec();
    var entities = await _repository.ListAsync(spec, cancellationToken);
    var result = entities.Select(a => new AccountDTO(a.Id, a.Number, a.Balance));
    return Result.Success(result);
  }
}
