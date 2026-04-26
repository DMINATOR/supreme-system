namespace SupremeEngine.Test;

using SupremeEngine;

public class CardTest
{
    [Fact]
    public void Card_StoresProperties()
    {
        // Arrange / Act
        var card = new Card("sword-001", "Iron Sword", CardRarity.Common, CardType.Equipment, 1.0f, level: 1);
        Assert.Equal("Iron Sword", card.Name);
        Assert.Equal(CardRarity.Common, card.Rarity);
        Assert.Equal(CardType.Equipment, card.Type);
    }

    [Fact]
    public void Card_CombatType_StoresCorrectly()
    {
        // Arrange / Act
        var card = new Card("slash-001", "Slash", CardRarity.Uncommon, CardType.Attack, 1.0f, level: 1);

        // Assert
        Assert.Equal(CardType.Attack, card.Type);
    }

    [Fact]
    public void Card_UniqueRarity_StoresCorrectly()
    {
        // Arrange / Act
        var card = new Card("unique-001", "Companion Bond", CardRarity.Unique, CardType.Spell, 1.0f, level: 1);

        // Assert
        Assert.Equal(CardRarity.Unique, card.Rarity);
    }

    [Fact]
    public void Card_StoreDurability()
    {
        // Arrange / Act
        var card = new Card("sword-001", "Iron Sword", CardRarity.Common, CardType.Equipment, 0.5f, level: 1);

        // Assert
        Assert.Equal(0.5f, card.Durability);
    }

    [Fact]
    public void Card_IsBroken_WhenDurabilityBelowThreshold()
    {
        // Arrange
        var card = new Card("sword-001", "Iron Sword", CardRarity.Common, CardType.Equipment, 1.0f, level: 1);

        // Act
        card.Durability = 0.1f;

        // Assert
        Assert.True(card.IsBroken);
    }

    [Fact]
    public void Card_IsNotBroken_WhenDurabilityAtThreshold()
    {
        // Arrange
        var card = new Card("sword-001", "Iron Sword", CardRarity.Common, CardType.Equipment, 1.0f, level: 1);

        // Act
        card.Durability = Card.BrokenThreshold;

        // Assert
        Assert.False(card.IsBroken);
    }

    [Fact]
    public void Card_IsNotBroken_WhenDurabilityAboveThreshold()
    {
        // Arrange / Act
        var card = new Card("sword-001", "Iron Sword", CardRarity.Common, CardType.Equipment, 1.0f, level: 1);

        // Assert
        Assert.False(card.IsBroken);
    }

    [Fact]
    public void Card_StoresLevel()
    {
        // Arrange / Act
        var card = new Card("sword-001", "Iron Sword", CardRarity.Common, CardType.Equipment, 1.0f, level: 5);

        // Assert
        Assert.Equal(5, card.Level);
    }

    [Fact]
    public void Card_ThrowsWhenLevelIsZero()
    {
        // Arrange / Act / Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Card("sword-001", "Iron Sword", CardRarity.Common, CardType.Equipment, 1.0f, level: 0));
    }

    [Fact]
    public void Card_ThrowsWhenLevelIsNegative()
    {
        // Arrange / Act / Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Card("sword-001", "Iron Sword", CardRarity.Common, CardType.Equipment, 1.0f, level: -1));
    }

    [Fact]
    public void Card_DtoRoundTrip_PreservesLevel()
    {
        // Arrange
        var original = new Card("sword-001", "Iron Sword", CardRarity.Common, CardType.Equipment, 1.0f, level: 7);

        // Act
        var restored = Card.FromDto(original.ToDto());

        // Assert
        Assert.Equal(7, restored.Level);
    }
}
