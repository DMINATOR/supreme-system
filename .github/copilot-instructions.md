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

## Documentation Conventions

All game design documentation lives in `docs/`. The entry point is `docs/README.md`.

### Folder Structure

Docs are organized into three subfolders plus a root reference:

- `docs/world/` — lore, setting, factions, and the state of the game world
- `docs/characters/` — player, companions, party, enemies
- `docs/systems/` — gameplay mechanics and rules
- `docs/glossary.md` — cross-cutting term reference (stays at root)

### Design Docs vs Spec Files

Each game element with implementable mechanics has three files:

| File | Purpose |
|------|-------------------------------|
| `element.md` | Narrative design — intent, rationale, open questions, lore context |
| `element_spec.md` | Structured reference — used for code generation and implementation |
| `element_tbd.md` | Open questions — unresolved decisions that block or affect implementation |

The design doc links to its spec at the top with:
```
> Facts, rules, behaviors, and relationships: [element_spec.md](element_spec.md)
```

Rules and structured facts must not be duplicated across both files — they belong in the spec only. The design doc keeps the intent and context; the spec keeps the decisions.

### Spec File Format

Every `_spec.md` file uses exactly these four sections (omit a section only if it has no settled content yet):

```markdown
## Facts
Concrete values and constants. Unknown values are listed as TBD — never omitted.

## Rules
Invariants and constraints that must always hold. Written as plain statements.

## Behaviors
Things the element can do or have done to it. Written as plain action phrases.

## Relationships
How this element connects to other elements. Written as plain ownership/association statements.
```

- No code syntax, types, or programming language constructs
- TBD values must be written explicitly as `TBD` — never left blank or omitted
- When a design decision changes, update the spec first, then the design doc if the context changes

### TBD File Format

Every `_tbd.md` file is a flat list of open questions for that element:

```markdown
# Element — Open Questions

> See [element_spec.md](element_spec.md) for current settled decisions.

- **Question label** — one sentence describing what needs to be decided and why it matters.
```

- One entry per unresolved item
- When a question is resolved, remove it from `_tbd.md` and update the value in `_spec.md`
- If a `_tbd.md` becomes empty, it can be deleted

### Glossary as Primary Reference
- `docs/glossary.md` is the **single source of truth** for all game design terms
- Every term that has a glossary entry must be linked to it when referenced in any doc page
- Link format: `[term](../glossary.md#anchor)` from within a subfolder (anchor is the heading in kebab-case)
- When a term is first introduced or defined in a design doc, add or update its entry in `docs/glossary.md`

### Iterating on Design
- When a design decision changes, update the spec file first, then the design doc and glossary as needed
- If a term is renamed or removed, update all links across all doc pages that reference it
- TBD items should be marked explicitly as `— TBD` inline so they are easy to locate and resolve later

### Do's and Don'ts for Docs
- **Do** link glossary terms wherever they appear in design pages
- **Do** keep glossary entries concise — one or two sentences, with TBD noted for unresolved details
- **Do** keep structured facts and rules in the spec file, not the design doc
- **Don't** define a term in a design doc without adding it to the glossary
- **Don't** leave stale links — if a glossary anchor changes, update all references
- **Don't** duplicate rules or facts between the design doc and the spec
