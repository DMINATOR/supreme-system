namespace SupremeEngine.Test;

using SupremeEngine;

public class CardTest
{
    [Fact]
    public void Card_StoresProperties()
    {
        // Arrange / Act
        var card = new Card("sword-001", "Iron Sword", CardRarity.Common, CardType.Equipment);

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
        var card = new Card("slash-001", "Slash", CardRarity.Uncommon, CardType.Attack);

        // Assert
        Assert.Equal(CardType.Attack, card.Type);
    }

    [Fact]
    public void Card_UniqueRarity_StoresCorrectly()
    {
        // Arrange / Act
        var card = new Card("unique-001", "Companion Bond", CardRarity.Unique, CardType.Spell);

        // Assert
        Assert.Equal(CardRarity.Unique, card.Rarity);
    }
}
