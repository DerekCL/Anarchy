# Anarchy.exe

A modern, high-performance Discord bot built with .NET 9, focusing on clean architecture and best practices.

## Features

- 🚀 Built with .NET 9 and Discord.NET
- ⚡ Native AOT compilation for optimal performance
- 🔒 Environment-based configuration management
- 📝 Comprehensive logging system
- 🎯 Command handling (currently supports: !ping)
- 🏗️ Dependency injection for modular design
- 🧪 Test-driven development approach

## Getting Started

### Prerequisites

- .NET 9 SDK
- A Discord Bot Token ([Create one here](https://discord.com/developers/applications))

### Configuration

1. Copy `appsettings.json` to `appsettings.Development.json`
2. Add your Discord bot token:
```json
{
  "Discord": {
    "Token": "your-bot-token-here"
  }
}
```

### Running the Bot

```bash
# Development
dotnet run

# Production Build
dotnet publish -c Release
```

## Development

- Follow C# coding standards defined in `.editorconfig`
- Document public members with XML comments
- Update CHANGELOG.md following [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
- Adhere to [Semantic Versioning](https://semver.org/spec/v2.0.0.html)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
