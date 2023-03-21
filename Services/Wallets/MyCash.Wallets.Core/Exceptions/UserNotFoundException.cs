namespace MyCash.Wallets.Core.Exceptions;

public class UserNotFoundException : CustomException
{
    public UserId Id { get; }

    public UserNotFoundException(UserId id)
        :base($"Cannot found user {id}.")
        => Id = id;
}
