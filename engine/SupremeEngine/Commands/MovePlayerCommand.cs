namespace SupremeEngine;

/// <summary>
/// Updates the player's world-space position.
/// <list type="bullet">
/// <item>Player must not be null.</item>
/// </list>
/// </summary>
/// <see href="../../../../docs/systems/map-and-structure_spec.md"/>
public class MovePlayerCommand : ICommand
{
    private readonly PlayerState _player;
    private readonly float _x;
    private readonly float _y;

    public MovePlayerCommand(PlayerState player, float x, float y)
    {
        ArgumentNullException.ThrowIfNull(player);
        _player = player;
        _x = x;
        _y = y;
    }

    public void Execute()
    {
        _player.SetPosition(_x, _y);
    }
}
