namespace ExaltIt.BankAccount.Web.AccountEndpoints;

public class GetAccountByIdRequest
{
  public const string Route = "/Accounts/{AccountId:Guid}";
  // Used in the test project
  public static string BuildRoute(Guid accountId) => Route.Replace("{AccountId:Guid}", accountId.ToString());

  public Guid AccountId { get; set; }
}
