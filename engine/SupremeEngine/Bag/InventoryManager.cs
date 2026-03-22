namespace SupremeEngine;

/// <see href="../../../../docs/systems/bag_spec.md"/>
public class InventoryManager
{
    public const int BagCapacity = 100;
    public const int DeckCapacity = 20;

    public CardCollection Bag { get; private set; } = new(BagCapacity);
    public CardCollection Deck { get; private set; } = new(DeckCapacity);

    public void Transfer(Card card, ICardCollection from, ICardCollection to)
    {
        from.RemoveCard(card);
        to.AddCard(card);
    }

    public InventoryDto ToDto() => new() { Bag = Bag.ToDto(), Deck = Deck.ToDto() };

    public static InventoryManager FromDto(InventoryDto dto) => new()
    {
        Bag = CardCollection.FromDto(dto.Bag, BagCapacity),
        Deck = CardCollection.FromDto(dto.Deck, DeckCapacity)
    };
}
