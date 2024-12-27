using Bot.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        _ = services.AddSingleton<ConfigurationService>();
        return services;
    }
}
