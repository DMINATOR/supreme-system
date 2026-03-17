using Godot;
using SupremeEngine;

public partial class WorldManager : Node
{
    public WorldState State { get; private set; } = new();

    public void StartNewGame(SaveManager saveManager, int slotIndex)
    {
        State = new WorldState();
        saveManager.SetActiveSlot(slotIndex);
    }

    public void LoadFromSlot(SaveManager saveManager, int slotIndex)
    {
        var data = saveManager.LoadSaveData(slotIndex);
        State = data != null ? WorldState.From(data) : new WorldState();
        saveManager.SetActiveSlot(slotIndex);
    }

    public void SaveToActiveSlot(SaveManager saveManager)
    {
        saveManager.SaveWorld(saveManager.ActiveSlotIndex, State.ToSaveData());
    }
}
