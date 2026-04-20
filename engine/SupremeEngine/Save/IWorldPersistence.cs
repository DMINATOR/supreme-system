namespace SupremeEngine;

/// <see href="../../../../docs/systems/save-slots_spec.md"/>
public interface IWorldPersistence
{
    WorldSaveData Load(int slotIndex);
    void Save(int slotIndex, WorldSaveData data);
    void SetActiveSlot(int slotIndex);
}
