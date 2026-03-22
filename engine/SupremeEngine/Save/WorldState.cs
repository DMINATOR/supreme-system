namespace SupremeEngine;

/// <see href="../../../../docs/systems/save-slots_spec.md"/>
public class WorldState
{
    public Random Random { get; }
    public BagManager BagManager { get; private set; } = new();

    public WorldState(int seed)
    {
        Random = new Random(seed);
        Seed = seed;
    }

    private int Seed { get; }

    public WorldSaveData ToSaveData() => new WorldSaveData { Seed = Seed, Bag = BagManager.ToDto() };

    public static WorldState From(WorldSaveData data)
    {
        var state = new WorldState(data.Seed);
        state.BagManager = BagManager.FromDto(data.Bag);
        return state;
    }
}
