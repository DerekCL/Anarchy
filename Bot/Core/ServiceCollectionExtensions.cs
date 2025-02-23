using Bot.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.Core;

/// <summary>
/// Extension methods for configuring core services in the dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds core services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to add core services to.</param>
    /// <returns>The service collection for method chaining.</returns>
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        _ = services.AddSingleton<ConfigurationService>();
        return services;
    }
}
