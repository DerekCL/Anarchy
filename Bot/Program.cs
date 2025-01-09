// <copyright file="Program.cs" company="DerekCL">
// Copyright (c) {year} DerekCL. All rights reserved.
// </copyright>

namespace Bot;

using Bot.Core;
using Bot.Discord;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Entry point for the Discord bot application.
/// </summary>
public static class Program
{
    /// <summary>
    /// Application entry point that configures and starts the Discord bot.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static Task Main()
    {
        IServiceCollection services = new ServiceCollection().AddCore().AddDiscordBot();

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        DiscordBotService botService = serviceProvider.GetRequiredService<DiscordBotService>();
        return botService.RunAsync();
    }
}
