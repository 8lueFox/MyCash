using Microsoft.EntityFrameworkCore;
using MyCash.WealthManager.Core.Entities;

namespace MyCash.WealthManager.Infrastructure.DAL;

internal class WealthDbContext : DbContext
{
    public DbSet<Income> Incomes { get; set; } = null!;
    public DbSet<Expense> Expenses { get; set; } = null!;
    public DbSet<Family> Families { get; set; } = null!;

    public WealthDbContext(DbContextOptions<WealthDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
