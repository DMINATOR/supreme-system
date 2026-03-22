namespace SupremeEngine;

public interface ICardCollection
{
    IReadOnlyList<Card> Cards { get; }
    void AddCard(Card card);
    void RemoveCard(Card card);
}
