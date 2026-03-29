namespace SupremeEngine;

/// <see href="../../../../docs/systems/equipment_spec.md"/>
public class EquipmentSlots
{
    public Card? Weapon { get; private set; }
    public Card? OffHand { get; private set; }
    public Card? Head { get; private set; }
    public Card? Chest { get; private set; }
    public Card? Hands { get; private set; }
    public Card? Feet { get; private set; }
    public Card? Amulet { get; private set; }
    public Card? Ring1 { get; private set; }
    public Card? Ring2 { get; private set; }

    public void Equip(EquipmentSlot slot, Card card)
    {
        switch (slot)
        {
            case EquipmentSlot.Weapon: Weapon = card; break;
            case EquipmentSlot.OffHand: OffHand = card; break;
            case EquipmentSlot.Head: Head = card; break;
            case EquipmentSlot.Chest: Chest = card; break;
            case EquipmentSlot.Hands: Hands = card; break;
            case EquipmentSlot.Feet: Feet = card; break;
            case EquipmentSlot.Amulet: Amulet = card; break;
            case EquipmentSlot.Ring1: Ring1 = card; break;
            case EquipmentSlot.Ring2: Ring2 = card; break;
        }
    }

    public void Unequip(EquipmentSlot slot)
    {
        switch (slot)
        {
            case EquipmentSlot.Weapon: Weapon = null; break;
            case EquipmentSlot.OffHand: OffHand = null; break;
            case EquipmentSlot.Head: Head = null; break;
            case EquipmentSlot.Chest: Chest = null; break;
            case EquipmentSlot.Hands: Hands = null; break;
            case EquipmentSlot.Feet: Feet = null; break;
            case EquipmentSlot.Amulet: Amulet = null; break;
            case EquipmentSlot.Ring1: Ring1 = null; break;
            case EquipmentSlot.Ring2: Ring2 = null; break;
        }
    }

    public EquipmentSlotsDto ToDto() => new()
    {
        Weapon = Weapon?.ToDto(),
        OffHand = OffHand?.ToDto(),
        Head = Head?.ToDto(),
        Chest = Chest?.ToDto(),
        Hands = Hands?.ToDto(),
        Feet = Feet?.ToDto(),
        Amulet = Amulet?.ToDto(),
        Ring1 = Ring1?.ToDto(),
        Ring2 = Ring2?.ToDto()
    };

    public static EquipmentSlots FromDto(EquipmentSlotsDto dto)
    {
        var slots = new EquipmentSlots();
        if (dto.Weapon is not null) slots.Equip(EquipmentSlot.Weapon, Card.FromDto(dto.Weapon));
        if (dto.OffHand is not null) slots.Equip(EquipmentSlot.OffHand, Card.FromDto(dto.OffHand));
        if (dto.Head is not null) slots.Equip(EquipmentSlot.Head, Card.FromDto(dto.Head));
        if (dto.Chest is not null) slots.Equip(EquipmentSlot.Chest, Card.FromDto(dto.Chest));
        if (dto.Hands is not null) slots.Equip(EquipmentSlot.Hands, Card.FromDto(dto.Hands));
        if (dto.Feet is not null) slots.Equip(EquipmentSlot.Feet, Card.FromDto(dto.Feet));
        if (dto.Amulet is not null) slots.Equip(EquipmentSlot.Amulet, Card.FromDto(dto.Amulet));
        if (dto.Ring1 is not null) slots.Equip(EquipmentSlot.Ring1, Card.FromDto(dto.Ring1));
        if (dto.Ring2 is not null) slots.Equip(EquipmentSlot.Ring2, Card.FromDto(dto.Ring2));
        return slots;
    }
}
