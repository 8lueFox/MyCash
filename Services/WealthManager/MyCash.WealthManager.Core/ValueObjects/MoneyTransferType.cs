namespace MyCash.WealthManager.Core.ValueObjects;

public record MoneyTransferType(string Value)
{
    public const string Periodical = nameof(Periodical);
    public const string Disposable = nameof(Disposable);

    public const string Default = nameof(Disposable);

    public static implicit operator string(MoneyTransferType type)
        => type.Value;

    public static implicit operator MoneyTransferType(string value)
        => new(value);
}
