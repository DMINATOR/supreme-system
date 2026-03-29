namespace SupremeEngine;

/// <see href="../../../../docs/characters/companions_spec.md"/>
public class CompanionState
{
    public string CompanionId { get; }
    public CardCollection Deck { get; private set; }
    public EquipmentSlots Equipment { get; private set; } = new();

    public CompanionState(string companionId, int deckCapacity = InventoryManager.DefaultCompanionDeckCapacity)
    {
        CompanionId = companionId;
        Deck = new CardCollection(deckCapacity);
    }

    public CompanionSaveData ToSaveData() => new()
    {
        CompanionId = CompanionId,
        Deck = Deck.ToDto(),
        Equipment = Equipment.ToDto()
    };

    public static CompanionState From(CompanionSaveData data, int deckCapacity = InventoryManager.DefaultCompanionDeckCapacity)
    {
        var state = new CompanionState(data.CompanionId, deckCapacity);
        state.Deck = CardCollection.FromDto(data.Deck, deckCapacity);
        state.Equipment = EquipmentSlots.FromDto(data.Equipment);
        return state;
    }
}
