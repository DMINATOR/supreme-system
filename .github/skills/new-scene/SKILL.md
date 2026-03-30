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

### Step 3 — Update SceneManager and GameScene
Edit `godot/supreme-godot/Managers/SceneManager.cs` and `godot/supreme-godot/Managers/GameScene.cs`:

1. Add a value to the `GameScene` enum (in `GameScene.cs`) at the end — note its integer index:
   ```csharp
   {SceneName},  // index N
   ```
2. Add a `public const string` path constant to `SceneManager` — alphabetical order:
   ```csharp
   public const string {SceneName} = "res://{Folder}/{SceneName}.tscn";
   ```
3. Add a case to `SceneManager.GoTo(GameScene)` and a `GoTo{SceneName}()` convenience method:
   ```csharp
   GameScene.{SceneName} => {SceneName},
   ```
   ```csharp
   public void GoTo{SceneName}() => GetTree().ChangeSceneToFile({SceneName});
   ```

### Step 4 — Update DebugScene.tscn
Edit `godot/supreme-godot/Scenes/Debug/DebugScene.tscn`.

Add a `SceneButtonPrefabScene` instance as a child of `VBoxContainer/TabContainer/Scenes`, after the last existing button node. The `SceneButtonPrefabScene` ext_resource must already be declared (id `2_scenebutton`). Set `TargetScene` to the integer index of the new `GameScene` enum value:
```
[node name="{SceneName}Button" parent="VBoxContainer/TabContainer/Scenes" instance=ExtResource("2_scenebutton")]
layout_mode = 2
text = "{Display Name}"
TargetScene = N
```
- Node name: `{SceneName}Button`
- Text: human-readable label (e.g. `"Combat Scene"` for `CombatScene`)
- Do NOT add a `unique_id`

### Step 5 — No DebugScene.cs changes needed
Navigation is handled entirely by `SceneButtonPrefabScene` via the `TargetScenePath` export. Do not add fields, `GetNode` calls, or signal wiring to `DebugScene.cs`.

## Checklist
- [ ] `{SceneName}.cs` created (partial class, LoadNodes/PrepareNodes)
- [ ] `{SceneName}.tscn` created (no UIDs)
- [ ] No `.cs.uid` created manually
- [ ] `GameScene.cs` — new enum value added
- [ ] `SceneManager.cs` — path constant added
- [ ] `SceneManager.cs` — `GoTo{SceneName}()` method added
- [ ] `SceneManager.cs` — case added to `GoTo(GameScene)`
- [ ] `DebugScene.tscn` — `SceneButtonPrefabScene` instance node added under `VBoxContainer/TabContainer/Scenes` with `TargetScene` bound
- [ ] `DebugScene.cs` — no changes needed
