# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- Added Dockerfile.tests for running tests in Linux containers:
  - Uses .NET 9 SDK base image
  - Configures test environment
  - Runs tests with detailed console logging
- Fixed linting issues in DiscordReadyHandler:
  - Added XML documentation
  - Renamed fields to follow naming convention
  - Added this prefix to local calls
- Fixed linting issues in DiscordBotService:
  - Renamed fields to follow naming convention
  - Added this prefix to local calls
  - Fixed bracing style
- Refined .editorconfig C# coding standards:
  - Disabled SA1009 (closing parenthesis spacing) and SA1111 (closing parenthesis placement)
  - Modified severity of namespace declarations and using directive placements to warning
  - Changed SA1200 severity to none
  - Adjusted various diagnostic severity levels for improved developer experience
- Fixed linting issues in DiscordMessageHandler:
  - Added XML documentation for class and methods
- Fixed linting issues in DiscordLoggingHandler:
  - Added XML documentation for class and methods
- Fixed linting issues in Core ServiceCollectionExtensions:
  - Added XML documentation for class and methods
- Fixed linting issues in LoggingHandler:
  - Added XML documentation for class and methods
- Fixed linting issues in ConfigurationService:
  - Added XML documentation
  - Renamed fields to follow naming convention
  - Added this prefix to local calls
- Reorganized test structure:
  - Moved tests next to their implementation files
  - Removed separate test project
  - Integrated test infrastructure into main project
- Fixed test file naming:
  - Renamed test files to match class names
  - ConfigurationService.Tests.cs → ConfigurationServiceTests.cs
  - DiscordMessageHandler.Tests.cs → DiscordMessageHandlerTests.cs
- Improved test configuration and maintainability:
  - Made ConfigurationService more testable through dependency injection
  - Added factory method for default configuration setup
  - Updated test mocking approach for better readability

### Changed

- Upgraded to .NET 9:
  - Updated target framework
  - Updated Docker base images
  - Updated NuGet package versions
  - Enabled latest C# language features

### Fixed

- Removed unnecessary async/await in Program.Main method
- Fixed linting errors in DiscordMessageHandlerTests:
  - Resolved expression tree issues with optional arguments in SendMessageAsync mocks

## [0.1.0] - 2024-03-21

### Added

- Initial Discord bot setup with basic configuration
- Core logging infrastructure
- Discord event handlers for Ready and Message events
- Basic dependency injection setup
- Configuration service for managing bot settings
