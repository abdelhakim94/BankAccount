using FastEndpoints;
using FluentValidation;

namespace ExaltIt.BankAccount.Web.AccountEndpoints;

/// <summary>
/// See: https://fast-endpoints.com/docs/validation
/// </summary>
public class GetAccountValidator : Validator<GetAccountByIdRequest>
{
  public GetAccountValidator()
  {
    RuleFor(x => x.AccountId)
      .NotEmpty();
  }
}
