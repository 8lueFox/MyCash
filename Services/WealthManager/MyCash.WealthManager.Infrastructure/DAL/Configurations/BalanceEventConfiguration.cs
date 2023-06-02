using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCash.WealthManager.Core.Entities;
using MyCash.WealthManager.Core.Types;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Infrastructure.DAL.Configurations;

internal sealed class BalanceEventConfiguration : IEntityTypeConfiguration<BalanceEvent>
{
    public void Configure(EntityTypeBuilder<BalanceEvent> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new BalanceEventId(x));
        builder.Property(x => x.BalanceId)
            .HasConversion(x => x.Value, x => new AggregateId(x));
        builder.Property(x => x.EventDate)
            .HasConversion(x => x.Value, x => new Date(x));
        builder.Property(x => x.BalanceEventType)
            .HasConversion(x => x.Value, x => new BalanceEventType(x));
        builder.OwnsOne(x => x.Value, sb =>
        {
            sb.Property(x => x.Currency).HasMaxLength(10);
        });
        builder.HasOne<Balance>()
            .WithMany()
            .HasForeignKey(x => x.BalanceId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
