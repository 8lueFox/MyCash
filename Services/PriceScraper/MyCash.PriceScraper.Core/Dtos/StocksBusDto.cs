namespace MyCash.PriceScraper.Core.Dtos;

internal record StocksBusDto(IEnumerable<StockDto> Stocks, string Event);
