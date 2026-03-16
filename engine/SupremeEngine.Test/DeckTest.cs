namespace SupremeEngine.Test;

using SupremeEngine;

public class DeckTest
{
    private static Card MakeCard(string id = "card-001") =>
        new Card(id, "Test Card", CardRarity.Common, CardType.Attack);

    [Fact]
    public void Deck_StartsEmpty()
    {
        // Arrange / Act
        var deck = new Deck();

        // Assert
        Assert.Empty(deck.Cards);
    }

    [Fact]
    public void AddCard_AddsCardToDeck()
    {
        // Arrange
        var deck = new Deck();
        var card = MakeCard();

        // Act
        deck.AddCard(card);

        // Assert
        Assert.Single(deck.Cards);
        Assert.Contains(card, deck.Cards);
    }

    [Fact]
    public void AddCard_AllowsDuplicates()
    {
        // Arrange
        var deck = new Deck();
        var card = MakeCard();

        // Act
        deck.AddCard(card);
        deck.AddCard(card);

        // Assert
        Assert.Equal(2, deck.Cards.Count);
    }

    [Fact]
    public void AddCard_ThrowsWhenAtMaxSize()
    {
        // Arrange
        var deck = new Deck();
        for (int i = 0; i < Deck.MaxSize; i++)
            deck.AddCard(MakeCard($"card-{i}"));

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() => deck.AddCard(MakeCard("overflow")));
    }

    [Fact]
    public void AddCard_ThrowsDuringCombat()
    {
        // Arrange
        var deck = new Deck();
        deck.InCombat = true;

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() => deck.AddCard(MakeCard()));
    }

    [Fact]
    public void RemoveCard_RemovesCardFromDeck()
    {
        // Arrange
        var deck = new Deck();
        var card = MakeCard();
        deck.AddCard(card);

        // Act
        deck.RemoveCard(card);

        // Assert
        Assert.Empty(deck.Cards);
    }

    [Fact]
    public void RemoveCard_ThrowsWhenCardNotFound()
    {
        // Arrange
        var deck = new Deck();

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() => deck.RemoveCard(MakeCard()));
    }

    [Fact]
    public void RemoveCard_ThrowsDuringCombat()
    {
        // Arrange
        var deck = new Deck();
        var card = MakeCard();
        deck.AddCard(card);
        deck.InCombat = true;

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() => deck.RemoveCard(card));
    }

    [Fact]
    public void Draw_ReturnsTopCard()
    {
        // Arrange
        var deck = new Deck();
        var first = MakeCard("card-001");
        var second = MakeCard("card-002");
        deck.AddCard(first);
        deck.AddCard(second);

        // Act
        var drawn = deck.Draw();

        // Assert
        Assert.Equal(first, drawn);
        Assert.Single(deck.Cards);
    }

    [Fact]
    public void Draw_ThrowsWhenDeckIsEmpty()
    {
        // Arrange
        var deck = new Deck();

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() => deck.Draw());
    }
}
