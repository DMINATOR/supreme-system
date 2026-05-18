using Godot;
using SupremeEngine;
using System.Collections.Generic;

public class WorldMapGridCache
{
    private readonly List<WorldMapCell> _cells = new();
    private readonly List<(Vector2 From, Vector2 To)> _lines = new();

    public bool IsDirty { get; private set; } = true;

    public IReadOnlyList<WorldMapCell> Cells => _cells;
    public IReadOnlyList<(Vector2 From, Vector2 To)> Lines => _lines;

    public void Invalidate() => IsDirty = true;

    public void Rebuild(WorldMapViewState state, Dictionary<(int X, int Y), Region> regions, Vector2 viewSize)
    {
        float cellSize = state.CellSize;
        int firstCol = (int)Mathf.Floor(-state.PanOffset.X / cellSize);
        int lastCol  = (int)Mathf.Ceil((viewSize.X - state.PanOffset.X) / cellSize);
        int firstRow = (int)Mathf.Floor(-state.PanOffset.Y / cellSize);
        int lastRow  = (int)Mathf.Ceil((viewSize.Y - state.PanOffset.Y) / cellSize);

        RebuildCells(state, regions, firstCol, lastCol, firstRow, lastRow);
        RebuildLines(state, viewSize, firstCol, lastCol, firstRow, lastRow);

        IsDirty = false;
    }

    private void RebuildCells(WorldMapViewState state, Dictionary<(int X, int Y), Region> regions, int firstCol, int lastCol, int firstRow, int lastRow)
    {
        _cells.Clear();
        for (int col = firstCol; col <= lastCol; col++)
            for (int row = firstRow; row <= lastRow; row++)
            {
                bool isDiscovered = regions.ContainsKey((state.MinX + col, state.MaxY - row));
                _cells.Add(new WorldMapCell(col, row, state.MinX, state.MaxY, state, isDiscovered));
            }
    }

    private void RebuildLines(WorldMapViewState state, Vector2 viewSize, int firstCol, int lastCol, int firstRow, int lastRow)
    {
        _lines.Clear();
        for (int col = firstCol; col <= lastCol + 1; col++)
        {
            float x = state.ColX(col);
            _lines.Add((new Vector2(x, 0), new Vector2(x, viewSize.Y)));
        }
        for (int row = firstRow; row <= lastRow + 1; row++)
        {
            float y = state.RowY(row);
            _lines.Add((new Vector2(0, y), new Vector2(viewSize.X, y)));
        }
    }
}
