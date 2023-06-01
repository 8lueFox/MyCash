using MyCash.WealthManager.Core.Exceptions;

namespace MyCash.WealthManager.Core.Types;

public record BalanceEventId
{
    public Guid Value { get; }

    public BalanceEventId(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidEntityIdException(value);

        Value = value;
    }

    public static BalanceEventId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(BalanceEventId obj)
        => obj.Value;

    public static implicit operator BalanceEventId(Guid value)
        => new(value);
}