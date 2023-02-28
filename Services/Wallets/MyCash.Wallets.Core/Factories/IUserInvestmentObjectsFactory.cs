using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Core.Factories;

public interface IUserInvestmentObjectsFactory
{
    Task<UserInvestmentObjects> CreateAsync(UserId userId, UserInvestmentObjectName name, CancellationToken cancellationToken = default);
}
