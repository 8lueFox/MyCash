using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCash.WealthManager.Core.Entities;
using MyCash.WealthManager.Core.Types;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Infrastructure.DAL.Configurations;

internal sealed class FamilyConfiguration : IEntityTypeConfiguration<Family>
{
    public void Configure(EntityTypeBuilder<Family> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new AggregateId(x));
        builder.Property(x => x.BalanceId)
            .HasConversion(x => x.Value, x => new AggregateId(x));
        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => new UserId(x));
        builder.Property(x => x.FamilyName)
            .HasConversion(x => x.Value, x => new FamilyName(x));
        builder.OwnsOne(x => x.Settings);

        builder.HasMany(x => x.Expenses);
        builder.HasMany(x => x.Incomes);
        builder.HasOne(x => x.Balance)
            .WithOne(x => x.Family)
            .HasForeignKey<Balance>(x => x.FamilyId);
    }
}
