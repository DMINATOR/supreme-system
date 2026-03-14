# Copilot Instructions

## Project Overview

This project is created with Godot (C#) and is a game project.

## Tech Stack

- Godot Engine
- C# for scripting
- Markdown is used for source of truth for documentation and instructions

## Project Structure

Key directories:

- `godot/`: Root project directory for Godot
  - `godot/supreme-godot/`: Godot project root (`SupremeGodot.csproj`) — references `SupremeEngine`
- `docs/`: Documentation directory, used to define game design rules
- `engine/`: Engine code (C# projects)
  - `engine/SupremeEngine/`: Core engine library (`SupremeEngine.csproj`)
  - `engine/SupremeEngine.Test/`: xUnit test project (`SupremeEngine.Test.csproj`)
- `SupremeEngine.slnx`: Solution file containing all projects

## Coding Conventions

Godot's [C# style guide](https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_style_guide.html) is the source of truth and is applied consistently across all layers (`SupremeEngine` and `SupremeGodot`).

### General
- One class per file; filename matches the class name
- Allman brace style (opening brace on its own line)
- 4 spaces for indentation — no tabs
- All Godot node scripts must be `partial class`

### Naming
- `PascalCase`: classes, methods, properties, signals, enums, enum members, constants, public fields
- `_camelCase`: private fields (underscore prefix)
- `camelCase`: local variables and parameters
- Signal delegates: `[Signal] delegate void MySignalEventHandler(...)` — suffix `EventHandler`
- Scene/Node names in the Godot editor: PascalCase

### Formatting
- Place `[Export]` and `[Signal]` attributes on the line immediately above the member
- Order within a class: signals → exported fields → private fields → properties → built-in overrides (`_Ready`, `_Process`, …) → public methods → private methods

## Architecture & Patterns

- `SupremeEngine` is a **pure C# library** — zero Godot API references allowed
- `SupremeGodot` is the **Godot layer** — wires engine logic to nodes and scenes
- Game logic goes in `SupremeEngine`; scene/node wiring and Godot API calls stay in `SupremeGodot`

## Testing

- Only `SupremeEngine` is unit-tested (xUnit); `SupremeGodot` is not directly testable
- Test class naming: `<Subject>Test` (e.g. `EngineTest`)
- Follow Arrange / Act / Assert structure within each test

## Do's and Don'ts

### Do
- Use `GD.Print` for logging inside `SupremeGodot`
- Prefer `[Export]` node references over hard-coded `GetNode<T>("path")` strings
- Keep `SupremeEngine` free of any Godot dependencies

### Don't
- Don't use `Console.WriteLine` in `SupremeGodot` — use `GD.Print`
- Don't put game logic directly in node scripts — delegate to `SupremeEngine`
- Don't use namespaces in `SupremeGodot` node scripts (Godot autoload/scene system expects top-level types)

## Additional Context

<!-- Anything else Copilot should know about this codebase -->
