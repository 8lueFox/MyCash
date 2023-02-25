using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.Types;

namespace MyCash.Wallets.Core.Repositories;

public interface IUserInvestmentObjectRepository
{
    Task<UserInvestmentObjects?> GetUserInvestmentObjectsAsync(UserId userId, CancellationToken cancellationToken);
    Task<IEnumerable<Transaction>> GetTransactionsForInvestmentObject(UserId userId, InvestmentObjectId investmentObjectsId, CancellationToken cancellationToken);
    Task AddAsync(UserInvestmentObjects userInvestmentObjects, CancellationToken cancellationToken);
    Task UpdateAsync(UserInvestmentObjects userInvestmentObjects, CancellationToken cancellationToken);
}
