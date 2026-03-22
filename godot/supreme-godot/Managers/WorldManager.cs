using System;
using Godot;
using SupremeEngine;

public partial class WorldManager : Node
{
    public WorldState State { get; private set; } = new(Random.Shared.Next());

    public void StartNewGame(SaveManager saveManager, int slotIndex)
    {
        State = new WorldState(Random.Shared.Next());
        saveManager.SetActiveSlot(slotIndex);
    }

    public bool LoadFromSlot(Node parent, SaveManager saveManager, int slotIndex)
    {
        var data = saveManager.LoadSaveData(slotIndex);
        if (data == null)
        {
            DialogHelper.ShowError(parent, $"Failed to load save data for slot {slotIndex}.");
            return false;
        }
        State = WorldState.From(data);
        saveManager.SetActiveSlot(slotIndex);
        return true;
    }

    public void SaveToActiveSlot(SaveManager saveManager)
    {
        saveManager.SaveWorld(saveManager.ActiveSlotIndex, State.ToSaveData());
    }
}
