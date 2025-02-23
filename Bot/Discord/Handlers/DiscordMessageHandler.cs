using Discord.WebSocket;

namespace Bot.Discord.Handlers;

/// <summary>
/// Handles Discord message events and commands.
/// </summary>
public static class DiscordMessageHandler
{
    /// <summary>
    /// Handles incoming Discord messages and responds to commands.
    /// </summary>
    /// <param name="message">The received Discord message.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when message is null.</exception>
    public static async Task HandleMessageAsync(SocketMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);

        if (message.Author.IsBot || message is not SocketUserMessage)
        {
            return;
        }

        if (message.Content.Equals("!ping", StringComparison.OrdinalIgnoreCase))
        {
            _ = await message
                .Channel.SendMessageAsync("Pong! 🏓")
                .ConfigureAwait(false);
        }
    }
}
