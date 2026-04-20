namespace SupremeEngine;

/// <see href="../../../../docs/systems/save-slots_spec.md"/>
public interface IWorldStateHolder
{
    WorldState State { get; }
    void SetState(WorldState state);
}
