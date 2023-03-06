using MyCash.Users.Core.ValueObjects;

namespace MyCash.Users.Core.DomainServices;

public interface ITokenStorage
{
    void Set(JsonWebToken jwt);
    JsonWebToken? Get();
}
