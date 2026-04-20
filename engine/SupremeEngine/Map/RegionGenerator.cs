namespace SupremeEngine;

/// <see href="../../../../docs/systems/map-and-structure_spec.md"/>
public class RegionGenerator
{
    public const int LocationsPerRegion = 3;
    public const double CitySpawnChance = 0.25;
    public const double DungeonWeight = 0.6;

    private readonly LocationGenerator _locationGenerator;

    public RegionGenerator(LocationGenerator locationGenerator)
    {
        _locationGenerator = locationGenerator;
    }

    public Region Generate(int x, int y, int worldLevel, Random random)
    {
        var types = new List<RegionLocationType>(LocationsPerRegion);

        if (random.NextDouble() < CitySpawnChance)
        {
            types.Add(RegionLocationType.City);
        }

        while (types.Count < LocationsPerRegion)
        {
            types.Add(random.NextDouble() < DungeonWeight
                ? RegionLocationType.Dungeon
                : RegionLocationType.Encounter);
        }

        var locations = _locationGenerator.Generate(types, worldLevel, random);

        return new Region(x, y, worldLevel, locations);
    }
}
