using System;
using Godot;
using SupremeEngine;

public partial class RegionDetailPrefabScene : Control
{
    public event Action ClosePressed;

    private Button _closeDetailButton;
    private Label _detailTitleLabel;
    private Label _areaLevelLabel;
    private VBoxContainer _locationsList;

    public override void _Ready()
    {
        LoadNodes();
        PrepareNodes();
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
        _closeDetailButton = GetNode<Button>("VBoxContainer/CloseDetailButton");
        _detailTitleLabel = GetNode<Label>("VBoxContainer/DetailTitleLabel");
        _areaLevelLabel = GetNode<Label>("VBoxContainer/AreaLevelLabel");
        _locationsList = GetNode<VBoxContainer>("VBoxContainer/LocationsList");
    }

    private void PrepareNodes()
    {
        _closeDetailButton.Pressed += () => ClosePressed?.Invoke();
    }
}
