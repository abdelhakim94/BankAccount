using Ardalis.SmartEnum.SystemTextJson;
using ExaltIt.BankAccount.Core.AccountAggregate;
using System.Text.Json.Serialization;

namespace ExaltIt.BankAccount.Web.AccountEndpoints;

public record TransactionRecord(Guid Id,
                                [property: JsonConverter(typeof(SmartEnumNameConverter<TransactionType, int>))] TransactionType Type,
                                decimal Amount);
