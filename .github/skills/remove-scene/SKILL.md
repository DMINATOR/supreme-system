---
name: remove-scene
description: 'Remove an existing Godot game scene from SupremeGodot. Covers all required coordinated changes: deleting .cs and .tscn files, removing the GameScene enum value and GoTo method from SceneManager.cs, removing the button node from DebugScene.tscn, and re-indexing any TargetScene values that shifted. Do NOT use for creating or renaming scenes.'
argument-hint: 'SceneName to remove (e.g. "DragAndDropScene")'
---

# Remove Scene

## When to Use
- Deleting a navigable scene (one registered in the `GameScene` enum)
- **Not for creating scenes** — use the [new-scene skill](../new-scene/SKILL.md)
- **Not for renaming scenes** — use the [rename-scene skill](../rename-scene/SKILL.md)
- **Not for prefab scenes** in `Scenes/Prefabs/` — those are not registered in `GameScene`

## Required Inputs
Before starting, determine:
1. **SceneName** — PascalCase name of the scene to remove (e.g. `DragAndDropScene`)
2. **Folder** — its location under `res://` (e.g. `Scenes/Demo`)
3. **EnumIndex** — its 0-based position in the `GameScene` enum (needed to identify which `TargetScene` buttons shift)

## Procedure

### Step 1 — Delete the files
Delete both files from `godot/supreme-godot/{Folder}/`:
- `{SceneName}.cs`
- `{SceneName}.tscn`

> Do not delete any `.cs.uid` files manually — Godot manages them.

### Step 2 — Update SceneManager.cs
Edit `godot/supreme-godot/Managers/SceneManager.cs`:

1. Remove the enum value (and its `[ScenePath]` attribute) from `GameScene`
2. Remove the `GoTo{SceneName}()` convenience method

> **Index shift warning:** Removing an enum value shifts the integer index of every enum value declared after it. Go to Step 3 immediately and fix all affected `TargetScene` values in `DebugScene.tscn`.

### Step 3 — Update DebugScene.tscn
Edit `godot/supreme-godot/Scenes/Debug/DebugScene.tscn`:

1. Remove the `{SceneName}Button` node entirely
2. For every remaining button whose `TargetScene` value is **greater than** the removed enum index, decrement it by 1

Example — if `DragAndDropScene` was index 6 and nothing followed it, no other buttons need updating. If it was index 3 and `BagScene` was 4, `CardCreatorScene` was 5, those must become 3 and 4 respectively.

> No changes needed in `DebugScene.cs`.

## Checklist
- [ ] `{SceneName}.cs` deleted
- [ ] `{SceneName}.tscn` deleted
- [ ] `SceneManager.cs` — enum value and `[ScenePath]` attribute removed
- [ ] `SceneManager.cs` — `GoTo{SceneName}()` method removed
- [ ] `DebugScene.tscn` — `{SceneName}Button` node removed
- [ ] `DebugScene.tscn` — all `TargetScene` values after the removed index decremented by 1
- [ ] `DebugScene.cs` — no changes needed
