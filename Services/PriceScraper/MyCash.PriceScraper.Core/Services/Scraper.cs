using AutoMapper;
using Micro.Messaging.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using MyCash.PriceScraper.Core.Dtos;
using MyCash.PriceScraper.Core.Entities;
using MyCash.PriceScraper.Core.Queries;
using MyCash.PriceScraper.Core.Repositories;
using RestSharp;
using System.Text.Json;

namespace MyCash.PriceScraper.Core.Services;

internal class Scraper : IScraper
{
    private IStockRepository _stockRepository;
    private readonly IMapper _mapper;
    private readonly IMessageBusClient _messageBus;
    private readonly IServiceProvider _serviceProvider;

    public Scraper(IMapper mapper, IMessageBusClient messageBus, IServiceProvider serviceProvider)
    {
        _mapper = mapper;
        _messageBus = messageBus;
        _serviceProvider = serviceProvider;
    }

    //https://api.nasdaq.com/api/screener/stocks?tableonly=true&limit=25&offset=0&download=true
    public async Task FetchNasdaqStocks(CancellationToken cancellationToken)
    {
        using IServiceScope scope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

        _stockRepository = scope.ServiceProvider.GetRequiredService<IStockRepository>();
        try
        {
            var client = new RestClient("https://api.nasdaq.com/api");
            var request = new RestRequest("screener/stocks?tableonly=true&limit=25&offset=0&download=true");

            client.AddDefaultHeader("User-Agent", "PostmanRuntime/7.29.2");
            client.AddDefaultHeader("Accept", "*/*");

            request.Timeout = 15 * 1000;

            var response = await client.GetAsync(request, cancellationToken);

            Root root;
            cancellationToken.ThrowIfCancellationRequested();
            if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(response.Content))
            {
                root = JsonSerializer.Deserialize<Root>(response.Content!)!;

                var stocks = _mapper.Map<IEnumerable<Stock>>(root.data.rows);
                await _stockRepository.AddRangeAsync(stocks, cancellationToken);

                var stocksDto = _mapper.Map<IEnumerable<StockDto>>(stocks);
                var stocksBusDto = new StocksBusDto(stocksDto, "StocksUpdated");

                _messageBus.Publish(stocksBusDto);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching NASDAQ data: {ex.Message}.");
        }
    }
}
