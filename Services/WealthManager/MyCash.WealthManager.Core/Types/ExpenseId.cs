using MyCash.WealthManager.Core.Exceptions;

namespace MyCash.WealthManager.Core.Types;

public record ExpenseId
{
    public Guid Value { get; }

    public ExpenseId(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidEntityIdException(value);

        Value = value;
    }

    public static ExpenseId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(ExpenseId obj)
        => obj.Value;

    public static implicit operator ExpenseId(Guid value)
        => new(value);
}