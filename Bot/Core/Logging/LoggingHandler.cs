namespace Bot.Core.Logging;

public static class LoggingHandler
{
    public static Task LogAsync(string message)
    {
        Console.WriteLine(message);
        return Task.CompletedTask;
    }
}
