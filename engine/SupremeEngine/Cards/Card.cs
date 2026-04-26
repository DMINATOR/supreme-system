namespace SupremeEngine;

/// <see href="../../../../docs/systems/cards_spec.md"/>
public class Card
{
    public const float BrokenThreshold = 0.2f;

    public string Id { get; }
    public string Name { get; }
    public CardRarity Rarity { get; }
    public CardType Type { get; }
    public int Level { get; }
    public float Durability { get; set; }
    public bool IsBroken => Durability < BrokenThreshold;

    public Card(string id, string name, CardRarity rarity, CardType type, float durability, int level)
    {
        if (level < 1)
            throw new ArgumentOutOfRangeException(nameof(level), "Card level must be at least 1.");

        Id = id;
        Name = name;
        Rarity = rarity;
        Type = type;
        Durability = durability;
        Level = level;
    }

    public CardDto ToDto() => new()
    {
        Id = Id,
        Name = Name,
        Rarity = Rarity,
        Type = Type,
        Durability = Durability,
        Level = Level
    };

    public static Card FromDto(CardDto dto) =>
        new(dto.Id, dto.Name, dto.Rarity, dto.Type, dto.Durability, dto.Level);
}
