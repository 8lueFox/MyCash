using Micro.Messaging.RabbitMQ;
using MyCash.BackgroundWorker.Application.Dtos;
using MyCash.BackgroundWorker.Application.Services.Interfaces;
using RestSharp;

namespace MyCash.BackgroundWorker.Application.Services;

internal class Scraper : IScraper
{
    private readonly IMessageBusClient _messageBus;

    public Scraper(IMessageBusClient messageBus)
    {
        _messageBus = messageBus;
    }

    //https://api.nasdaq.com/api/screener/stocks?tableonly=true&limit=25&offset=0&download=true
    public async Task ScrapNasdaqPrices(CancellationToken cancellationToken)
    {
        var siteUrl = "https://api.nasdaq.com/api";
        var requestUrl = "screener/stocks?tableonly=true&limit=25&offset=0&download=true";
        var headers = new Dictionary<string, string>
        {
            { "User-Agent", "PostmanRuntime/7.29.2" },
            { "Accept", "*/*" }
        };

        try
        {
            var client = new RestClient(baseUrl: siteUrl);
            client.AddDefaultHeaders(headers);
            
            var request = new RestRequest(requestUrl);
            request.Timeout = 15 * 1_000;

            var response = await client.GetAsync(request, cancellationToken);

            if(response.IsSuccessStatusCode && !string.IsNullOrEmpty(response.Content))
            {
                var msg = new NasdaqPricesDto
                {
                    Content = response.Content,
                    Event = "NasdaqFetched"
                };
                _messageBus.Publish(msg);
            }

        }catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching NASDAQ data: {ex.Message}.");
        }
    }
}
