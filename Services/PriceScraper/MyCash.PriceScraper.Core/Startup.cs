using Microsoft.Extensions.DependencyInjection;
using MyCash.PriceScraper.Core.Profiles;
using MyCash.PriceScraper.Core.Services;
using System.Reflection;

namespace MyCash.PriceScraper.Core;

public static class Startup
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    => services
            .AddAutoMapper(Assembly.GetAssembly(typeof(StockProfile)))
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly))
            .AddScoped<IScraper, Scraper>();
}
