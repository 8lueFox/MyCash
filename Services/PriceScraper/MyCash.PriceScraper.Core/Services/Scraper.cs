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

internal class Scraper : IScraperProcessor
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

    public void ProcessEvent(string message)
    {
        var eventType = DetermineEvent(message);

        switch (eventType)
        {
            case EventType.NasdaqFetched:
                SaveAndPublicNasdaq(message);
                break;
            default:
                break;
        }
    }

    private async void SaveAndPublicNasdaq(string message)
    {
        using IServiceScope scope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

        _stockRepository = scope.ServiceProvider.GetRequiredService<IStockRepository>();
        var response = JsonSerializer.Deserialize<NasdaqPricesDto>(message);
        var root = JsonSerializer.Deserialize<Root>(response.Content);

        var stocks = _mapper.Map<IEnumerable<Stock>>(root.data.rows);
        await _stockRepository.AddRangeAsync(stocks);

        var stocksDto = _mapper.Map<IEnumerable<StockDto>>(stocks);
        var stocksBusDto = new StocksBusDto(stocksDto, "StocksUpdated");

        _messageBus.Publish(stocksBusDto);
    }

    private static EventType DetermineEvent(string message)
    {
        var eventType = JsonSerializer.Deserialize<BusPublishDto>(message);

        if (eventType is null)
            return EventType.Undetermined;

        return eventType.Event switch
        {
            "NasdaqFetched" => EventType.NasdaqFetched,
            _ => EventType.Undetermined
        };
    }
}

enum EventType
{
    Undetermined,
    NasdaqFetched
}