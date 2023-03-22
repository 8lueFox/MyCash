using Micro.Messaging.RabbitMQ.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.Messaging.RabbitMQ;

public static class Startup
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration config)
    {
        var section = config.GetSection("rabbitmq");
        services.Configure<RabbitMQOptions>(section);
        var options = section.Get<RabbitMQOptions>();

        return services
            .AddSingleton<IMessageBusClient, MessageBusClient>()
            .AddSingleton<IEventProcessor, DefaultEventProcessor>()
            .AddHostedService<MessageBusSubscriber>();
    }
}
