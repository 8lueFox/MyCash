using Microsoft.Extensions.DependencyInjection;
using MyCash.Auth;
using MyCash.Micro;

namespace MyCash.Framework;

public static class Extensions
{
    public static IServiceCollection AddMicroFramework(this IServiceCollection services)
        => services
            .AddMicro();
}