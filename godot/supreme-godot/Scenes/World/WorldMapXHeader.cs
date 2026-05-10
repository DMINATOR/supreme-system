using Godot;

public partial class WorldMapXHeader : Control
{
    private static readonly Color ColorBackground = new Color(0.10f, 0.10f, 0.14f);
    private static readonly Color ColorText = new Color(0.70f, 0.78f, 1.00f);

    private WorldMapViewState _state;
    private Font _font;
    private int _fontSize;

    public override void _Ready()
    {
        _font = ThemeDB.Singleton.FallbackFont;
        _fontSize = ThemeDB.Singleton.FallbackFontSize;
    }

    public void Setup(WorldMapViewState state)
    {
        _state = state;
        _state.ViewChanged += QueueRedraw;
        QueueRedraw();
    }

    public override void _Draw()
    {
        DrawRect(new Rect2(Vector2.Zero, Size), ColorBackground);

        if (_state == null)
            return;

        float cellSize = _state.CellSize;

        // find first visible column index
        int firstCol = (int)Mathf.Floor(-_state.PanOffset.X / cellSize);
        int lastCol  = (int)Mathf.Ceil((Size.X - _state.PanOffset.X) / cellSize);

        for (int col = firstCol; col <= lastCol; col++)
        {
            float x = _state.ColX(col);
            if (x + cellSize < 0 || x > Size.X)
                continue;

            string label = (_state.MinX + col).ToString();
            var sz = _font.GetStringSize(label, HorizontalAlignment.Left, -1, _fontSize);
            float cx = x + (cellSize - sz.X) / 2f;
            float cy = (Size.Y + sz.Y) / 2f;

            DrawString(_font, new Vector2(cx, cy), label, HorizontalAlignment.Left, -1, _fontSize, ColorText);
        }
    }
}
