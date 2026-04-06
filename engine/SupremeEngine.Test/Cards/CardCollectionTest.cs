namespace SupremeEngine.Test;

using SupremeEngine;

public class CardCollectionTest
{
    private static Card MakeCard(string id = "card-001") =>
        new Card(id, "Test Card", CardRarity.Common, CardType.Attack, 1.0f);

    [Fact]
    public void CardCollection_StartsEmpty()
    {
        // Arrange / Act
        var collection = new CardCollection(10);

        // Assert
        Assert.Empty(collection.Cards);
    }

    [Fact]
    public void AddCard_AddsCardToCollection()
    {
        // Arrange
        var collection = new CardCollection(10);
        var card = MakeCard();

        // Act
        collection.AddCard(card);

        // Assert
        Assert.Single(collection.Cards);
        Assert.Contains(card, collection.Cards);
    }

    [Fact]
    public void AddCard_AllowsDuplicates()
    {
        // Arrange
        var collection = new CardCollection(10);
        var card = MakeCard();

        // Act
        collection.AddCard(card);
        collection.AddCard(card);

        // Assert
        Assert.Equal(2, collection.Cards.Count);
    }

    [Fact]
    public void AddCard_ThrowsWhenAtCapacity()
    {
        // Arrange
        var collection = new CardCollection(2);
        collection.AddCard(MakeCard("card-1"));
        collection.AddCard(MakeCard("card-2"));

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() => collection.AddCard(MakeCard("overflow")));
    }

    [Fact]
    public void AddCard_ThrowsWhenLocked()
    {
        // Arrange
        var collection = new CardCollection(10);
        collection.IsLocked = true;

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() => collection.AddCard(MakeCard()));
    }

    [Fact]
    public void RemoveCard_RemovesCardFromCollection()
    {
        // Arrange
        var collection = new CardCollection(10);
        var card = MakeCard();
        collection.AddCard(card);

        // Act
        collection.RemoveCard(card);

        // Assert
        Assert.Empty(collection.Cards);
    }

    [Fact]
    public void RemoveCard_ThrowsWhenCardNotPresent()
    {
        // Arrange
        var collection = new CardCollection(10);

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() => collection.RemoveCard(MakeCard()));
    }

    [Fact]
    public void RemoveCard_ThrowsWhenLocked()
    {
        // Arrange
        var collection = new CardCollection(10);
        var card = MakeCard();
        collection.AddCard(card);
        collection.IsLocked = true;

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() => collection.RemoveCard(card));
    }

    [Fact]
    public void ToDto_FromDto_RoundTrip()
    {
        // Arrange
        var collection = new CardCollection(10);
        collection.AddCard(MakeCard("card-001"));
        collection.AddCard(MakeCard("card-002"));

        // Act
        var dto = collection.ToDto();
        var restored = CardCollection.FromDto(dto, 10);

        // Assert
        Assert.Equal(2, restored.Cards.Count);
        Assert.Equal("card-001", restored.Cards[0].Id);
        Assert.Equal("card-002", restored.Cards[1].Id);
    }
}
