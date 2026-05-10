using Godot;
using SupremeEngine;

public partial class WorldMapScene : Control
{
	private WorldManager _worldManager;
	private GridContainer _mapArea;
	private WorldMapXHeader _xHeader;
	private WorldMapYHeader _yHeader;
	private WorldMapGrid _grid;
	private RegionDetailPrefabScene _regionDetailPanel;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_mapArea = GetNode<GridContainer>("VBoxContainer/MapArea");
		_xHeader = GetNode<WorldMapXHeader>("VBoxContainer/MapArea/XHeader");
		_yHeader = GetNode<WorldMapYHeader>("VBoxContainer/MapArea/YHeader");
		_grid = GetNode<WorldMapGrid>("VBoxContainer/MapArea/Grid");
		_regionDetailPanel = GetNode<RegionDetailPrefabScene>("VBoxContainer/RegionDetailPanel");
	}

	private void PrepareNodes()
	{
		_regionDetailPanel.ClosePressed += OnCloseDetailPressed;
	}

	private void OnRegionSelected(Region region)
	{
		_regionDetailPanel.Setup(region);
		_mapArea.Visible = false;
		_regionDetailPanel.Visible = true;
	}

	private void OnCloseDetailPressed()
	{
		_regionDetailPanel.Visible = false;
		_mapArea.Visible = true;
	}
}
