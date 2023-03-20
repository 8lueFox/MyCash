using Micro.DAL.Internals;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.DAL;

public static class Startup
{
    public static IServiceCollection AddDAL(this IServiceCollection services)
    {
        services.AddHostedService<DataInitalizer>();

        return services;
    }

    public static IServiceCollection AddInitalizer<T>(this IServiceCollection services)
        where T : class, IDataInitalizer
        => services.AddTransient<IDataInitalizer, T>();
}
