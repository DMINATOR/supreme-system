namespace SupremeEngine.Test;

using SupremeEngine;

public class BagManagerTest
{
    private static Card MakeCard(string id = "card-001") =>
        new Card(id, "Test Card", CardRarity.Common, CardType.Attack);

    [Fact]
    public void BagManager_StartsEmpty()
    {
        // Arrange / Act
        var manager = new BagManager();

        // Assert
        Assert.Empty(manager.Cards);
    }

    [Fact]
    public void AddCard_AddsCardToManager()
    {
        // Arrange
        var manager = new BagManager();
        var card = MakeCard();

        // Act
        manager.AddCard(card);

        // Assert
        Assert.Single(manager.Cards);
        Assert.Contains(card, manager.Cards);
    }

    [Fact]
    public void RemoveCard_RemovesCardFromManager()
    {
        // Arrange
        var manager = new BagManager();
        var card = MakeCard();
        manager.AddCard(card);

        // Act
        manager.RemoveCard(card);

        // Assert
        Assert.Empty(manager.Cards);
    }

    [Fact]
    public void RemoveCard_ThrowsWhenCardNotPresent()
    {
        // Arrange
        var manager = new BagManager();
        var card = MakeCard();

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() => manager.RemoveCard(card));
    }

    [Fact]
    public void ToDto_FromDto_RoundTrip()
    {
        // Arrange
        var manager = new BagManager();
        manager.AddCard(MakeCard("card-001"));
        manager.AddCard(MakeCard("card-002"));

        // Act
        var dto = manager.ToDto();
        var restored = BagManager.FromDto(dto);

        // Assert
        Assert.Equal(2, restored.Cards.Count);
        Assert.Equal("card-001", restored.Cards[0].Id);
        Assert.Equal("card-002", restored.Cards[1].Id);
    }
}
