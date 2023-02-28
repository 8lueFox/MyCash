namespace MyCash.Wallets.Core.ValueObjects;

public sealed record UserInvestmentObjectName
{
    public string Value { get; }

    public UserInvestmentObjectName(string value)
    {
        if (value is null || value.Length < 3)
            throw new InvalidInvestmentObjectException(value);

        Value = value;
    }

    public static implicit operator string(UserInvestmentObjectName obj)
        => obj.Value;

    public static implicit operator UserInvestmentObjectName(string value)
        => new(value);
}
