using ExaltIt.BankAccount.Core.AccountAggregate;
using ExaltIt.BankAccount.UseCases.Accounts.AddTransaction;
using FastEndpoints;
using MediatR;

namespace ExaltIt.BankAccount.Web.AccountEndpoints;

/// <summary>
/// Adds a new Transaction
/// </summary>
/// <remarks>
/// Adds a new Transaction to the Account
/// </remarks>
public class CreateTransaction(IMediator mediator) : Endpoint<CreateTransactionRequest>
{
  private readonly IMediator _mediator = mediator;

  public override void Configure()
  {
    Post(CreateTransactionRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.ExampleRequest = new CreateTransactionRequest
      {
        AccountId = new Guid(),
        Type = TransactionType.Deposit,
        Amount = 100
      };
    });
  }

  public override async Task HandleAsync(CreateTransactionRequest request,
    CancellationToken cancellationToken)
  {
    await _mediator.Send(new AddTransactionCommand(request.AccountId, request.Type!, request.Amount), cancellationToken);
    return;
  }
}
