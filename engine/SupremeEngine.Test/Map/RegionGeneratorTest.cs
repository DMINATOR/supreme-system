namespace SupremeEngine.Test;

using SupremeEngine;

public class RegionGeneratorTest
{
    private static RegionGenerator MakeGenerator() =>
        new RegionGenerator(new LocationGenerator(new RandomScatterPositionStrategy()));

    [Fact]
    public void Generate_SetsCorrectCoordinates()
    {
        // Arrange
        var generator = MakeGenerator();
        var random = new Random(0);

        // Act
        var region = generator.Generate(x: 4, y: -2, worldLevel: 1, random);

        // Assert
        Assert.Equal(4, region.X);
        Assert.Equal(-2, region.Y);
    }

    [Fact]
    public void Generate_SetsAreaLevelToWorldLevel()
    {
        // Arrange
        var generator = MakeGenerator();
        var random = new Random(0);
        const int worldLevel = 7;

        // Act
        var region = generator.Generate(x: 0, y: 0, worldLevel, random);

        // Assert
        Assert.Equal(worldLevel, region.AreaLevel);
    }

    [Fact]
    public void Generate_HasExpectedLocationCount()
    {
        // Arrange
        var generator = MakeGenerator();
        var random = new Random(0);

        // Act
        var region = generator.Generate(x: 0, y: 0, worldLevel: 1, random);

        // Assert
        Assert.Equal(RegionGenerator.LocationsPerRegion, region.Locations.Count);
    }

    [Fact]
    public void Generate_LocationTypesAreValid()
    {
        // Arrange
        var generator = MakeGenerator();
        var random = new Random(0);
        var validTypes = new[] { RegionLocationType.Dungeon, RegionLocationType.City, RegionLocationType.Encounter };

        // Act
        var region = generator.Generate(x: 0, y: 0, worldLevel: 1, random);

        // Assert
        foreach (var location in region.Locations)
        {
            Assert.Contains(location.Type, validTypes);
        }
    }
}
