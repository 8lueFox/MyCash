using MyCash.WealthManager.Core.Exceptions;

namespace MyCash.WealthManager.Core.Types;

public record IncomeId
{
    public Guid Value { get; }

    public IncomeId(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidEntityIdException(value);

        Value = value;
    }

    public static IncomeId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(IncomeId obj)
        => obj.Value;

    public static implicit operator IncomeId(Guid value)
        => new(value);
}