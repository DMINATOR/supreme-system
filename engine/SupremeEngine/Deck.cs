namespace SupremeEngine;

public class Deck
{
    public const int MaxSize = 20;

    private readonly List<Card> _cards = new();

    public bool InCombat { get; set; }

    public IReadOnlyList<Card> Cards => _cards.AsReadOnly();

    public void AddCard(Card card)
    {
        if (InCombat)
            throw new InvalidOperationException("Cannot modify deck during combat.");

        if (_cards.Count >= MaxSize)
            throw new InvalidOperationException($"Deck is at maximum size of {MaxSize}.");

        _cards.Add(card);
    }

    public void RemoveCard(Card card)
    {
        if (InCombat)
            throw new InvalidOperationException("Cannot modify deck during combat.");

        if (!_cards.Remove(card))
            throw new InvalidOperationException("Card not found in deck.");
    }

    public Card Draw()
    {
        if (_cards.Count == 0)
            throw new InvalidOperationException("Cannot draw from an empty deck.");

        var card = _cards[0];
        _cards.RemoveAt(0);
        return card;
    }
}
