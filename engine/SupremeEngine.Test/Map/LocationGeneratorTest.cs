namespace SupremeEngine.Test;

using SupremeEngine;

public class LocationGeneratorTest
{
    private static LocationGenerator MakeGenerator() =>
        new LocationGenerator(new RandomScatterPositionStrategy());

    [Fact]
    public void Generate_ReturnsExpectedCount()
    {
        // Arrange
        var generator = MakeGenerator();
        var types = new List<RegionLocationType> { RegionLocationType.City, RegionLocationType.Dungeon, RegionLocationType.Encounter };
        var random = new Random(0);

        // Act
        var locations = generator.Generate(types, worldLevel: 3, random);

        // Assert
        Assert.Equal(3, locations.Count);
    }

    [Fact]
    public void Generate_CityLocationHasWorldLevelAreaLevel()
    {
        // Arrange
        var generator = MakeGenerator();
        var types = new List<RegionLocationType> { RegionLocationType.City };
        var random = new Random(0);
        const int worldLevel = 5;

        // Act
        var locations = generator.Generate(types, worldLevel, random);

        // Assert
        Assert.Equal(worldLevel, locations[0].AreaLevel);
    }

    [Fact]
    public void Generate_NonCityLocationAreaLevelInRange()
    {
        // Arrange
        var generator = MakeGenerator();
        var types = new List<RegionLocationType> { RegionLocationType.Dungeon, RegionLocationType.Encounter };
        var random = new Random(0);
        const int worldLevel = 3;

        // Act
        var locations = generator.Generate(types, worldLevel, random);

        // Assert
        foreach (var location in locations)
        {
            Assert.InRange(location.AreaLevel, worldLevel, worldLevel + LocationGenerator.MaxAreaLevelBonus);
        }
    }

    [Fact]
    public void Generate_AllPositionsInUnitRange()
    {
        // Arrange
        var generator = MakeGenerator();
        var types = new List<RegionLocationType> { RegionLocationType.Dungeon, RegionLocationType.City, RegionLocationType.Encounter };
        var random = new Random(0);

        // Act
        var locations = generator.Generate(types, worldLevel: 1, random);

        // Assert
        foreach (var location in locations)
        {
            Assert.InRange(location.PositionX, 0f, 1f);
            Assert.InRange(location.PositionY, 0f, 1f);
        }
    }
}
