using Microsoft.Extensions.DependencyInjection;
using MyCash.WealthManager.Core.DomainServices;
using MyCash.WealthManager.Core.Factories;

namespace MyCash.WealthManager.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
        => services
           .AddTransient<IFamilyFactory, FamilyFactory>()
           .AddSingleton<IFamilyService, FamilyService>();
}
