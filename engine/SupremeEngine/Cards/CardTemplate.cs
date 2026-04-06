namespace SupremeEngine;

/// <see href="../../../../docs/systems/cards_spec.md"/>
public class CardTemplate
{
    public string TemplateId { get; }
    public string Name { get; }
    public CardRarity Rarity { get; }
    public CardType Type { get; }
    public float DurabilityOnUse { get; }

    public CardTemplate(string templateId, string name, CardRarity rarity, CardType type, float durabilityOnUse)
    {
        TemplateId = templateId;
        Name = name;
        Rarity = rarity;
        Type = type;
        DurabilityOnUse = durabilityOnUse;
    }
}
