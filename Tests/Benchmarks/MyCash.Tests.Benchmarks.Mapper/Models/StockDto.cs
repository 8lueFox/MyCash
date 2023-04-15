namespace MyCash.Tests.Benchmarks.Mapper.Models;

public class StockDto
{
    public string? Symbol { get; set; }

    public string? Name { get; set; }

    public decimal? LastSale { get; set; }

    public decimal? NetChange { get; set; }

    public decimal? Change { get; set; }

    public decimal? PctChange { get; set; }

    public decimal? Volume { get; set; }

    public decimal? MarketCap { get; set; }

    public string? Country { get; set; }

    public string? Ipoyear { get; set; }

    public string? Industry { get; set; }

    public string? Sector { get; set; }

    public string? Url { get; set; }

    public DateTime FetchData { get; set; }
}
