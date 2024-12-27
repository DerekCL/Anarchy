using Bot.Core.Logging;
using Discord;

namespace Bot.Discord.Handlers;

public static class DiscordLoggingHandler
{
    public static Task HandleLogAsync(LogMessage msg)
    {
        return LoggingHandler.LogAsync(msg.ToString());
    }
}
