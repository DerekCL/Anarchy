using Bot.Core.Logging;
using Discord.WebSocket;

namespace Bot.Discord.Handlers;

/// <summary>
/// Handles the Discord client's ready state events.
/// </summary>
/// <param name="client">The Discord client instance.</param>
public class DiscordReadyHandler(DiscordSocketClient client)
{
    private readonly DiscordSocketClient discordClient = client;

    /// <summary>
    /// Handles the Discord ready event by logging the connected user.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task HandleReadyAsync()
    {
        return LoggingHandler.LogAsync($"{this.discordClient.CurrentUser} is connected!");
    }
}
