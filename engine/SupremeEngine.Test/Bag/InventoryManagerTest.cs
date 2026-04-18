namespace SupremeEngine.Test;

using System.Linq;
using SupremeEngine;

public class InventoryManagerTest
{
    private static Card MakeCard(string id = "card-001") =>
        new Card(id, "Test Card", CardRarity.Common, CardType.Attack, 1.0f, level: 1);

    [Fact]
    public void InventoryManager_StartsWithEmptyBagAndPlayerAndNoCompanions()
    {
        // Arrange / Act
        var inventory = new InventoryManager();

        // Assert
        Assert.True(inventory.Bag.Slots.All(s => s.Card is null));
        Assert.True(inventory.Player.Deck.Slots.All(s => s.Card is null));
        Assert.Empty(inventory.Companions);
    }

    [Fact]
    public void From_RoundTrip()
    {
        // Arrange
        var inventory = new InventoryManager();
        inventory.Bag.Slots[0].Equip(MakeCard("bag-001"));
        inventory.Player.Deck.Slots[0].Equip(MakeCard("player-deck-001"));
        inventory.Companions.Add(new CompanionState("aria"));
        inventory.Companions[0].Deck.Slots[0].Equip(MakeCard("companion-deck-001"));

        var saveData = new WorldSaveData
        {
            Seed = 42,
            Bag = inventory.Bag.ToDto(),
            Player = inventory.Player.ToSaveData(),
            Companions = inventory.Companions.Select(c => c.ToSaveData()).ToList()
        };

        // Act
        var restored = InventoryManager.From(saveData);

        // Assert
        Assert.Single(restored.Bag.Slots, s => s.Card is not null);
        Assert.Equal("bag-001", restored.Bag.Slots[0].Card!.Id);
        Assert.Single(restored.Player.Deck.Slots, s => s.Card is not null);
        Assert.Equal("player-deck-001", restored.Player.Deck.Slots[0].Card!.Id);
        Assert.Single(restored.Companions);
        Assert.Equal("aria", restored.Companions[0].CompanionId);
        Assert.Single(restored.Companions[0].Deck.Slots, s => s.Card is not null);
        Assert.Equal("companion-deck-001", restored.Companions[0].Deck.Slots[0].Card!.Id);
    }
}

