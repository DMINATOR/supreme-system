namespace SupremeEngine.Test;

using SupremeEngine;

public class GenerateInitialMapCommandTest
{
    private static WorldMapGenerator MakeGenerator() =>
        new WorldMapGenerator(new RegionGenerator(new LocationGenerator(new RandomScatterPositionStrategy())));

    [Fact]
    public void Execute_PopulatesRegionsOnWorldState()
    {
        // Arrange
        var state = new WorldState(seed: 0);
        var generator = MakeGenerator();
        var command = new GenerateInitialMapCommand(state, generator);

        // Act
        command.Execute();

        // Assert
        var expectedCount = (int)Math.Pow(2 * WorldMapGenerator.InitialRadius + 1, 2);
        Assert.Equal(expectedCount, state.Regions.Count);
    }

    [Fact]
    public void Execute_IsIdempotentWhenRunTwice()
    {
        // Arrange
        var state = new WorldState(seed: 0);
        var generator = MakeGenerator();
        var command = new GenerateInitialMapCommand(state, generator);
        command.Execute();
        var regionSnapshot = state.Regions[(0, 0)];

        // Act
        command.Execute();

        // Assert
        Assert.Same(regionSnapshot, state.Regions[(0, 0)]);
    }

    [Fact]
    public void Constructor_ThrowsIfStateIsNull()
    {
        // Arrange
        var generator = MakeGenerator();

        // Act / Assert
        Assert.Throws<ArgumentNullException>(() => new GenerateInitialMapCommand(null!, generator));
    }

    [Fact]
    public void Constructor_ThrowsIfGeneratorIsNull()
    {
        // Arrange
        var state = new WorldState(seed: 0);

        // Act / Assert
        Assert.Throws<ArgumentNullException>(() => new GenerateInitialMapCommand(state, null!));
    }
}
