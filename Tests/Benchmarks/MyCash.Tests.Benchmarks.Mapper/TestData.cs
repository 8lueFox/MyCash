using MyCash.Tests.Benchmarks.Mapper.Models;

namespace MyCash.Tests.Benchmarks.Mapper;

internal static class TestData
{
    public static StockDto ExampleStock = new StockDto
    {
        Name = "TestStock",
        Symbol = "TS",
        Country = "NonexistentCountry",
        Industry = "SomeIndustry",
        Ipoyear = "2023",
        Sector = "TestingSector",
        Url = "/testingSector/",
        LastSale = 153442,
        Change = 5.3M,
        MarketCap = 1,
        NetChange = 213,
        PctChange = 1.5M,
        Volume = 1000000000000000000000M,
        FetchData = DateTime.Now
    };

    public static IEnumerable<StockDto> ExampleStocks = Enumerable.Repeat(ExampleStock, 1_000);
}
