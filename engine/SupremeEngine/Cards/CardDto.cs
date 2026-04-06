namespace SupremeEngine;

/// <see href="../../../../docs/systems/cards_spec.md"/>
public record CardDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public CardRarity Rarity { get; init; }
    public CardType Type { get; init; }
    public float Durability { get; init; }
}
