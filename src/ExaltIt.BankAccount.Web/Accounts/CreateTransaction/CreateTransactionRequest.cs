using System.ComponentModel.DataAnnotations;
using ExaltIt.BankAccount.Core.AccountAggregate;


namespace ExaltIt.BankAccount.Web.AccountEndpoints;

public class CreateTransactionRequest
{
  public const string Route = "/Accounts/{AccountId:Guid}/Transactions";
  public static string BuildRoute(Guid accountId) => Route.Replace("{AccountId:Guid}", accountId.ToString());

  [Required]
  public Guid AccountId { get; set; }

  [Required]
  [System.Text.Json.Serialization.JsonConverter(typeof(Ardalis.SmartEnum.SystemTextJson.SmartEnumNameConverter<TransactionType, int>))]
  [Newtonsoft.Json.JsonConverter(typeof(Ardalis.SmartEnum.JsonNet.SmartEnumNameConverter<TransactionType, int>))]
  public TransactionType? Type { get; set; }

  [Required]
  public decimal Amount { get; set; }
}
