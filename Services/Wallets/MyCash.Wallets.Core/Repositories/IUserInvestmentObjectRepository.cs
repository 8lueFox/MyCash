using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.Types;
using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Core.Repositories;

public interface IUserInvestmentObjectRepository
{
    Task<UserInvestmentObjects?> GetUserInvestmentObjectsAsync(UserId userId, CancellationToken cancellationToken);
    Task<UserInvestmentObjects?> GetUserInvestmentObjectAsync(UserId userId, UserInvestmentObjectName userInvestmentObjectName, CancellationToken cancellationToken);
    Task<IEnumerable<Transaction>> GetTransactionsForInvestmentObject(UserId userId, InvestmentObjectId investmentObjectsId, CancellationToken cancellationToken);
    Task AddAsync(UserInvestmentObjects userInvestmentObjects, CancellationToken cancellationToken);
    Task UpdateAsync(UserInvestmentObjects userInvestmentObjects, CancellationToken cancellationToken);
}
