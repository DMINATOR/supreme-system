namespace SupremeEngine;

/// <see href="../../../../docs/systems/map-and-structure_spec.md"/>
public class RegionLocation
{
    public RegionLocationType Type { get; }
    public int AreaLevel { get; }
    public float PositionX { get; }
    public float PositionY { get; }

    public RegionLocation(RegionLocationType type, int areaLevel, float positionX, float positionY)
    {
        Type = type;
        AreaLevel = areaLevel;
        PositionX = positionX;
        PositionY = positionY;
    }
}
