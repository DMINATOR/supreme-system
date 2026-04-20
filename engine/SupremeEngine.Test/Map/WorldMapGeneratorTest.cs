namespace SupremeEngine.Test;

using SupremeEngine;

public class WorldMapGeneratorTest
{
    private static WorldMapGenerator MakeGenerator() =>
        new WorldMapGenerator(new RegionGenerator(new LocationGenerator(new RandomScatterPositionStrategy())));

    [Fact]
    public void GenerateInitialNeighborhood_GeneratesExpectedRegionCount()
    {
        // Arrange
        var generator = MakeGenerator();
        var state = new WorldState(seed: 0);
        var expectedCount = (int)Math.Pow(2 * WorldMapGenerator.InitialRadius + 1, 2);

        // Act
        generator.GenerateInitialNeighborhood(state);

        // Assert
        Assert.Equal(expectedCount, state.Regions.Count);
    }

    [Fact]
    public void GenerateInitialNeighborhood_DoesNotOverwriteExistingRegions()
    {
        // Arrange
        var generator = MakeGenerator();
        var state = new WorldState(seed: 0);
        var existingRegion = new Region(0, 0, 99, new List<RegionLocation>());
        state.Regions[(0, 0)] = existingRegion;

        // Act
        generator.GenerateInitialNeighborhood(state);

        // Assert
        Assert.Same(existingRegion, state.Regions[(0, 0)]);
    }

    [Fact]
    public void GetOrGenerateRegion_GeneratesAndCaches()
    {
        // Arrange
        var generator = MakeGenerator();
        var state = new WorldState(seed: 0);

        // Act
        var region = generator.GetOrGenerateRegion(state, x: 5, y: -3);

        // Assert
        Assert.NotNull(region);
        Assert.Equal(5, region.X);
        Assert.Equal(-3, region.Y);
        Assert.True(state.Regions.ContainsKey((5, -3)));
    }

    [Fact]
    public void GetOrGenerateRegion_ReturnsSameOnSecondCall()
    {
        // Arrange
        var generator = MakeGenerator();
        var state = new WorldState(seed: 0);

        // Act
        var first = generator.GetOrGenerateRegion(state, x: 2, y: 2);
        var second = generator.GetOrGenerateRegion(state, x: 2, y: 2);

        // Assert
        Assert.Same(first, second);
    }
}
