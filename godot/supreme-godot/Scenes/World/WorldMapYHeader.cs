using Godot;

public partial class WorldMapYHeader : Control
{
    private WorldMapViewState _state;

    public void Setup(WorldMapViewState state)
    {
        _state = state;
        _state.ViewChanged += QueueRedraw;
        QueueRedraw();
    }

    public override void _Draw()
    {
    }
}
