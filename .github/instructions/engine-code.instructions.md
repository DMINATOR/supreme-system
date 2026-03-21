---
description: "Use when writing, editing, or reviewing code in the SupremeEngine library. Covers the no-Godot rule and XML doc spec references."
applyTo: "engine/SupremeEngine/*.cs"
---

# SupremeEngine Code Conventions

### No Godot Dependencies
- `SupremeEngine` is a pure C# library — zero Godot API references allowed
- All game logic lives here; no scene, node, or Godot-specific code

### Spec References
Every class that implements a game design concept must include an XML doc comment referencing its spec file:

```csharp
/// <see href="../../../docs/systems/element_spec.md"/>
public class MyClass { }
```

- The path is relative from the `.cs` file to the `docs/` spec file
- Enums that support a concept share the same spec reference as the class they belong to
- If no spec exists yet, omit the comment rather than linking a non-existent file
