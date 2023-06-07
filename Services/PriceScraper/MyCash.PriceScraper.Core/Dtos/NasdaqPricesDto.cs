using Micro.Messaging.RabbitMQ;

namespace MyCash.PriceScraper.Core.Dtos;

internal class NasdaqPricesDto : BusPublishDto
{
    public string? Content { get; set; }
}
