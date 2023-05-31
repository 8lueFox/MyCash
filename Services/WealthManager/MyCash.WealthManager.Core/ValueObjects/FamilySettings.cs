namespace MyCash.WealthManager.Core.ValueObjects;

public class FamilySettings
{
    public string Currency { get; set; } = "USD";
    public string Colour { get; set; } = "green";
    public decimal ExpectedMonthyExpenses { get; set; }
}
