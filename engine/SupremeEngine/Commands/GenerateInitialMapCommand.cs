namespace SupremeEngine;

/// <summary>
/// Generates the initial 3×3 region neighborhood around the world origin.
/// <list type="bullet">
/// <item>State must not be null.</item>
/// <item>Generator must not be null.</item>
/// </list>
/// </summary>
/// <see href="../../../../docs/systems/map-and-structure_spec.md"/>
public class GenerateInitialMapCommand : ICommand
{
    private readonly WorldState _state;
    private readonly WorldMapGenerator _generator;

    public GenerateInitialMapCommand(WorldState state, WorldMapGenerator generator)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(generator);

        _state = state;
        _generator = generator;
    }

    public void Execute()
    {
        _generator.GenerateInitialNeighborhood(_state);
    }
}
