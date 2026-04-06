namespace SupremeEngine;

/// <see href="../../../../docs/systems/cards_spec.md"/>
public class Card
{
    public string Id { get; }
    public string Name { get; }
    public CardRarity Rarity { get; }
    public CardType Type { get; }
    public float Durability { get; set; }
    public bool IsUsable => Durability > 0;

    public Card(string id, string name, CardRarity rarity, CardType type, float durability)
    {
        Id = id;
        Name = name;
        Rarity = rarity;
        Type = type;
        Durability = durability;
    }

    public CardDto ToDto() => new()
    {
        Id = Id,
        Name = Name,
        Rarity = Rarity,
        Type = Type,
        Durability = Durability
    };

    public static Card FromDto(CardDto dto) =>
        new(dto.Id, dto.Name, dto.Rarity, dto.Type, dto.Durability);
}
