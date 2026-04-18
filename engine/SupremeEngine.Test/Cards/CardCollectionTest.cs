namespace SupremeEngine.Test;

using System.Linq;
using SupremeEngine;

public class CardCollectionTest
{
    private static Card MakeCard(string id = "card-001") =>
        new Card(id, "Test Card", CardRarity.Common, CardType.Attack, 1.0f, level: 1);

    [Fact]
    public void CardCollection_StartsWithAllSlotsEmpty()
    {
        // Arrange / Act
        var collection = new CardCollection(5);

        // Assert
        Assert.Equal(5, collection.Slots.Count);
        Assert.All(collection.Slots, s => Assert.Null(s.Card));
    }

    [Fact]
    public void Slots_ReflectEquippedCard()
    {
        // Arrange
        var collection = new CardCollection(3);
        var card = MakeCard();

        // Act
        collection.Slots[0].Equip(card);

        // Assert
        Assert.Same(card, collection.Slots[0].Card);
        Assert.Null(collection.Slots[1].Card);
    }

    [Fact]
    public void Slots_ReflectUnequippedCard()
    {
        // Arrange
        var collection = new CardCollection(3);
        var card = MakeCard();
        collection.Slots[0].Equip(card);

        // Act
        collection.Slots[0].Unequip();

        // Assert
        Assert.Null(collection.Slots[0].Card);
    }

    [Fact]
    public void Cards_ReturnsOnlyOccupiedSlots()
    {
        // Arrange
        var collection = new CardCollection(4);
        var card1 = MakeCard("card-001");
        var card2 = MakeCard("card-002");
        collection.Slots[0].Equip(card1);
        collection.Slots[2].Equip(card2);

        // Act / Assert
        var occupiedCards = collection.Slots.Where(s => s.Card is not null).Select(s => s.Card!).ToList();
        Assert.Equal(2, occupiedCards.Count);
        Assert.Contains(card1, occupiedCards);
        Assert.Contains(card2, occupiedCards);
    }

    [Fact]
    public void ToDto_FromDto_RoundTrip()
    {
        // Arrange
        var collection = new CardCollection(10);
        collection.Slots[0].Equip(MakeCard("card-001"));
        collection.Slots[1].Equip(MakeCard("card-002"));

        // Act
        var dto = collection.ToDto();
        var restored = CardCollection.FromDto(dto, 10);

        // Assert
        Assert.Equal(2, restored.Slots.Count(s => s.Card is not null));
        Assert.Equal("card-001", restored.Slots[0].Card!.Id);
        Assert.Equal("card-002", restored.Slots[1].Card!.Id);
    }

    [Fact]
    public void ToDto_FromDto_PreservesSlotPositions()
    {
        // Arrange — cards at non-contiguous positions
        var collection = new CardCollection(5);
        collection.Slots[1].Equip(MakeCard("card-a"));
        collection.Slots[3].Equip(MakeCard("card-b"));

        // Act
        var dto = collection.ToDto();
        var restored = CardCollection.FromDto(dto, 5);

        // Assert
        Assert.Null(restored.Slots[0].Card);
        Assert.Equal("card-a", restored.Slots[1].Card!.Id);
        Assert.Null(restored.Slots[2].Card);
        Assert.Equal("card-b", restored.Slots[3].Card!.Id);
        Assert.Null(restored.Slots[4].Card);
    }
}
