---
description: "Use when writing, editing, or reviewing ICommand implementations in SupremeEngine. Covers the command pattern conventions, validation rules, and the catalogue of existing commands."
applyTo: "engine/SupremeEngine/Commands/**/*.cs"
---

# Commands Conventions

### Pattern
- Commands live in `engine/SupremeEngine/Commands/`
- Every command implements `ICommand` and exposes a single `Execute()` method
- Commands are pure engine-layer — no Godot dependencies
- Commands are dispatched via `CommandDispatcher` in the Godot layer

### Validation Rules
- Validate all constructor arguments eagerly — throw before storing invalid state
- Use `ArgumentNullException.ThrowIfNull(param)` for null checks
- Use `InvalidOperationException` for semantic violations (e.g. slot empty, slot occupied)
- Never silently no-op on bad input — failing loudly prevents silent state desyncs

### XML Docs
- Include a `<summary>` describing what the command does
- List all preconditions as `<list type="bullet">` items inside the summary
- Include a `<see href="..."/>` spec reference (same convention as other engine classes)

### Godot-Layer Commands
Commands that need no Godot API can live in `engine/SupremeEngine/Commands/` even if they orchestrate persistence — use `IWorldPersistence` and `IWorldStateHolder` interfaces so the engine layer stays Godot-free. Only add a command to `godot/supreme-godot/Commands/` if it genuinely requires a Godot API (e.g. scene navigation, node signals).

### Existing Commands

| Command | Location | Description |
|---------|----------|-------------|
| `TransferCardCommand(CardSlot source, CardSlot target)` | engine | Moves a card from `source` to `target`. Source must be non-null and occupied; target must be non-null and empty. |
| `GenerateInitialMapCommand(WorldState state, WorldMapGenerator generator)` | engine | Generates the initial 3×3 region neighborhood around the world origin. |
| `SaveWorldCommand(IWorldStateHolder, IWorldPersistence, int slotIndex, Action? onSuccess, Action<Exception>? onFailure)` | engine | Sets the active slot and saves current world state to it; invokes `onSuccess` on success, `onFailure(ex)` on write failure (exception is not re-thrown). |
| `LoadWorldCommand(IWorldStateHolder, IWorldPersistence, int slotIndex, Action? onSuccess, Action<Exception>? onFailure)` | engine | Loads a slot and applies state to the holder; invokes `onSuccess` on success, `onFailure(ex)` if slot is empty (`InvalidOperationException`) or corrupt (exception is not re-thrown). |
