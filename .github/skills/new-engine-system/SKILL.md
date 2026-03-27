---
name: new-engine-system
description: 'Scaffold a new SupremeEngine game system from scratch. Use when implementing a new gameplay mechanic (e.g. combat, hand, initiative, equipment) that requires engine classes, DTOs, xUnit tests, and design docs. Covers Manager class, DTO record, test class, and the three docs files (design doc, spec, TBD).'
argument-hint: 'SystemName and docs folder (e.g. "Hand system in docs/systems")'
---

# New Engine System

## When to Use
- Implementing a brand-new gameplay system (e.g. `Hand`, `Combat`, `Initiative`)
- Any time a new folder under `engine/SupremeEngine/` is needed
- When a spec file exists but no engine code yet (see [implement-spec](../implement-spec/SKILL.md) for reading an existing spec)

## Required Inputs
Before starting, determine:
1. **SystemName** — PascalCase (e.g. `Hand`, `Combat`, `Initiative`)
2. **element** — lowercase-hyphenated doc filename root (e.g. `hand`, `combat`, `initiative`)
3. **DocsFolder** — subfolder under `docs/` (almost always `docs/systems/`)
4. **Spec source** — does a `_spec.md` already exist? If yes, read it before coding (see [implement-spec](../implement-spec/SKILL.md))

## Procedure

### Step 1 — Create the engine folder and classes
Create `engine/SupremeEngine/{SystemName}/` and add:

**`{SystemName}Manager.cs`** — main game logic class, using [ManagerTemplate.cs](./assets/ManagerTemplate.cs):
- `/// <see href="../../../../docs/systems/{element}_spec.md"/>` XML doc on the class
- `namespace SupremeEngine;`
- `public class {SystemName}Manager`
- Properties map from spec **Facts**
- Public methods map from spec **Behaviors**
- `ToDto()` / `static FromDto(dto)` pair if state needs to be saved
- No Godot references, no `using Godot;`

**`{SystemName}Dto.cs`** — serialization record, using [DtoTemplate.cs](./assets/DtoTemplate.cs):
- `/// <see href="../../../../docs/systems/{element}_spec.md"/>` XML doc on the record
- `namespace SupremeEngine;`
- `public record {SystemName}Dto`
- One `{ get; init; }` property per field that needs to round-trip through JSON

### Step 2 — Create the test class
Create `engine/SupremeEngine.Test/{SystemName}/{SystemName}ManagerTest.cs` using [ManagerTestTemplate.cs](./assets/ManagerTestTemplate.cs):
- `namespace SupremeEngine.Test;` + `using SupremeEngine;`
- `public class {SystemName}ManagerTest`
- At minimum: one test per public method + one DTO round-trip test
- AAA structure with a blank line between Arrange / Act / Assert
- Test names: `{Subject}_{ExpectedOutcome}` (e.g. `AddCard_ThrowsWhenLocked`)

### Step 3 — Create the docs (if they don't exist)
Create three files in `docs/systems/` using the templates:

| File | Template |
|------|---------|
| `{element}.md` | [DocTemplate.md](./assets/DocTemplate.md) |
| `{element}_spec.md` | [SpecTemplate.md](./assets/SpecTemplate.md) |
| `{element}_tbd.md` | [TbdTemplate.md](./assets/TbdTemplate.md) |

Rules:
- Design doc links to spec at the top: `> Facts, rules, behaviors, and relationships: [{element}_spec.md]({element}_spec.md)`
- Spec has exactly four sections: Facts / Rules / Behaviors / Relationships
- Unknown values written as `TBD` — never left blank
- Any glossary terms linked to `../glossary.md#{anchor}`
- If the spec already exists, skip creating docs

### Step 4 — Update the glossary (if needed)
If this system introduces a new game design term, add an entry to `docs/glossary.md`.

## Spec Reference Path Rule
The `/// <see href>` path in engine classes is **always** `../../../../docs/systems/{element}_spec.md` for classes in `engine/SupremeEngine/{SystemName}/`. This is the established pattern used by all existing engine classes.

## Checklist
- [ ] `engine/SupremeEngine/{SystemName}/{SystemName}Manager.cs` created
- [ ] `engine/SupremeEngine/{SystemName}/{SystemName}Dto.cs` created (if state is saveable)
- [ ] `engine/SupremeEngine.Test/{SystemName}/{SystemName}ManagerTest.cs` created
- [ ] `docs/systems/{element}.md` created or already exists
- [ ] `docs/systems/{element}_spec.md` created or already exists
- [ ] `docs/systems/{element}_tbd.md` created or already exists
- [ ] XML doc `/// <see href>` present on every engine class
- [ ] No Godot references anywhere in `engine/SupremeEngine/`
- [ ] Glossary updated if new terms introduced
