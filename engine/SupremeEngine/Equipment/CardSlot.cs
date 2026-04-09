namespace SupremeEngine;

/// <see href="../../../../docs/systems/equipment_spec.md"/>
public class CardSlot
{
    public Card? Card { get; private set; }

    public void Equip(Card card)
    {
        Card = card;
    }

    public void Unequip()
    {
        Card = null;
    }

    public CardDto? ToDto() => Card?.ToDto();
}
