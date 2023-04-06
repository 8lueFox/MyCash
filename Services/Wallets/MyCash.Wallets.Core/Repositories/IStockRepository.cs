using MyCash.Wallets.Core.Entities;

namespace MyCash.Wallets.Core.Repositories;

public interface IStockRepository
{
    Task AddOrUpdateRangeAsync(IEnumerable<Stock> stocks, CancellationToken cancellationToken);
    Task<Stock> GetStockByName(string name, CancellationToken cancellationToken);
}
