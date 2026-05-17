using Godot;
using SupremeEngine;
using System;
using System.Collections.Generic;

public partial class WorldMapGrid : Control
{
    private const float MinZoom = 0.5f;
    private const float MaxZoom = 5.0f;
    private const float ZoomStep = 0.15f;

    public event Action<Region> RegionSelected;

    private WorldMapViewState _state;
    private Dictionary<(int X, int Y), Region> _regions;

    private bool _isDragging;
    private bool _didDrag;
    private Vector2 _dragStartMouse;
    private Vector2 _dragStartPan;

    private Vector2I _hoveredCell = new Vector2I(int.MinValue, int.MinValue);

    public void Setup(WorldMapViewState state, Dictionary<(int X, int Y), Region> regions)
    {
        _state = state;
        _regions = regions;

        if (regions.Count == 0)
            return;

        int minX = int.MaxValue, maxX = int.MinValue;
        int minY = int.MaxValue, maxY = int.MinValue;

        foreach (var key in regions.Keys)
        {
            if (key.X < minX) minX = key.X;
            if (key.X > maxX) maxX = key.X;
            if (key.Y < minY) minY = key.Y;
            if (key.Y > maxY) maxY = key.Y;
        }

        _state.SetBounds(minX, maxX, minY, maxY);
        _state.ViewChanged += QueueRedraw;
    }

    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mb)
        {
            if (mb.ButtonIndex == MouseButton.Left)
            {
                if (mb.Pressed)
                {
                    _isDragging = true;
                    _didDrag = false;
                    _dragStartMouse = mb.Position;
                    _dragStartPan = _state.PanOffset;
                }
                else
                {
                    _isDragging = false;
                    if (!_didDrag)
                        TrySelectCell(mb.Position);
                }
            }
            else if (mb.Pressed && mb.ButtonIndex == MouseButton.WheelUp)
                ApplyZoom(ZoomStep, mb.Position);
            else if (mb.Pressed && mb.ButtonIndex == MouseButton.WheelDown)
                ApplyZoom(-ZoomStep, mb.Position);
        }
        else if (@event is InputEventMouseMotion mm)
        {
            UpdateHover(mm.Position);
            if (_isDragging)
            {
                if ((mm.Position - _dragStartMouse).Length() > 4f)
                    _didDrag = true;
                _state.SetZoomAndPan(_state.Zoom, _dragStartPan + (mm.Position - _dragStartMouse));
            }
        }
    }

    public override void _Notification(int what)
    {
        if (what == NotificationMouseExit)
        {
            _hoveredCell = new Vector2I(int.MinValue, int.MinValue);
            QueueRedraw();
        }
    }

    private void UpdateHover(Vector2 mousePos)
    {
        var cell = ScreenToCell(mousePos);
        if (cell != _hoveredCell)
        {
            _hoveredCell = cell;
            QueueRedraw();
        }
    }

    private void TrySelectCell(Vector2 mousePos)
    {
        var cellPos = ScreenToCell(mousePos);
        var cell = new WorldMapCell(cellPos.X, cellPos.Y, _state.MinX, _state.MaxY);
        if (_regions.TryGetValue((cell.WorldX, cell.WorldY), out var region))
            RegionSelected?.Invoke(region);
    }

    private Vector2I ScreenToCell(Vector2 pos)
    {
        float cellSize = _state.CellSize;
        return new Vector2I(
            (int)Mathf.Floor((pos.X - _state.PanOffset.X) / cellSize),
            (int)Mathf.Floor((pos.Y - _state.PanOffset.Y) / cellSize)
        );
    }

    private void ApplyZoom(float delta, Vector2 pivot)
    {
        float oldZoom = _state.Zoom;
        float newZoom = Mathf.Clamp(oldZoom + delta, MinZoom, MaxZoom);
        float ratio = newZoom / oldZoom;
        var newPan = pivot - ratio * (pivot - _state.PanOffset);
        _state.SetZoomAndPan(newZoom, newPan);
    }

    public override void _Draw()
    {
        DrawRect(new Rect2(Vector2.Zero, Size), WorldMapConstants.GridBackground);

        float cellSize = _state.CellSize;
        int firstCol = (int)Mathf.Floor(-_state.PanOffset.X / cellSize);
        int lastCol  = (int)Mathf.Ceil((Size.X - _state.PanOffset.X) / cellSize);
        int firstRow = (int)Mathf.Floor(-_state.PanOffset.Y / cellSize);
        int lastRow  = (int)Mathf.Ceil((Size.Y - _state.PanOffset.Y) / cellSize);

        DrawCells(firstCol, lastCol, firstRow, lastRow);
        DrawGridLines(firstCol, lastCol, firstRow, lastRow);
    }

    private void DrawCells(int firstCol, int lastCol, int firstRow, int lastRow)
    {
        for (int col = firstCol; col <= lastCol; col++)
        {
            for (int row = firstRow; row <= lastRow; row++)
                DrawCell(col, row);
        }
    }

    private void DrawCell(int col, int row)
    {
        var cell = new WorldMapCell(col, row, _state.MinX, _state.MaxY);
        bool isDiscovered = _regions.ContainsKey((cell.WorldX, cell.WorldY));
        bool isHovered = _hoveredCell.X == col && _hoveredCell.Y == row;
        cell.Draw(this, _state, isDiscovered, isHovered);
    }

    private void DrawGridLines(int firstCol, int lastCol, int firstRow, int lastRow)
    {
        for (int col = firstCol; col <= lastCol + 1; col++)
        {
            float x = _state.ColX(col);
            DrawLine(new Vector2(x, 0), new Vector2(x, Size.Y), WorldMapConstants.GridLine);
        }

        for (int row = firstRow; row <= lastRow + 1; row++)
        {
            float y = _state.RowY(row);
            DrawLine(new Vector2(0, y), new Vector2(Size.X, y), WorldMapConstants.GridLine);
        }
    }
}

