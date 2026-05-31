namespace SupremeEngine;

/// <summary>
/// Generates a 3×3 region neighborhood centered on the given region coordinates,
/// ensuring the entered region and all its immediate neighbors exist in world state.
/// <list type="bullet">
/// <item>State must not be null.</item>
/// <item>Generator must not be null.</item>
/// </list>
/// </summary>
/// <see href="../../../../docs/systems/map-and-structure_spec.md"/>
public class EnterRegionCommand : ICommand
{
    private readonly WorldState _state;
    private readonly WorldMapGenerator _generator;
    private readonly int _regionX;
    private readonly int _regionY;

    public EnterRegionCommand(WorldState state, WorldMapGenerator generator, int regionX, int regionY)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(generator);

        _state = state;
        _generator = generator;
        _regionX = regionX;
        _regionY = regionY;
    }

    public void Execute()
    {
        for (int x = _regionX - 1; x <= _regionX + 1; x++)
        {
            for (int y = _regionY - 1; y <= _regionY + 1; y++)
            {
                _generator.GetOrGenerateRegion(_state, x, y);
            }
        }
    }
}
