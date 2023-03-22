using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Micro.Messaging.RabbitMQ.Internal;

internal class MessageBusClient : IMessageBusClient
{
    private readonly RabbitMQOptions _options;
    private readonly IConnection _connection = null!;
    private readonly IModel _channel = null!;

    public MessageBusClient(IOptions<RabbitMQOptions> options)
    {
        _options = options.Value;

        var factory = new ConnectionFactory
        {
            HostName = _options.Host,
            Port = _options.Port,
            UserName = _options.Username,
            Password = _options.Password
        };

        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Couldn't connect to RabbitMQ: {ex.Message}");
        }
    }

    public void Publish<T>(T obj)
        where T : BusPublishDto
    {
        var message = JsonSerializer.Serialize<T>(obj);

        if (_connection.IsOpen)
        {
            Console.WriteLine("--> RabbitMQ Connection Open, sending message...");
            SendMessage(message);
        }
        else
            Console.WriteLine("--> RabbitMQ Connection Closed, not sending");
    }

    private void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);

        Console.WriteLine($"--> RabbitMQ We have sent {message}");
    }

    private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
        Console.WriteLine("--> RabbitMQ Connection shutdown.");
    }
}
