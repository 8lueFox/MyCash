using MyCash.WealthManager.Core.Entities;

namespace MyCash.WealthManager.Application.Services;

public interface IUserDataClient
{
    IEnumerable<User> ReturnAllUsers();
}
