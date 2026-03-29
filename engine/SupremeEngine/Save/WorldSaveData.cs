namespace SupremeEngine;

/// <see href="../../../../docs/systems/save-slots_spec.md"/>
public class WorldSaveData
{
    public int Seed { get; set; }
    public CardCollectionDto Bag { get; set; } = new();
    public PlayerSaveData Player { get; set; } = new();
    public List<CompanionSaveData> Companions { get; set; } = new();
}
