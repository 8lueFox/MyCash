using Microsoft.EntityFrameworkCore;
using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.Repositories;
using MyCash.Wallets.Core.Types;

namespace MyCash.Wallets.Infrastructure.DAL.Repositories;

internal sealed class UserInvestmentObjectsRepository : IUserInvestmentObjectRepository
{
    private readonly DbSet<UserInvestmentObjects> _userInvestmentObjects;
    private readonly WalletDbContext _dbContext;

    public UserInvestmentObjectsRepository(WalletDbContext dbContext)
    {
        _dbContext = dbContext;
        _userInvestmentObjects = dbContext.UsersInvestmentObjects;
    }

    public async Task AddAsync(UserInvestmentObjects userInvestmentObjects, CancellationToken cancellationToken)
    {
        await _userInvestmentObjects.AddAsync(userInvestmentObjects, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsForInvestmentObject(UserId userId, InvestmentObjectId investmentObjectsId, CancellationToken cancellationToken)
    {
        if(userId is null || investmentObjectsId is null)
            return new HashSet<Transaction>();

        var userInvestmentObjects = await _userInvestmentObjects.SingleOrDefaultAsync(x => x.UserId == userId);

        if (userInvestmentObjects is null)
            return new HashSet<Transaction>();

        return userInvestmentObjects.InvestmentObjects.SingleOrDefault(x => x.Id == investmentObjectsId).Transactions;
    }

    public async Task<UserInvestmentObjects?> GetUserInvestmentObjectsAsync(UserId userId, CancellationToken cancellationToken)
    {
        var userInvestmentObjects = await _userInvestmentObjects.Where(x => x.UserId == userId).SingleOrDefaultAsync();
        return userInvestmentObjects;
    }

    public async Task UpdateAsync(UserInvestmentObjects userInvestmentObjects, CancellationToken cancellationToken)
    {
        _userInvestmentObjects.Update(userInvestmentObjects);
        await _dbContext.SaveChangesAsync();
    }
}
