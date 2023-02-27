using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.Types;
using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Infrastructure.DAL.Configurations;

internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new TransactionId(x));
        builder.OwnsOne(x => x.Amount, sb =>
        {
            sb.Property(x => x.Currency).HasMaxLength(10).HasColumnName("Currency");
            sb.Property(x => x.Price).HasColumnName("Price");
            sb.Property(x => x.Count).HasColumnName("Count");
        });
        builder.Property(x => x.Date)
            .HasConversion(x => x.Value, x => new Date(x));
        builder.HasOne<InvestmentObject>().WithMany().HasForeignKey(x => x.InvestmentObjectId);
    }
}
