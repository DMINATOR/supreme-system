---
name: rename-scene
description: 'Rename an existing Godot game scene in SupremeGodot. Covers all required coordinated changes: renaming .cs and .tscn files, updating the GameScene enum value name and [ScenePath] attribute, GoTo method name in SceneManager.cs, and the button node name in DebugScene.tscn. Do NOT use for creating or removing scenes.'
argument-hint: 'OldSceneName and NewSceneName (e.g. "DefaultScene → WorldScene")'
---

# Rename Scene

## When to Use
- Renaming an existing navigable scene (one registered in the `GameScene` enum)
- **Not for creating scenes** — use the [new-scene skill](../new-scene/SKILL.md)
- **Not for removing scenes** — use the [remove-scene skill](../remove-scene/SKILL.md)
- **Not for prefab scenes** in `Scenes/Prefabs/` — those are not registered in `GameScene`

## Required Inputs
Before starting, determine:
1. **OldSceneName** — current PascalCase name (e.g. `DefaultScene`)
2. **NewSceneName** — new PascalCase name (e.g. `WorldScene`)
3. **Folder** — current location under `res://` (e.g. `Scenes/World`)

## Procedure

### Step 1 — Rename the files
Rename both files in `godot/supreme-godot/{Folder}/`:
- `{OldSceneName}.cs` → `{NewSceneName}.cs`
- `{OldSceneName}.tscn` → `{NewSceneName}.tscn`

Also update inside the `.tscn` file:
- The `path` in the `[ext_resource]` for the script: `res://{Folder}/{NewSceneName}.cs`
- The root `[node name=...]` to match `{NewSceneName}`

And inside the `.cs` file:
- The `partial class` name to `{NewSceneName}`

### Step 2 — Update SceneManager.cs
Edit `godot/supreme-godot/Managers/SceneManager.cs`:

1. Rename the enum value and update its `[ScenePath]` attribute:
   ```csharp
   [ScenePath("res://{Folder}/{NewSceneName}.tscn")]
   {NewSceneName},
   ```
2. Rename the `GoTo` convenience method:
   ```csharp
   public void GoTo{NewSceneName}() => GoTo(GameScene.{NewSceneName});
   ```

> The `TargetScene` integer index in `DebugScene.tscn` does **not** change — the enum value's position in the list is unchanged, only its name.

### Step 3 — Update DebugScene.tscn
Edit `godot/supreme-godot/Scenes/Debug/DebugScene.tscn`:

Rename the button node — only the `name` changes; `text` and `TargetScene` stay the same:
```
[node name="{NewSceneName}Button" parent="VBoxContainer/TabContainer/Scenes" instance=ExtResource("2_scenebutton")]
```

> No changes needed in `DebugScene.cs`.

## Checklist
- [ ] `{OldSceneName}.cs` renamed to `{NewSceneName}.cs`; `partial class` name updated
- [ ] `{OldSceneName}.tscn` renamed to `{NewSceneName}.tscn`; `ext_resource` path and root node name updated
- [ ] `SceneManager.cs` — enum value renamed with updated `[ScenePath]` attribute
- [ ] `SceneManager.cs` — `GoTo` method renamed
- [ ] `DebugScene.tscn` — button node name updated to `{NewSceneName}Button`
- [ ] `DebugScene.cs` — no changes needed
