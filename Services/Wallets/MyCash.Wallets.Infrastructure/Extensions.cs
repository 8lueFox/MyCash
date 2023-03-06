using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyCash.Wallets.Core.Repositories;
using MyCash.Wallets.Infrastructure.DAL;
using MyCash.Wallets.Infrastructure.DAL.Repositories;

namespace MyCash.Wallets.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        => services
            .AddDbContext<WalletDbContext>(opt =>
                opt.UseInMemoryDatabase("WalletsDB"))
            .AddScoped<WalletDataInitializator>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserInvestmentObjectRepository, UserInvestmentObjectsRepository>();

    public async static void InitDb(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        var initiazlier = serviceProvider.GetRequiredService<WalletDataInitializator>();

        await initiazlier.InitAsync(cancellationToken);
    }
}
