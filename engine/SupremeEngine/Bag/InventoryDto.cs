namespace SupremeEngine;

/// <see href="../../../../docs/systems/save-slots_spec.md"/>
public record InventoryDto
{
    public CardCollectionDto Bag { get; init; } = new();
    public CardCollectionDto Deck { get; init; } = new();
}
