using Micro.Messaging.RabbitMQ.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.Messaging.RabbitMQ;

public static class Startup
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
        => services
            .AddSingleton<IMessageBusClient, MessageBusClient>()
            .AddHostedService<MessageBusSubscriber>();
}
