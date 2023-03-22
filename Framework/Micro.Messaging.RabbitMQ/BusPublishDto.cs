namespace Micro.Messaging.RabbitMQ;

public abstract class BusPublishDto
{
    public string Event { get; set; } = string.Empty;
}
