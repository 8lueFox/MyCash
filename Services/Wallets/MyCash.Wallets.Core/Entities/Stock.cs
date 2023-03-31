namespace MyCash.Wallets.Core.Entities;

public class Stock
{
    public StockId Id { get; set; } = null!;

    public string Symbol { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

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

    public void Update(Stock stock)
    {
        Symbol = stock.Symbol;
        Name = stock.Name;
        LastSale = stock.LastSale;
        NetChange = stock.NetChange;
        Change = stock.Change;
        PctChange = stock.PctChange;
        Volume = stock.Volume;
        MarketCap = stock.MarketCap;
        Country = stock.Country;
        Ipoyear = stock.Ipoyear;
        Industry = stock.Industry;
        Sector = stock.Sector;
        Url = stock.Url;
        FetchData = stock.FetchData;
    }
}
