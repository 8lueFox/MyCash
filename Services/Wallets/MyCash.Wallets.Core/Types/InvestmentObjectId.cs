namespace MyCash.Wallets.Core.Types;

public record InvestmentObjectId
{
    public Guid Value { get; }

    public InvestmentObjectId(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidEntityIdException(value);

        Value = value;
    }

    public static InvestmentObjectId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(InvestmentObjectId obj)
        => obj.Value;

    public static implicit operator InvestmentObjectId(Guid value)
        => new(value);
}
