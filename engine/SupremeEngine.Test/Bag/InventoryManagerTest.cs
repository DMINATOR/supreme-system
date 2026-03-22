namespace SupremeEngine.Test;

using SupremeEngine;

public class InventoryManagerTest
{
    private static Card MakeCard(string id = "card-001") =>
        new Card(id, "Test Card", CardRarity.Common, CardType.Attack);

    [Fact]
    public void InventoryManager_StartsWithEmptyBagAndDeck()
    {
        // Arrange / Act
        var inventory = new InventoryManager();

        // Assert
        Assert.Empty(inventory.Bag.Cards);
        Assert.Empty(inventory.Deck.Cards);
    }

    [Fact]
    public void Transfer_MovesCardFromBagToDeck()
    {
        // Arrange
        var inventory = new InventoryManager();
        var card = MakeCard();
        inventory.Bag.AddCard(card);

        // Act
        inventory.Transfer(card, inventory.Bag, inventory.Deck);

        // Assert
        Assert.Empty(inventory.Bag.Cards);
        Assert.Single(inventory.Deck.Cards);
        Assert.Contains(card, inventory.Deck.Cards);
    }

    [Fact]
    public void Transfer_MovesCardFromDeckToBag()
    {
        // Arrange
        var inventory = new InventoryManager();
        var card = MakeCard();
        inventory.Deck.AddCard(card);

        // Act
        inventory.Transfer(card, inventory.Deck, inventory.Bag);

        // Assert
        Assert.Empty(inventory.Deck.Cards);
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
        Assert.Throws<InvalidOperationException>(() => inventory.Transfer(card, inventory.Bag, inventory.Deck));
    }

    [Fact]
    public void Transfer_DoesNotAddCardWhenSourceThrows()
    {
        // Arrange
        var inventory = new InventoryManager();
        var card = MakeCard();

        // Act
        try { inventory.Transfer(card, inventory.Bag, inventory.Deck); } catch { }

        // Assert — deck must still be empty, no phantom card added
        Assert.Empty(inventory.Deck.Cards);
    }

    [Fact]
    public void ToDto_FromDto_RoundTrip()
    {
        // Arrange
        var inventory = new InventoryManager();
        inventory.Bag.AddCard(MakeCard("bag-001"));
        inventory.Deck.AddCard(MakeCard("deck-001"));

        // Act
        var dto = inventory.ToDto();
        var restored = InventoryManager.FromDto(dto);

        // Assert
        Assert.Single(restored.Bag.Cards);
        Assert.Equal("bag-001", restored.Bag.Cards[0].Id);
        Assert.Single(restored.Deck.Cards);
        Assert.Equal("deck-001", restored.Deck.Cards[0].Id);
    }
}
