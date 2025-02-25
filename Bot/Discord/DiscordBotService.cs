using Bot.Core.Configuration;
using Bot.Discord.Handlers;
using Discord;
using Discord.WebSocket;

namespace Bot.Discord;

/// <summary>
/// Manages the Discord bot's lifecycle and core functionality.
/// </summary>
public sealed class DiscordBotService : IDisposable
{
    private readonly DiscordSocketClient _client;
    private readonly ConfigurationService _configuration;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="DiscordBotService"/> class.
    /// </summary>
    /// <param name="client">The Discord client for bot interactions.</param>
    /// <param name="configuration">The configuration service for bot settings.</param>
    /// <param name="readyHandler">The handler for Discord ready events.</param>
    /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
    public DiscordBotService(
        DiscordSocketClient client,
        ConfigurationService configuration,
        DiscordReadyHandler readyHandler
    )
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(readyHandler);

        _client = client;
        _configuration = configuration;

        _client.Log += DiscordLoggingHandler.HandleLogAsync;
        _client.Ready += readyHandler.HandleReadyAsync;
        _client.MessageReceived += DiscordMessageHandler.HandleMessageAsync;
    }

    /// <summary>
    /// Starts the Discord bot and keeps it running.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task RunAsync()
    {
        var token = _configuration.GetValue("Discord", "Token");

        await _client.LoginAsync(TokenType.Bot, token).ConfigureAwait(false);
        await _client.StartAsync().ConfigureAwait(false);

        // Keep the service running
        await Task.Delay(Timeout.Infinite).ConfigureAwait(false);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _client?.Dispose();
            }

            _disposed = true;
        }
    }
}
