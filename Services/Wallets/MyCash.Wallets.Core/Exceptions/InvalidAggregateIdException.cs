using MyCash.Micro.Exceptions;

namespace MyCash.Wallets.Core.Exceptions;

public class InvalidAggregateIdException : CustomException
{
    public Guid Id { get; }

    public InvalidAggregateIdException(Guid id) : base($"Invalid aggregate id: {id}")
        => Id = id;
}