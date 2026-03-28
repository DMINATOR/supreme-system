---
name: new-scene
description: 'Create a new Godot game scene in SupremeGodot. Use when adding a new scene, screen, or UI view that is navigated to via SceneManager. Covers all required coordinated changes: .cs script, .tscn file, SceneManager path constant and GoTo method, and DebugScene button wiring. Do NOT use for renaming or removing scenes. Do NOT use for prefab scenes (Scenes/Prefabs/) — those are instantiated programmatically via Util/ helpers, not navigated to.'
argument-hint: 'SceneName and folder (e.g. "CombatScene in Scenes/World")'
---

# New Scene

## When to Use
- Adding any new playable, menu, or debug scene that is navigated to via `SceneManager`
- Any time a `.tscn` file and its backing `.cs` are being created together
- **Not for prefab scenes** — if the scene lives in `Scenes/Prefabs/` and is instantiated programmatically, do not use this skill; add a helper in `Util/` instead

## Required Inputs
Before starting, determine:
1. **SceneName** — PascalCase (e.g. `CombatScene`)
2. **Folder** — relative path under `res://` (e.g. `Scenes/World`, `Scenes/Player`, `Scenes/Debug`, `Scenes/Menu`)
3. **BaseType** — Godot node type to inherit (`Control` for UI, `Node2D` for game world, `Node` for logic-only)

## Procedure

### Step 1 — Create the C# script
Create `godot/supreme-godot/{Folder}/{SceneName}.cs` using [SceneTemplate.cs](./assets/SceneTemplate.cs) as the base.

Rules:
- `partial class`, no namespace
- Class name matches file name exactly
- `_Ready` → `LoadNodes()` → `PrepareNodes()` (omit `PrepareNodes` only if there are no signals and no state init)
- All `GetNode<T>(...)` calls in `LoadNodes` only — never inline
- All signal wiring in `PrepareNodes` only
- Only autoloads the scene actually uses (check `AutoloadPath.cs` for available constants)
- No game logic — delegate to `SupremeEngine` classes

### Step 2 — Create the scene file
Create `godot/supreme-godot/{Folder}/{SceneName}.tscn` using [SceneTemplate.tscn](./assets/SceneTemplate.tscn) as the base.

Rules:
- **Never invent a UID** — omit the `uid=` attribute from `[gd_scene]` and from `[ext_resource]`; Godot assigns them on first project load
- **Never create a `.cs.uid` file** — Godot generates it automatically
- Set `path` in `[ext_resource]` to `res://{Folder}/{SceneName}.cs`
- The root node `name` must match `SceneName` exactly
- Use `type="{BaseType}"` on the root node
- Add child nodes to match the scene's layout; keep it minimal for now

### Step 3 — Update SceneManager
Edit `godot/supreme-godot/Managers/SceneManager.cs`:

1. Add a `public const string` path constant — alphabetical order among existing constants:
   ```csharp
   public const string {SceneName} = "res://{Folder}/{SceneName}.tscn";
   ```
2. Add a `public void GoTo{SceneName}()` method — alphabetical order among existing methods:
   ```csharp
   public void GoTo{SceneName}() => GetTree().ChangeSceneToFile({SceneName});
   ```

### Step 4 — Update DebugScene.tscn
Edit `godot/supreme-godot/Scenes/Debug/DebugScene.tscn`.

Add a `Button` node as a child of `VBoxContainer/TabContainer/Scenes`, after the last existing Button:
```
[node name="{SceneName}Button" type="Button" parent="VBoxContainer/TabContainer/Scenes"]
layout_mode = 2
text = "{Display Name}"
```
- Button name: `{SceneName}Button`
- Text: human-readable label (e.g. `"Combat Scene"` for `CombatScene`)
- Do NOT add a `unique_id` — omit entirely

### Step 5 — Update DebugScene.cs
Edit `godot/supreme-godot/Scenes/Debug/DebugScene.cs`:

1. Add a private field at the top of the fields block (with other `Button` fields):
   ```csharp
   private Button _{sceneName}Button;
   ```
2. Add a `GetNode` call in `LoadNodes()` after the last button `GetNode`:
   ```csharp
   _{sceneName}Button = GetNode<Button>("VBoxContainer/TabContainer/Scenes/{SceneName}Button");
   ```
3. Add a signal wire in `PrepareNodes()` after the last button signal:
   ```csharp
   _{sceneName}Button.Pressed += _sceneManager.GoTo{SceneName};
   ```

## Checklist
- [ ] `{SceneName}.cs` created (partial class, LoadNodes/PrepareNodes)
- [ ] `{SceneName}.tscn` created (no UIDs)
- [ ] No `.cs.uid` created manually
- [ ] `SceneManager.cs` — path constant added
- [ ] `SceneManager.cs` — `GoTo{SceneName}()` method added
- [ ] `DebugScene.tscn` — Button node added under `VBoxContainer/TabContainer/Scenes`
- [ ] `DebugScene.cs` — private field added
- [ ] `DebugScene.cs` — `GetNode` in `LoadNodes` added
- [ ] `DebugScene.cs` — signal wired in `PrepareNodes`
