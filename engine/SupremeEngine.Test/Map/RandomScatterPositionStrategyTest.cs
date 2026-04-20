namespace SupremeEngine.Test;

using SupremeEngine;

public class RandomScatterPositionStrategyTest
{
    [Fact]
    public void Place_ReturnsRequestedCount()
    {
        // Arrange
        var strategy = new RandomScatterPositionStrategy();
        var random = new Random(0);

        // Act
        var positions = strategy.Place(5, random);

        // Assert
        Assert.Equal(5, positions.Count);
    }

    [Fact]
    public void Place_AllPositionsInUnitRange()
    {
        // Arrange
        var strategy = new RandomScatterPositionStrategy();
        var random = new Random(0);

        // Act
        var positions = strategy.Place(20, random);

        // Assert
        foreach (var (x, y) in positions)
        {
            Assert.InRange(x, 0f, 1f);
            Assert.InRange(y, 0f, 1f);
        }
    }
}
