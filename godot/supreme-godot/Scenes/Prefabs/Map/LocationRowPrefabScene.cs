using Godot;
using SupremeEngine;

public partial class LocationRowPrefabScene : HBoxContainer
{
	private Label _typeLabel;
	private Label _areaLevelLabel;
	private Label _positionLabel;

	public override void _Ready()
	{
		LoadNodes();
	}

	public void Setup(RegionLocation location)
	{
		_typeLabel.Text = location.Type.ToString();
		_areaLevelLabel.Text = $"Level {location.AreaLevel}";
		_positionLabel.Text = $"({location.PositionX:F2}, {location.PositionY:F2})";
	}

	private void LoadNodes()
	{
		_typeLabel = GetNode<Label>("TypeLabel");
		_areaLevelLabel = GetNode<Label>("AreaLevelLabel");
		_positionLabel = GetNode<Label>("PositionLabel");
	}
}
