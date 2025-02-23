# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Changed

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

### Added

- Enhanced C# coding standards in .editorconfig
  - Consistent section header formatting
  - Consolidated naming conventions
  - Expanded linting and analyzer configurations
  - Modern C# feature support
  - Improved whitespace and indentation rules
  - Standardized accessibility modifier requirements
- XML documentation for ServiceCollectionExtensions class and methods
- XML documentation for DiscordBotService class and methods

### Fixed

- Removed unnecessary async/await in Program.Main method

## [0.1.0] - 2024-03-21

### Added

- Initial Discord bot setup with basic configuration
- Core logging infrastructure
- Discord event handlers for Ready and Message events
- Basic dependency injection setup
- Configuration service for managing bot settings
