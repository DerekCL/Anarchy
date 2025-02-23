using Microsoft.Extensions.Configuration;

namespace Bot.Core.Configuration;

/// <summary>
/// Manages application configuration settings from appsettings.json files.
/// </summary>
public class ConfigurationService
{
    private readonly IConfiguration configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurationService"/> class.
    /// </summary>
    /// <param name="configuration">The configuration instance to use.</param>
    public ConfigurationService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    /// <summary>
    /// Creates a new instance of ConfigurationService with the default configuration setup.
    /// </summary>
    /// <returns>A new instance of ConfigurationService.</returns>
    public static ConfigurationService CreateDefault()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        if (environment.Equals("Development", StringComparison.OrdinalIgnoreCase))
        {
            _ = builder.AddJsonFile(
                "appsettings.Development.json",
                optional: true,
                reloadOnChange: true
            );
        }

        return new ConfigurationService(builder.Build());
    }

    /// <summary>
    /// Gets a configuration value from the specified section and key.
    /// </summary>
    /// <param name="section">The configuration section name.</param>
    /// <param name="key">The configuration key.</param>
    /// <returns>The configuration value.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the configuration value is not found.</exception>
    public string GetValue(string section, string key)
    {
        return this.configuration.GetSection(section)[key]
            ?? throw new InvalidOperationException(
                $"{section}:{key} is not configured in appsettings.json"
            );
    }
}
