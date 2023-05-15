namespace MyCash.WealthManager.Core.Types;

public record UserId
{
    public Guid Value { get; }

    public UserId()
    {
        Value = Guid.NewGuid();
    }

    public UserId(Guid value)
    {
        //if (value == Guid.Empty)
        //     throw new InvalidEntityIdException(value);

        Value = value;
    }

    public static UserId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(UserId obj)
        => obj.Value;

    public static implicit operator UserId(Guid value)
        => new(value);
}