using Ardalis.HttpClientTestExtensions;
using ExaltIt.BankAccount.Infrastructure.Data;
using ExaltIt.BankAccount.Web.AccountEndpoints;
using Xunit;

namespace ExaltIt.BankAccount.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class AccountGetById(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
{
  private readonly HttpClient _client = factory.CreateClient();

  [Fact]
  public async Task GetById_WithValidId_ShouldReturnAccount()
  {
    // Arrange
    var accountId = SeedData.Account1.Id;

    // Act
    var result = await _client.GetAndDeserializeAsync<AccountWithAllTransactionsRecord>(
      GetAccountByIdRequest.BuildRoute(accountId));

    // Assert
    Assert.Equal(accountId, result.Id);
    Assert.Equal(SeedData.Account1.Number, result.Number);
  }

  [Fact]
  public async Task GetById_WithInvalidId_ShouldReturnNotFound()
  {
    // Arrange
    string route = GetAccountByIdRequest.BuildRoute(Guid.NewGuid());

    // Act & Assert
    _ = await _client.GetAndEnsureNotFoundAsync(route);
  }
}
