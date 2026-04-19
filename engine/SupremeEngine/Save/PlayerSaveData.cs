namespace SupremeEngine;

/// <see href="../../../../docs/systems/save-slots_spec.md"/>
public record PlayerSaveData
{
    public int Level { get; init; } = 1;
    public CardCollectionDto Deck { get; init; } = new();
    public EquipmentSlotsDto Equipment { get; init; } = new();
}
