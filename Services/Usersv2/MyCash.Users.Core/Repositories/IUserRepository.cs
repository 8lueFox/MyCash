using MyCash.Users.Core.Entites;

namespace MyCash.Users.Core.Repositories;

public interface IUserRepository
{
    Task<User?> GetAsync(string email);
    Task AddAsync(User user);
}
