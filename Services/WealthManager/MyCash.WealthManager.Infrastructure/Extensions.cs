using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCash.WealthManager.Core.Repositories;
using MyCash.WealthManager.Infrastructure.DAL;
using MyCash.WealthManager.Infrastructure.DAL.Repositories;

namespace MyCash.WealthManager.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    => services
            .AddDbContext<WealthDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("Wealth")))
            .AddScoped<IFamilyRepository, FamilyRepository>();
}
