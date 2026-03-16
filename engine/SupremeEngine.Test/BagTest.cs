namespace SupremeEngine.Test;

using SupremeEngine;

public class BagTest
{
    private static Card MakeCard(string id = "card-001") =>
        new Card(id, "Test Card", CardRarity.Common, CardType.Attack);

    [Fact]
    public void Bag_StartsEmpty()
    {
        // Arrange / Act
        var bag = new Bag();

        // Assert
        Assert.Empty(bag.Cards);
    }

    [Fact]
    public void AddCard_AddsCardToBag()
    {
        // Arrange
        var bag = new Bag();
        var card = MakeCard();

        // Act
        bag.AddCard(card);

        // Assert
        Assert.Single(bag.Cards);
        Assert.Contains(card, bag.Cards);
    }

    [Fact]
    public void AddCard_AllowsDuplicates()
    {
        // Arrange
        var bag = new Bag();
        var card = MakeCard();

        // Act
        bag.AddCard(card);
        bag.AddCard(card);

        // Assert
        Assert.Equal(2, bag.Cards.Count);
    }

    [Fact]
    public void AddCard_ThrowsWhenAtCapacity()
    {
        // Arrange
        var bag = new Bag();
        for (int i = 0; i < Bag.Capacity; i++)
            bag.AddCard(MakeCard($"card-{i}"));

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() => bag.AddCard(MakeCard("overflow")));
    }

    [Fact]
    public void RemoveCard_RemovesCardFromBag()
    {
        // Arrange
        var bag = new Bag();
        var card = MakeCard();
        bag.AddCard(card);

        // Act
        bag.RemoveCard(card);

        // Assert
        Assert.Empty(bag.Cards);
    }

    [Fact]
    public void RemoveCard_ThrowsWhenCardNotFound()
    {
        // Arrange
        var bag = new Bag();

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() => bag.RemoveCard(MakeCard()));
    }
}
