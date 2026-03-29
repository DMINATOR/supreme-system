namespace SupremeEngine;

/// <see href="../../../../docs/characters/player-character_spec.md"/>
public class PlayerState
{
    public CardCollection Deck { get; private set; } = new(InventoryManager.PlayerDeckCapacity);
    public EquipmentSlots Equipment { get; private set; } = new();

    public PlayerSaveData ToSaveData() => new()
    {
        Deck = Deck.ToDto(),
        Equipment = Equipment.ToDto()
    };

    public static PlayerState From(PlayerSaveData data)
    {
        var state = new PlayerState();
        state.Deck = CardCollection.FromDto(data.Deck, InventoryManager.PlayerDeckCapacity);
        state.Equipment = EquipmentSlots.FromDto(data.Equipment);
        return state;
    }
}
