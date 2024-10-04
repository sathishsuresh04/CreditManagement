using CreditManagement.Domain.Accounts;
using CreditManagement.Persistence.Common;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditManagement.Persistence.Configurations;

internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable(nameof(Account).Singularize().Underscore());
        builder.HasKey(account => account.Id);
        builder.Property(account => account.AccountNumber).HasColumnType(EfConstants.ColumnTypes.NormalText)
            .IsRequired();
        builder.HasIndex(account => account.AccountNumber)
            .IsUnique();
        builder.Property(account => account.AccountHolder).HasColumnType(EfConstants.ColumnTypes.NormalText)
            .IsRequired();
        builder.Property(account => account.Balance)
            .HasColumnType(EfConstants.ColumnTypes.DecimalTenTwo)
            .IsRequired();

        builder.Property(account => account.CreatedOnUtc).IsRequired();

        builder.Property(account => account.ModifiedOnUtc);

        builder.Property(account => account.DeletedOnUtc);

        builder.Property(account => account.Deleted).HasDefaultValue(false);
        builder.HasQueryFilter(account => !account.Deleted); // default filter and always ignores Deleted accounts
    }
}