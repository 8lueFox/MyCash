using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyCash.Wallets.Core.DomainServices;
using MyCash.Wallets.Core.Policies;
using System.Reflection;

namespace MyCash.Wallets.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assemblies = typeof(IInvestmentObjectPolicy).Assembly;
        services.AddSingleton<IUserInvestmentObjectsService, UserInvestmentObjectsService>();

        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IInvestmentObjectPolicy)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}
