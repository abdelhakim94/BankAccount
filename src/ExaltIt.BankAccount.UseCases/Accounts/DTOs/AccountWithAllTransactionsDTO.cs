namespace ExaltIt.BankAccount.UseCases.Accounts.DTOs;

public record AccountWithAllTransactionsDTO(Guid Id,
                                            string Number,
                                            decimal Balance,
                                            IEnumerable<TransactionDTO> Transactions);
