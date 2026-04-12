using SupremeEngine;

public class CardDropContent : DropContent
{
    public Card Card { get; }
    public CardSlot CardSlot { get; }

    public CardDropContent(Card card, CardSlot cardSlot = null)
    {
        Card = card;
        CardSlot = cardSlot;
    }
}
