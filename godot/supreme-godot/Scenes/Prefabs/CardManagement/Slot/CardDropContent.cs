using SupremeEngine;

public class CardDropContent : DropContent
{
    public CardSlot CardSlot { get; }

    public CardDropContent(CardSlot cardSlot)
    {
        CardSlot = cardSlot;
    }
}
