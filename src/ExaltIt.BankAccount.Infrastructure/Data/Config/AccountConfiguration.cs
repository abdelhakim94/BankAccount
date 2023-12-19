using ExaltIt.BankAccount.Core.AccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaltIt.BankAccount.Infrastructure.Data.Config;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
  public void Configure(EntityTypeBuilder<Account> builder)
  {
    builder.Property(p => p.Number)
      .HasMaxLength(DataSchemaConstants.BANK_ACCOUNT_NUMBER_LENGTH)
      .IsRequired();
  }
}
