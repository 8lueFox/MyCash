using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MyCash.Wallets.Core.Factories;

namespace MyCash.Wallets.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
        => services.AddTransient<IUserInvestmentObjectsFactory, UserInvestmentObjectsFactory>()
                   .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
}
