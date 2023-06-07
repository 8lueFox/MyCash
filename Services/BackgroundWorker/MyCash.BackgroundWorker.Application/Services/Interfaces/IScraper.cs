namespace MyCash.BackgroundWorker.Application.Services.Interfaces;

public interface IScraper
{
    Task ScrapNasdaqPrices(CancellationToken cancellationToken);
}
