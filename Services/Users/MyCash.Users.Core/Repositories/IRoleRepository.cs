using MyCash.Users.Core.ValueObjects;

namespace MyCash.Users.Core.Repositories;

internal interface IRoleRepository
{
    Task<Role?> GetAsync(string name);
    Task<IReadOnlyList<Role>> GetAllAsync();
    Task AddAsync(Role role);
}
