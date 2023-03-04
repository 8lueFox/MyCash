namespace MyCash.Wallets.Application.DTO;

public class InvestmentObjectDto
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public decimal Count { get; set; }
    public decimal AvgPrice { get; set; }
    public string? Currency { get; set; }
}
