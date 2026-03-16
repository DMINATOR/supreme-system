namespace SupremeEngine;

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
