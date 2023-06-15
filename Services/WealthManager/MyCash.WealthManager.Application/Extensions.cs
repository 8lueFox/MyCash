using Micro.Messaging.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using MyCash.WealthManager.Application.EventProcessing;
using MyCash.WealthManager.Application.Services;

namespace MyCash.WealthManager.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Extensions).Assembly));

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddSingleton<IEventProcessor, EventProcessor>();
        services.AddScoped<IUserDataClient, UserDataClient>();

        return services;
    }
}
