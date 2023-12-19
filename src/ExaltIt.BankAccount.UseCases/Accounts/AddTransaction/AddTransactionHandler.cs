using Ardalis.Result;
using Ardalis.SharedKernel;
using ExaltIt.BankAccount.Core.AccountAggregate;
using ExaltIt.BankAccount.UseCases.Accounts.Specifications;

namespace ExaltIt.BankAccount.UseCases.Accounts.AddTransaction;

public class AddTransactionHandler(IRepository<Account> repository) : ICommandHandler<AddTransactionCommand, Result>
{
  private readonly IRepository<Account> _repository = repository;

  public async Task<Result> Handle(AddTransactionCommand request,
    CancellationToken cancellationToken)
  {
    var spec = new AccountByIdWithTransactionsSpec(request.AccountId);
    var entity = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    if (entity == null)
    {
      return Result.NotFound();
    }

    var transaction = new Transaction(request.Type, request.Amount);
    entity.AddTransaction(transaction);

    await _repository.UpdateAsync(entity, cancellationToken);
    return Result.Success();
  }
}
