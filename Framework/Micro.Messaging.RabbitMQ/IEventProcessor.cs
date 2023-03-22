namespace Micro.Messaging.RabbitMQ;

public interface IEventProcessor
{
    void ProcessEvent(string message);
}
