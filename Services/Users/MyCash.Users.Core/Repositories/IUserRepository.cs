using MyCash.Users.Core.Entities;

namespace MyCash.Users.Core.Repositories;

public interface IUserRepository
{
    Task<User?> GetAsync(string email);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
}
