namespace MyCash.Wallets.Application.DTO;

internal record StocksBusDto(IEnumerable<StockDto> Stocks, string Event);
