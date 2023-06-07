using Micro.Messaging.RabbitMQ;

namespace MyCash.BackgroundWorker.Application.Dtos;

internal class NasdaqPricesDto : BusPublishDto
{
    public string? Content { get; set; }
}
