using AutoMapper;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Mapster;
using MyCash.Tests.Benchmarks.Mapper.Models;
using Nelibur.ObjectMapper;

namespace MyCash.Tests.Benchmarks.Mapper;

[MemoryDiagnoser(false)]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class Benchmarks
{
    private readonly IEnumerable<StockDto> _stocksDtos = TestData.ExampleStocks;
    private readonly StockDto _stockDto = TestData.ExampleStock;
    private readonly IMapper _autoMapper;
    private readonly MapperlyMapper _mapperlyMapper;

    public Benchmarks()
    {
        // AutoMapper Configuration
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<StockDto, Stock>();
        });

        _autoMapper = mapperConfig.CreateMapper();

        // Mapperly requires other partial class to define which mapping should be available 
        _mapperlyMapper = new MapperlyMapper();

        // TinyMapper Configuration
        TinyMapper.Bind<StockDto, Stock>();
        TinyMapper.Bind<List<StockDto>, List<Stock>>();

        //Mapster don't need configuration
    }

    [Benchmark]
    public Stock AutoMapperSingle()
    {
        return _autoMapper.Map<Stock>(_stockDto);
    }

    [Benchmark]
    public List<Stock> AutoMapperList()
    {
        return _autoMapper.Map<IEnumerable<Stock>>(_stocksDtos).ToList();
    }

    [Benchmark]
    public Stock MapperlySingle()
    {
        return _mapperlyMapper.Map(_stockDto);
    }

    [Benchmark]
    public List<Stock> MapperlyList()
    {
        return _mapperlyMapper.Map(_stocksDtos).ToList();
    }

    [Benchmark]
    public Stock MapsterSingle()
    {
        return _stockDto.Adapt<Stock>();
    }

    [Benchmark]
    public List<Stock> MapsterList()
    {
        return _stocksDtos.Adapt<List<Stock>>();
    }

    [Benchmark]
    public Stock TinyMapperSingle()
    {
        return TinyMapper.Map<Stock>(_stockDto);
    }

    [Benchmark]
    public List<Stock> TinyMapperList()
    {
        return TinyMapper.Map<List<Stock>>(_stocksDtos.ToList());
    }
}
