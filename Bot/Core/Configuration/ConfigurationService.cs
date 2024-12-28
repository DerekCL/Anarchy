using Microsoft.Extensions.Configuration;

namespace Bot.Core.Configuration;

public class ConfigurationService
{
    private readonly IConfiguration _configuration;

    public ConfigurationService()
    {
        var environment =
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            ?? "Production";

        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(
                "appsettings.json",
                optional: false,
                reloadOnChange: true
            );

        if (
            environment.Equals(
                "Development",
                StringComparison.OrdinalIgnoreCase
            )
        )
        {
            _ = builder.AddJsonFile(
                "appsettings.Development.json",
                optional: true,
                reloadOnChange: true
            );
        }

        _configuration = builder.Build();
    }

    public string GetValue(string section, string key)
    {
        return _configuration.GetSection(section)[key]
            ?? throw new InvalidOperationException(
                $"{section}:{key} is not configured in appsettings.json"
            );
    }
}
