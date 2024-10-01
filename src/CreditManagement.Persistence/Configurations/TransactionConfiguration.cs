using CreditManagement.Domain.Accounts;
using CreditManagement.Persistence.Common;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditManagement.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable(nameof(Transaction).Singularize().Underscore());
        builder.HasKey(transaction => transaction.Id);
        builder.Property(transaction => transaction.Date)
            .IsRequired();
        builder.Property(transaction => transaction.Amount)
            .HasColumnType(EfConstants.ColumnTypes.DecimalTenTwo)
            .IsRequired();
        builder.Property(transaction => transaction.Description)
            .HasColumnType(EfConstants.ColumnTypes.ExtraLongText)
            .IsRequired(false);
        builder.Property(transaction => transaction.CreatedOnUtc).IsRequired();
        builder.Property(transaction => transaction.ModifiedOnUtc);
        builder.HasOne<Account>()
            .WithMany(account => account.Transactions)
            .HasForeignKey(transaction => transaction.AccountId);
    }
}
