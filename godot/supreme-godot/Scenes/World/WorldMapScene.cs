using Godot;
using SupremeEngine;

public partial class WorldMapScene : Control
{
	private WorldManager _worldManager;
	private GridContainer _mapArea;
	private WorldMapHeader _xHeader;
	private WorldMapHeader _yHeader;
	private WorldMapGrid _grid;
	private RegionDetailPrefabScene _regionDetailPanel;

	private WorldMapViewState _viewState = new WorldMapViewState();

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_mapArea = GetNode<GridContainer>("VBoxContainer/MapArea");
		_xHeader = GetNode<WorldMapHeader>("VBoxContainer/MapArea/XHeader");
		_yHeader = GetNode<WorldMapHeader>("VBoxContainer/MapArea/YHeader");
		_grid = GetNode<WorldMapGrid>("VBoxContainer/MapArea/Grid");
		_regionDetailPanel = GetNode<RegionDetailPrefabScene>("VBoxContainer/RegionDetailPanel");
	}

	private void PrepareNodes()
	{
		_regionDetailPanel.ClosePressed += OnCloseDetailPressed;
		_xHeader.Setup(_viewState, WorldMapHeader.Orientation.Horizontal);
		_yHeader.Setup(_viewState, WorldMapHeader.Orientation.Vertical);
		_grid.Setup(_viewState, _worldManager.State.Regions);
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
