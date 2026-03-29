namespace SupremeEngine;

/// <see href="../../../../docs/systems/equipment_spec.md"/>
public record EquipmentSlotsDto
{
    public CardDto? Weapon { get; init; }
    public CardDto? OffHand { get; init; }
    public CardDto? Head { get; init; }
    public CardDto? Chest { get; init; }
    public CardDto? Hands { get; init; }
    public CardDto? Feet { get; init; }
    public CardDto? Amulet { get; init; }
    public CardDto? Ring1 { get; init; }
    public CardDto? Ring2 { get; init; }
}
