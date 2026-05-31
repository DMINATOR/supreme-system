using System.Collections.Generic;
using Godot;
using SupremeEngine;

public partial class FreeRoamMapScene : Node2D
{
	private WorldManager _worldManager;
	private CommandDispatcher _commandDispatcher;
	private PlayerPrefabScene _player;
	private Label _infoLabel;
	private FreeRoamRegionPrefabScene[] _regionPool;

	private Vector2 _lastDispatchedPosition;
	private Vector2I _currentRegionCoords;
	private readonly Dictionary<Vector2I, FreeRoamRegionPrefabScene> _regionTiles = new();

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	public override void _Process(double delta)
	{
		SyncPlayerPosition();
		CheckRegionTransition();
		UpdateInfoLabel();
	}

	private void LoadNodes()
	{
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_commandDispatcher = GetNode<CommandDispatcher>(AutoloadPath.CommandDispatcher);
		_player = GetNode<PlayerPrefabScene>("Player");
		_infoLabel = GetNode<Label>("Hud/InfoLabel");
		_regionPool = new FreeRoamRegionPrefabScene[9];
		string[] poolNames = ["RegionNW", "RegionN", "RegionNE", "RegionW", "RegionCenter", "RegionE", "RegionSW", "RegionS", "RegionSE"];
		for (int i = 0; i < 9; i++)
			_regionPool[i] = GetNode<FreeRoamRegionPrefabScene>(poolNames[i]);
	}

	private void PrepareNodes()
	{
		var playerState = _worldManager.State.Player;
		_player.Position = new Vector2(playerState.X, playerState.Y);
		_lastDispatchedPosition = _player.Position;
		_currentRegionCoords = ToRegionCoords(playerState.X, playerState.Y);
		_commandDispatcher.Dispatch(new EnterRegionCommand(_worldManager.State, _worldManager.MapGenerator, _currentRegionCoords.X, _currentRegionCoords.Y));
		SyncRegionTiles(_currentRegionCoords);
	}

	private void SyncPlayerPosition()
	{
		var pos = _player.Position;
		if (pos == _lastDispatchedPosition)
			return;

		_commandDispatcher.Dispatch(new MovePlayerCommand(_worldManager.State.Player, pos.X, pos.Y));
		_lastDispatchedPosition = pos;
	}

	private void CheckRegionTransition()
	{
		var pos = _player.Position;
		var newRegionCoords = ToRegionCoords(pos.X, pos.Y);
		if (newRegionCoords == _currentRegionCoords)
			return;

		_currentRegionCoords = newRegionCoords;
		_commandDispatcher.Dispatch(new EnterRegionCommand(_worldManager.State, _worldManager.MapGenerator, _currentRegionCoords.X, _currentRegionCoords.Y));
		SyncRegionTiles(_currentRegionCoords);
	}

	private void SyncRegionTiles(Vector2I center)
	{
		var needed = Neighborhood(center);

		var freed = new List<FreeRoamRegionPrefabScene>();
		var stale = new List<Vector2I>();
		foreach (var (coord, tile) in _regionTiles)
			if (!needed.Contains(coord))
			{
				freed.Add(tile);
				stale.Add(coord);
			}
		foreach (var coord in stale)
			_regionTiles.Remove(coord);

		if (_regionTiles.Count == 0)
			freed.AddRange(_regionPool);

		int freeIndex = 0;
		foreach (var coord in needed)
		{
			if (_regionTiles.ContainsKey(coord))
				continue;
			var tile = freed[freeIndex++];
			tile.Setup(coord.X, coord.Y);
			_regionTiles[coord] = tile;
		}
	}

	private static HashSet<Vector2I> Neighborhood(Vector2I center)
	{
		var set = new HashSet<Vector2I>();
		for (int dx = -1; dx <= 1; dx++)
			for (int dy = -1; dy <= 1; dy++)
				set.Add(new Vector2I(center.X + dx, center.Y + dy));
		return set;
	}

	private static Vector2I ToRegionCoords(float x, float y)
	{
		var (rx, ry) = WorldMapGenerator.WorldToRegionCoords(x, y);
		return new Vector2I(rx, ry);
	}

	private void UpdateInfoLabel()
	{
		var pos = _player.Position;
		var tileCoords = _regionTiles[_currentRegionCoords].LocalToMap(pos);
		var region = _worldManager.State.Regions[(_currentRegionCoords.X, _currentRegionCoords.Y)];
		_infoLabel.Text = $"Region ({region.X}, {region.Y}) — Level {region.AreaLevel}  |  World: ({pos.X:F0}, {pos.Y:F0})  |  Tile: ({tileCoords.X}, {tileCoords.Y})";
	}
}
