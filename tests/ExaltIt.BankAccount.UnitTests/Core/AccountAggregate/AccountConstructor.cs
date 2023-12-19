using ExaltIt.BankAccount.Core.AccountAggregate;
using Xunit;

namespace ExaltIt.BankAccount.UnitTests.Core.AccountAggregate;

public class AccountConstructor
{
  [Fact]
  public void Constructor_ValidNumber_SetNumberProperty()
  {
    // Arrange
    string validNumber = "FR6214508000506166136129K69";

    // Act
    var account = new Account(validNumber);

    // Assert
    Assert.Equal(validNumber, account.Number);
  }

  [Fact]
  public void Constructor_NullAccountNumber_ThrowsException()
  {
    Assert.Throws<ArgumentNullException>(() => new Account(null!));
  }

  [Theory]
  [InlineData("")]
  [InlineData("   ")]
  public void Constructor_EmptyAccountNumber_ThrowsException(string invalidNumber)
  {
    Assert.Throws<ArgumentException>(() => new Account(invalidNumber));
  }
}
