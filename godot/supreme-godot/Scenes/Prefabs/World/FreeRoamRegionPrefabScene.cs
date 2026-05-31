using System;
using Godot;
using SupremeEngine;

public partial class FreeRoamRegionPrefabScene : Node2D
{
	private ColorRect _background;
	private TileMapLayer _backgroundLayer;
	private TileMapLayer _foregroundLayer;

	public override void _Ready()
	{
		LoadNodes();
	}

	public void Setup(int rx, int ry)
	{
		Position = new Vector2(rx * WorldMapGenerator.RegionSize, ry * WorldMapGenerator.RegionSize);
		_background.Color = RegionColor(rx, ry);
	}

	public Vector2I LocalToMap(Vector2 worldPosition)
	{
		return _backgroundLayer.LocalToMap(_backgroundLayer.ToLocal(worldPosition));
	}

	private void LoadNodes()
	{
		_background = GetNode<ColorRect>("Background");
		_backgroundLayer = GetNode<TileMapLayer>("BackgroundLayer");
		_foregroundLayer = GetNode<TileMapLayer>("ForegroundLayer");
	}

	private static Color RegionColor(int rx, int ry)
	{
		var hue = (uint)HashCode.Combine(rx, ry) / (float)uint.MaxValue;
		return Color.FromHsv(hue, 0.5f, 0.9f);
	}
}
