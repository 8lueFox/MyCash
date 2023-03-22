﻿using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Core.Entities;

public class User
{
    public UserId Id { get; private set; }
    public UserId ExternalId { get; private set; }
    public UserPackage UserPackage { get; private set; }

    public User(UserId id, UserId externalId, UserPackage userPackage)
    {
        Id = id;
        ExternalId = externalId;
        UserPackage = userPackage;
    }

    public User(UserId id, UserPackage userPackage)
    {
        Id = id;
        ExternalId = Guid.Empty;
        UserPackage = userPackage;
    }
}
