namespace SupremeEngine;

/// <see href="../../../../docs/systems/map-and-structure_spec.md"/>
public class WorldMapGenerator
{
    public const int InitialRadius = 1;

    private readonly RegionGenerator _regionGenerator;

    public WorldMapGenerator(RegionGenerator regionGenerator)
    {
        _regionGenerator = regionGenerator;
    }

    public void GenerateInitialNeighborhood(WorldState state)
    {
        for (int x = -InitialRadius; x <= InitialRadius; x++)
        {
            for (int y = -InitialRadius; y <= InitialRadius; y++)
            {
                GetOrGenerateRegion(state, x, y);
            }
        }
    }

    public Region GetOrGenerateRegion(WorldState state, int x, int y)
    {
        if (state.Regions.TryGetValue((x, y), out var existing))
        {
            return existing;
        }

        var region = _regionGenerator.Generate(x, y, state.WorldLevel, state.Random);
        state.Regions[(x, y)] = region;

        return region;
    }
}
