using Microsoft.Extensions.DependencyInjection;
using MyCash.PriceScraper.Core.Profiles;
using MyCash.PriceScraper.Core.Services;
using System.Reflection;

namespace MyCash.PriceScraper.Core;

public static class Startup
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        ///TODO: repair joining StockProfile, it doesn't work
        var ass = Assembly.GetAssembly(typeof(StockProfile));
        services
            .AddAutoMapper(Assembly.GetAssembly(typeof(StockProfile)));
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
        services
            .AddScoped<IScraper, Scraper>();

        return services;
    }
}
