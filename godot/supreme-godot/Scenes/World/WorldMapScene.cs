using Godot;
using SupremeEngine;

public partial class WorldMapScene : Control
{
	private WorldManager _worldManager;

	private ScrollContainer _worldView;
	private VBoxContainer _regionList;
	private RegionDetailPrefabScene _regionDetailPanel;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_worldView = GetNode<ScrollContainer>("VBoxContainer/WorldView");
		_regionList = GetNode<VBoxContainer>("VBoxContainer/WorldView/RegionList");
		_regionDetailPanel = GetNode<RegionDetailPrefabScene>("VBoxContainer/RegionDetailPanel");
	}

	private void PrepareNodes()
	{
		_regionDetailPanel.ClosePressed += OnCloseDetailPressed;
		PopulateRegionList();
	}

	private void PopulateRegionList()
	{
		foreach (var (_, region) in _worldManager.State.Regions)
		{
			PrefabFactory.CreateRegionCellScene(_regionList, region, OnRegionSelected);
		}
	}

	private void OnRegionSelected(Region region)
	{
		_regionDetailPanel.Setup(region);
		_worldView.Visible = false;
		_regionDetailPanel.Visible = true;
	}

	private void OnCloseDetailPressed()
	{
		_regionDetailPanel.Visible = false;
		_worldView.Visible = true;
	}
}
