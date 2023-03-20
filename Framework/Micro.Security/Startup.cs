using Micro.Security.Encryption;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.Security;

public static class Startup
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
        => services
            .AddSingleton<IPasswordManager, PasswordManager>()
            .AddSingleton<IPasswordHasher<object>, PasswordHasher<object>>();
}
