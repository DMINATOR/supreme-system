namespace SupremeEngine;

/// <see href="../../../../docs/systems/save-slots_spec.md"/>
public class WorldState
{
    public Random Random { get; }
    public InventoryManager Inventory { get; private set; } = new();

    public WorldState(int seed)
    {
        Random = new Random(seed);
        Seed = seed;
    }

    private int Seed { get; }

    public WorldSaveData ToSaveData() => new WorldSaveData { Seed = Seed, Inventory = Inventory.ToDto() };

    public static WorldState From(WorldSaveData data)
    {
        var state = new WorldState(data.Seed);
        state.Inventory = InventoryManager.FromDto(data.Inventory);
        return state;
    }
}
