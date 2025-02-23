using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Bot.Core.Configuration;

/// <summary>
/// Tests for the <see cref="ConfigurationService"/> class.
/// </summary>
[TestFixture]
public class ConfigurationServiceTests
{
    private IConfiguration configuration;

    /// <summary>
    /// Sets up the test environment before each test.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var inMemorySettings = new Dictionary<string, string?>(StringComparer.Ordinal)
        {
            { "Discord:Token", "test-token" },
        };

        this.configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
    }

    /// <summary>
    /// Tests that existing configuration values can be retrieved.
    /// </summary>
    [Test]
    public void ShouldReturnValueWhenKeyExists()
    {
        // Arrange
        var service = new ConfigurationService(this.configuration);

        // Act
        var token = service.GetValue("Discord", "Token");

        // Assert
        Assert.That(token, Is.EqualTo("test-token"));
    }

    /// <summary>
    /// Tests that an exception is thrown when accessing non-existent configuration values.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the configuration value is not found.</exception>
    [Test]
    public void ShouldThrowExceptionWhenKeyDoesNotExist()
    {
        // Arrange
        var service = new ConfigurationService(this.configuration);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => service.GetValue("NonExistent", "Key"));
    }
}
