namespace SupremeEngine.Test;

using SupremeEngine;

public class MovePlayerCommandTest
{
    [Fact]
    public void Execute_SetsPlayerPosition()
    {
        // Arrange
        var player = new PlayerState();
        var command = new MovePlayerCommand(player, 128f, 256f);

        // Act
        command.Execute();

        // Assert
        Assert.Equal(128f, player.X);
        Assert.Equal(256f, player.Y);
    }

    [Fact]
    public void Execute_OverwritesPreviousPosition()
    {
        // Arrange
        var player = new PlayerState();
        new MovePlayerCommand(player, 10f, 20f).Execute();
        var command = new MovePlayerCommand(player, 99f, -50f);

        // Act
        command.Execute();

        // Assert
        Assert.Equal(99f, player.X);
        Assert.Equal(-50f, player.Y);
    }

    [Fact]
    public void Constructor_ThrowsIfPlayerIsNull()
    {
        // Act / Assert
        Assert.Throws<ArgumentNullException>(() => new MovePlayerCommand(null!, 0f, 0f));
    }

    [Fact]
    public void ToSaveData_From_RoundTripsPlayerPosition()
    {
        // Arrange
        var player = new PlayerState();
        new MovePlayerCommand(player, 320f, -64f).Execute();

        // Act
        var restored = PlayerState.From(player.ToSaveData());

        // Assert
        Assert.Equal(320f, restored.X);
        Assert.Equal(-64f, restored.Y);
    }
}
