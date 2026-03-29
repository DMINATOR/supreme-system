namespace SupremeEngine.Test;

using SupremeEngine;

public class PlayerStateTest
{
    private static Card MakeCard(string id = "card-001") =>
        new Card(id, "Test Card", CardRarity.Common, CardType.Attack);

    [Fact]
    public void PlayerState_StartsWithEmptyDeckAndEquipment()
    {
        // Arrange / Act
        var player = new PlayerState();

        // Assert
        Assert.Empty(player.Deck.Cards);
        Assert.Null(player.Equipment.Weapon);
    }

    [Fact]
    public void ToSaveData_From_RoundTrip()
    {
        // Arrange
        var player = new PlayerState();
        player.Deck.AddCard(MakeCard("deck-001"));
        player.Equipment.Equip(EquipmentSlot.Weapon, MakeCard("weapon-001"));

        // Act
        var saveData = player.ToSaveData();
        var restored = PlayerState.From(saveData);

        // Assert
        Assert.Single(restored.Deck.Cards);
        Assert.Equal("deck-001", restored.Deck.Cards[0].Id);
        Assert.NotNull(restored.Equipment.Weapon);
        Assert.Equal("weapon-001", restored.Equipment.Weapon!.Id);
    }
}
