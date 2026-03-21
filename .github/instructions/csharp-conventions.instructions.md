---
description: "Use when writing, editing, or reviewing any C# code in this project. Covers naming, formatting, and class member ordering conventions."
applyTo: "**/*.cs"
---

# C# Coding Conventions

Godot's [C# style guide](https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_style_guide.html) is the source of truth and is applied consistently across all layers.

### General
- One class per file; filename matches the class name
- Allman brace style (opening brace on its own line)
- 4 spaces for indentation — no tabs

### Naming
- `PascalCase`: classes, methods, properties, signals, enums, enum members, constants, public fields
- `_camelCase`: private fields (underscore prefix)
- `camelCase`: local variables and parameters
- Signal delegates: `[Signal] delegate void MySignalEventHandler(...)` — suffix `EventHandler`
- Scene/Node names in the Godot editor: PascalCase

### Formatting
- Place `[Export]` and `[Signal]` attributes on the line immediately above the member
- Order within a class: signals → exported fields → private fields → properties → built-in overrides (`_Ready`, `_Process`, …) → public methods → private methods
