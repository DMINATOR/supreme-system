using Godot;
using SupremeEngine;

public partial class FreeRoamMapScene : Node2D
{
	private SceneManager _sceneManager;
	private WorldManager _worldManager;
	private CommandDispatcher _commandDispatcher;
	private TileMapLayer _backgroundLayer;
	private TileMapLayer _foregroundLayer;
	private PlayerPrefabScene _player;
	private Label _infoLabel;

	private Vector2 _lastDispatchedPosition;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	public override void _Process(double delta)
	{
		SyncPlayerPosition();
		UpdateInfoLabel();
	}

	private void LoadNodes()
	{
		_sceneManager = GetNode<SceneManager>(AutoloadPath.SceneManager);
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_commandDispatcher = GetNode<CommandDispatcher>(AutoloadPath.CommandDispatcher);
		_backgroundLayer = GetNode<TileMapLayer>("BackgroundLayer");
		_foregroundLayer = GetNode<TileMapLayer>("ForegroundLayer");
		_player = GetNode<PlayerPrefabScene>("Player");
		_infoLabel = GetNode<Label>("Hud/InfoLabel");
	}

	private void PrepareNodes()
	{
		var playerState = _worldManager.State.Player;
		_player.Position = new Vector2(playerState.X, playerState.Y);
		_lastDispatchedPosition = _player.Position;
	}

	private void SyncPlayerPosition()
	{
		var pos = _player.Position;
		if (pos == _lastDispatchedPosition)
			return;

		_commandDispatcher.Dispatch(new MovePlayerCommand(_worldManager.State.Player, pos.X, pos.Y));
		_lastDispatchedPosition = pos;
	}

	private void UpdateInfoLabel()
	{
		var pos = _player.Position;
		var tileCoords = _backgroundLayer.LocalToMap(pos);
		_infoLabel.Text = $"World: ({pos.X:F0}, {pos.Y:F0})  |  Tile: ({tileCoords.X}, {tileCoords.Y})";
	}
}
