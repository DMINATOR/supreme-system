---
description: "Use when writing, editing, or reviewing xUnit tests in SupremeEngine.Test. Covers test class naming, method naming, and AAA structure."
applyTo: "engine/SupremeEngine.Test/**/*.cs"
---

# Testing Conventions

- Only `SupremeEngine` is unit-tested (xUnit); `SupremeGodot` is not directly testable
- Test class naming: `<Subject>Test` (e.g. `EngineTest`, `DeckTest`)
- Follow Arrange / Act / Assert structure within each test method, with blank lines separating each section
