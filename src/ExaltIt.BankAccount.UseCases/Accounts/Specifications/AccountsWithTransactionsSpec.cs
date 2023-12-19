using Ardalis.Specification;
using ExaltIt.BankAccount.Core.AccountAggregate;

namespace ExaltIt.BankAccount.UseCases.Accounts.Specifications;

public class AccountsWithTransactionsSpec : Specification<Account>
{
  public AccountsWithTransactionsSpec()
  {
    Query
      .Include(a => a.Transactions);
  }
}
