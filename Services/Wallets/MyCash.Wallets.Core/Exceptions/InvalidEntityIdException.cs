using MyCash.Micro.Exceptions;

namespace MyCash.Wallets.Core.Exceptions;

internal class InvalidEntityIdException : CustomException
{
    public object Id { get; }

    public InvalidEntityIdException(object id)
        : base($"Cannot set: {id} as entity identifier.")
        => Id = id;
}