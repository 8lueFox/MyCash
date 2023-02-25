﻿using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.Repositories;

namespace MyCash.Wallets.Core.Factories;

public class UserInvestmentObjectsFactory : IUserInvestmentObjectsFactory
{
    private readonly IUserRepository _userRepository;

    public UserInvestmentObjectsFactory(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserInvestmentObjects> CreateAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(userId, cancellationToken);
        if (user is null)
            throw new UserNotFoundException(userId);

        return new UserInvestmentObjects(AggregateId.Create(), user);
    }
}
