using Micro.Auth.JWT;

namespace MyCash.Users.Core.Services;

public interface ITokenStorage
{
    void Set(JsonWebToken jwt);
    JsonWebToken? Get();
}
