using Hangfire;
using Hangfire.MemoryStorage;
using Micro.Framework;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCash.BackgroundWorker.Application.Services;
using MyCash.BackgroundWorker.Application.Services.Interfaces;
using MyCash.WealthManager.Application;
using MyCash.WealthManager.Core;
using MyCash.WealthManager.Infrastructure;

namespace MyCash.BackgroundWorker.Application;

public static class Startup
{
    public static IServiceCollection AddApp(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddHangfire(x => x.UseMemoryStorage())
            .AddHangfireServer()
            .AddCore()
            .AddApplication()
            .AddMicroFramework(configuration)
            .AddInfrastructure(configuration)
            .AddTransient<IBalanceOrganizator, BalanceOrganizator>()
            .AddTransient<IScraper, Scraper>()
            .AddHostedService<Starter>();

    public static IApplicationBuilder UseApp(this IApplicationBuilder app)
        => app.UseHangfireDashboard();
}
