using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExaltIt.BankAccount.Core.AccountAggregate;

namespace ExaltIt.BankAccount.Infrastructure.Data.Config;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
  public void Configure(EntityTypeBuilder<Transaction> builder)
  {
    builder.Property(x => x.Amount);

    builder.Property(x => x.Type)
      .HasConversion(
        x => x.Value,
        x => TransactionType.FromValue(x));
  }
}
