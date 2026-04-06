namespace SupremeEngine;

/// <see href="../../../../docs/systems/bag_spec.md"/>
public class CardCollection : ICardCollection
{
    private readonly int _capacity;
    private readonly List<Card> _cards = new();

    public CardCollection(int capacity)
    {
        _capacity = capacity;
    }

    public bool IsLocked { get; set; }

    public int Capacity => _capacity;

    public IReadOnlyList<Card> Cards => _cards.AsReadOnly();

    public void AddCard(Card card)
    {
        if (IsLocked)
            throw new InvalidOperationException("Cannot modify a locked collection.");

        if (_cards.Count >= _capacity)
            throw new InvalidOperationException($"Collection is at maximum capacity of {_capacity}.");

        _cards.Add(card);
    }

    public void RemoveCard(Card card)
    {
        if (IsLocked)
            throw new InvalidOperationException("Cannot modify a locked collection.");

        if (!_cards.Remove(card))
            throw new InvalidOperationException("Card not found in collection.");
    }

    public CardCollectionDto ToDto() => new()
    {
        Cards = _cards.Select(c => c.ToDto()).ToList()
    };

    public static CardCollection FromDto(CardCollectionDto dto, int capacity)
    {
        var collection = new CardCollection(capacity);
        foreach (var cardDto in dto.Cards)
            collection.AddCard(Card.FromDto(cardDto));
        return collection;
    }
}
