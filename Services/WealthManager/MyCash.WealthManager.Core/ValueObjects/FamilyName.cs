namespace MyCash.WealthManager.Core.ValueObjects;
public sealed record FamilyName
{
    public string Value { get; }

    public FamilyName(string value)
    {
        if (value is null || value.Length < 3)
            throw new InvalidInvestmentObjectException(value);

        Value = value;
    }

    public static implicit operator string(FamilyName obj)
        => obj.Value;

    public static implicit operator FamilyName(string value)
        => new(value);
}
