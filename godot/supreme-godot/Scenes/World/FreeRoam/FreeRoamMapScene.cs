using Godot;

public partial class FreeRoamMapScene : Node2D
{
	private SceneManager _sceneManager;
	private TileMapLayer _backgroundLayer;
	private TileMapLayer _foregroundLayer;
	private PlayerPrefabScene _player;

	public override void _Ready()
	{
		LoadNodes();
	}

	private void LoadNodes()
	{
		_sceneManager = GetNode<SceneManager>(AutoloadPath.SceneManager);
		_backgroundLayer = GetNode<TileMapLayer>("BackgroundLayer");
		_foregroundLayer = GetNode<TileMapLayer>("ForegroundLayer");
		_player = GetNode<PlayerPrefabScene>("Player");
	}
}
