namespace MyCash.Wallets.Core.Types;

public record StockId
{
    public Guid Value { get; }

    public StockId(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidEntityIdException(value);

        Value = value;
    }

    public static StockId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(StockId obj)
        => obj.Value;

    public static implicit operator StockId(Guid value)
        => new(value);
}