namespace SupremeEngine;

/// <see href="../../../../docs/systems/save-slots_spec.md"/>
public record CompanionSaveData
{
    public required string CompanionId { get; init; }
    public CardCollectionDto Deck { get; init; } = new();
    public EquipmentSlotsDto Equipment { get; init; } = new();
}
