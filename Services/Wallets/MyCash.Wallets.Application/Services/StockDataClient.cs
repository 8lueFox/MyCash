using AutoMapper;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using MyCash.PriceScraper.Core;
using MyCash.Wallets.Core;
using MyCash.Wallets.Core.Entities;

namespace MyCash.Wallets.Application.Services;

public class StockDataClient : IStockDataClient
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public StockDataClient(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }


    public IEnumerable<Stock> ReturnAllStocks()
    {
        Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcStock"]}");

        var channel = GrpcChannel.ForAddress(_configuration["GrpcStock"]);
        var client = new GrpcStock.GrpcStockClient(channel);
        var request = new PriceScraper.Core.GetAllRequest();

        try
        {
            var reply = client.GetAllStocks(request);
            return _mapper.Map<IEnumerable<Stock>>(reply.Stock.ToList());
        }
        catch(Exception ex)
        {
            Console.WriteLine($"--> Couldn't call GRPC Server {ex.Message}");
            return new List<Stock>();
        }
    }
}