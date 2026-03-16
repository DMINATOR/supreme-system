namespace SupremeEngine;

/// <see href="../../../docs/systems/save-slots_spec.md"/>
public record BagDto
{
    public List<CardDto> Cards { get; init; } = new();
}
