namespace MyCash.WealthManager.Application.DTO;

public class FamilySummaryDto
{
    public string? FamilyName { get; set; }
    public string? Colour { get; set; }
    public string? ExpectedMonthyExpenses { get; set; }
    public string? Currency { get; set; }
    public decimal SumOfIncomes { get; set; }
    public decimal SumOfExpenses { get; set; }
    public decimal CurrentValue { get; set; }
}
