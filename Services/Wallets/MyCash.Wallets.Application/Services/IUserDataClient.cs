using MyCash.Wallets.Core.Entities;

namespace MyCash.Wallets.Application.Services;

public interface IUserDataClient
{
    IEnumerable<User> ReturnAllUsers();
}
