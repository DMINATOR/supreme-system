using Godot;

public partial class WorldMapHeader : Control
{
    public enum Orientation { Horizontal, Vertical }

    private Orientation _orientation;
    private WorldMapViewState _state;

    public void Setup(WorldMapViewState state, Orientation orientation)
    {
        _state = state;
        _orientation = orientation;
        _state.ViewChanged += QueueRedraw;
        QueueRedraw();
    }

    public override void _Draw()
    {
        DrawRect(new Rect2(Vector2.Zero, Size), WorldMapConstants.HeaderBackground);

        if (_orientation == Orientation.Horizontal)
            DrawColumns();
        else
            DrawRows();
    }

    private void DrawColumns()
    {
        float cellSize = _state.CellSize;
        int first = (int)Mathf.Floor(-_state.PanOffset.X / cellSize);
        int last  = (int)Mathf.Ceil((Size.X - _state.PanOffset.X) / cellSize);

        for (int col = first; col <= last; col++)
            DrawLabel((_state.MinX + col).ToString(), _state.ColX(col), 0f, cellSize, Size.Y);
    }

    private void DrawRows()
    {
        float cellSize = _state.CellSize;
        int first = (int)Mathf.Floor(-_state.PanOffset.Y / cellSize);
        int last  = (int)Mathf.Ceil((Size.Y - _state.PanOffset.Y) / cellSize);

        for (int row = first; row <= last; row++)
            DrawLabel((_state.MaxY - row).ToString(), 0f, _state.RowY(row), Size.X, cellSize);
    }

    private void DrawLabel(string text, float x, float y, float w, float h)
    {
        var sz = _state.Font.GetStringSize(text, HorizontalAlignment.Left, -1, _state.FontSize);
        float cx = x + (w - sz.X) / 2f;
        float cy = y + (h + sz.Y) / 2f;
        DrawString(_state.Font, new Vector2(cx, cy), text, HorizontalAlignment.Left, -1, _state.FontSize, WorldMapConstants.HeaderText);
    }
}
