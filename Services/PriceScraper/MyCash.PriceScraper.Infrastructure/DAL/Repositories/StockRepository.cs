using Microsoft.EntityFrameworkCore;
using MyCash.PriceScraper.Core.Entities;
using MyCash.PriceScraper.Core.Repositories;

namespace MyCash.PriceScraper.Infrastructure.DAL.Repositories;

internal sealed class StockRepository : IStockRepository
{
    private readonly PriceScraperDbContext _dbContext;

    public StockRepository(PriceScraperDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRangeAsync(IEnumerable<Stock> entities, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Stock>> GetAllStocks(CancellationToken cancellationToken = default)
        => await _dbContext.Stocks.ToListAsync();
}
