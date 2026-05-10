using Godot;
using System;

public class WorldMapViewState
{
    public const float BaseCellSize = 80f;

    public event Action ViewChanged;

    public int MinX { get; private set; }
    public int MaxX { get; private set; }
    public int MinY { get; private set; }
    public int MaxY { get; private set; }

    public float Zoom { get; private set; } = 1f;
    public Vector2 PanOffset { get; private set; } = Vector2.Zero;

    public float CellSize => BaseCellSize * Zoom;

    public float ColX(int col) => PanOffset.X + col * CellSize;
    public float RowY(int row) => PanOffset.Y + row * CellSize;

    public void SetBounds(int minX, int maxX, int minY, int maxY)
    {
        MinX = minX;
        MaxX = maxX;
        MinY = minY;
        MaxY = maxY;
        Zoom = 1f;
        PanOffset = Vector2.Zero;
        ViewChanged?.Invoke();
    }

    public void SetZoomAndPan(float zoom, Vector2 panOffset)
    {
        Zoom = zoom;
        PanOffset = panOffset;
        ViewChanged?.Invoke();
    }
}
