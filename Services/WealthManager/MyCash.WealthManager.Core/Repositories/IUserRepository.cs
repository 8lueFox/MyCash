using MyCash.WealthManager.Core.Entities;
using MyCash.WealthManager.Core.Types;

namespace MyCash.WealthManager.Core.Repositories;

public interface IUserRepository
{
    Task<User?> GetAsync(UserId userId, CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task<bool> ExternalUserExists(UserId externalUserId);
}
