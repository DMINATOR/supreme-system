namespace SupremeEngine.Test;

using SupremeEngine;

public class WorldStateTest
{
    [Fact]
    public void WorldState_StartsAtWorldLevelOne()
    {
        // Arrange / Act
        var world = new WorldState(seed: 42);

        // Assert
        Assert.Equal(1, world.WorldLevel);
    }

    [Fact]
    public void ToSaveData_From_RoundTrip_IncludesWorldLevel()
    {
        // Arrange
        var world = new WorldState(seed: 42);

        // Act
        var saveData = world.ToSaveData();
        var restored = WorldState.From(saveData);

        // Assert
        Assert.Equal(1, restored.WorldLevel);
    }
}
