namespace SupremeEngine.Test;

using SupremeEngine;

public class EquipmentSlotsTest
{
    private static Card MakeCard(string id = "equip-001") =>
        new Card(id, "Test Equipment", CardRarity.Common, CardType.Equipment);

    [Fact]
    public void EquipmentSlots_StartsWithAllSlotsEmpty()
    {
        // Arrange / Act
        var slots = new EquipmentSlots();

        // Assert
        Assert.Null(slots.Weapon);
        Assert.Null(slots.OffHand);
        Assert.Null(slots.Head);
        Assert.Null(slots.Chest);
        Assert.Null(slots.Hands);
        Assert.Null(slots.Feet);
        Assert.Null(slots.Amulet);
        Assert.Null(slots.Ring1);
        Assert.Null(slots.Ring2);
    }

    [Fact]
    public void Equip_SetsCardInSlot()
    {
        // Arrange
        var slots = new EquipmentSlots();
        var card = MakeCard("weapon-001");

        // Act
        slots.Equip(EquipmentSlot.Weapon, card);

        // Assert
        Assert.Same(card, slots.Weapon);
    }

    [Fact]
    public void Equip_OverwritesExistingCard()
    {
        // Arrange
        var slots = new EquipmentSlots();
        var first = MakeCard("first");
        var second = MakeCard("second");
        slots.Equip(EquipmentSlot.Chest, first);

        // Act
        slots.Equip(EquipmentSlot.Chest, second);

        // Assert
        Assert.Same(second, slots.Chest);
    }

    [Fact]
    public void Unequip_ClearsSlot()
    {
        // Arrange
        var slots = new EquipmentSlots();
        slots.Equip(EquipmentSlot.Head, MakeCard());

        // Act
        slots.Unequip(EquipmentSlot.Head);

        // Assert
        Assert.Null(slots.Head);
    }

    [Fact]
    public void ToDto_FromDto_RoundTrip()
    {
        // Arrange
        var slots = new EquipmentSlots();
        slots.Equip(EquipmentSlot.Weapon, MakeCard("weapon-001"));
        slots.Equip(EquipmentSlot.Ring1, MakeCard("ring1-001"));
        slots.Equip(EquipmentSlot.Ring2, MakeCard("ring2-001"));

        // Act
        var dto = slots.ToDto();
        var restored = EquipmentSlots.FromDto(dto);

        // Assert
        Assert.NotNull(restored.Weapon);
        Assert.Equal("weapon-001", restored.Weapon!.Id);
        Assert.NotNull(restored.Ring1);
        Assert.Equal("ring1-001", restored.Ring1!.Id);
        Assert.NotNull(restored.Ring2);
        Assert.Equal("ring2-001", restored.Ring2!.Id);
        Assert.Null(restored.OffHand);
        Assert.Null(restored.Head);
    }
}
