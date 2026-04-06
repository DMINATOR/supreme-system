namespace SupremeEngine.Test;

using SupremeEngine;

public class CardTest
{
    [Fact]
    public void Card_StoresProperties()
    {
        // Arrange / Act
        var card = new Card("sword-001", "Iron Sword", CardRarity.Common, CardType.Equipment, 1.0f);

        // Assert
        Assert.Equal("sword-001", card.Id);
        Assert.Equal("Iron Sword", card.Name);
        Assert.Equal(CardRarity.Common, card.Rarity);
        Assert.Equal(CardType.Equipment, card.Type);
    }

    [Fact]
    public void Card_CombatType_StoresCorrectly()
    {
        // Arrange / Act
        var card = new Card("slash-001", "Slash", CardRarity.Uncommon, CardType.Attack, 1.0f);

        // Assert
        Assert.Equal(CardType.Attack, card.Type);
    }

    [Fact]
    public void Card_UniqueRarity_StoresCorrectly()
    {
        // Arrange / Act
        var card = new Card("unique-001", "Companion Bond", CardRarity.Unique, CardType.Spell, 1.0f);

        // Assert
        Assert.Equal(CardRarity.Unique, card.Rarity);
    }

    [Fact]
    public void Card_StoreDurability()
    {
        // Arrange / Act
        var card = new Card("sword-001", "Iron Sword", CardRarity.Common, CardType.Equipment, 0.5f);

        // Assert
        Assert.Equal(0.5f, card.Durability);
    }

    [Fact]
    public void Card_IsUsable_WhenDurabilityAboveZero()
    {
        // Arrange / Act
        var card = new Card("sword-001", "Iron Sword", CardRarity.Common, CardType.Equipment, 0.5f);

        // Assert
        Assert.True(card.IsUsable);
    }

    [Fact]
    public void Card_IsNotUsable_WhenDurabilityIsZero()
    {
        // Arrange
        var card = new Card("sword-001", "Iron Sword", CardRarity.Common, CardType.Equipment, 0.5f);

        // Act
        card.Durability = 0f;

        // Assert
        Assert.False(card.IsUsable);
    }
}
