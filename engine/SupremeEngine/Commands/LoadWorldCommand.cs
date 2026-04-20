namespace SupremeEngine;

using System;

/// <summary>
/// Loads world state from the specified save slot and applies it to the state holder.
/// <list type="bullet">
/// <item>stateHolder must not be null.</item>
/// <item>persistence must not be null.</item>
/// <item>Invokes onSuccess on success.</item>
/// <item>Invokes onFailure with the exception if the slot is empty or data is corrupt; exception is not re-thrown.</item>
/// </list>
/// </summary>
/// <see href="../../../../docs/systems/save-slots_spec.md"/>
public class LoadWorldCommand : CallbackCommand
{
    private readonly IWorldStateHolder _stateHolder;
    private readonly IWorldPersistence _persistence;
    private readonly int _slotIndex;

    public LoadWorldCommand(IWorldStateHolder stateHolder, IWorldPersistence persistence, int slotIndex, Action? onSuccess = null, Action<Exception>? onFailure = null)
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
            var data = _persistence.Load(_slotIndex);

            if (data == null)
                throw new InvalidOperationException($"Save slot {_slotIndex} is empty or could not be read.");

            _stateHolder.SetState(WorldState.From(data));
            _persistence.SetActiveSlot(_slotIndex);
            InvokeSuccess();
        }
        catch (Exception ex)
        {
            InvokeFailure(ex);
        }
    }
}
