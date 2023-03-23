using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyCash.PriceScraper.Core.Repositories;
using MyCash.PriceScraper.Infrastructure.DAL;
using MyCash.PriceScraper.Infrastructure.DAL.Repositories;

namespace MyCash.PriceScraper.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        => services
            .AddHangfire(x => x.UseMemoryStorage())
            .AddHangfireServer()
            .AddDbContext<PriceScraperDbContext>(opt =>
                opt.UseInMemoryDatabase("WalletsDB"))
            .AddScoped<IStockRepository, StockRepository>();

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        => app.UseHangfireDashboard();
}