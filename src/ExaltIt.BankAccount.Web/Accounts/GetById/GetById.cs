using FastEndpoints;
using MediatR;
using Ardalis.Result;
using ExaltIt.BankAccount.UseCases.Accounts.Get;

namespace ExaltIt.BankAccount.Web.AccountEndpoints;

/// <summary>
/// Get an Account by ID.
/// </summary>
/// <remarks>
/// Takes a Guid ID and returns a matching Account record.
/// </remarks>
public class GetById(IMediator _mediator)
  : Endpoint<GetAccountByIdRequest, AccountWithAllTransactionsRecord>
{
  public override void Configure()
  {
    Get(GetAccountByIdRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetAccountByIdRequest request,
    CancellationToken cancellationToken)
  {
    var command = new GetAccountQuery(request.AccountId);

    var result = await _mediator.Send(command, cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (result.IsSuccess)
    {
      var transactions = result.Value.Transactions
        .Select(t => new TransactionRecord(t.Id, t.Type, t.Amount));

      Response = new AccountWithAllTransactionsRecord(result.Value.Id, result.Value.Number, result.Value.Balance, transactions);
    }
  }
}
