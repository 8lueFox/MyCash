using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Core.Entities;

public class User
{
    public UserId Id { get; private set; }
    public UserPackage UserPackage { get; private set; }

    public User(UserId id, UserPackage userPackage)
    {
        Id = id;
        UserPackage = userPackage;
    }
}
