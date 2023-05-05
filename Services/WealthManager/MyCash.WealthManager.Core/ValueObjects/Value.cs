namespace MyCash.WealthManager.Core.ValueObjects;

public sealed class Value
{
    public decimal Count { get; set; }
    public string Currency { get; set; } = string.Empty;
}
