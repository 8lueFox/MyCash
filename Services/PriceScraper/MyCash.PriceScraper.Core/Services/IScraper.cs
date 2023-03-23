namespace MyCash.PriceScraper.Core.Services;

public interface IScraper
{
    Task FetchNasdaqStocks(CancellationToken cancellationToken);
}
