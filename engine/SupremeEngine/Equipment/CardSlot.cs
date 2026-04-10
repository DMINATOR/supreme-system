namespace SupremeEngine;

/// <see href="../../../../docs/systems/equipment_spec.md"/>
public class CardSlot
{
    public Card? Card { get; private set; }

    public void Equip(Card card)
    {
        if (Card is not null)
            throw new InvalidOperationException("Slot is already occupied. Unequip the current card before equipping a new one.");

        Card = card;
    }

    public void Unequip()
    {
        Card = null;
    }

    public CardDto? ToDto() => Card?.ToDto();
}
