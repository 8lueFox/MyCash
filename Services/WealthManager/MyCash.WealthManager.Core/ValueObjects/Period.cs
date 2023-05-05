namespace MyCash.WealthManager.Core.ValueObjects;

public record Period(int Days)
{
    public const int Daily = 1;
    public const int Weekly = 7;
    public const int Monthly = 30;
    public const int SemiAnnually = 182;
    public const int Annually = 356;

    public static implicit operator int(Period type)
            => type.Days;

    public static implicit operator Period(string value)
        => new(value);
}
