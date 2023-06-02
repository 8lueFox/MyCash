namespace MyCash.WealthManager.Core.ValueObjects;

public record BalanceEventType(string Value)
{
    public const string Expense = nameof(Expense);
    public const string Income = nameof(Income);

    public const string Default = nameof(Income);

    public static implicit operator string(BalanceEventType type)
        => type.Value;

    public static implicit operator BalanceEventType(string value)
        => new(value);
}