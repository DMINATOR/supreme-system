using Godot;
using SupremeEngine;

public partial class WorldMapScene : Control
{
	private WorldManager _worldManager;

	private WorldMapControl _worldMapControl;
	private RegionDetailPrefabScene _regionDetailPanel;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_worldMapControl = GetNode<WorldMapControl>("VBoxContainer/WorldMapControl");
		_regionDetailPanel = GetNode<RegionDetailPrefabScene>("VBoxContainer/RegionDetailPanel");
	}

	private void PrepareNodes()
	{
		_regionDetailPanel.ClosePressed += OnCloseDetailPressed;
		_worldMapControl.RegionSelected += OnRegionSelected;
		_worldMapControl.Setup(_worldManager.State.Regions);
	}

	private void OnRegionSelected(Region region)
	{
		_regionDetailPanel.Setup(region);
		_worldMapControl.Visible = false;
		_regionDetailPanel.Visible = true;
	}

	private void OnCloseDetailPressed()
	{
		_regionDetailPanel.Visible = false;
		_worldMapControl.Visible = true;
	}
}
