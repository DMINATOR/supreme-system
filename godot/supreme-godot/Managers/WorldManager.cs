using System;
using Godot;
using SupremeEngine;

public partial class WorldManager : Node, IWorldStateHolder
{
    private readonly WorldMapGenerator _mapGenerator = new WorldMapGenerator(
        new RegionGenerator(
            new LocationGenerator(
                new RandomScatterPositionStrategy())));

    private CommandDispatcher _commandDispatcher;

    public WorldState State { get; private set; } = new(Random.Shared.Next());

    public WorldMapGenerator MapGenerator => _mapGenerator;

    public override void _Ready()
    {
        LoadNodes();
    }

    public void StartNewGame(int slotIndex)
    {
        State = new WorldState(Random.Shared.Next());
        new GenerateInitialMapCommand(State, _mapGenerator).Execute();
    }

    public void SetState(WorldState state)
    {
        ArgumentNullException.ThrowIfNull(state);
        State = state;
    }

    public void DispatchSave(SaveManager saveManager, int slotIndex, Action? onSuccess = null, Action<Exception>? onFailure = null)
    {
        _commandDispatcher.Dispatch(new SaveWorldCommand(this, saveManager, slotIndex, onSuccess, onFailure));
    }

    public void DispatchLoad(int slotIndex, SaveManager saveManager, Action? onSuccess = null, Action<Exception>? onFailure = null)
    {
        _commandDispatcher.Dispatch(new LoadWorldCommand(this, saveManager, slotIndex, onSuccess, onFailure));
    }

    private void LoadNodes()
    {
        _commandDispatcher = GetNode<CommandDispatcher>(AutoloadPath.CommandDispatcher);
    }
}
