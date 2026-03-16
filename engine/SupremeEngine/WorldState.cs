namespace SupremeEngine;

/// <see href="../../../docs/systems/save-slots_spec.md"/>
public class WorldState
{
    public double TotalSecondsPlayed { get; set; }
    public List<string> BagCardIds { get; set; } = new();
}
