using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.Repositories;
using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Core.Factories;

public class UserInvestmentObjectsFactory : IUserInvestmentObjectsFactory
{
    private readonly IUserRepository _userRepository;

    public UserInvestmentObjectsFactory(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserInvestmentObjects> CreateAsync(UserId userId, UserInvestmentObjectName name, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(userId, cancellationToken);
        return user is null ? 
            throw new UserNotFoundException(userId) : 
            new UserInvestmentObjects(AggregateId.Create(), user, name);
    }
}
