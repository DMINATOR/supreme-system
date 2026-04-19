namespace SupremeEngine;

/// <see href="../../../../docs/systems/map-and-structure_spec.md"/>
public class WorldState
{
    public Random Random { get; }
    public InventoryManager Inventory { get; private set; } = new();
    public int WorldLevel { get; private set; } = 1;

    public WorldState(int seed)
    {
        Random = new Random(seed);
        Seed = seed;
    }

    private int Seed { get; }

    public WorldSaveData ToSaveData() => new WorldSaveData
    {
        Seed = Seed,
        WorldLevel = WorldLevel,
        Bag = Inventory.Bag.ToDto(),
        Player = Inventory.Player.ToSaveData(),
        Companions = Inventory.Companions.Select(c => c.ToSaveData()).ToList()
    };

    public static WorldState From(WorldSaveData data)
    {
        var state = new WorldState(data.Seed);
        state.WorldLevel = data.WorldLevel;
        state.Inventory = InventoryManager.From(data);
        return state;
    }
}
