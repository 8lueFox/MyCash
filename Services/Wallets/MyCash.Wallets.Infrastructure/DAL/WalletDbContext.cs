using Microsoft.EntityFrameworkCore;
using MyCash.Wallets.Core.Entities;

namespace MyCash.Wallets.Infrastructure.DAL;

internal class WalletDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<InvestmentObject> InvestmentObjects { get; set; } = null!;
    public DbSet<UserInvestmentObjects> UsersInvestmentObjects { get; set; } = null!;

    public WalletDbContext(DbContextOptions<WalletDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
