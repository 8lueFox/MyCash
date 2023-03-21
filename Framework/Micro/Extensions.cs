using Micro.Time;
using Microsoft.Extensions.DependencyInjection;

namespace Micro;

public static class Extensions
{
    public static IServiceCollection AddMicro(this IServiceCollection services)
        => services
            .AddSingleton<IClock, UtcNow>();
}
