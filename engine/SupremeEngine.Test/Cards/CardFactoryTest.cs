namespace SupremeEngine.Test;

using SupremeEngine;

public class CardFactoryTest
{
    [Fact]
    public void CardFactory_Create_SetsPropertiesFromTemplate()
    {
        // Arrange
        var template = new CardTemplate("iron-sword", "Iron Sword", CardRarity.Common, CardType.Equipment);
        var factory = new CardFactory(new Random(0));

        // Act
        var card = factory.Create(template);

        // Assert
        Assert.Equal("Iron Sword", card.Name);
        Assert.Equal(CardRarity.Common, card.Rarity);
        Assert.Equal(CardType.Equipment, card.Type);
    }

    [Fact]
    public void CardFactory_Create_IdContainsTemplateId()
    {
        // Arrange
        var template = new CardTemplate("iron-sword", "Iron Sword", CardRarity.Common, CardType.Equipment);
        var factory = new CardFactory(new Random(0));

        // Act
        var card = factory.Create(template);

        // Assert
        Assert.StartsWith("iron-sword-", card.Id);
    }

    [Fact]
    public void CardFactory_Create_EachCardHasUniqueId()
    {
        // Arrange
        var template = new CardTemplate("dagger", "Dagger", CardRarity.Common, CardType.Attack);
        var factory = new CardFactory(new Random(0));

        // Act
        var id1 = factory.Create(template).Id;
        var id2 = factory.Create(template).Id;

        // Assert
        Assert.NotEqual(id1, id2);
    }
}
