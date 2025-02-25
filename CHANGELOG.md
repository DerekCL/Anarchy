# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- Significantly enhanced .editorconfig to focus on modern C# practices:
  - Added comprehensive modern C# 12 features section with stronger enforcement
  - Added support for required members, index/range operators, and method group conversion
  - Added pattern matching preferences with warning severity
  - Enhanced expression-bodied member rules to cover all member types
  - Added comprehensive control flow pattern preferences
  - Added type & value preferences for modern coding styles
  - Removed redundant StyleCop suppressions and conflicting settings
  - Consolidated primary constructor rules into a dedicated section
  - Added detailed explanatory comments for each rule
  - Added dedicated Discord.Net specific rules section
  - Promoted key C# 12 features from suggestion to warning level
  - Enforced immutability with readonly field warnings
  - Added warnings for async/await best practices
  - Improved record type usage recommendations
  - Enhanced rules for collection expressions and pattern matching
- Added example classes demonstrating C# 12 primary constructor usage:
  - Created ModernDiscordHandler showing preferred primary constructor pattern
  - Created OldStyleDiscordHandler showing traditional constructor approach
  - Created ModernDiscordService demonstrating primary constructors with calculated properties
  - Added DiscordHandlerExample with practical event handling implementations
  - All examples include full XML documentation and follow modern C# best practices
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
  - Added Discord.Net specific best practices
  - Added Serilog logging best practices
  - Increased severity level of naming rules to warning
  - Added specific AsyncFixer configuration
  - Consolidated Roslynator configuration
  - Added SonarAnalyzer specific rules
  - Enhanced primary constructor support with dedicated section and warnings
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
- Comprehensive logging infrastructure with Serilog
- Microsoft.Extensions.Hosting for improved service management
- Specific Serilog sinks for console and file logging
- Added CodeRabbit configuration based on official schema:
  - Implemented path-specific instructions for Discord.Net components
  - Added style instructions for C# 12 and .NET 9 patterns
  - Configured analysis for best practices, performance, and security
  - Added SonarQube and Semgrep tool integrations
  - Set up web search knowledge base for improved context

### Changed

- Upgraded to .NET 9:
  - Updated target framework
  - Updated Docker base images
  - Updated NuGet package versions
  - Enabled latest C# language features
- Optimized project dependencies by removing redundancies and organizing into logical categories
- Removed StyleCop.Analyzers (redundant with other analyzers)
- Removed redundant Roslynator packages, keeping only core Roslynator.Analyzers
- Removed Menees.Analyzers to reduce overlap
- Reduced Discord.Net packages, keeping only the meta-package and essential components
- Moved AsyncFixer from Database category to Analysis & Code Quality
- Updated .editorconfig to align with dependency changes:
  - Removed StyleCop specific rules and configurations
  - Added modern C# 12 and .NET 9 coding standards
  - Simplified Roslynator configuration to match reduced packages
  - Upgraded primary constructor rule from suggestion to warning
- Fixed CodeRabbit configuration to comply with official schema:
  - Moved Discord.Net specific rules into style_instructions
  - Replaced custom rules with schema-compliant structure
  - Adjusted analysis configuration to match CodeRabbit capabilities
  - Set appropriate test framework and coverage thresholds
  - Emphasized primary constructor usage in style instructions
- Enhanced build configuration for code style enforcement:
  - Added EnforceCodeStyleInBuild property set to true
  - Added AnalysisMode set to All for comprehensive analysis
  - Explicitly included .editorconfig in build process

### Fixed

- Removed unnecessary async/await in Program.Main method
- Fixed linting errors in DiscordMessageHandlerTests:
  - Resolved expression tree issues with optional arguments in SendMessageAsync mocks
  - Added 'Async' suffix to async test methods to follow naming convention
  - Replaced 'var' with explicit types for collection variables
  - Fixed unused expression values using discard operator
  - Improved parameter naming in lambda expressions for better readability
  - Used named parameters in Returns method calls
  - Converted to file-scoped namespace
  - Added ConfigureAwait to awaited tasks
- Fixed naming rule violation in ConfigurationService by adding underscore prefix to private fields
- Fixed ConfigurationService linting issues:
  - Added underscore prefix to private fields to follow naming convention
  - Converted to use primary constructor
  - Simplified name references in methods
- Fixed ConfigurationServiceTests linting issues:
  - Added underscore prefix to private fields to follow naming convention
  - Simplified name references by removing unnecessary 'this.' prefixes
  - Fixed unused expression value warning by using discard operator
- Fixed DiscordBotService linting issues:
  - Added underscore prefix to private fields to follow naming convention
  - Simplified name references by removing unnecessary 'this.' prefixes
- Fixed DiscordReadyHandler linting issues:
  - Added underscore prefix to private fields to follow naming convention
  - Simplified name references by removing unnecessary 'this.' prefixes
- Fixed SendMessageAsyncWrapper method in DiscordMessageHandlerTests to match Discord.Net API by simplifying the method signature
- Fixed namespace declaration in Program.cs by moving using directives outside the namespace

## [0.1.0] - 2024-03-21

### Added

- Initial Discord bot setup with basic configuration
- Core logging infrastructure
- Discord event handlers for Ready and Message events
- Basic dependency injection setup
- Configuration service for managing bot settings
