namespace SupremeEngine;

/// <see href="../../../../docs/systems/bag_spec.md"/>
public class InventoryManager
{
    public const int BagCapacity = 100;
    public const int PlayerDeckCapacity = 20;
    public const int DefaultCompanionDeckCapacity = 15; // TBD — per-companion limits will differ

    public CardCollection Bag { get; private set; } = new(BagCapacity);
    public PlayerState Player { get; private set; } = new();
    public List<CompanionState> Companions { get; private set; } = new();

    public void Transfer(Card card, ICardCollection from, ICardCollection to)
    {
        from.RemoveCard(card);
        to.AddCard(card);
    }

    public static InventoryManager From(WorldSaveData data)
    {
        var manager = new InventoryManager();
        manager.Bag = CardCollection.FromDto(data.Bag, BagCapacity);
        manager.Player = PlayerState.From(data.Player);
        manager.Companions = data.Companions.Select(c => CompanionState.From(c)).ToList();
        return manager;
    }
}
