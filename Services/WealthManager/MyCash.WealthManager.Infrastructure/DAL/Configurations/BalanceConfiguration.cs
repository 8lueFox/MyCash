using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCash.WealthManager.Core.Entities;

namespace MyCash.WealthManager.Infrastructure.DAL.Configurations;

internal sealed class BalanceConfiguration : IEntityTypeConfiguration<Balance>
{
    public void Configure(EntityTypeBuilder<Balance> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new Core.Types.AggregateId(x));
        builder.Property(x => x.FamilyId)
            .HasConversion(x => x.Value, x => new Core.Types.AggregateId(x));
        builder.OwnsOne(x => x.Value, sb =>
        {
            sb.Property(x => x.Currency).HasMaxLength(10);
        });

        builder.HasMany(x => x.Events);
        builder.HasOne(x => x.Family)
            .WithOne(x => x.Balance)
            .HasForeignKey<Family>(x => x.BalanceId);

    }
}
