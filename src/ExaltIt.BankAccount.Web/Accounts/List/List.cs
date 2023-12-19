using FastEndpoints;
using MediatR;
using ExaltIt.BankAccount.UseCases.Accounts.List;

namespace ExaltIt.BankAccount.Web.AccountEndpoints;

/// <summary>
/// List all accounts
/// </summary>
/// <remarks>
/// List all accounts - returns an AccountListResponse containing the accounts.
/// </remarks>
public class List(IMediator _mediator) : EndpointWithoutRequest<AccountListResponse>
{
  public override void Configure()
  {
    Get("/Accounts");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new ListAccountsQuery(), cancellationToken);

    if (result.IsSuccess)
    {
      Response = new AccountListResponse
      {
        Accounts = result.Value.Select(a => new AccountRecord(a.Id, a.Number, a.Balance)).ToList()
      };
    }
  }
}
