using Discord;
using Discord.WebSocket;

namespace Bot;

class Program
{
    private DiscordSocketClient _client;
    
    public static Task Main(string[] args) => new Program().MainAsync();

    public async Task MainAsync()
    {
        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        });

        _client.Log += Log;
        _client.Ready += Ready;
        _client.MessageReceived += MessageReceived;

        // Replace "YOUR_BOT_TOKEN" with your bot's token
        string token = Environment.GetEnvironmentVariable("DISCORD_TOKEN") ?? throw new Exception("Please set your bot token in the DISCORD_TOKEN environment variable");
        
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        // Block the program until it is closed.
        await Task.Delay(Timeout.Infinite);
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    private Task Ready()
    {
        Console.WriteLine($"{_client.CurrentUser} is connected!");
        return Task.CompletedTask;
    }

    private async Task MessageReceived(SocketMessage message)
    {
        // Ignore system messages and messages from bots
        if (!(message is SocketUserMessage userMessage) || message.Author.IsBot)
            return;

        // Check if the message starts with "!ping"
        if (message.Content.Equals("!ping", StringComparison.OrdinalIgnoreCase))
        {
            await message.Channel.SendMessageAsync("Pong! 🏓");
        }
    }
}
