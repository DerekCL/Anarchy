using Bot.Core.Configuration;
using Bot.Discord.Handlers;
using Discord;
using Discord.WebSocket;

namespace Bot.Discord;

public sealed class DiscordBotService : IDisposable
{
    private readonly DiscordSocketClient _client;
    private readonly ConfigurationService _configuration;
    private bool _disposed;

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

    public async Task RunAsync()
    {
        var token = _configuration.GetValue("Discord", "Token");

        await _client.LoginAsync(TokenType.Bot, token).ConfigureAwait(false);
        await _client.StartAsync().ConfigureAwait(false);

        // Keep the service running
        await Task.Delay(Timeout.Infinite).ConfigureAwait(false);
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

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
