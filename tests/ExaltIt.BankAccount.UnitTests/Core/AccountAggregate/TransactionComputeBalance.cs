using ExaltIt.BankAccount.Core.AccountAggregate;
using Xunit;

namespace ExaltIt.BankAccount.UnitTests.Core.AccountAggregate;

public class TransactionComputeBalance
{
  [Fact]
  public void ComputeBalance_DepositTransaction_CalculatesCorrectBalance()
  {
    // Arrange
    var transactionType = TransactionType.Deposit;
    decimal amount = 100;
    var transaction = new Transaction(transactionType, amount);
    decimal initialBalance = 50;

    // Act
    var balance = transaction.ComputeBalance(initialBalance);

    // Assert
    Assert.Equal(initialBalance + amount, balance);
  }

  [Fact]
  public void ComputeBalance_WithdrawalTransaction_CalculatesCorrectBalance()
  {
    // Arrange
    var transactionType = TransactionType.Withdrawal;
    decimal amount = 50;
    var transaction = new Transaction(transactionType, amount);
    decimal initialBalance = 100;

    // Act
    var balance = transaction.ComputeBalance(initialBalance);

    // Assert
    Assert.Equal(initialBalance - amount, balance);
  }
}
