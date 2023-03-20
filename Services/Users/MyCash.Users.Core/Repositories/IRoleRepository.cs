using MyCash.Users.Core.Entities;

namespace MyCash.Users.Core.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetAsync(string name);
    Task<IReadOnlyList<Role>> GetAllAsync();
    Task AddAsync(Role role);
}
