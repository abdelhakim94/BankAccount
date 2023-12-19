using ExaltIt.BankAccount.Core.AccountAggregate;
using Xunit;

namespace ExaltIt.BankAccount.UnitTests.Core.AccountAggregate;

public class AccountAddTransaction
{
  [Fact]
  public void Transactions_EmptyAccount_ReturnsEmptyList()
  {
    // Arrange
    var account = new Account("FR6214508000506166136129K69");

    // Act & Assert
    Assert.Empty(account.Transactions);
  }

  [Fact]
  public void AddTransaction_ValidTransaction_AddsTransactionToList()
  {
    // Arrange
    var account = new Account("FR6214508000506166136129K69");
    var transaction = new Transaction(TransactionType.Deposit, 100);

    // Act
    account.AddTransaction(transaction);

    // Assert
    Assert.Contains(transaction, account.Transactions);
  }

  [Fact]
  public void AddTransaction_ValidTransaction_RaisesDomainEvent()
  {
    // Arrange
    var account = new Account("FR6214508000506166136129K69");
    var transaction = new Transaction(TransactionType.Deposit, 100);

    // Act
    account.AddTransaction(transaction);

    // Assert
    Assert.Single(account.DomainEvents);
  }

  [Fact]
  public void AddTransaction_NullTransaction_ThrowsException()
  {
    // Arrange
    var account = new Account("FR6214508000506166136129K69");

    // Act & Assert
    Assert.Throws<ArgumentNullException>(() => account.AddTransaction(null!));
  }
}
