using ExaltIt.BankAccount.Core.AccountAggregate;

namespace ExaltIt.BankAccount.UseCases.Accounts.DTOs;

public record TransactionDTO(Guid Id, TransactionType Type, decimal Amount);
