namespace SupremeEngine.Test;

using SupremeEngine;

public class CompanionStateTest
{
    private static Card MakeCard(string id = "card-001") =>
        new Card(id, "Test Card", CardRarity.Common, CardType.Attack, 1.0f);

    [Fact]
    public void CompanionState_StoresCompanionId()
    {
        // Arrange / Act
        var companion = new CompanionState("aria");

        // Assert
        Assert.Equal("aria", companion.CompanionId);
    }

    [Fact]
    public void ToSaveData_From_RoundTrip_PreservesCompanionId()
    {
        // Arrange
        var companion = new CompanionState("rex");

        // Act
        var saveData = companion.ToSaveData();
        var restored = CompanionState.From(saveData);

        // Assert
        Assert.Equal("rex", restored.CompanionId);
    }

    [Fact]
    public void ToSaveData_From_RoundTrip_PreservesDeckAndEquipment()
    {
        // Arrange
        var companion = new CompanionState("aria");
        companion.Deck.AddCard(MakeCard("deck-001"));
        companion.Equipment.Weapon.Equip(MakeCard("weapon-001"));

        // Act
        var saveData = companion.ToSaveData();
        var restored = CompanionState.From(saveData);

        // Assert
        Assert.Single(restored.Deck.Cards);
        Assert.Equal("deck-001", restored.Deck.Cards[0].Id);
        Assert.NotNull(restored.Equipment.Weapon.Card);
        Assert.Equal("weapon-001", restored.Equipment.Weapon.Card!.Id);
    }
}
