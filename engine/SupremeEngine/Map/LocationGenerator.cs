namespace SupremeEngine;

/// <see href="../../../../docs/systems/map-and-structure_spec.md"/>
public class LocationGenerator
{
    public const int MaxAreaLevelBonus = 5;

    private readonly IPositionStrategy _positionStrategy;

    public LocationGenerator(IPositionStrategy positionStrategy)
    {
        _positionStrategy = positionStrategy;
    }

    public IReadOnlyList<RegionLocation> Generate(IReadOnlyList<RegionLocationType> types, int worldLevel, Random random)
    {
        var positions = _positionStrategy.Place(types.Count, random);
        var locations = new List<RegionLocation>(types.Count);

        for (int i = 0; i < types.Count; i++)
        {
            var type = types[i];
            var areaLevel = type == RegionLocationType.City
                ? worldLevel
                : worldLevel + random.Next(0, MaxAreaLevelBonus + 1);

            locations.Add(new RegionLocation(type, areaLevel, positions[i].X, positions[i].Y));
        }

        return locations;
    }
}
