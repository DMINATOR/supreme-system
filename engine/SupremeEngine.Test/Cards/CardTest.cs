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
    public void Card_IsUsable_WhenDurabilityAboveZero()
    {
        // Arrange / Act
        var card = new Card("sword-001", "Iron Sword", CardRarity.Common, CardType.Equipment, 0.5f, level: 1);

        // Assert
        Assert.True(card.IsUsable);
    }

    [Fact]
    public void Card_IsNotUsable_WhenDurabilityIsZero()
    {
        // Arrange
        var card = new Card("sword-001", "Iron Sword", CardRarity.Common, CardType.Equipment, 0.5f, level: 1);

        // Act
        card.Durability = 0f;

        // Assert
        Assert.False(card.IsUsable);
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
