namespace SupremeEngine;

/// <see href="../../../../docs/systems/save-slots_spec.md"/>
public record CardDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public CardRarity Rarity { get; init; }
    public CardType Type { get; init; }
}
