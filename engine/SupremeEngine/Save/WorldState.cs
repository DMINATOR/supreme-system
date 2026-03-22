namespace SupremeEngine;

/// <see href="../../../../docs/systems/save-slots_spec.md"/>
public class WorldState
{
    public Bag Bag { get; set; } = new();

    public WorldSaveData ToSaveData() => new WorldSaveData { Bag = Bag.ToDto() };

    public static WorldState From(WorldSaveData data) => new WorldState { Bag = Bag.FromDto(data.Bag) };
}
