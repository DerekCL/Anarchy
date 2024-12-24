using Discord;
using Discord.WebSocket;

namespace Bot;

sealed class Program : IDisposable
{
    private readonly DiscordSocketClient _client;
    private bool _disposed;

    public Program()
    {
        _client = new DiscordSocketClient(
            new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent,
            }
        );
    }

    public static async Task Main()
    {
        using var program = new Program();
        await program.MainAsync().ConfigureAwait(false);
    }

    public async Task MainAsync()
    {
        _client.Log += Log;
        _client.Ready += Ready;
        _client.MessageReceived += MessageReceived;

        var token =
            Environment.GetEnvironmentVariable("DISCORD_TOKEN")
            ?? throw new InvalidOperationException("DISCORD_TOKEN environment variable is not set");

        await _client.LoginAsync(TokenType.Bot, token).ConfigureAwait(false);
        await _client.StartAsync().ConfigureAwait(false);

        await Task.Delay(Timeout.Infinite).ConfigureAwait(false);
    }

    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    private Task Ready()
    {
        Console.WriteLine($"{_client.CurrentUser} is connected!");
        return Task.CompletedTask;
    }

    private static async Task MessageReceived(SocketMessage message)
    {
        if (message.Author.IsBot || message is not SocketUserMessage)
            return;

        if (message.Content.Equals("!ping", StringComparison.OrdinalIgnoreCase))
        {
            await message.Channel.SendMessageAsync("Pong! 🏓").ConfigureAwait(false);
        }
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
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
