using Microsoft.EntityFrameworkCore;
using MyCash.PriceScraper.Core.Entities;

namespace MyCash.PriceScraper.Infrastructure.DAL;

internal sealed class PriceScraperDbContext : DbContext
{
    public DbSet<Stock> Stocks { get; set; } = null!;

    public PriceScraperDbContext(DbContextOptions<PriceScraperDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
