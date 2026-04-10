namespace SupremeEngine.Test;

using SupremeEngine;

public class PlayerStateTest
{
    private static Card MakeCard(string id = "card-001") =>
        new Card(id, "Test Card", CardRarity.Common, CardType.Attack, 1.0f);

    [Fact]
    public void PlayerState_StartsWithEmptyDeckAndEquipment()
    {
        // Arrange / Act
        var player = new PlayerState();

        // Assert
        Assert.True(player.Deck.Slots.All(s => s.Card is null));
        Assert.Null(player.Equipment.Weapon.Card);
    }

    [Fact]
    public void ToSaveData_From_RoundTrip()
    {
        // Arrange
        var player = new PlayerState();
        player.Deck.Slots[0].Equip(MakeCard("deck-001"));
        player.Equipment.Weapon.Equip(MakeCard("weapon-001"));

        // Act
        var saveData = player.ToSaveData();
        var restored = PlayerState.From(saveData);

        // Assert
        Assert.Single(restored.Deck.Slots, s => s.Card is not null);
        Assert.Equal("deck-001", restored.Deck.Slots[0].Card!.Id);
        Assert.NotNull(restored.Equipment.Weapon.Card);
        Assert.Equal("weapon-001", restored.Equipment.Weapon.Card!.Id);
    }
}
