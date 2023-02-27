using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.Types;
using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Infrastructure.DAL.Configurations;

internal sealed class UserInvestmentObjectsConfiguration : IEntityTypeConfiguration<UserInvestmentObjects>
{
    public void Configure(EntityTypeBuilder<UserInvestmentObjects> builder)
    {
        var userPackageConverter = new ValueConverter<UserPackage, string>(x => x.Value,
            x => new UserPackage(x));

        builder.HasKey(x => x.Id);
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new AggregateId(x));
        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => new UserId(x));

        builder.Property(typeof(UserPackage), "_userPackage")
            .HasConversion(userPackageConverter)
            .HasColumnName(nameof(UserPackage));

        builder.HasMany(x => x.InvestmentObjects);
    }
}
