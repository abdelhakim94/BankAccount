using Ardalis.Specification;
using ExaltIt.BankAccount.Core.AccountAggregate;

namespace ExaltIt.BankAccount.UseCases.Accounts.Specifications;

public class AccountByIdWithTransactionsSpec : Specification<Account>
{
  public AccountByIdWithTransactionsSpec(Guid accountId)
  {
    Query
      .Where(account => account.Id == accountId)
      .Include(a => a.Transactions);
  }
}
