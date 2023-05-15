using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCash.WealthManager.Core.Entities;
using MyCash.WealthManager.Core.Types;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Infrastructure.DAL.Configurations;

internal sealed class IncomeConfiguration : IEntityTypeConfiguration<Income>
{
    public void Configure(EntityTypeBuilder<Income> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new IncomeId(x));
        builder.Property(x => x.IncomeType)
            .HasConversion(x => x.Value, x => new MoneyTransferType(x));
        builder.Property(x => x.Period)
            .HasConversion(x => x.Days, x => new Period(x));
        builder.Property(x => x.ReceiveDate)
            .HasConversion(x => x.Value, x => new Date(x));
        builder.OwnsOne(x => x.ValueGross, sb =>
        {
            sb.Property(x => x.Currency).HasMaxLength(10);
        });
        builder.OwnsOne(x => x.ValueNet, sb =>
        {
            sb.Property(x => x.Currency).HasMaxLength(10);
        });
    }
}
