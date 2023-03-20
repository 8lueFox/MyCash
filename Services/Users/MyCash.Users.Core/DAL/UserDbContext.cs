using Microsoft.EntityFrameworkCore;
using MyCash.Users.Core.Entities;

namespace MyCash.Users.Core.DAL;

internal sealed class UserDbContext : DbContext
{
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public UserDbContext(DbContextOptions<UserDbContext> options)
        :base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
