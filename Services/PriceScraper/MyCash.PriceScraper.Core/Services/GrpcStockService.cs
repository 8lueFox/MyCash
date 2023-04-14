using AutoMapper;
using Grpc.Core;
using MyCash.PriceScraper.Core.Repositories;

namespace MyCash.PriceScraper.Core.Services;

public class GrpcStockService : GrpcStock.GrpcStockBase
{
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;

    public GrpcStockService(IStockRepository stockRepository, IMapper mapper)
    {
        _stockRepository = stockRepository;
        _mapper = mapper;
    }

    public override async Task<StockResponse> GetAllStocks(GetAllRequest request, ServerCallContext context)
    {
        var stocks = await _stockRepository.GetAllStocks();
        var stocksMapped = _mapper.Map<IEnumerable<GrpcStockModel>>(stocks);

        var response = new StockResponse();
        response.Stock.AddRange(stocksMapped);

        return response;
    }
}