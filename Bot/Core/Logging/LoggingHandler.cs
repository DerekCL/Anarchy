namespace Bot.Core.Logging;

/// <summary>
/// Provides logging functionality for the application.
/// </summary>
public static class LoggingHandler
{
    /// <summary>
    /// Logs a message to the console asynchronously.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <returns>A completed task.</returns>
    public static Task LogAsync(string message)
    {
        Console.WriteLine(message);
        return Task.CompletedTask;
    }
}
