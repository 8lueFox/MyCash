using Microsoft.EntityFrameworkCore;
using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.Repositories;
using MyCash.Wallets.Core.Types;

namespace MyCash.Wallets.Infrastructure.DAL.Repositories;

internal class StockRepository : IStockRepository
{
    private readonly WalletDbContext _dbContext;

    public StockRepository(WalletDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddOrUpdateRangeAsync(IEnumerable<Stock> stocks, CancellationToken cancellationToken)
    {
        foreach (var currentItem in stocks)
        {
            var stock = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Name == currentItem.Name, cancellationToken);
            if (stock is null)
            {
                currentItem.Id = Guid.NewGuid();
                _dbContext.Stocks.Add(currentItem);
            }
            else
            {
                stock.Update(currentItem);
            }
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Stock> GetStockByName(string name, CancellationToken cancellationToken)
        => await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Name.ToLower().Contains(name.ToLower()), cancellationToken);
}
