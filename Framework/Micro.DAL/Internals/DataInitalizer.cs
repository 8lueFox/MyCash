using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.DAL.Internals;

internal sealed class DataInitalizer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DataInitalizer> _logger;

    public DataInitalizer(IServiceProvider serviceProvider, ILogger<DataInitalizer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var initalizers = scope.ServiceProvider.GetServices<IDataInitalizer>();

        foreach (var initalizer in initalizers)
        {
            try
            {
                _logger.LogInformation($"Running the initializer: {initalizer.GetType().Name}...");
                await initalizer.InitAsync();
            }catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}
