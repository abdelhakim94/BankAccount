namespace ExaltIt.BankAccount.Web.AccountEndpoints;

public record AccountWithAllTransactionsRecord(Guid Id, string Number, decimal Balance, IEnumerable<TransactionRecord> Transactions);
