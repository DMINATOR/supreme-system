namespace SupremeEngine.Test;

using SupremeEngine;

public class RegionTest
{
    private static Region MakeRegion(int x = 0, int y = 0, int areaLevel = 1) =>
        new Region(x, y, areaLevel, new List<RegionLocation>
        {
            new RegionLocation(RegionLocationType.Dungeon, areaLevel + 2, 0.2f, 0.8f),
            new RegionLocation(RegionLocationType.City, areaLevel, 0.5f, 0.5f),
            new RegionLocation(RegionLocationType.Encounter, areaLevel + 4, 0.9f, 0.1f)
        });

    [Fact]
    public void Region_StoresCoordinatesAndAreaLevel()
    {
        // Arrange / Act
        var region = MakeRegion(x: 3, y: -7, areaLevel: 5);

        // Assert
        Assert.Equal(3, region.X);
        Assert.Equal(-7, region.Y);
        Assert.Equal(5, region.AreaLevel);
    }

    [Fact]
    public void Region_StoresLocations()
    {
        // Arrange / Act
        var region = MakeRegion(areaLevel: 2);

        // Assert
        Assert.Equal(3, region.Locations.Count);
        Assert.Contains(region.Locations, l => l.Type == RegionLocationType.Dungeon && l.AreaLevel == 4);
        Assert.Contains(region.Locations, l => l.Type == RegionLocationType.City && l.AreaLevel == 2);
        Assert.Contains(region.Locations, l => l.Type == RegionLocationType.Encounter && l.AreaLevel == 6);
    }

    [Fact]
    public void ToDto_FromDto_RoundTrip()
    {
        // Arrange
        var region = MakeRegion(x: 1, y: 2, areaLevel: 3);

        // Act
        var dto = region.ToDto();
        var restored = Region.FromDto(dto);

        // Assert
        Assert.Equal(region.X, restored.X);
        Assert.Equal(region.Y, restored.Y);
        Assert.Equal(region.AreaLevel, restored.AreaLevel);
        Assert.Equal(region.Locations.Count, restored.Locations.Count);

        for (int i = 0; i < region.Locations.Count; i++)
        {
            Assert.Equal(region.Locations[i].Type, restored.Locations[i].Type);
            Assert.Equal(region.Locations[i].AreaLevel, restored.Locations[i].AreaLevel);
            Assert.Equal(region.Locations[i].PositionX, restored.Locations[i].PositionX);
            Assert.Equal(region.Locations[i].PositionY, restored.Locations[i].PositionY);
        }
    }
}
