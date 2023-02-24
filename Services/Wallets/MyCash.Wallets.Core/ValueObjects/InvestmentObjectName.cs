namespace MyCash.Wallets.Core.ValueObjects;

public sealed record InvestmentObjectName
{
    public string Value { get; }

    public InvestmentObjectName(string value)
    {
        if (value is null || value.Length < 3)
            throw new InvalidInvestmentObjectException(value);

        Value = value;
    }

    public static implicit operator string(InvestmentObjectName obj)
        => obj.Value;

    public static implicit operator InvestmentObjectName(string value)
        => new(value);
}
