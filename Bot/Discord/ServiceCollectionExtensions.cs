using Bot.Discord.Handlers;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.Discord;

public static class ServiceCollectionExtensions
{
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
