namespace SupremeEngine;

/// <see href="../../../docs/systems/bag_spec.md"/>
public class Bag
{
    public const int Capacity = 100;

    private readonly List<Card> _cards = new();

    public IReadOnlyList<Card> Cards => _cards.AsReadOnly();

    public void AddCard(Card card)
    {
        if (_cards.Count >= Capacity)
            throw new InvalidOperationException($"Bag is at maximum capacity of {Capacity}.");

        _cards.Add(card);
    }

    public void RemoveCard(Card card)
    {
        if (!_cards.Remove(card))
            throw new InvalidOperationException("Card not found in bag.");
    }
}
