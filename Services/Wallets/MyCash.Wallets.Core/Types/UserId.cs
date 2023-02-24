namespace MyCash.Wallets.Core.Types;

public record UserId
{
    public Guid Value { get; }

    public UserId(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidEntityIdException(value);

        Value = value;
    }

    public static InvestmentObjectId Create() => new(Guid.NewGuid());

    public static implicit operator UserId(TransactionId obj)
        => obj.Value;

    public static implicit operator UserId(Guid value)
        => new(value);
}