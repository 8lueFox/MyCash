using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCash.WealthManager.Core.Entities;

namespace MyCash.WealthManager.Infrastructure.DAL.Configurations;

internal sealed class BalanceEventConfiguration : IEntityTypeConfiguration<BalanceEvent>
{
    public void Configure(EntityTypeBuilder<BalanceEvent> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new Core.Types.BalanceEventId(x));
        builder.OwnsOne(x => x.Value, sb =>
        {
            sb.Property(x => x.Currency).HasMaxLength(10);
        });
        builder.HasOne<Balance>().WithMany().HasForeignKey(x => x.BalanceId);
    }
}
