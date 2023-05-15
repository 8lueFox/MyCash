using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCash.WealthManager.Core.Entities;
using MyCash.WealthManager.Core.Types;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Infrastructure.DAL.Configurations;

internal sealed class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new ExpenseId(x));
        builder.Property(x => x.Period)
            .HasConversion(x => x.Days, x => new Period(x));
        builder.Property(x => x.ExpenseType)
            .HasConversion(x => x.Value, x => new MoneyTransferType(x));
        builder.Property(x => x.SendDate)
            .HasConversion(x => x.Value, x => new Date(x));
        builder.OwnsOne(x => x.Value, sb =>
        {
            sb.Property(x => x.Currency).HasMaxLength(10);
        });
        builder.HasOne<Family>().WithMany().HasForeignKey(x => x.FamilyId);
    }
}
