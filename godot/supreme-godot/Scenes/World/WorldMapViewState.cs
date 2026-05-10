using Godot;
using System;

public class WorldMapViewState
{
    public const float BaseCellSize = 80f;

    public static readonly Color ColorHeaderBackground = new Color(0.10f, 0.10f, 0.14f);
    public static readonly Color ColorHeaderText       = new Color(0.70f, 0.78f, 1.00f);
    public static readonly Color ColorGridBackground   = new Color(0.08f, 0.08f, 0.10f);
    public static readonly Color ColorGridLine         = new Color(0.25f, 0.25f, 0.28f);
    public static readonly Color ColorCellDiscovered   = new Color(0.15f, 0.16f, 0.20f);
    public static readonly Color ColorCellUndiscovered = new Color(0.06f, 0.06f, 0.07f);
    public static readonly Color ColorCellHover        = new Color(0.28f, 0.30f, 0.42f);

    // Whenever the map view changes, will be called for all the subscribed headers and grid to redraw themselves
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
