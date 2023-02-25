using MyCash.Wallets.Core.Entities;

namespace MyCash.Wallets.Core.Repositories;

public interface IUserRepository
{
    Task<User?> GetAsync(UserId userId, CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
}
