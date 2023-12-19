using ExaltIt.BankAccount.Core.AccountAggregate;
using Xunit;

namespace ExaltIt.BankAccount.UnitTests.Core.AccountAggregate;

public class TransactionConstructor
{
  [Fact]
  public void Constructor_ValidTypeAndAmount_SetTypeAndAmountProperties()
  {
    // Arrange
    var transactionType = TransactionType.Deposit;
    decimal amount = 100;

    // Act
    var transaction = new Transaction(transactionType, amount);

    // Assert
    Assert.Equal(transactionType, transaction.Type);
    Assert.Equal(amount, transaction.Amount);
  }

  [Fact]
  public void Constructor_InvalidAmount_ThrowsException()
  {
    // Arrange
    var transactionType = TransactionType.Deposit;
    decimal amount = 0;

    // Act & Assert
    Assert.Throws<ArgumentException>(() => new Transaction(transactionType, amount));
  }
}
