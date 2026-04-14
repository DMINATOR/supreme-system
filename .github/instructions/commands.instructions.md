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

### Existing Commands

| Command | Description |
|---------|-------------|
| `TransferCardCommand(CardSlot source, CardSlot target)` | Moves a card from `source` to `target`. Source must be non-null and occupied; target must be non-null and empty. |
