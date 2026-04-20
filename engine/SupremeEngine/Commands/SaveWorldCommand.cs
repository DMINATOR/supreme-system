namespace SupremeEngine;

using System;

/// <summary>
/// Saves the current world state to a specific save slot and sets it as the active slot.
/// <list type="bullet">
/// <item>stateHolder must not be null.</item>
/// <item>persistence must not be null.</item>
/// <item>Invokes onSuccess on success.</item>
/// <item>Invokes onFailure with the exception if the write fails; exception is not re-thrown.</item>
/// </list>
/// </summary>
/// <see href="../../../../docs/systems/save-slots_spec.md"/>
public class SaveWorldCommand : CallbackCommand
{
    private readonly IWorldStateHolder _stateHolder;
    private readonly IWorldPersistence _persistence;
    private readonly int _slotIndex;

    public SaveWorldCommand(IWorldStateHolder stateHolder, IWorldPersistence persistence, int slotIndex, Action onSuccess = null, Action<Exception> onFailure = null)
        : base(onSuccess, onFailure)
    {
        ArgumentNullException.ThrowIfNull(stateHolder);
        ArgumentNullException.ThrowIfNull(persistence);

        _stateHolder = stateHolder;
        _persistence = persistence;
        _slotIndex = slotIndex;
    }

    public override void Execute()
    {
        try
        {
            _persistence.SetActiveSlot(_slotIndex);
            _persistence.Save(_slotIndex, _stateHolder.State.ToSaveData());
            InvokeSuccess();
        }
        catch (Exception ex)
        {
            InvokeFailure(ex);
        }
    }
}
