namespace SupremeEngine.Test;

using SupremeEngine;

public class InventoryManagerTest
{
    private static Card MakeCard(string id = "card-001") =>
        new Card(id, "Test Card", CardRarity.Common, CardType.Attack, 1.0f);

    [Fact]
    public void InventoryManager_StartsWithEmptyBagAndPlayerAndNoCompanions()
    {
        // Arrange / Act
        var inventory = new InventoryManager();

        // Assert
        Assert.Empty(inventory.Bag.Cards);
        Assert.Empty(inventory.Player.Deck.Cards);
        Assert.Empty(inventory.Companions);
    }

    [Fact]
    public void Transfer_MovesCardFromBagToPlayerDeck()
    {
        // Arrange
        var inventory = new InventoryManager();
        var card = MakeCard();
        inventory.Bag.AddCard(card);

        // Act
        inventory.Transfer(card, inventory.Bag, inventory.Player.Deck);

        // Assert
        Assert.Empty(inventory.Bag.Cards);
        Assert.Single(inventory.Player.Deck.Cards);
        Assert.Contains(card, inventory.Player.Deck.Cards);
    }

    [Fact]
    public void Transfer_MovesCardFromPlayerDeckToBag()
    {
        // Arrange
        var inventory = new InventoryManager();
        var card = MakeCard();
        inventory.Player.Deck.AddCard(card);

        // Act
        inventory.Transfer(card, inventory.Player.Deck, inventory.Bag);

        // Assert
        Assert.Empty(inventory.Player.Deck.Cards);
        Assert.Single(inventory.Bag.Cards);
        Assert.Contains(card, inventory.Bag.Cards);
    }

    [Fact]
    public void Transfer_ThrowsWhenCardNotInSource()
    {
        // Arrange
        var inventory = new InventoryManager();
        var card = MakeCard();

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() => inventory.Transfer(card, inventory.Bag, inventory.Player.Deck));
    }

    [Fact]
    public void Transfer_DoesNotAddCardWhenSourceThrows()
    {
        // Arrange
        var inventory = new InventoryManager();
        var card = MakeCard();

        // Act
        try { inventory.Transfer(card, inventory.Bag, inventory.Player.Deck); } catch { }

        // Assert — player deck must still be empty, no phantom card added
        Assert.Empty(inventory.Player.Deck.Cards);
    }

    [Fact]
    public void From_RoundTrip()
    {
        // Arrange
        var inventory = new InventoryManager();
        inventory.Bag.AddCard(MakeCard("bag-001"));
        inventory.Player.Deck.AddCard(MakeCard("player-deck-001"));
        inventory.Companions.Add(new CompanionState("aria"));
        inventory.Companions[0].Deck.AddCard(MakeCard("companion-deck-001"));

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
        Assert.Single(restored.Bag.Cards);
        Assert.Equal("bag-001", restored.Bag.Cards[0].Id);
        Assert.Single(restored.Player.Deck.Cards);
        Assert.Equal("player-deck-001", restored.Player.Deck.Cards[0].Id);
        Assert.Single(restored.Companions);
        Assert.Equal("aria", restored.Companions[0].CompanionId);
        Assert.Single(restored.Companions[0].Deck.Cards);
        Assert.Equal("companion-deck-001", restored.Companions[0].Deck.Cards[0].Id);
    }
}

