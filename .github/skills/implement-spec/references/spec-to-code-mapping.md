# Spec-to-Code Mapping

Reference for translating each section of a `_spec.md` into C# constructs in `SupremeEngine`.

## Facts → Code

Facts are concrete values and constants.

| Spec pattern | C# construct | Example |
|---|---|---|
| Numeric limit ("capacity: 20") | `public const int XCapacity = 20;` | `DeckCapacity = 20` |
| Fixed set of named values | `public enum XType { A, B, C }` | `CardRarity` |
| One-time computed state | `public X Y { get; private set; }` | `public bool IsLocked { get; set; }` |
| Derived/computed value | `public X Y => ...;` | `public int Count => _items.Count;` |

## Rules → Code

Rules are invariants that must always hold. They become **guard conditions** — the first lines of the affected method(s).

| Spec pattern | C# construct |
|---|---|
| "Cannot do X while in state Y" | `if (IsY) throw new InvalidOperationException("...")` |
| "X must be present before Y" | `if (x == null) throw new InvalidOperationException("...")` |
| "Limit of N items" | `if (_items.Count >= Capacity) throw new InvalidOperationException("...")` |
| "Only one X per Y" | Check for duplicates before adding; throw if found |

Always use `InvalidOperationException` for rule violations. Never use `ArgumentException` for state violations.

## Behaviors → Code

Behaviors are things the system can do. Each becomes one `public` method on the Manager.

| Spec pattern | C# signature pattern |
|---|---|
| "Receive/add X" | `public void Add{X}({X} x)` |
| "Remove/discard X" | `public void Remove{X}({X} x)` |
| "Transfer X from A to B" | `public void Transfer({X} x, IY from, IY to)` |
| "Draw X" | `public {X} Draw{X}()` |
| "Check if can do X" | `public bool Can{X}()` |
| "Get current state" | `public {X} Get{X}()` or property |

Methods that modify state: `void`. Methods that return a value without side effects: typed return. Methods that both modify and return: rare — prefer two separate methods.

## Relationships → Code

Relationships describe ownership and association.

| Spec pattern | C# construct |
|---|---|
| "One X owns one Y" | `public Y Y { get; private set; } = new();` |
| "One X owns many Ys" | `private readonly List<Y> _ys = new();` + `public IReadOnlyList<Y> Ys => _ys.AsReadOnly();` |
| "X depends on Y (injected)" | Constructor parameter: `public XManager(Y y) { _y = y; }` |
| "X references Y (looked up)" | Property or parameter on the method that needs it |

Prefer composition over injection for objects the manager creates and owns. Use constructor injection for shared services (e.g. `CardFactory`).

## DTO Design Rules

- One DTO per Manager that is persisted (not for every class)
- DTO properties mirror Manager state, not methods
- Use `List<ChildDto>` for collections (not IReadOnlyList — JSON serializer needs concrete type)
- DTOs have no logic — no methods except `ToDto`/`FromDto` on the Manager side
- Do not nest Manager types inside a DTO — flatten to primitive types and child DTOs

## Handling TBD Items

If a spec entry says `TBD`:
- Do **not** implement a guess — leave a stub:
  ```csharp
  // TODO: TBD — {the open question from _tbd.md}
  public void ExampleMethod() => throw new NotImplementedException();
  ```
- The stub compiles but makes the incompleteness visible
