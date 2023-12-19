using Ardalis.Result;
using Ardalis.SharedKernel;
using ExaltIt.BankAccount.Core.AccountAggregate;
using ExaltIt.BankAccount.UseCases.Accounts.DTOs;
using ExaltIt.BankAccount.UseCases.Accounts.Specifications;

namespace ExaltIt.BankAccount.UseCases.Accounts.Get;

public class GetAccountHandler(IReadRepository<Account> _repository)
  : IQueryHandler<GetAccountQuery, Result<AccountWithAllTransactionsDTO>>
{
  public async Task<Result<AccountWithAllTransactionsDTO>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
  {
    var spec = new AccountByIdWithTransactionsSpec(request.accountId);
    var entity = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    if (entity == null) return Result.NotFound();

    var transactions = entity.Transactions
      .Select(t => new TransactionDTO(t.Id, t.Type, t.Amount));

    return Result.Success(new AccountWithAllTransactionsDTO(entity.Id, entity.Number, entity.Balance, transactions));
  }
}
