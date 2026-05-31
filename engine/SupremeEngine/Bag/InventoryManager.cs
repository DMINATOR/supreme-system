using System.Linq;

namespace SupremeEngine;

/// <see href="../../../../docs/systems/bag_spec.md"/>
public class InventoryManager
{
    public const int BagCapacity = 100;
    public const int DefaultCompanionDeckCapacity = 15; // TBD — per-companion limits will differ

    public CardCollection Bag { get; private set; } = new(BagCapacity);
    public List<CompanionState> Companions { get; private set; } = new();

    public static InventoryManager From(WorldSaveData data)
    {
        var manager = new InventoryManager();
        manager.Bag = CardCollection.FromDto(data.Bag, BagCapacity);
        manager.Companions = data.Companions.Select(c => CompanionState.From(c)).ToList();
        return manager;
    }
}
