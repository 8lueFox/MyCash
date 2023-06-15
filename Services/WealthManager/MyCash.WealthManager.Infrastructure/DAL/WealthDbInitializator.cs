using MyCash.WealthManager.Application.Services;

namespace MyCash.WealthManager.Infrastructure.DAL;

internal class WealthDbInitializator
{
    private readonly WealthDbContext _context;
    private readonly IUserDataClient _userDataClient;

    public WealthDbInitializator(WealthDbContext context, IUserDataClient userDataClient)
    {
        _context = context;
        _userDataClient = userDataClient;
    }

    internal async Task InitAsync(CancellationToken cancellationToken)
    {
        var users = _userDataClient.ReturnAllUsers();

        users.ToList().ForEach(async user =>
        {
            if(!_context.Users.Any(y => y.ExternalId == user.ExternalId))
                await _context.Users.AddAsync(user);
        });
        await _context.SaveChangesAsync(cancellationToken);
    }
}
