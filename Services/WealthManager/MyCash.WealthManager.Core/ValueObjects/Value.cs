namespace MyCash.WealthManager.Core.ValueObjects;

public sealed class Value
{
    public decimal Count { get; set; }
    public string Currency { get; set; } = string.Empty;

    public Value(decimal count, string currency)
    {
        Count = count;
        Currency = currency;
    }

    public decimal GetValueInSpecificCurrency(string currency)
    {
        if (currency == null || currency == Currency)
            return Count;

        //TODO: Pobieranie kursu walut
        return Count;
    }
}
