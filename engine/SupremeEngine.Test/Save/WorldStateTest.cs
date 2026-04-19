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
}
