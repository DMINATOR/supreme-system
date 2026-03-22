namespace SupremeEngine;

/// <see href="../../../../docs/systems/save-slots_spec.md"/>
public record CardCollectionDto
{
    public List<CardDto> Cards { get; init; } = new();
}
