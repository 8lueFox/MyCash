using Hangfire;
using MediatR;
using Micro.WebAPI;
using MyCash.PriceScraper.Core.Services;

namespace MyCash.PriceScraper.Core.Queries;

public record FetchStocksRequest : Request;

public class FetchStocksRequestHandler : IRequestHandler<FetchStocksRequest>
{
    private readonly IScraper _scraper;

    public FetchStocksRequestHandler(IScraper scraper)
    {
        _scraper = scraper;
    }

    public Task Handle(FetchStocksRequest request, CancellationToken cancellationToken)
    {
        RecurringJob.AddOrUpdate(
            "fetchingNasdaqStocks",
            () => _scraper.FetchNasdaqStocks(cancellationToken),
            "*/15 * * * *");
        _scraper.FetchNasdaqStocks(cancellationToken);

        return Task.CompletedTask;
    }
}
