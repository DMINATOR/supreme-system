namespace SupremeEngine;

/// <see href="../../../../docs/systems/equipment_spec.md"/>
public class EquipmentSlots
{
    public CardSlot Weapon { get; } = new();
    public CardSlot OffHand { get; } = new();
    public CardSlot Head { get; } = new();
    public CardSlot Chest { get; } = new();
    public CardSlot Hands { get; } = new();
    public CardSlot Feet { get; } = new();
    public CardSlot Amulet { get; } = new();
    public CardSlot Ring1 { get; } = new();
    public CardSlot Ring2 { get; } = new();

    public EquipmentSlotsDto ToDto() => new()
    {
        Weapon = Weapon.ToDto(),
        OffHand = OffHand.ToDto(),
        Head = Head.ToDto(),
        Chest = Chest.ToDto(),
        Hands = Hands.ToDto(),
        Feet = Feet.ToDto(),
        Amulet = Amulet.ToDto(),
        Ring1 = Ring1.ToDto(),
        Ring2 = Ring2.ToDto()
    };

    public static EquipmentSlots FromDto(EquipmentSlotsDto dto)
    {
        var slots = new EquipmentSlots();
        if (dto.Weapon is not null) slots.Weapon.Equip(Card.FromDto(dto.Weapon));
        if (dto.OffHand is not null) slots.OffHand.Equip(Card.FromDto(dto.OffHand));
        if (dto.Head is not null) slots.Head.Equip(Card.FromDto(dto.Head));
        if (dto.Chest is not null) slots.Chest.Equip(Card.FromDto(dto.Chest));
        if (dto.Hands is not null) slots.Hands.Equip(Card.FromDto(dto.Hands));
        if (dto.Feet is not null) slots.Feet.Equip(Card.FromDto(dto.Feet));
        if (dto.Amulet is not null) slots.Amulet.Equip(Card.FromDto(dto.Amulet));
        if (dto.Ring1 is not null) slots.Ring1.Equip(Card.FromDto(dto.Ring1));
        if (dto.Ring2 is not null) slots.Ring2.Equip(Card.FromDto(dto.Ring2));
        return slots;
    }
}
