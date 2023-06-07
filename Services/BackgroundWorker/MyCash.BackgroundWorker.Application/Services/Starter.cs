using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCash.BackgroundWorker.Application.Services.Interfaces;

namespace MyCash.BackgroundWorker.Application.Services;

public class Starter : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public Starter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using IServiceScope scope = _serviceProvider.CreateScope();
        var balanceOrganizator = scope.ServiceProvider.GetRequiredService<IBalanceOrganizator>();
        var scraper = scope.ServiceProvider.GetRequiredService<IScraper>();
        RecurringJob.AddOrUpdate(
            "balanceCheck",
            () => balanceOrganizator.CheckAllFamilies(stoppingToken),
            "30 */1 * * *");
        RecurringJob.AddOrUpdate(
            "priceScrap",
            () => scraper.ScrapNasdaqPrices(stoppingToken),
            "*/15 * * * *");
    }
}
