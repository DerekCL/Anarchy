using Discord.WebSocket;

namespace Bot.Discord.Handlers;

public static class DiscordMessageHandler
{
    public static async Task HandleMessageAsync(SocketMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);

        if (message.Author.IsBot || message is not SocketUserMessage)
        {
            return;
        }

        if (message.Content.Equals("!ping", StringComparison.OrdinalIgnoreCase))
        {
            _ = await message.Channel.SendMessageAsync("Pong! 🏓").ConfigureAwait(false);
        }
    }
}
