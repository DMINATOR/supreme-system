using System;
using Godot;
using SupremeEngine;

public partial class RegionDetailPrefabScene : Control
{
	private Label _detailTitleLabel;
	private Label _areaLevelLabel;
	private VBoxContainer _locationsList;

	public override void _Ready()
	{
		LoadNodes();
	}

	public void Setup(Region region)
	{
		_detailTitleLabel.Text = $"Region ({region.X}, {region.Y})";
		_areaLevelLabel.Text = $"Area Level: {region.AreaLevel}";

		foreach (Node child in _locationsList.GetChildren())
			child.QueueFree();

		foreach (var location in region.Locations)
			PrefabFactory.CreateLocationRowScene(_locationsList, location);
	}

	private void LoadNodes()
	{
		_detailTitleLabel = GetNode<Label>("VBoxContainer/DetailTitleLabel");
		_areaLevelLabel = GetNode<Label>("VBoxContainer/AreaLevelLabel");
		_locationsList = GetNode<VBoxContainer>("VBoxContainer/LocationsList");
	}
}
