using MyCash.PriceScraper.Core.Entities;

namespace MyCash.PriceScraper.Core.Repositories;

public interface IStockRepository
{
    Task AddRangeAsync(IEnumerable<Stock> entities, CancellationToken cancellationToken = default);
}