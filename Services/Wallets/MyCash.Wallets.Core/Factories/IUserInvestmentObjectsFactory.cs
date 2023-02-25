using MyCash.Wallets.Core.Entities;

namespace MyCash.Wallets.Core.Factories;

public interface IUserInvestmentObjectsFactory
{
    Task<UserInvestmentObjects> CreateAsync(UserId userId, CancellationToken cancellationToken = default);
}
