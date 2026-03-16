namespace SupremeEngine;

/// <see href="../../../docs/systems/cards_spec.md"/>
public class Card
{
    public string Id { get; }
    public string Name { get; }
    public CardRarity Rarity { get; }
    public CardType Type { get; }

    public Card(string id, string name, CardRarity rarity, CardType type)
    {
        Id = id;
        Name = name;
        Rarity = rarity;
        Type = type;
    }
}
