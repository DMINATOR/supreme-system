namespace SupremeEngine.Test;

using SupremeEngine;

public class EnterRegionCommandTest
{
    private static WorldMapGenerator MakeGenerator() =>
        new WorldMapGenerator(new RegionGenerator(new LocationGenerator(new RandomScatterPositionStrategy())));

    [Fact]
    public void Execute_PopulatesAllNineNeighborRegions()
    {
        // Arrange
        var state = new WorldState(seed: 0);
        var generator = MakeGenerator();
        var command = new EnterRegionCommand(state, generator, regionX: 3, regionY: 5);

        // Act
        command.Execute();

        // Assert
        for (int x = 2; x <= 4; x++)
        {
            for (int y = 4; y <= 6; y++)
            {
                Assert.True(state.Regions.ContainsKey((x, y)), $"Expected region ({x},{y}) to exist.");
            }
        }
    }

    [Fact]
    public void Execute_DoesNotOverwriteAlreadyGeneratedRegions()
    {
        // Arrange
        var state = new WorldState(seed: 0);
        var generator = MakeGenerator();
        var existingRegion = new Region(3, 5, 99, new List<RegionLocation>());
        state.Regions[(3, 5)] = existingRegion;
        var command = new EnterRegionCommand(state, generator, regionX: 3, regionY: 5);

        // Act
        command.Execute();

        // Assert
        Assert.Same(existingRegion, state.Regions[(3, 5)]);
    }

    [Fact]
    public void Constructor_ThrowsIfStateIsNull()
    {
        // Arrange
        var generator = MakeGenerator();

        // Act / Assert
        Assert.Throws<ArgumentNullException>(() => new EnterRegionCommand(null!, generator, 0, 0));
    }

    [Fact]
    public void Constructor_ThrowsIfGeneratorIsNull()
    {
        // Arrange
        var state = new WorldState(seed: 0);

        // Act / Assert
        Assert.Throws<ArgumentNullException>(() => new EnterRegionCommand(state, null!, 0, 0));
    }
}
