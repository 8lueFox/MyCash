namespace MyCash.WealthManager.Core.ValueObjects;

public record UserPackage(string Value)
{
    public const string None = nameof(None);
    public const string Standard = nameof(Standard);
    public const string Premium = nameof(Premium);

    public static implicit operator string(UserPackage type)
        => type.Value;

    public static implicit operator UserPackage(string value)
        => new(value);
}