using Godot;
using System;
using SupremeEngine;

public partial class RegionCellPrefabScene : Button
{
	private Region _region;

	public event Action<Region> RegionSelected;

	public override void _Ready()
	{
		PrepareNodes();
	}

	public void Setup(Region region)
	{
		_region = region;
		Text = $"({region.X}, {region.Y})  —  Level {region.AreaLevel}  —  {region.Locations.Count} locations";
	}

	private void PrepareNodes()
	{
		Pressed += OnPressed;
	}

	private void OnPressed()
	{
		RegionSelected?.Invoke(_region);
	}
}
