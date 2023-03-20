using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCash.Users.Core.Entities;
using MyCash.Users.Core.ValueObjects;

namespace MyCash.Users.Core.DAL.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => x.Email)
            .IsUnique();
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(500);
        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(500);
        builder.Property(x => x.Package)
            .HasConversion(x => x.Value, x => new UserPackage(x));
    }
}
