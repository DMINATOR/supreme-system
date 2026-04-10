namespace SupremeEngine.Test;

using SupremeEngine;

public class TransferCardCommandTest
{
    private static Card MakeCard(string id = "card-001") =>
        new Card(id, "Test Card", CardRarity.Common, CardType.Equipment, 1.0f);

    [Fact]
    public void Execute_EquipsCardInTargetSlot()
    {
        // Arrange
        var target = new CardSlot();
        var card = MakeCard();
        var command = new TransferCardCommand(null, target, card);

        // Act
        command.Execute();

        // Assert
        Assert.Same(card, target.Card);
    }

    [Fact]
    public void Execute_UnequipsSourceSlot()
    {
        // Arrange
        var source = new CardSlot();
        var target = new CardSlot();
        var card = MakeCard();
        source.Equip(card);
        var command = new TransferCardCommand(source, target, card);

        // Act
        command.Execute();

        // Assert
        Assert.Null(source.Card);
    }

    [Fact]
    public void Execute_WithNullSource_OnlyEquipsTarget()
    {
        // Arrange
        var target = new CardSlot();
        var card = MakeCard();
        var command = new TransferCardCommand(null, target, card);

        // Act
        command.Execute();

        // Assert
        Assert.Same(card, target.Card);
    }

    [Fact]
    public void Execute_ThrowsIfTargetSlotIsOccupied()
    {
        // Arrange
        var source = new CardSlot();
        var target = new CardSlot();
        var original = MakeCard("original");
        var incoming = MakeCard("incoming");
        source.Equip(incoming);
        target.Equip(original);
        var command = new TransferCardCommand(source, target, incoming);

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }
}
