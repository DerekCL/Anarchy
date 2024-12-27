using Bot.Core.Logging;
using Discord.WebSocket;

namespace Bot.Discord.Handlers;

public class DiscordReadyHandler(DiscordSocketClient client)
{
    private readonly DiscordSocketClient _client = client;

    public Task HandleReadyAsync()
    {
        return LoggingHandler.LogAsync($"{_client.CurrentUser} is connected!");
    }
}
