using Bot.Core.Logging;
using Discord;

namespace Bot.Discord.Handlers;

/// <summary>
/// Handles Discord logging events.
/// </summary>
public static class DiscordLoggingHandler
{
    /// <summary>
    /// Handles Discord log messages by forwarding them to the application logger.
    /// </summary>
    /// <param name="msg">The Discord log message to handle.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static Task HandleLogAsync(LogMessage msg)
    {
        return LoggingHandler.LogAsync(msg.ToString());
    }
}
