namespace SupremeEngine;

/// <see href="../../../../docs/systems/save-slots_spec.md"/>
public record CardCollectionDto
{
    public CardDto?[] Slots { get; init; } = Array.Empty<CardDto?>();
}
