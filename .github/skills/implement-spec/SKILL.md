---
name: implement-spec
description: 'Translate an existing _spec.md into working SupremeEngine C# code. Use when a spec file already exists but the engine implementation is missing or incomplete (e.g. Deck, Combat, Hand, Initiative). Covers reading the spec, mapping spec sections to code constructs, generating the Manager, DTO, and xUnit tests, and clearing resolved TBDs.'
argument-hint: 'System name or path to _spec.md (e.g. "hand" or "docs/systems/hand_spec.md")'
---

# Implement Spec

## When to Use
- A `_spec.md` exists but `engine/SupremeEngine/{System}/` is empty or missing
- Adding a method that is described in the spec but not yet coded
- Filling in `throw new NotImplementedException()` stubs

## Procedure

### Step 1 — Read the spec
Open `docs/systems/{element}_spec.md` and read all four sections. Also open `docs/systems/{element}_tbd.md` to see which questions are still open.

**Do not implement anything that is marked TBD in the spec** — leave those as stubs with a `// TODO: TBD — {question}` comment.

### Step 2 — Map spec to code
Use the [spec-to-code mapping](./references/spec-to-code-mapping.md) to translate each spec entry into the appropriate C# construct before writing any code.

### Step 3 — Implement the Manager class
Create or edit `engine/SupremeEngine/{SystemName}/{SystemName}Manager.cs`:
- `namespace SupremeEngine;`
- `/// <see href="../../../../docs/systems/{element}_spec.md"/>` on the class
- **Facts** → `public const` or `public static readonly` fields, or read-only `{ get; private set; }` properties
- **Behaviors** → one `public` method per behavior; guard conditions from Rules become the first lines of each method
- **Relationships** → constructor parameters or property types referencing other engine classes
- `ToDto()` / `static FromDto(dto)` pair if the system needs to be saved (check if spec mentions save/load)
- No Godot references

### Step 4 — Implement the DTO (if needed)
Create or edit `engine/SupremeEngine/{SystemName}/{SystemName}Dto.cs`:
- `namespace SupremeEngine;`
- `/// <see href="../../../../docs/systems/{element}_spec.md"/>` on the record
- One `{ get; init; }` property per field that must survive a JSON round-trip

### Step 5 — Write xUnit tests
Create or edit `engine/SupremeEngine.Test/{SystemName}/{SystemName}ManagerTest.cs`:
- **One test per public method** — both the normal path and every error path (thrown exceptions, edge cases)
- **One DTO round-trip test** if ToDto/FromDto exist
- Test name pattern: `{Subject}_{ExpectedOutcome}` — e.g. `DrawCard_ThrowsWhenHandFull`
- AAA structure with a blank line between each section
- Use a private static helper method (e.g. `MakeCard(...)`) to keep Arrange sections short

### Step 6 — Resolve TBDs (optional)
If implementing a behavior resolves an open question in `_tbd.md`, remove that entry from the file. If all questions are resolved, the file should contain only the header and `No open questions.`

## Checklist
- [ ] Spec read in full before writing any code
- [ ] TBD items identified — not implemented, left as stubs with comments
- [ ] Every spec Fact has a matching constant or property
- [ ] Every spec Behavior has a matching public method
- [ ] Every spec Rule is enforced as a guard condition in the relevant method(s)
- [ ] Every spec Relationship is represented as a constructor parameter or property type
- [ ] `ToDto()` / `FromDto()` implemented if system is saveable
- [ ] Tests written for every public method (normal + error paths)
- [ ] DTO round-trip test written (if applicable)
- [ ] `/// <see href>` present on the Manager and DTO classes
- [ ] No Godot references in engine code
- [ ] `_tbd.md` updated to remove resolved questions
