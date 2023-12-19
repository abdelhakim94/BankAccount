using Ardalis.HttpClientTestExtensions;
using ExaltIt.BankAccount.Infrastructure.Data;
using ExaltIt.BankAccount.Web.AccountEndpoints;
using Xunit;

namespace ExaltIt.BankAccount.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class AccountList(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
{
  private readonly HttpClient _client = factory.CreateClient();

  [Fact]
  public async Task List_ShouldReturnAllAccounts()
  {
    // Act
    var result = await _client.GetAndDeserializeAsync<AccountListResponse>("/Accounts");

    // Assert
    Assert.Equal(2, result.Accounts.Count);
    Assert.Contains(result.Accounts, i => i.Id == SeedData.Account1.Id);
    Assert.Contains(result.Accounts, i => i.Id == SeedData.Account2.Id);
  }
}
