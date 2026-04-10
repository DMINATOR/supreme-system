# Copilot Instructions

## Project Overview

This project is a game built with Godot (C#).

## Tech Stack

- Godot Engine with C# scripting
- Markdown as source of truth for game design documentation

## Project Structure

- `godot/supreme-godot/` — Godot project root (`SupremeGodot.csproj`); references `SupremeEngine`
- `engine/SupremeEngine/` — Core engine library (`SupremeEngine.csproj`); pure C#, no Godot
- `engine/SupremeEngine.Test/` — xUnit test project
- `docs/` — Game design documentation
- `SupremeEngine.slnx` — Solution file

## Architecture

- `SupremeEngine` is a **pure C# library** — zero Godot API references allowed
- `SupremeGodot` is the **Godot layer** — wires engine logic to nodes and scenes
- Game logic goes in `SupremeEngine`; scene/node wiring and Godot API calls stay in `SupremeGodot`

## Detailed Conventions

Area-specific rules are in `.github/instructions/`:

| File | Applies to |
|------|-----------|
| `csharp-conventions.instructions.md` | All `**/*.cs` files |
| `godot-scripting.instructions.md` | `godot/**/*.cs` and `.tscn` work |
| `engine-code.instructions.md` | `engine/SupremeEngine/*.cs` |
| `testing.instructions.md` | `engine/SupremeEngine.Test/**/*.cs` |
| `game-docs.instructions.md` | `docs/**/*.md` |

### Scene Registration
Whenever a Godot scene is **created, renamed, or removed**, automatically apply all relevant changes below without being asked:

**Creating a scene:** Use the [new-scene skill](.github/skills/new-scene/SKILL.md) — read it before starting.

**Renaming a scene:** Use the [rename-scene skill](.github/skills/rename-scene/SKILL.md) — read it before starting.

**Removing a scene:** Use the [remove-scene skill](.github/skills/remove-scene/SKILL.md) — read it before starting.

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
