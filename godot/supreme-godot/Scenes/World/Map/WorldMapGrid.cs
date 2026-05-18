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
    public event Action<Region> RegionHovered;
    public event Action RegionUnhovered;

    private WorldMapViewState _state;
    private Dictionary<(int X, int Y), Region> _regions;

    private bool _isDragging;
    private bool _didDrag;
    private Vector2 _dragStartMouse;
    private Vector2 _dragStartPan;

    private Vector2I _hoveredCell = new Vector2I(int.MinValue, int.MinValue);

    private readonly WorldMapGridCache _cache = new();

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
        _state.ViewChanged += OnViewStateChanged;
    }

    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mb)
            OnMouseButton(mb);
        else if (@event is InputEventMouseMotion mm)
            OnMouseMotion(mm);
    }

    private void OnMouseButton(InputEventMouseButton mb)
    {
        if (mb.ButtonIndex == MouseButton.Left)
            OnMouseLeft(mb);
        else if (mb.Pressed && mb.ButtonIndex == MouseButton.WheelUp)
            ApplyZoom(ZoomStep, mb.Position);
        else if (mb.Pressed && mb.ButtonIndex == MouseButton.WheelDown)
            ApplyZoom(-ZoomStep, mb.Position);
    }

    private void OnMouseLeft(InputEventMouseButton mb)
    {
        if (mb.Pressed)
            OnMousePressed(mb.Position);
        else
            OnMouseReleased(mb.Position);
    }

    private void OnMousePressed(Vector2 position)
    {
        _isDragging = true;
        _didDrag = false;
        _dragStartMouse = position;
        _dragStartPan = _state.PanOffset;
    }

    private void OnMouseReleased(Vector2 position)
    {
        _isDragging = false;
        if (!_didDrag)
            TrySelectCell(position);
    }

    private void OnMouseMotion(InputEventMouseMotion mm)
    {
        UpdateHover(mm.Position);
        if (_isDragging)
        {
            if ((mm.Position - _dragStartMouse).Length() > 4f)
                _didDrag = true;
            _state.SetZoomAndPan(_state.Zoom, _dragStartPan + (mm.Position - _dragStartMouse));
        }
    }

    public override void _Notification(int what)
    {
        if (what == NotificationMouseExit)
        {
            _hoveredCell = new Vector2I(int.MinValue, int.MinValue);
            QueueRedraw();
            RegionUnhovered?.Invoke();
        }
        else if (what == NotificationResized)
        {
            _cache.Invalidate();
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
            if (TryGetRegionAtCell(cell, out var region))
                RegionHovered?.Invoke(region);
            else
                RegionUnhovered?.Invoke();
        }
    }

    private void TrySelectCell(Vector2 mousePos)
    {
        var cell = ScreenToCell(mousePos);
        if (TryGetRegionAtCell(cell, out var region))
            RegionSelected?.Invoke(region);
    }

    private bool TryGetRegionAtCell(Vector2I cell, out Region region)
    {
        int wx = _state.MinX + cell.X;
        int wy = _state.MaxY - cell.Y;
        return _regions.TryGetValue((wx, wy), out region);
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
        if (_cache.IsDirty)
            _cache.Rebuild(_state, _regions, Size);

        DrawBackground();
        DrawCells();
        DrawGridLines();
    }

    private void DrawBackground()
    {
        DrawRect(new Rect2(Vector2.Zero, Size), WorldMapConstants.GridBackground);
    }

    private void DrawCells()
    {
        foreach (var cell in _cache.Cells)
        {
            bool isHovered = _hoveredCell.X == cell.Col && _hoveredCell.Y == cell.Row;
            cell.Draw(this, isHovered);
        }
    }

    private void DrawGridLines()
    {
        foreach (var (from, to) in _cache.Lines)
            DrawLine(from, to, WorldMapConstants.GridLine);
    }

    private void OnViewStateChanged()
    {
        _cache.Invalidate();
        QueueRedraw();
    }
}

