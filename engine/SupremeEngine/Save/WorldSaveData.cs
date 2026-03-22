namespace SupremeEngine;

/// <see href="../../../../docs/systems/save-slots_spec.md"/>
public class WorldSaveData
{
    public int Seed { get; set; }
    public BagDto Bag { get; set; } = new();
}
