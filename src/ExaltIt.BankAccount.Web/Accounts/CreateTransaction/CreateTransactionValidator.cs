using FastEndpoints;
using FluentValidation;

namespace ExaltIt.BankAccount.Web.AccountEndpoints;

public class CreateTransactionValidator : Validator<CreateTransactionRequest>
{
  public CreateTransactionValidator()
  {
    RuleFor(x => x.AccountId)
      .NotEmpty();

    RuleFor(x => x.Amount)
      .NotEmpty()
      .Must(x => x > 0);

    RuleFor(x => x.Type)
      .NotEmpty();
  }
}
