using Godot;
using SupremeEngine;

public partial class WorldMapScene : Control
{
	private SceneManager _sceneManager;
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
		_sceneManager = GetNode<SceneManager>(AutoloadPath.SceneManager);
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_mapArea = GetNode<GridContainer>("VBoxContainer/HBoxContainer/MapArea");
		_xHeader = GetNode<WorldMapHeader>("VBoxContainer/HBoxContainer/MapArea/XHeader");
		_yHeader = GetNode<WorldMapHeader>("VBoxContainer/HBoxContainer/MapArea/YHeader");
		_grid = GetNode<WorldMapGrid>("VBoxContainer/HBoxContainer/MapArea/Grid");
		_regionDetailPanel = GetNode<RegionDetailPrefabScene>("VBoxContainer/HBoxContainer/RegionDetailPanel");
	}

	private void PrepareNodes()
	{
		_xHeader.Setup(_viewState, WorldMapHeader.Orientation.Horizontal);
		_yHeader.Setup(_viewState, WorldMapHeader.Orientation.Vertical);
		_grid.Setup(_viewState, _worldManager.State.Regions);
		_grid.RegionHovered += OnRegionHovered;
		_grid.RegionUnhovered += OnRegionUnhovered;
		_grid.RegionSelected += OnRegionSelected;
	}

	private void OnRegionHovered(Region region)
	{
		_regionDetailPanel.Visible = true;
		_regionDetailPanel.Setup(region);
	}

	private void OnRegionUnhovered()
	{
		_regionDetailPanel.Visible = false;
	}

	private void OnRegionSelected(Region region)
	{
		_sceneManager.GoToRegionMapScene();
	}
}
