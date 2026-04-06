namespace SupremeEngine;

public interface ICardCollection
{
    int Capacity { get; }
    IReadOnlyList<Card> Cards { get; }
    void AddCard(Card card);
    void RemoveCard(Card card);
}
