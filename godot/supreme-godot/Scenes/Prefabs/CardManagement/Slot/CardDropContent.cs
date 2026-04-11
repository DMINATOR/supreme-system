using SupremeEngine;

public class CardDropContent : DropContent
{
    public Card Card { get; }

    public CardDropContent(Card card)
    {
        Card = card;
    }
}
