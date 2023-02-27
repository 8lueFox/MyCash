using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.Types;
using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Infrastructure.DAL.Configurations;

internal sealed class InvestmentObjectConfiguration : IEntityTypeConfiguration<InvestmentObject>
{
    public void Configure(EntityTypeBuilder<InvestmentObject> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new InvestmentObjectId(x));
        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => new InvestmentObjectName(x));
        builder.Property(x => x.Type)
            .HasConversion(x => x.Value, x => new InvestmentObjectType(x));
        builder.HasMany<Transaction>().WithOne();
    }
}
