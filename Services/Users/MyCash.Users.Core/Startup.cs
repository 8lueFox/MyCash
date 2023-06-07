using Micro.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCash.Users.Core.DAL;
using MyCash.Users.Core.DAL.Repositories;
using MyCash.Users.Core.Entities;
using MyCash.Users.Core.Repositories;
using MyCash.Users.Core.Services;

namespace MyCash.Users.Core;

public static class Startup
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSingleton<ITokenStorage, HttpContextTokenStorage>()
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly))
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddDbContext<UserDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("Users")))
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
            .AddInitalizer<UserDbInitalizer>()
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddDAL();
    }
}
