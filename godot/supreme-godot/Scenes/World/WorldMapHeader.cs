using Godot;

public partial class WorldMapHeader : Control
{
    public enum Orientation { Horizontal, Vertical }

    private Orientation _orientation;
    private WorldMapViewState _state;
    private Font _font;
    private int _fontSize;

    public void Setup(WorldMapViewState state, Orientation orientation)
    {
        _state = state;
        _orientation = orientation;
        _state.ViewChanged += QueueRedraw;
        QueueRedraw();
    }

    public override void _Ready()
    {
        _font = ThemeDB.Singleton.FallbackFont;
        _fontSize = ThemeDB.Singleton.FallbackFontSize;
    }

    public override void _Draw()
    {
        DrawRect(new Rect2(Vector2.Zero, Size), WorldMapViewState.ColorHeaderBackground);

        float cellSize = _state.CellSize;

        if (_orientation == Orientation.Horizontal)
            DrawColumns(cellSize);
        else
            DrawRows(cellSize);
    }

    private void DrawColumns(float cellSize)
    {
        int first = (int)Mathf.Floor(-_state.PanOffset.X / cellSize);
        int last  = (int)Mathf.Ceil((Size.X - _state.PanOffset.X) / cellSize);

        for (int col = first; col <= last; col++)
        {
            float x = _state.ColX(col);
            DrawLabel((_state.MinX + col).ToString(), x, 0f, cellSize, Size.Y);
        }
    }

    private void DrawRows(float cellSize)
    {
        int first = (int)Mathf.Floor(-_state.PanOffset.Y / cellSize);
        int last  = (int)Mathf.Ceil((Size.Y - _state.PanOffset.Y) / cellSize);

        for (int row = first; row <= last; row++)
        {
            float y = _state.RowY(row);
            DrawLabel((_state.MaxY - row).ToString(), 0f, y, Size.X, cellSize);
        }
    }

    private void DrawLabel(string text, float x, float y, float w, float h)
    {
        var sz = _font.GetStringSize(text, HorizontalAlignment.Left, -1, _fontSize);
        float cx = x + (w - sz.X) / 2f;
        float cy = y + (h + sz.Y) / 2f;
        DrawString(_font, new Vector2(cx, cy), text, HorizontalAlignment.Left, -1, _fontSize, WorldMapViewState.ColorHeaderText);
    }
}
