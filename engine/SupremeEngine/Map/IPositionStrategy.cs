namespace SupremeEngine;

/// <see href="../../../../docs/systems/map-and-structure_spec.md"/>
public interface IPositionStrategy
{
    IReadOnlyList<(float X, float Y)> Place(int count, Random random);
}
