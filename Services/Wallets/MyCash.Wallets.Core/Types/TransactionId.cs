namespace MyCash.Wallets.Core.Types;

public record TransactionId
{
    public Guid Value { get; }

    public TransactionId(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidEntityIdException(value);

        Value = value;
    }

    public static InvestmentObjectId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(TransactionId obj)
        => obj.Value;

    public static implicit operator TransactionId(Guid value)
        => new(value);
}