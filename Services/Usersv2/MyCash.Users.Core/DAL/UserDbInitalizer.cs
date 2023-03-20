using Micro.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyCash.Users.Core.Entities;

namespace MyCash.Users.Core.DAL;

internal class UserDbInitalizer : IDataInitalizer
{
    private readonly HashSet<string> _permissions = new()
    {
        "users"
    };

    private readonly UserDbContext _dbContext;
    private readonly ILogger<UserDbInitalizer> _logger;

    public UserDbInitalizer(UserDbContext dbContext, ILogger<UserDbInitalizer> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task InitAsync()
    {
        if (await _dbContext.Roles.AnyAsync())
            return;

        await AddRolesAsync();
        await _dbContext.SaveChangesAsync();
    }

    private async Task AddRolesAsync()
    {
        await _dbContext.Roles.AddAsync(new Role
        {
            Name = "admin",
            Permissions = _permissions
        });
        await _dbContext.Roles.AddAsync(new Role
        {
            Name = "user",
            Permissions = new List<string>()
        });

        _logger.LogInformation("Roles initalized.");
    }
}
