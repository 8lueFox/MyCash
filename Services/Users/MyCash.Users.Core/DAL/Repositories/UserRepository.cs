using Microsoft.EntityFrameworkCore;
using MyCash.Users.Core.Entities;
using MyCash.Users.Core.Repositories;

namespace MyCash.Users.Core.DAL.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;
    private readonly DbSet<User> _users;

    public UserRepository(UserDbContext context)
    {
        _context = context;
        _users = _context.Users;    
    }

    public async Task AddAsync(User user)
    {
        await _users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
        => await _users.ToListAsync();

    public Task<User?> GetAsync(string email)   
        => _users
            //.Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Email == email);
}
