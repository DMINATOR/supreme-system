namespace SupremeEngine;

/// <see href="../../../../docs/characters/player-character_spec.md"/>
public class PlayerState
{
    public const int DeckCapacity = 20;

    public int Level { get; private set; } = 1;
    public CardCollection Deck { get; private set; } = new(DeckCapacity);
    public EquipmentSlots Equipment { get; private set; } = new();
    public float X { get; private set; }
    public float Y { get; private set; }

    public void SetPosition(float x, float y)
    {
        X = x;
        Y = y;
    }

    public PlayerSaveData ToSaveData() => new()
    {
        Level = Level,
        Deck = Deck.ToDto(),
        Equipment = Equipment.ToDto(),
        X = X,
        Y = Y
    };

    public static PlayerState From(PlayerSaveData data)
    {
        var state = new PlayerState();
        state.Level = data.Level;
        state.Deck = CardCollection.FromDto(data.Deck, DeckCapacity);
        state.Equipment = EquipmentSlots.FromDto(data.Equipment);
        state.X = data.X;
        state.Y = data.Y;
        return state;
    }
}
