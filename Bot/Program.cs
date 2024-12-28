using Bot.Core;
using Bot.Discord;
using Microsoft.Extensions.DependencyInjection;

namespace Bot;

public static class Program
{
    public static async Task Main()
    {
        IServiceCollection services = new ServiceCollection()
            .AddCore()
            .AddDiscordBot();

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        DiscordBotService botService =
            serviceProvider.GetRequiredService<DiscordBotService>();
        await botService.RunAsync().ConfigureAwait(false);
    }
}
