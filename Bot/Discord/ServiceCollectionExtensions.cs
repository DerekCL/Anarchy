using Bot.Discord.Handlers;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.Discord;

/// <summary>
/// Extension methods for configuring Discord bot services in the dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Discord bot services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to add Discord bot services to.</param>
    /// <returns>The service collection for method chaining.</returns>
    public static IServiceCollection AddDiscordBot(this IServiceCollection services)
    {
        // Core Discord services
        _ = services.AddSingleton<DiscordSocketClient>();
        _ = services.AddSingleton<DiscordBotService>();

        // Discord handlers
        _ = services.AddSingleton<DiscordReadyHandler>();

        return services;
    }
}
