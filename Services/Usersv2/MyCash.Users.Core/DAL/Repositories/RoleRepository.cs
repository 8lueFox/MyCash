using Microsoft.EntityFrameworkCore;
using MyCash.Users.Core.Entities;
using MyCash.Users.Core.Repositories;

namespace MyCash.Users.Core.DAL.Repositories;

internal class RoleRepository : IRoleRepository
{
    private readonly UserDbContext _userDbContext;
    private readonly DbSet<Role> _roles;

    public RoleRepository(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
        _roles = _userDbContext.Roles;
    }

    public async Task AddAsync(Role role)
    {
        await _roles.AddAsync(role);
        await _userDbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Role>> GetAllAsync()
        => await _roles.ToListAsync();

    public Task<Role?> GetAsync(string name)
        => _roles.SingleOrDefaultAsync(x => x.Name == name);
}
