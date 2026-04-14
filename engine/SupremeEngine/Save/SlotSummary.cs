namespace SupremeEngine;

/// <see href="../../../../docs/systems/save-slots_spec.md"/>
public class SlotSummary
{
    public int Index { get; init; }
    public SlotState State { get; init; }
    public WorldSaveData? Data { get; init; }

    public override string ToString() => State switch
    {
        SlotState.Empty => ToEmptyString(),
        SlotState.InProgress => ToInProgressString(),
        _ => $"Slot {Index + 1}"
    };

    private string ToEmptyString() =>
        $"Slot {Index + 1} — Empty";

    private string ToInProgressString() =>
        $"Slot {Index + 1} — {Data!.Name}";
}
