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

    //private readonly IServiceCollection _services;
    //public Starter(IServiceCollection services)
    //{
    //    _services = services;
    //}

    //public Task StartAsync(CancellationToken cancellationToken)
    //    => StartBalanceOrganizator(cancellationToken);

    //public Task StopAsync(CancellationToken cancellationToken)
    //    => Task.CompletedTask;

    //public async Task StartBalanceOrganizator(CancellationToken cancellationToken)
    //{
    //    var serviceCollection = new ServiceCollection();
    //    using (ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider())
    //    {
    //        var balanceOrganizator = serviceProvider.GetRequiredService<IBalanceOrganizator>();
    //        await balanceOrganizator.CheckAllFamilies(cancellationToken);
    //    }
    //}
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using IServiceScope scope = _serviceProvider.CreateScope();
        var balanceOrganizator = scope.ServiceProvider.GetRequiredService<IBalanceOrganizator>();
        RecurringJob.AddOrUpdate(
            "balanceCheck",
            () => balanceOrganizator.CheckAllFamilies(stoppingToken),
            "30 */1 * * *");
    }
}
