using MyCash.Wallets.Core.Entities;

namespace MyCash.Wallets.Core.Repositories;

public interface IStockRepository
{
    Task AddOrUpdateRangeAsync(IEnumerable<Stock> stocks, CancellationToken cancellationToken);
}
