using ExaltIt.BankAccount.Core.AccountAggregate;
using Xunit;

namespace ExaltIt.BankAccount.UnitTests.Core.AccountAggregate;

public class AccountBalance
{
  [Fact]
  public void Balance_EmptyAccount_ReturnsInitialBalance()
  {
    // Arrange
    var account = new Account("FR6214508000506166136129K69");

    // Act & Assert
    Assert.Equal(0, account.Balance);
  }

  [Fact]
  public void Balance_AccountWithTransactions_CalculatesCorrectBalance()
  {
    // Arrange
    var account = new Account("FR6214508000506166136129K69");
    var transaction1 = new Transaction(TransactionType.Deposit, 100);
    var transaction2 = new Transaction(TransactionType.Withdrawal, 50);

    // Act
    account.AddTransaction(transaction1);
    account.AddTransaction(transaction2);

    // Assert
    Assert.Equal(transaction2.ComputeBalance(transaction1.ComputeBalance(0)), account.Balance);
  }
}
