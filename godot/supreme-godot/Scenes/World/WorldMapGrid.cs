using Godot;
using SupremeEngine;
using System.Collections.Generic;

public partial class WorldMapGrid : Control
{
    private const float MinZoom = 0.5f;
    private const float MaxZoom = 5.0f;
    private const float ZoomStep = 0.15f;

    private WorldMapViewState _state;

    private bool _isDragging;
    private Vector2 _dragStartMouse;
    private Vector2 _dragStartPan;

    public void Setup(WorldMapViewState state, Dictionary<(int X, int Y), Region> regions)
    {
        _state = state;

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
                    _dragStartMouse = mb.Position;
                    _dragStartPan = _state.PanOffset;
                }
                else
                {
                    _isDragging = false;
                }
            }
            else if (mb.Pressed && mb.ButtonIndex == MouseButton.WheelUp)
                ApplyZoom(ZoomStep, mb.Position);
            else if (mb.Pressed && mb.ButtonIndex == MouseButton.WheelDown)
                ApplyZoom(-ZoomStep, mb.Position);
        }
        else if (@event is InputEventMouseMotion mm && _isDragging)
        {
            var newPan = _dragStartPan + (mm.Position - _dragStartMouse);
            _state.SetZoomAndPan(_state.Zoom, newPan);
        }
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
    }
}

