using Micro.Auth;
using Micro.Messaging.RabbitMQ;
using Micro.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Micro.Framework;

public static class Startup
{
    public static IServiceCollection AddMicroFramework(this IServiceCollection services, IConfiguration config)
    {
        var appInfo = config.GetSection("app").Get<AppInfo>();
        if (appInfo is null)
            throw new InvalidOperationException("Missing section 'app'.");

        services.AddSingleton(appInfo);

        services.AddMicro();
        services.AddAuth(config);
        services.AddSecurity();
        services.AddRabbitMq(config);

        return services;
    }
}
