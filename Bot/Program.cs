using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Bot;

sealed class DiscordBot : IDisposable
{
    public static async Task Main()
    {
        using var bot = new DiscordBot();
        await bot.RunAsync().ConfigureAwait(false);
    }

    private readonly DiscordSocketClient _client;
    private readonly IConfiguration _configuration;
    private bool _disposed;

    private DiscordBot()
    {
        _client = new DiscordSocketClient(
            new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent,
            }
        );

        var environment =
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        if (environment.Equals("Development", StringComparison.OrdinalIgnoreCase))
        {
            builder.AddJsonFile(
                $"appsettings.Development.json",
                optional: true,
                reloadOnChange: true
            );
        }

        _configuration = builder.Build();
    }

    private async Task RunAsync()
    {
        _client.Log += Log;
        _client.Ready += Ready;
        _client.MessageReceived += MessageReceived;

        var token =
            _configuration.GetSection("Discord")["Token"]
            ?? throw new InvalidOperationException(
                "Discord:Token is not configured in appsettings.Development.json"
            );

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
