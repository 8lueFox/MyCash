using Micro.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Micro.Framework;

public static class Startup
{
    public static IServiceCollection AddMicroFramework(this IServiceCollection services, IConfiguration config)
    {
        var appInfo = new AppInfo("", "");
        config.GetSection("app").Bind(appInfo);
        services.AddSingleton(appInfo);

        services.AddAuth(config);

        return services;
    }
}
