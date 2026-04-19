namespace SupremeEngine;

/// <see href="../../../../docs/systems/map-and-structure_spec.md"/>
public class Region
{
    public int X { get; }
    public int Y { get; }
    public int AreaLevel { get; }
    public IReadOnlyList<RegionLocation> Locations { get; }

    public Region(int x, int y, int areaLevel, IReadOnlyList<RegionLocation> locations)
    {
        X = x;
        Y = y;
        AreaLevel = areaLevel;
        Locations = locations;
    }

    public RegionDto ToDto() => new RegionDto
    {
        X = X,
        Y = Y,
        AreaLevel = AreaLevel,
        Locations = Locations.Select(l => new RegionLocationDto
        {
            Type = l.Type,
            AreaLevel = l.AreaLevel,
            PositionX = l.PositionX,
            PositionY = l.PositionY
        }).ToList()
    };

    public static Region FromDto(RegionDto dto) =>
        new Region(
            dto.X,
            dto.Y,
            dto.AreaLevel,
            dto.Locations.Select(l => new RegionLocation(l.Type, l.AreaLevel, l.PositionX, l.PositionY)).ToList()
        );
}
