using Ardalis.HttpClientTestExtensions;
using ExaltIt.BankAccount.Core.AccountAggregate;
using ExaltIt.BankAccount.Infrastructure.Data;
using ExaltIt.BankAccount.Web.AccountEndpoints;
using FluentAssertions;
using Xunit;

namespace ExaltIt.BankAccount.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class AccountAddTransaction(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
{
  private readonly HttpClient _client = factory.CreateClient();

  [Fact]
  public async Task AddTransaction_WithValidTransaction_ShouldBeSuccessful()
  {
    // Arrange
    var accountId = SeedData.Account1.Id;
    var transactionType = TransactionType.Deposit;
    var transactionAmount = 100m;

    var request = new CreateTransactionRequest()
    {
      AccountId = accountId,
      Type = transactionType,
      Amount = transactionAmount
    };

    var content = StringContentHelpers.FromModelAsJson(request);

    // Act
    var result = await _client.PostAsync(CreateTransactionRequest.BuildRoute(accountId), content);

    // Assert
    var expectedRoute = GetAccountByIdRequest.BuildRoute(accountId);

    var updatedAccount = await _client.GetAndDeserializeAsync<AccountWithAllTransactionsRecord>(expectedRoute);
    updatedAccount.Transactions
      .Should()
      .ContainSingle(transaction => transaction.Type == transactionType && transaction.Amount == transactionAmount);
  }
}
