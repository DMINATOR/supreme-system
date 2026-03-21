---
description: "Use when creating or editing game design documentation in docs/. Covers folder structure, spec/design/tbd file formats, glossary linking, and TBD conventions."
applyTo: "docs/**/*.md"
---

# Game Documentation Conventions

All game design documentation lives in `docs/`. The entry point is `docs/README.md`.

### Folder Structure
- `docs/world/` — lore, setting, factions, and the state of the game world
- `docs/characters/` — player, companions, party, enemies
- `docs/systems/` — gameplay mechanics and rules
- `docs/glossary.md` — cross-cutting term reference (stays at root)

### Design Docs vs Spec Files

Each game element with implementable mechanics has three files:

| File | Purpose |
|------|---------|
| `element.md` | Narrative design — intent, rationale, lore context |
| `element_spec.md` | Structured reference — used for code generation and implementation |
| `element_tbd.md` | Open questions — unresolved decisions that block or affect implementation |

The design doc links to its spec at the top:
```
> Facts, rules, behaviors, and relationships: [element_spec.md](element_spec.md)
```

Rules and structured facts belong in the spec only — never duplicated in the design doc.

### Spec File Format

Every `_spec.md` uses exactly these four sections (omit only if entirely empty):

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
- TBD values must be written explicitly as `TBD` — never left blank
- When a design decision changes, update the spec first, then the design doc if context changes

### TBD File Format

```markdown
# Element — Open Questions

> See [element_spec.md](element_spec.md) for current settled decisions.

- **Question label** — one sentence describing what needs to be decided and why it matters.
```

- One entry per unresolved item
- When resolved, remove from `_tbd.md` and update `_spec.md`
- Delete `_tbd.md` if it becomes empty

### Glossary as Primary Reference
- `docs/glossary.md` is the **single source of truth** for all game design terms
- Every term that has a glossary entry must be linked to it when referenced in any doc page
- Link format: `[term](../glossary.md#anchor)` from within a subfolder (anchor is the heading in kebab-case)
- When a term is first introduced or defined in a design doc, add or update its entry in `docs/glossary.md`
- TBD items should be marked explicitly as `— TBD` inline
