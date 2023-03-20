using Micro.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyCash.Users.Core.DAL;
using MyCash.Users.Core.DAL.Repositories;
using MyCash.Users.Core.Entities;
using MyCash.Users.Core.Repositories;
using MyCash.Users.Core.Services;
using System.Reflection;

namespace MyCash.Users.Core;

public static class Startup
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services
            .AddSingleton<ITokenStorage, HttpContextTokenStorage>()
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly))
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddDbContext<UserDbContext>(opt =>
                opt.UseInMemoryDatabase("MyCashDb"))
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
            .AddInitalizer<UserDbInitalizer>()
            .AddDAL();
    }
}
