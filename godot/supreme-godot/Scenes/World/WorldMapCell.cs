using Godot;

public readonly struct WorldMapCell
{
    public int Col { get; }
    public int Row { get; }
    public int WorldX { get; }
    public int WorldY { get; }

    public WorldMapCell(int col, int row, int minX, int maxY)
    {
        Col = col;
        Row = row;
        WorldX = minX + col;
        WorldY = maxY - row;
    }

    public void Draw(CanvasItem canvas, WorldMapViewState state, bool isDiscovered, bool isHovered)
    {
        float cellSize = state.CellSize;
        var rect = new Rect2(state.ColX(Col), state.RowY(Row), cellSize, cellSize);
        canvas.DrawRect(rect, isDiscovered ? WorldMapConstants.CellDiscovered : WorldMapConstants.CellUndiscovered);

        if (isDiscovered && isHovered)
            canvas.DrawRect(rect, WorldMapConstants.CellHover, filled: false, width: 2f);
    }
}
