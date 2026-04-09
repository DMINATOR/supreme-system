namespace SupremeEngine.Test;

using SupremeEngine;

public class EquipmentSlotsTest
{
    private static Card MakeCard(string id = "equip-001") =>
        new Card(id, "Test Equipment", CardRarity.Common, CardType.Equipment, 1.0f);

    [Fact]
    public void EquipmentSlots_StartsWithAllSlotsEmpty()
    {
        // Arrange / Act
        var slots = new EquipmentSlots();

        // Assert
        Assert.Null(slots.Weapon.Card);
        Assert.Null(slots.OffHand.Card);
        Assert.Null(slots.Head.Card);
        Assert.Null(slots.Chest.Card);
        Assert.Null(slots.Hands.Card);
        Assert.Null(slots.Feet.Card);
        Assert.Null(slots.Amulet.Card);
        Assert.Null(slots.Ring1.Card);
        Assert.Null(slots.Ring2.Card);
    }

    [Fact]
    public void Equip_SetsCardInSlot()
    {
        // Arrange
        var slots = new EquipmentSlots();
        var card = MakeCard("weapon-001");

        // Act
        slots.Weapon.Equip(card);

        // Assert
        Assert.Same(card, slots.Weapon.Card);
    }

    [Fact]
    public void Equip_OverwritesExistingCard()
    {
        // Arrange
        var slots = new EquipmentSlots();
        var first = MakeCard("first");
        var second = MakeCard("second");
        slots.Chest.Equip(first);

        // Act
        slots.Chest.Equip(second);

        // Assert
        Assert.Same(second, slots.Chest.Card);
    }

    [Fact]
    public void Unequip_ClearsSlot()
    {
        // Arrange
        var slots = new EquipmentSlots();
        slots.Head.Equip(MakeCard());

        // Act
        slots.Head.Unequip();

        // Assert
        Assert.Null(slots.Head.Card);
    }

    [Fact]
    public void ToDto_FromDto_RoundTrip()
    {
        // Arrange
        var slots = new EquipmentSlots();
        slots.Weapon.Equip(MakeCard("weapon-001"));
        slots.Ring1.Equip(MakeCard("ring1-001"));
        slots.Ring2.Equip(MakeCard("ring2-001"));

        // Act
        var dto = slots.ToDto();
        var restored = EquipmentSlots.FromDto(dto);

        // Assert
        Assert.NotNull(restored.Weapon.Card);
        Assert.Equal("weapon-001", restored.Weapon.Card!.Id);
        Assert.NotNull(restored.Ring1.Card);
        Assert.Equal("ring1-001", restored.Ring1.Card!.Id);
        Assert.NotNull(restored.Ring2.Card);
        Assert.Equal("ring2-001", restored.Ring2.Card!.Id);
        Assert.Null(restored.OffHand.Card);
        Assert.Null(restored.Head.Card);
    }
}
