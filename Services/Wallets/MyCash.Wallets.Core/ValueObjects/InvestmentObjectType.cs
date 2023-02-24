namespace MyCash.Wallets.Core.ValueObjects;

public record InvestmentObjectType(string Value)
{
    public const string Stock = nameof(Stock);
    public const string Bond = nameof(Bond);
    public const string Crypto = nameof(Crypto);
    public const string Metal = nameof(Metal);
    public const string Cash = nameof(Cash);

    public static implicit operator string(InvestmentObjectType type)
        => type.Value;

    public static implicit operator InvestmentObjectType(string value)
        => new(value);
}