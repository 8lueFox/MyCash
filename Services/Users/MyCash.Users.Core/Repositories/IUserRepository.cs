using MyCash.Users.Core.Entites;

namespace MyCash.Users.Core.Repositories;

internal interface IUserRepository
{
    Task<User?> GetAsync(string email);
    Task AddAsync(User user);
}
