using Godot;
using SupremeEngine;

public partial class WorldMapScene : Control
{
	private SceneManager _sceneManager;
	private WorldManager _worldManager;

	private ScrollContainer _worldView;
	private VBoxContainer _regionList;
	private Control _regionDetailPanel;
	private Label _detailTitleLabel;
	private Label _areaLevelLabel;
	private VBoxContainer _locationsList;
	private Button _backButton;
	private Button _closeDetailButton;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_sceneManager = GetNode<SceneManager>(AutoloadPath.SceneManager);
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_backButton = GetNode<Button>("VBoxContainer/BackButton");
		_worldView = GetNode<ScrollContainer>("VBoxContainer/WorldView");
		_regionList = GetNode<VBoxContainer>("VBoxContainer/WorldView/RegionList");
		_regionDetailPanel = GetNode<Control>("VBoxContainer/RegionDetailPanel");
		_closeDetailButton = GetNode<Button>("VBoxContainer/RegionDetailPanel/VBoxContainer/CloseDetailButton");
		_detailTitleLabel = GetNode<Label>("VBoxContainer/RegionDetailPanel/VBoxContainer/DetailTitleLabel");
		_areaLevelLabel = GetNode<Label>("VBoxContainer/RegionDetailPanel/VBoxContainer/AreaLevelLabel");
		_locationsList = GetNode<VBoxContainer>("VBoxContainer/RegionDetailPanel/VBoxContainer/LocationsList");
	}

	private void PrepareNodes()
	{
		_backButton.Pressed += OnBackPressed;
		_closeDetailButton.Pressed += OnCloseDetailPressed;
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
		ShowDetailPanel(region);
	}

	private void ShowDetailPanel(Region region)
	{
		_detailTitleLabel.Text = $"Region ({region.X}, {region.Y})";
		_areaLevelLabel.Text = $"Area Level: {region.AreaLevel}";

		foreach (Node child in _locationsList.GetChildren())
			child.QueueFree();

		foreach (var location in region.Locations)
			PrefabFactory.CreateLocationRowScene(_locationsList, location);

		_worldView.Visible = false;
		_regionDetailPanel.Visible = true;
	}

	private void OnCloseDetailPressed()
	{
		_regionDetailPanel.Visible = false;
		_worldView.Visible = true;
	}

	private void OnBackPressed()
	{
		_sceneManager.GoToDefaultScene();
	}
}
