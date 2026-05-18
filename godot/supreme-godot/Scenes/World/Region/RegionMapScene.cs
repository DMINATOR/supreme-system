using Godot;
using SupremeEngine;

public partial class RegionMapScene : Control
{
	private SceneManager _sceneManager;
	private Label _regionTitleLabel;
	private Control _mapCanvas;

	public override void _Ready()
	{
		LoadNodes();
	}

	public void Setup(Region region)
	{
		_regionTitleLabel.Text = $"Region ({region.X}, {region.Y}) — Level {region.AreaLevel}";
		PlaceLocations(region);
	}

	private void LoadNodes()
	{
		_sceneManager = GetNode<SceneManager>(AutoloadPath.SceneManager);
		_regionTitleLabel = GetNode<Label>("VBoxContainer/HeaderBar/RegionTitleLabel");
		_mapCanvas = GetNode<Control>("VBoxContainer/MapCanvas");
	}

	private void PlaceLocations(Region region)
	{
		foreach (var location in region.Locations)
		{
			var btn = new Button();
			btn.Text = location.Type.ToString();
			btn.TooltipText = $"Level {location.AreaLevel}";
			btn.AnchorLeft = location.PositionX;
			btn.AnchorTop = location.PositionY;
			btn.AnchorRight = location.PositionX;
			btn.AnchorBottom = location.PositionY;
			btn.OffsetLeft = -50;
			btn.OffsetTop = -18;
			btn.OffsetRight = 50;
			btn.OffsetBottom = 18;
			_mapCanvas.AddChild(btn);
		}
	}
}
