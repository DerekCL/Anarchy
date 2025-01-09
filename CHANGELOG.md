# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Changed

- Refined .editorconfig C# coding standards:
  - Disabled SA1009 (closing parenthesis spacing) and SA1111 (closing parenthesis placement)
  - Modified severity of namespace declarations and using directive placements to warning
  - Changed SA1200 severity to none
  - Adjusted various diagnostic severity levels for improved developer experience

### Added

- Enhanced C# coding standards in .editorconfig
  - Consistent section header formatting
  - Consolidated naming conventions
  - Expanded linting and analyzer configurations
  - Modern C# feature support
  - Improved whitespace and indentation rules
  - Standardized accessibility modifier requirements

### Fixed

- Removed unnecessary async/await in Program.Main method

## [0.1.0] - 2024-03-21

### Added

- Initial Discord bot setup with basic configuration
- Core logging infrastructure
- Discord event handlers for Ready and Message events
- Basic dependency injection setup
- Configuration service for managing bot settings
