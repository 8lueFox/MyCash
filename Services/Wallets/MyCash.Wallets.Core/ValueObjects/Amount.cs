namespace MyCash.Wallets.Core.ValueObjects;

public sealed record Amount
{
    public decimal Count { get; }
    public decimal Price { get; }
    public string Currency { get; }

    public Amount(decimal count, string currency, decimal price)
    {
        if (count is 0 || currency is null || price is 0)
            throw new InvalidAmountException(count, price, currency);
        Count = count;
        Price = price;
        Currency = currency;
    }

    public override string ToString() => $"{Count} - {Price}{Currency}";
}