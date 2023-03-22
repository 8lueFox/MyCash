namespace Micro.Messaging.RabbitMQ;

public interface IMessageBusClient
{
    void Publish<T>(T obj)
        where T : BusPublishDto;
}
