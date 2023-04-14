using MyCash.Wallets.Core.Entities;

namespace MyCash.Wallets.Application.Services;

public interface IStockDataClient
{
    IEnumerable<Stock> ReturnAllStocks();
}
