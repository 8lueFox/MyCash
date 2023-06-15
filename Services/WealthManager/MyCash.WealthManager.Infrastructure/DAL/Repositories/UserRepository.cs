using Microsoft.EntityFrameworkCore;
using MyCash.WealthManager.Core.Entities;
using MyCash.WealthManager.Core.Repositories;
using MyCash.WealthManager.Core.Types;

namespace MyCash.WealthManager.Infrastructure.DAL.Repositories;
internal sealed class UserRepository : IUserRepository
{
    private readonly WealthDbContext _context;
    private readonly DbSet<User> _users;

    public UserRepository(WealthDbContext context)
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
