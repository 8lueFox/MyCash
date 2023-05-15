using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyCash.WealthManager.Core.Repositories;
using MyCash.WealthManager.Infrastructure.DAL;
using MyCash.WealthManager.Infrastructure.DAL.Repositories;

namespace MyCash.WealthManager.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    => services
            .AddDbContext<WealthDbContext>(opt =>
                opt.UseInMemoryDatabase("WealthDb"))
            .AddScoped<IFamilyRepository, FamilyRepository>();
}
