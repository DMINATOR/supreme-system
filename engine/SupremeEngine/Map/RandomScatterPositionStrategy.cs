namespace SupremeEngine;

/// <see href="../../../../docs/systems/map-and-structure_spec.md"/>
public class RandomScatterPositionStrategy : IPositionStrategy
{
    public IReadOnlyList<(float X, float Y)> Place(int count, Random random)
    {
        var positions = new List<(float X, float Y)>(count);

        for (int i = 0; i < count; i++)
        {
            positions.Add((random.NextSingle(), random.NextSingle()));
        }

        return positions;
    }
}
