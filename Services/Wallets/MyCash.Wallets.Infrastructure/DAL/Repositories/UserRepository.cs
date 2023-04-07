using Microsoft.EntityFrameworkCore;
using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.Repositories;
using MyCash.Wallets.Core.Types;

namespace MyCash.Wallets.Infrastructure.DAL.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly WalletDbContext _context;
    private readonly DbSet<User> _users;

    public UserRepository(WalletDbContext context)
    {
        _context = context;
        _users = context.Users;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExternalUserExists(UserId externalUserId)
        => await _users.SingleOrDefaultAsync(x => x.ExternalId == externalUserId) is not null;

    public Task<User?> GetAsync(UserId userId, CancellationToken cancellationToken = default)
        => _users.SingleOrDefaultAsync(x => x.Id == userId, cancellationToken);
        
}
