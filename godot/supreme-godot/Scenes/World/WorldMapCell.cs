using Godot;

public readonly struct WorldMapCell
{
    public int Col { get; }
    public int Row { get; }
    public int WorldX { get; }
    public int WorldY { get; }
    public Rect2 ScreenRect { get; }
    public bool IsDiscovered { get; }

    public WorldMapCell(int col, int row, int minX, int maxY, WorldMapViewState state, bool isDiscovered)
    {
        Col = col;
        Row = row;
        WorldX = minX + col;
        WorldY = maxY - row;
        float cellSize = state.CellSize;
        ScreenRect = new Rect2(state.ColX(col), state.RowY(row), cellSize, cellSize);
        IsDiscovered = isDiscovered;
    }

    public void Draw(CanvasItem canvas, bool isHovered)
    {
        canvas.DrawRect(ScreenRect, IsDiscovered ? WorldMapConstants.CellDiscovered : WorldMapConstants.CellUndiscovered);

        if (IsDiscovered && isHovered)
            canvas.DrawRect(ScreenRect, WorldMapConstants.CellHover, filled: false, width: 2f);
    }
}
