using System.IO.Abstractions.TestingHelpers;
using NUnit.Framework;

namespace Bot.Core.Configuration;

/// <summary>
/// Tests for the <see cref="ConfigurationService"/> class.
/// </summary>
[TestFixture]
public class ConfigurationServiceTests
{
    private const string TestAppSettings = @"{
        ""Discord"": {
            ""Token"": ""test-token""
        }
    }";

    private MockFileSystem fileSystem;
    private string baseDir;

    /// <summary>
    /// Sets up the test environment before each test.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        this.baseDir = AppContext.BaseDirectory;
        this.fileSystem = new MockFileSystem();
        this.fileSystem.AddFile(Path.Combine(this.baseDir, "appsettings.json"), new MockFileData(TestAppSettings));
    }

    /// <summary>
    /// Tests that existing configuration values can be retrieved.
    /// </summary>
    [Test]
    public void ShouldReturnValueWhenKeyExists()
    {
        // Arrange
        var service = new ConfigurationService();

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
        var service = new ConfigurationService();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => service.GetValue("NonExistent", "Key"));
    }
}
